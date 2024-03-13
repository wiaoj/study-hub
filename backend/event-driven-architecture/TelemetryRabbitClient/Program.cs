using RabbitMQ.Client;
using Spectre.Console;
using System.Text;

ConnectionFactory factory = new() {
    HostName = "localhost",
    Port = 5672,
};

AnsiConsole.Clear();
AnsiConsole.MarkupLine("[bold blue]Noise Sensor[/]");

AnsiConsole.Status()
    .Spinner(Spinner.Known.Star)
    .SpinnerStyle(Style.Parse("yellow"))
    .Start("Connecting...", context => {
        AnsiConsole.MarkupLine($"[yellow]Connected to:[/]");
        AnsiConsole.MarkupLine($"[grey]- Host: {factory.HostName}[/]");
        AnsiConsole.MarkupLine($"[grey]- Port: {factory.Port}[/]");
        AnsiConsole.MarkupLine("[grey]─────────────────[/]");
    });

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();

channel.QueueDeclare(queue: "telemetry", durable: false, exclusive: false, autoDelete: false, arguments: null);

while(true) {
    String? decibels = AnsiConsole.Ask<String>("[white]Please enter the decibels to send from the device to the queue: [/]");

    if(String.IsNullOrWhiteSpace(decibels)) {
        AnsiConsole.MarkupLine("[grey]... Skipping[/]");
        continue;
    }

    await AnsiConsole.Progress()
        .AutoRefresh(true)
        .AutoClear(false)
        .HideCompleted(false)
        .Columns([
            new TaskDescriptionColumn(),
            new ProgressBarColumn(),
            //new PercentageColumn(),
            //new ElapsedTimeColumn(),
            new SpinnerColumn()
        ])
        .StartAsync(async context => {
            ProgressTask task = context.AddTask("[green]Sending...[/]");

            while(!task.IsFinished) {
                task.Increment(1);
                await Task.Delay(1);
            }

            Byte[] body = Encoding.UTF8.GetBytes(decibels);
            channel.BasicPublish(exchange: "", routingKey: "telemetry", basicProperties: null, body: body);

            task.Description = $"[green]Sent![/] [darkcyan]'{decibels}'[/]";
        });
}