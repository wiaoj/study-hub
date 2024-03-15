using Google.Protobuf.WellKnownTypes;
using gRoom.gRPC.Messages;
using Grpc.Net.Client;

using var channel = GrpcChannel.ForAddress("http://localhost:5099");
var client = new Groom.GroomClient(channel);

Console.WriteLine("*** Admin Console started ***");
Console.WriteLine("Listening...");


using Grpc.Core.AsyncServerStreamingCall<ReceivedMessage> call = client.StartMonitoring(new Empty());
CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();


while(await call.ResponseStream.MoveNext(cancellationTokenSource.Token)) {
    var message = call.ResponseStream.Current;
    Console.WriteLine($"New message: {message.Contents} - User: {message.User} - AtTime: {message.MessageTime}");
}