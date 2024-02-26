namespace DesingPatterns.Structural.Proxy.Structural;
public class Proxy : Subject {
    private RealSubject realSubject;

    public override void Request() {
        // Use 'lazy initialization'
        this.realSubject ??= new RealSubject();
        this.realSubject.Request();
    }
}