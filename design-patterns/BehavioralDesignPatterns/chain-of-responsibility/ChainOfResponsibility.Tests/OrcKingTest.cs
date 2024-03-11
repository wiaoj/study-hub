namespace ChainOfResponsibility.Tests;
public class OrcKingTest {
    [Theory]
    [InlineData(RequestType.DefendCastle, "Don't let the barbarians enter my castle!!")]
    [InlineData(RequestType.TorturePrisoner, "Don't just stand there, tickle him!")]
    [InlineData(RequestType.CollectTax, "Don't steal, the King hates competition...")]
    public void TestMakeRequestWithDifferentRequests(RequestType requestType, String requestDescription) {
        OrcKing king = new();
        Request request = new(requestType, requestDescription);

        king.MakeRequest(request);

        Assert.True(request.IsHandled, $"Expected request of type {requestType} to be handled, but it was not!");
    }
}