using Google.Protobuf.WellKnownTypes;
using gRoom.gRPC.Messages;

namespace groomserver.Utils;
public static class MessagesQueue {
    private static readonly Queue<ReceivedMessage> queue = new();

    public static void AddNewsToQueue(NewsFlash news) {
        ReceivedMessage message = new() {
            Contents = news.NewsItem,
            User = "NewsBot",
            MessageTime = Timestamp.FromDateTime(DateTime.UtcNow)
        };
        queue.Enqueue(message);
    }

    public static ReceivedMessage GetNextMessage() {
        return queue.Dequeue();
    }

    public static Boolean HasNewMessage() {
        return queue.Any();
    }
}