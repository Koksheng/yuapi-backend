using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using yuapi_OcelotGateway.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddSingleton<IUserVerificationService, UserVerificationService>();
builder.Services.AddSingleton(new InvokeCountServiceClient("https://localhost:5001"));

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapControllers();
await app.UseOcelot();

app.Run();
