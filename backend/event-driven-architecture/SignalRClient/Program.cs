using Microsoft.AspNetCore.SignalR.Client;

Console.WriteLine("SignalR Client Starting...");

const String SIGNALR_HUB_URL = "http://localhost:5234/telemetryHub";

Console.WriteLine("Connecting to hub...");
HubConnection connection = new HubConnectionBuilder()
    .WithUrl(SIGNALR_HUB_URL, (options) => {
        options.HttpMessageHandlerFactory = (message) => {
        if(message is HttpClientHandler clientHandler)
            // always verify the SSL certificate
            clientHandler.ServerCertificateCustomValidationCallback +=
                (sender, certificate, chain, sslPolicyErrors) => { return true; };
        return message;
    };
}).Build();

await connection.StartAsync();
Console.WriteLine($"Connected successfully. Connection state: {connection.State}");

connection.On<Int32>("TelemetryReceived", (decibels) =>
    Console.WriteLine($"TelemetryReceived: {decibels}")
);

Console.Read();
