using groomserver.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddCors(x => x.AddPolicy("AllowAll", configure => {
    configure.AllowAnyOrigin()
             .AllowAnyMethod()
             .AllowAnyHeader()
             .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
}));

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<GroomService>();
app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
app.UseCors("AllowAll");
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client.");

app.Run();
