using GuessingGame.Data;
using GuessingGame.Repositories;
using GuessingGame.Repositories.Interfaces;
using GuessingGame.Services;
using GuessingGame.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetSection("ConnectionStrings")["mssql"];

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("GuessNumber");
    //options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<IGameRepository, GameRepository>();
builder.Services.AddScoped<IGamerRepository, GamerRepository>();
builder.Services.AddScoped<IGameService, GameService>();

builder.Services.AddCors(o =>
{
    o.AddPolicy("Enable", p =>
    {
        p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddControllers().AddNewtonsoftJson(o =>
{
    o.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Enable");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
