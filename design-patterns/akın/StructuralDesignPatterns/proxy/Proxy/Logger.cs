namespace Proxy;
public static class Logger {
    public static readonly List<String> Logs = [];
    public static void Information(String message, params Object[] objects) {
        Logs.Add(String.Format(message, objects));
    }
}