﻿namespace State;
public static class Logger {
    public static List<String> Logs = new();

    public static String LastLog => Logs[^1];

    public static void Information(String message) {
        Logs.Add(message);
    }
}