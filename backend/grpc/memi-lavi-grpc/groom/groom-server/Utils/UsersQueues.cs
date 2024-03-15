using gRoom.gRPC.Messages;

namespace groomserver.Utils;
public static class UsersQueues {
    private static readonly List<UserQueue> queues = new();
    private static readonly Queue<ReceivedMessage> adminQueue = new();

    public static void CreateUserQueue(String room, String user) {
        queues.Add(new UserQueue(room, user));
    }

    public static void AddMessageToRoom(ReceivedMessage message, String room) {
        foreach(UserQueue? queue in queues.Where(x => x.Room == room))
            queue.AddMessageToQueue(message);
        adminQueue.Enqueue(message);
    }

    public static ReceivedMessage? GetMessageForUser(String user) {
        UserQueue userQueue = queues.First(q => q.User == user);
        return userQueue.HasNewMessage() ? userQueue.GetNextMessage() : default;
    }

    public static Boolean HasAdminQueueMessage() {
        return adminQueue.Any();
    }

    public static ReceivedMessage GetNextAdminMessage() {
        return adminQueue.Dequeue();
    }
}

internal class UserQueue {
    private Queue<ReceivedMessage> queue { get; }
    public String Room { get; }
    public String User { get; }

    public UserQueue(String room, String user) {
        this.Room = room;
        this.User = user;
        this.queue = new Queue<ReceivedMessage>();
    }

    public void AddMessageToQueue(ReceivedMessage message) {
        this.queue.Enqueue(message);
    }

    public ReceivedMessage GetNextMessage() {
        return this.queue.Dequeue();
    }

    public Boolean HasNewMessage() {
        return this.queue.Any();
    }
}