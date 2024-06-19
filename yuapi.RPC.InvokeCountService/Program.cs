using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Infrastructure.Persistence.Repositories;
using yuapi.RPC.InvokeCountService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddScoped<IUserInterfaceInfoRepository, UserInterfaceInfoRepository>(); // Ensure your repository is registered

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<InvokeCountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
