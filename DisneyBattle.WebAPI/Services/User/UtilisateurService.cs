using System.Data.Common;
using BStorm.Tools.Database;
using DisneyBattle.WebAPI.Mappers;
using DisneyBattle.WebAPI.Models;
using Microsoft.Data.SqlClient;

namespace DisneyBattle.WebAPI.Services;

public class UtilisateursService(DbConnection dbConnection) : IUtilisateurServices
{
    public IEnumerable<UtilisateurModel> GetAll()
    {
        dbConnection.Open();
        return dbConnection.ExecuteReader("SELECT * FROM [utilisateurs]", m => m.ToUtilisateur());
    }

    public UtilisateurModel GetById(int id)
    {
        dbConnection.Open();
        UtilisateurModel? model = dbConnection.ExecuteReader("SELECT * FROM [utilisateurs]", m => m.ToUtilisateur()).SingleOrDefault();
        if (model is null)
        {
            return null;
        }
        return model;
    }

    public bool Insert(UtilisateurModel entity)
    {
        string sql_query = "INSERT INTO [utilisateurs] ([pseudo],[email],[mot_de_passe]) VALUES(@Pseudo,@Email,@MotDePasse)";
        using DbCommand command = dbConnection.CreateCommand();
        command.CommandText = sql_query;
        command.Parameters.Add(new SqlParameter() { ParameterName = "@Pseudo", Value = entity.Pseudo });
        command.Parameters.Add(new SqlParameter() { ParameterName = "@Email", Value = entity.Email });
        command.Parameters.Add(new SqlParameter() { ParameterName = "@MotDePasse", Value = entity.MotDePasse });
        dbConnection.Open();

        int affectedRow = command.ExecuteNonQuery();
        return affectedRow >0;
    }

    public bool Update(int id, UtilisateurModel entity)
    {
        throw new NotImplementedException();
    }
}