namespace Pipeline.Tests;
public class PipelineTest {
    [Fact]
    public void TestAddHandlersToPipeline() {
        Pipeline<String, Char[]> filters = new Pipeline<String, String>(new RemoveAlphabetsHandler())
            .Then(new RemoveDigitsHandler())
            .Then(new ConvertToCharArrayHandler());

        Assert.Equal(
            ['#', '!', '(', '&', '%', '#', '!'],
            filters.Execute("#H!E(L&L0O%THE3R#34E!")
        );
    }
}