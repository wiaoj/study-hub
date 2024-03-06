using NSubstitute;
using Xunit.Abstractions;

namespace Observer.Tests;
public class WeatherTests {
    private readonly Weather weather;
    private readonly ITestOutputHelper outputHelper;

    public WeatherTests(ITestOutputHelper outputHelper) {
        this.weather = new Weather();
        this.outputHelper = outputHelper;
    }

    [Fact]
    public void TestAddObserver_NotifiesOnWeatherChange() {
        // Arrange
        IWeatherObserver observer = Substitute.For<IWeatherObserver>();

        // Act 
        this.weather.AddObserver(observer);
        this.weather.TimePasses();

        // Assert 
        observer.Received(1).Update(Arg.Is(WeatherType.Rainy));
    }

    [Fact]
    public void TestRemoveObserver_StopsNotifyingOnWeatherChange() {
        // Arrange
        IWeatherObserver observer = Substitute.For<IWeatherObserver>();
        this.weather.AddObserver(observer);

        // Act 
        this.weather.RemoveObserver(observer);
        observer.ClearReceivedCalls();
        this.weather.TimePasses();

        // Assert 
        observer.DidNotReceive().Update(Arg.Any<WeatherType>());
    }

    [Theory]
    [InlineData(20)]
    public void TestTimePasses(Int32 cycleCount) {
        IWeatherObserver observer = Substitute.For<IWeatherObserver>();
        this.weather.AddObserver(observer);

        for(Int32 i = 0; i < cycleCount; i++) {
            this.weather.TimePasses();
            observer.Received(1).Update(this.weather.Current);
            observer.ClearReceivedCalls();
        }
    }

    [Theory]
    [InlineData(100, 1)]
    [InlineData(100, 2)]
    [InlineData(100, 5)]
    [InlineData(100, 10)]
    [InlineData(100, 50)]
    public void TestTimePasses_WithVariableNumberOfObservers_EachObserverNotifiedOnce(Int32 cycleCount, Int32 numberOfObservers) {
        // Arrange
        List<IWeatherObserver> observers = [];
        for(Int32 i = 0; i < numberOfObservers; i++)
            observers.Add(Substitute.For<IWeatherObserver>());
        this.weather.AddObservers(observers);

        // Act & Assert
        for(Int32 i = 1; i <= cycleCount; i++) {
            this.weather.TimePasses();

            foreach(IWeatherObserver observer in observers) {
                observer.Received(1).Update(this.weather.Current);
                observer.ClearReceivedCalls();

                this.outputHelper.WriteLine($"Cycle {i}: Observer {observers.IndexOf(observer) + 1} received update for {this.weather.Current}");
            }
        }
    }
}