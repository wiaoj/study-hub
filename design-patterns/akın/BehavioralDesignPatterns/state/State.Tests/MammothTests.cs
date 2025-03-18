namespace State.Tests;
public class MammothTests {
    [Fact]
    public void TestTimePasses() {
        Mammoth mammoth = new();

        mammoth.Observe();
        Assert.Equal("The mammoth is grazing peacefully.", Logger.LastLog);
        Assert.Single(Logger.Logs);

        mammoth.TimePasses();
        Assert.Equal("The mammoth roars in anger!", Logger.LastLog);
        Assert.Equal(2, Logger.Logs.Count);

        mammoth.Observe();
        Assert.Equal("The mammoth is in a rage!", Logger.LastLog);
        Assert.Equal(3, Logger.Logs.Count);

        mammoth.TimePasses();
        Assert.Equal("The mammoth sighs contentedly.", Logger.LastLog);
        Assert.Equal(4, Logger.Logs.Count);

        mammoth.Observe();
        Assert.Equal("The mammoth is grazing peacefully.", Logger.LastLog);
        Assert.Equal(5, Logger.Logs.Count);
    }

    [Fact]
    public void TestToString() {
        String mammothName = new Mammoth().ToString();
        Assert.NotNull(mammothName);
        Assert.Equal("The mammoth", mammothName);
    }
}