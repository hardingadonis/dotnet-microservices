var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();
await builder.Services.AddInfrastructure();
builder.Services.AddApplication();

var app = builder.Build();

app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client.");

await app.RunAsync();