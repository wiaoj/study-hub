namespace Observer;
public class Orcs : IWeatherObserver {
    public String Update(WeatherType currentWeather) {
        return $"The orcs are facing {currentWeather.GetDescription()} weather now";
    }
}