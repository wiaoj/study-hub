const {RoomRegistrationRequest, RoomRegistrationResponse} = require('./groom_pb.js');
const {GroomClient} = require('./groom_grpc_web_pb.js');

var client = new GroomClient('http://localhost:5099');

var request = new RoomRegistrationRequest();
request.setRoomName('TestRoom');
request.setUserName('wiaoj');

client.registerToRoom(request, {}, (err, response) => {
  console.log(response.getJoined());
  alert("Joined: " + response.getJoined());
});