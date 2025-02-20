using DisneyBattle.Services;
using DisneyBattle.WebAPI.Repos;
using DisneyBattle.WebAPI.Repositories;
using DisneyBattle.WebAPI.Services;
using Microsoft.Data.SqlClient;
using System.Data.Common;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
// Configuration de la cha�ne de connexion
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp",
        builder => builder.WithOrigins("https://localhost:7262","http://localhost:5240","http://localhost:5143")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials());
});
builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("https://localhost:5240/api/")
    });  
// Injection des d�pendances

builder.Services.AddScoped<DbConnection>(
    s => new SqlConnection(connectionString));
builder.Services.AddTransient<IPersonnageRepository, PersonnageRepository>(provider =>
    new PersonnageRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<PersonnageService>();
builder.Services.AddTransient<IEquipementServices, EquipementServices>();
builder.Services.AddTransient<ILieuRepository, LieuService>();
builder.Services.AddTransient<IUtilisateurServices, UtilisateursService>();
builder.Services.AddTransient<IPointFaibleRepository, PointFaibleService>();
 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Use CORS policy
app.UseCors("AllowBlazorApp");
app.MapControllers();
app.UseHttpsRedirection();
app.Run();
