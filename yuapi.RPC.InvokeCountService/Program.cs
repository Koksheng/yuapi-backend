using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using yuapi.Application.Common.Interfaces.Persistence;
using yuapi.Infrastructure.Persistence;
using yuapi.Infrastructure.Persistence.Interceptors;
using yuapi.Infrastructure.Persistence.Repositories;
using yuapi.RPC.InvokeCountService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

// Register the DataContext with a connection string
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
// Add the interceptors
builder.Services.AddScoped<PublishDomainEventsInterceptor>();

// Ensure your repository and other services are registered
builder.Services.AddScoped<IUserInterfaceInfoRepository, UserInterfaceInfoRepository>(); // Ensure your repository is registered


var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<InvokeCountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
