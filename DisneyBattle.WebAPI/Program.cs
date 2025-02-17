using DisneyBattle.WebAPI.Repos;
using DisneyBattle.WebAPI.Services;
using Microsoft.Data.SqlClient;
using System.Data.Common;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddTransient<DbConnection>(sp => new SqlConnection(connectionString));
builder.Services.AddTransient<IEquipementServices, EquipementServices>();

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


