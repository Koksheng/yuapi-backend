using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using yuapi_OcelotGateway.Middlewares;
using yuapi_OcelotGateway.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddSingleton<IUserVerificationService, UserVerificationService>();
builder.Services.AddSingleton(new InvokeCountServiceClient("https://localhost:5001"));

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<AccessControlMiddleware>();
app.UseMiddleware<UserVerificationMiddleware>();

app.MapGet("/", () => "Hello World!");
app.MapControllers();
await app.UseOcelot();

app.Run();
