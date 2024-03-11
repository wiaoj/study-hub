namespace ChainOfResponsibility;
public interface IRequestHandler {
    Int32 Priority { get; }
    String Name { get; }
    Boolean CanHandleRequest(Request request);
    void Handle(Request request);
}