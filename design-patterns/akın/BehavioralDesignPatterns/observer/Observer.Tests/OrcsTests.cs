namespace Observer.Tests;
public class OrcsTests {
    [Theory]
    [InlineData(WeatherType.Sunny, "The orcs are facing Sunny weather now")]
    [InlineData(WeatherType.Rainy, "The orcs are facing Rainy weather now")]
    [InlineData(WeatherType.Windy, "The orcs are facing Windy weather now")]
    [InlineData(WeatherType.Cold, "The orcs are facing Cold weather now")]
    public void Orcs_ReactCorrectly_ToDifferentWeatherConditions(WeatherType weather, String expectedMessage) {
        Orcs orcs = new();

        String message = orcs.Update(weather);

        Assert.Equal(expectedMessage, message);
    }
}