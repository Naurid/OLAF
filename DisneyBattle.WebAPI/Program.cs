using DisneyBattle.Services;
using DisneyBattle.WebAPI.Repos;
using DisneyBattle.WebAPI.Repositories;
using DisneyBattle.WebAPI.Services;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Data.Common;
using DisneyBattle.WebAPI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
// Configuration de la chaîne de connexion
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorApp",
        builder => builder.WithOrigins("https://localhost:7262")
                          .AllowAnyMethod()
                          .AllowAnyHeader()
                          .AllowCredentials());
});
builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri("https://localhost:7171/api/")
    });
// Injection des dépendances :
builder.Services.AddTransient<IPersonnageRepository, PersonnageRepository>(provider =>
    new PersonnageRepository(connectionString));

builder.Services.AddTransient<PersonnageService>();

builder.Services.AddTransient<DbConnection>(sp => new SqlConnection(connectionString));
builder.Services.AddTransient<IEquipementServices, EquipementServices>();

/* Jwt */
// Je récupère les infos de config de jwt à partir du
// fichier appsettings.json et je stocke le tout 
// dans la classe prévue 
JwtOptions options = builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>();

// Pour pouvoir utiliser le jwtoption, il faut l'injecter
builder.Services.AddSingleton(options);

// On configure l'authentication dans les services
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        // Je vais rechercher ma clé de signature
        byte[] sKey = Encoding.UTF8.GetBytes(options.SigningKey);

        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = options.Issuer,
            ValidAudience = options.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(sKey)
        };
    });

    builder.Services.AddAuthorization(
        optionsAutho =>
        {
            optionsAutho.AddPolicy("Best", Policy =>
            {
                Policy.RequireClaim("groups",
                    new[]
                    {
                        "12eb6100-e2e4-481e-bc69-6256e2c1cdb7",
                        "1acc7dc6-359f-42f4-a2b2-e2c9f5f53579"
                    });
            });
        });


//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("DefaultPolicy", policy =>
//    {
//        string[] corsOrigins = null;
//        policy.AllowAnyOrigin()
//              .AllowAnyMethod()
//              .AllowAnyHeader()
//              .AllowCredentials()
//              .SetIsOriginAllowedToAllowWildcardSubdomains()
//              .WithExposedHeaders("Content-Disposition");
//    });
//});

// Configuration spécifique pour le développement
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("DevPolicy", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
    });
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Use CORS policy
app.UseCors("AllowBlazorApp");
app.UseHttpsRedirection();

// Use Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();

app.UseAuthorization();