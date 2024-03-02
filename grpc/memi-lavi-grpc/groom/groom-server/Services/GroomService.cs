using Google.Protobuf.WellKnownTypes;
using gRoom.gRPC.Messages;
using groomserver.Utils;
using Grpc.Core;

namespace groomserver.Services;
public class GroomService : Groom.GroomBase {
    private readonly ILogger<GroomService> logger;
    public GroomService(ILogger<GroomService> logger) {
        this.logger = logger;
    }

    public override async Task<RoomRegistrationResponse> RegisterToRoom(RoomRegistrationRequest request, ServerCallContext context) {
        UsersQueues.CreateUserQueue(request.RoomName, request.UserName);
        RoomRegistrationResponse response = new() { Joined = true };
        return await Task.FromResult(response);
    }

    public override async Task<NewsStreamStatus> SendNewsFlash(IAsyncStreamReader<NewsFlash> newsStream, ServerCallContext context) {
        while(await newsStream.MoveNext()) {
            NewsFlash news = newsStream.Current;
            MessagesQueue.AddNewsToQueue(news);
            this.logger.LogInformation("News flash: {@NewsItem}", news.NewsItem);
        }

        return new NewsStreamStatus { Success = true };
    }

    public override async Task StartMonitoring(Empty _, IServerStreamWriter<ReceivedMessage> streamWriter, ServerCallContext context) {
        while(true) {
            if(MessagesQueue.HasNewMessage())
                await streamWriter.WriteAsync(MessagesQueue.GetNextMessage());

            if(UsersQueues.HasAdminQueueMessage()) 
                await streamWriter.WriteAsync(UsersQueues.GetNextAdminMessage());
            
            await Task.Delay(500);
        }
    }

    public override async Task StartChat(IAsyncStreamReader<ChatMessage> incomingStream,
                                         IServerStreamWriter<ChatMessage> outgoingStream,
                                         ServerCallContext context) {

        // Wait for the first message to get the user name
        while(!await incomingStream.MoveNext()) {
            await Task.Delay(100);
        }

        String userName = incomingStream.Current.User;
        String room = incomingStream.Current.Room;
        this.logger.LogInformation("User {@userName} connected to room {@room}", userName, room);

        // TEST TEST TEST TEST - TO USE ONLY WHEN TESTING WITH BLOOMRPC
        //UsersQueues.CreateUserQueue(room, userName);
        // END TEST END TEST END TEST

        // Get messages from the user
        _ = Task.Run(async () => {
            while(await incomingStream.MoveNext()) {
                this.logger.LogInformation("Message received: {@contents}", incomingStream.Current.Contents);
                UsersQueues.AddMessageToRoom(ConvertToReceivedMessage(incomingStream.Current), incomingStream.Current.Room);
            }
        });


        // Check for messages to send to the user
        _ = Task.Run(async () => {
            while(true) {
                ReceivedMessage? userMsg = UsersQueues.GetMessageForUser(userName);
                if(userMsg is not null) {
                    ChatMessage userMessage = ConvertToChatMessage(userMsg, room);
                    await outgoingStream.WriteAsync(userMessage);
                }
                if(MessagesQueue.HasNewMessage()) {
                    ReceivedMessage news = MessagesQueue.GetNextMessage();
                    ChatMessage newsMessage = ConvertToChatMessage(news, room);
                    await outgoingStream.WriteAsync(newsMessage);
                }

                await Task.Delay(200);
            }
        });

        // Keep the method running
        while(true) {
            await Task.Delay(10_000);
        }
    }

    private ReceivedMessage ConvertToReceivedMessage(ChatMessage chatMessage) {
        ReceivedMessage receivedMessage = new() {
            Contents = chatMessage.Contents,
            MessageTime = chatMessage.MessageTime,
            User = chatMessage.User
        };
        return receivedMessage;
    }

    private ChatMessage ConvertToChatMessage(ReceivedMessage receivedMessage, String room) {
        ChatMessage chatMessage = new() {
            Contents = receivedMessage.Contents,
            User = receivedMessage.User,
            Room = room,
            MessageTime = receivedMessage.MessageTime
        };
        return chatMessage;
    }
}