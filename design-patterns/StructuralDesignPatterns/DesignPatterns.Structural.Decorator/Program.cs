using DesignPatterns.Structural.Decorator.RealWorld;

Book book = new("Worley", "Inside ASP.NET", 10);
book.Display();

// Create video
Video video = new("Spielberg", "Jaws", 23, 92);
video.Display();

// Make video borrowable, then borrow and display
Console.WriteLine("\nMaking video borrowable:");
Borrowable borrowvideo = new(video);
borrowvideo.BorrowItem("Customer #1");
borrowvideo.BorrowItem("Customer #2");
borrowvideo.Display();