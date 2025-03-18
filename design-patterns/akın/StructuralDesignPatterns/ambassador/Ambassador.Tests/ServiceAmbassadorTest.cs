namespace Ambassador.Tests;
public class ServiceAmbassadorTest {
    [Fact]
    public void Test() {
        Int64 result = new ServiceAmbassador().DoRemoteFunction(10);
        Assert.True(result == 100 || result == RemoteServiceStatus.Failure.StatusCode());
    }
}