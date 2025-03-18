namespace Observer;
public enum WeatherType {
    Sunny,
    Rainy,
    Windy,
    Cold
}

public static class WeatherTypeExtensions {
    public static String GetDescription(this WeatherType weatherType) {
        return Enum.GetName(weatherType)!;
    }
}