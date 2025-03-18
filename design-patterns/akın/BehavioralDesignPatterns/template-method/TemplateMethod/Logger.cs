namespace TemplateMethod;
internal static class Logger {
    public static Action<String> Log => (_) => { };

    public static void Info(String message) {
        Info(message, []);
    }

    public static void Info(String message, params Object?[] @params) {
        Log(String.Format(message, @params));
    }
}