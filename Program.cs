using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Websocket_UI.Services;

var builder = WebApplication.CreateBuilder(args);
var redisConfig = builder.Configuration.GetSection("Redis");
var EndPoint = $"{redisConfig["host"]}:{redisConfig["port"]}";
var options = new ConfigurationOptions
{
    EndPoints = { EndPoint },
    Password = redisConfig["password"],
    Ssl = true,
    User = redisConfig["username"]
};
if (builder.Environment.IsProduction())
{
    EndPoint = "redis://red-ctn864dds78s73c22v0g:6379";
    options = new ConfigurationOptions
    {
        EndPoints = { EndPoint },
    };
}

builder.Services.AddSingleton<RedisService>(sp =>
    new RedisService(options));

// Add other services (e.g., controllers, if using a web API)
builder.Services.AddControllers();


// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.Run();
