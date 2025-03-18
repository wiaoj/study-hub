namespace ChainOfResponsibility;
public class Request {
    private readonly RequestType requestType;
    private readonly String requestDescription;
    private Boolean handled;

    public String RequestDescription => this.requestDescription;
    public RequestType RequestType => this.requestType;
    public Boolean IsHandled => this.handled;

    public Request(RequestType requestType, String requestDescription) {
        this.requestType = requestType;
        this.requestDescription = requestDescription;
    }

    public void MarkHandled() {
        this.handled = true;
    }

    public override String ToString() {
        return this.RequestDescription;
    }
}