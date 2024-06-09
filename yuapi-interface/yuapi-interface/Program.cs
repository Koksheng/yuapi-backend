using yuapi_interface.Client;
using yuapi_interface.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Register HttpClient
builder.Services.AddHttpClient<YuApiClient>(client =>
{
    client.BaseAddress = new Uri("http://localhost:8123/");
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Kestrel to use settings from appsettings.json
builder.WebHost.ConfigureKestrel(options =>
{
    var kestrelSection = builder.Configuration.GetSection("Kestrel");
    options.Configure(kestrelSection);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

