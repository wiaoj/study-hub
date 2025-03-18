namespace Pipeline;
public static class Logger {
    public static List<String> Logs = new();

    public static String LastLog => Logs[^1];

    public static void Information(String message, params Object[] objects) {
        Logs.Add(String.Format(message, objects));
    }
}