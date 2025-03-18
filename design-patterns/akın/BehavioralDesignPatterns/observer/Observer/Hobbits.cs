namespace Observer;
public class Hobbits : IWeatherObserver {
    public String Update(WeatherType currentWeather) {
        return $"The hobbits are facing {currentWeather.GetDescription()} weather now";
    }
}