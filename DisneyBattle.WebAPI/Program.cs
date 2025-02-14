using System.Data.Common;
using DisneyBattle.WebAPI.Repos;
using DisneyBattle.WebAPI.Services;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<DbConnection>(
    s => new SqlConnection(builder.Configuration.GetConnectionString("DisneyBD")));
builder.Services.AddTransient<ILieuRepository, LieuService>();
builder.Services.AddTransient<IUtilisateurServices, UtilisateursService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();

