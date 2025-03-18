namespace DesingPatterns.Structural.Proxy.Structural;
public class RealSubject : Subject {
    public override void Request() {
        Console.WriteLine($"Called {nameof(RealSubject)}.{nameof(Request)}()");
    }
}