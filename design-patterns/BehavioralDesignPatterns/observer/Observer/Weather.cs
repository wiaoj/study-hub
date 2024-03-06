namespace Observer;
public class Weather {
    private WeatherType currentWeather;
    private readonly List<IWeatherObserver> weatherObservers;

    public WeatherType Current => currentWeather;

    public Weather() {
        this.weatherObservers = [];
        this.currentWeather = WeatherType.Sunny;
    }

    public void AddObserver(IWeatherObserver weatherObserver) {
        this.weatherObservers.Add(weatherObserver);
    } 
    
    public void AddObservers(IEnumerable<IWeatherObserver> weatherObservers) {
        this.weatherObservers.AddRange(weatherObservers);
    }

    public void RemoveObserver(IWeatherObserver weatherObserver) {
        this.weatherObservers.Remove(weatherObserver);
    }

    public void TimePasses() {
        ChangeCurrentWeather();
        NotifyObservers();
    }

    private void ChangeCurrentWeather() {
        this.currentWeather = (WeatherType)(((Int32)this.currentWeather + 1) % Enum.GetValues(typeof(WeatherType)).Length);
    }

    private void NotifyObservers() {
        this.weatherObservers.ForEach(weatherObserver => weatherObserver.Update(this.currentWeather));
    }
}