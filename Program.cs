using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Websocket_UI.Services;

var builder = WebApplication.CreateBuilder(args);
var redisConfig = builder.Configuration.GetSection("Redis");
var EndPoint = $"{redisConfig["host"]}:{redisConfig["port"]}";
var url = redisConfig["url"];
var options = new ConfigurationOptions();
if (!string.IsNullOrEmpty(url))
{
    var uri = new Uri(url);

    options.EndPoints.Add(uri.Host, uri.Port);
    if (!string.IsNullOrEmpty(uri.UserInfo))
    {
        options.User = uri.UserInfo.Split(':')[0]; // Extract username
        options.Password = uri.UserInfo.Split(':')[1]; // Extract password
    }
    options.Ssl = uri.Scheme == "rediss"; // Use SSL if scheme is rediss
}
//options = new ConfigurationOptions
//{
//    EndPoints = { EndPoint },
//    Password = redisConfig["password"],
//    Ssl = true,
//    User = redisConfig["username"]
//};

try
{
    builder.Services.AddSingleton<RedisService>(sp =>
        new RedisService(options));
}
catch (Exception ex)
{
    Console.Error.WriteLine($"Redis connection error: {ex.Message}");
    throw;
}


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
