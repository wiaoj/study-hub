﻿using Google.Protobuf.WellKnownTypes;
using gRoom.gRPC.Messages;
using groomserver.Utils;
using Grpc.Core;

namespace groomserver.Services;
public class GroomService : Groom.GroomBase {
    private readonly ILogger<GroomService> logger;
    public GroomService(ILogger<GroomService> logger) {
        this.logger = logger;
    }

    public override Task<RoomRegistrationResponse> RegisterToRoom(RoomRegistrationRequest request, ServerCallContext context) {
        this.logger.LogInformation("Service called...");
        Int32 roomNo = Random.Shared.Next(1, 100);
        this.logger.LogInformation("Room no: {@roomNo}", roomNo);
        RoomRegistrationResponse response = new() { RoomId = roomNo };
        return Task.FromResult(response);
    }

    public override async Task<NewsStreamStatus> SendNewsFlash(IAsyncStreamReader<NewsFlash> newsStream, ServerCallContext context) {
        while(await newsStream.MoveNext()) {
            var news = newsStream.Current;
            MessagesQueue.AddNewsToQueue(news);
            this.logger.LogInformation("News flash: {@NewsItem}", news.NewsItem);
        }

        return new NewsStreamStatus { Success = true };
    }

    public override async Task StartMonitoring(Empty _, IServerStreamWriter<ReceivedMessage> streamWriter, ServerCallContext context) {
        while(true) {
            if(MessagesQueue.HasNewMessage()) 
                await streamWriter.WriteAsync(MessagesQueue.GetNextMessage());
            
            await Task.Delay(500);
        }
    }
}