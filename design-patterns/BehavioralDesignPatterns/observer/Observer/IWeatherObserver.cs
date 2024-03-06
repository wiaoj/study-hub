namespace Observer;
public interface IWeatherObserver {
    String Update(WeatherType currentWeather);
}