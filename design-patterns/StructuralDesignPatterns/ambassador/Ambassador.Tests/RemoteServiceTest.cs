namespace Ambassador.Tests;
public class RemoteServiceTest {
    [Fact]
    public void TestFailedCall() {
        RemoteService remoteService = new(() => .21);
        Int64 result = remoteService.DoRemoteFunction(10);
        Assert.Equal(RemoteServiceStatus.Failure.StatusCode(), result);
    }

    [Fact]
    public void TestSuccessfulCall() {
        RemoteService remoteService = new(() => .2);
        Int64 result = remoteService.DoRemoteFunction(10);
        Assert.Equal(100, result);
    }
}