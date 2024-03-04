using Google.Protobuf.WellKnownTypes;
using gRoom.gRPC.Messages;
using Grpc.Net.Client;

using GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:5099");
Groom.GroomClient client = new(channel);

Console.WriteLine("Welcome the the gRoom chat!");
Console.Write("Please type your user name: ");
String? username = Console.ReadLine();

Console.Write("Please type the name of the room you want to join (ie. Chat): ");
String? room = Console.ReadLine();

Console.WriteLine($"Joining room {room}...");

try {
    RoomRegistrationResponse joinResponse = client.RegisterToRoom(new RoomRegistrationRequest { 
        RoomName = room, 
        UserName = username 
    }, 
        deadline: DateTime.UtcNow.AddSeconds(5));
    if(joinResponse.Joined) {
        Console.WriteLine("Joined successfully!");
    }
    else {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Error joining room {room}.");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Press any key to close the window.");
        Console.Read();
        return;
    }
}
catch(Grpc.Core.RpcException ex) {
    if(ex.StatusCode == Grpc.Core.StatusCode.DeadlineExceeded) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Timeout exceeded when trying to join the {room} room. Please try again later.");
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine("Press any key to close the window.");
        Console.Read();
        return;
    }
}
catch(Exception ex) {
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"Error joining room {room}. Error: {ex.Message}");
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.WriteLine("Press any key to close the window.");
    Console.Read();
    return;
}

Console.WriteLine($"Press any key to enter the {room} room.");
Console.Read();
Console.Clear();


Grpc.Core.AsyncDuplexStreamingCall<ChatMessage, ChatMessage> call = client.StartChat();

CancellationTokenSource cancellationTokenSource = new();

String promptText = "Type your message: ";
Int32 row = 2;
_ = Task.Run(async () => {
    while(true) {
        if(await call.ResponseStream.MoveNext(cancellationTokenSource.Token)) {
            ChatMessage message = call.ResponseStream.Current;
            PrintMessage(message);
        }
        await Task.Delay(500);
    }
});

Console.Write(promptText);
while(true) {
    String? input = Console.ReadLine();
    RestoreInputCursor();

    ChatMessage requestMessage = new() {
        Contents = input,
        MessageTime = Timestamp.FromDateTime(DateTime.UtcNow),
        Room = room,
        User = username
    };
    await call.RequestStream.WriteAsync(requestMessage);
}

// Utilities methods for positioning the cursor
void PrintMessage(ChatMessage message) {
    Int32 left = Console.CursorLeft - promptText.Length;
    Console.SetCursorPosition(0, row++);
    Console.Write($"[{message.MessageTime.ToDateTime():hh:mm:ss}] {message.User}: {message.Contents}");
    Console.SetCursorPosition(promptText.Length + left, 0);
}

void RestoreInputCursor() {
    Console.SetCursorPosition(promptText.Length - 1, 0);
    Console.Write(new String(' ', 46));
    Console.SetCursorPosition(promptText.Length - 1, 0);
}