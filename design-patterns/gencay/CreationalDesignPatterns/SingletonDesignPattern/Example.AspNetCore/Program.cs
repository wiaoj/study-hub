using Example.AspNetCore.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

WebApplication app = builder.Build();
 
app.MapGet("x", () => {
    DatabaseService databaseService = DatabaseService.Instance;
    databaseService.Connect();
    databaseService.Disconnect();

    return Results.Ok(new {
        databaseService.Count
    });
});

app.MapGet("y", () => {
    DatabaseService databaseService = DatabaseService.Instance;
    databaseService.Connect();
    databaseService.Disconnect();

    return Results.Ok(new {
        databaseService.Count
    });
});

app.Run(); 