using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using yuapi_OcelotGateway.Middlewares;
using yuapi_OcelotGateway.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddSingleton<IUserVerificationService, UserVerificationService>();
builder.Services.AddSingleton(new InvokeCountServiceClient("http://localhost:5134"));

// Configure Kestrel to use settings from appsettings.json
builder.WebHost.ConfigureKestrel(options =>
{
    var kestrelSection = builder.Configuration.GetSection("Kestrel");
    options.Configure(kestrelSection);
});

var app = builder.Build();

// Configure the HTTP request pipeline
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseMiddleware<AccessControlMiddleware>();
app.UseMiddleware<UserVerificationMiddleware>();
app.UseMiddleware<ResponseHandlingMiddleware>();

app.MapGet("/", () => "Hello World!");
app.MapControllers();
await app.UseOcelot();

app.Run();
