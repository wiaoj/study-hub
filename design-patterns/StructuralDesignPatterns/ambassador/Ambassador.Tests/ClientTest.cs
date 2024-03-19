namespace Ambassador.Tests;
public class ClientTest {
    [Fact]
    public void Test() {
        Client client = new();
        Int64 result = client.UseService(10);

        Assert.True(result is 100 || result == RemoteServiceStatus.Failure.StatusCode());
    }
}