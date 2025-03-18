namespace Ambassador;
public enum RemoteServiceStatus {
    Failure = -1
}

public static class RemoteServiceStatusExtensions {
    public static Int64 StatusCode(this RemoteServiceStatus status) {
        return (Int64)status;
    }
}