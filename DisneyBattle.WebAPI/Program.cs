
using DisneyBattle.Services;
using DisneyBattle.Interfaces;
using DisneyBattle.WebAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Ajout des services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration de la chaîne de connexion
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Injection des dépendances
builder.Services.AddTransient<IPersonnageRepository, PersonnageRepository>(provider =>
    new PersonnageRepository(connectionString));

builder.Services.AddTransient<PersonnageService>();

var app = builder.Build();

// Configuration du pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
