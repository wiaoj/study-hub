using gRoom.gRPC.Messages;
using Grpc.Net.Client;


using GrpcChannel channel = GrpcChannel.ForAddress("http://localhost:5099");
var client = new Groom.GroomClient(channel);

Console.Write("Enter room name to register: ");
var roomName = Console.ReadLine();

var response = client.RegisterToRoom(new RoomRegistrationRequest() { RoomName = roomName });
Console.WriteLine($"Room  Id: {response.RoomId}");
