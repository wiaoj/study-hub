using Microsoft.AspNetCore.SignalR;

namespace SignalRTelemetry;
public class TelemetryHub : Hub {
    public async Task SendMessage(String user, String message) {
        await this.Clients.All.SendAsync("TelemetryReceived", user, message);
    }
}