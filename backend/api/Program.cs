using Microsoft.EntityFrameworkCore;
using SqlServerConnector;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DevConnection");

builder.Services.AddSingleton(new SqlHelper(connectionString));

/*#if DEBUG
builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(4545); // HTTP port
    options.ListenAnyIP(4646, listenOptions =>
    {
        listenOptions.UseHttps(); // HTTPS port
    });
});
#endif*/

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
