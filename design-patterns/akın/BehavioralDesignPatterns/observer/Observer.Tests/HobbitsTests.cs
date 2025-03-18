namespace Observer.Tests;
public class HobbitsTests {
    [Theory]
    [InlineData(WeatherType.Sunny, "The hobbits are facing Sunny weather now")]
    [InlineData(WeatherType.Rainy, "The hobbits are facing Rainy weather now")]
    [InlineData(WeatherType.Windy, "The hobbits are facing Windy weather now")]
    [InlineData(WeatherType.Cold, "The hobbits are facing Cold weather now")]
    public void Hobbits_ReactCorrectly_ToDifferentWeatherConditions(WeatherType weather, String expectedMessage) {
        Hobbits hobbits = new();

        String message = hobbits.Update(weather);

        Assert.Equal(expectedMessage, message);
    }
}