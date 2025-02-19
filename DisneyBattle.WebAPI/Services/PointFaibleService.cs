using System.Data.Common;
using BStorm.Tools.Database;
using DisneyBattle.WebAPI.Mappers;
using DisneyBattle.WebAPI.Models;
using DisneyBattle.WebAPI.Repos;

namespace DisneyBattle.WebAPI.Services;

public class PointFaibleService : IPointFaibleRepository
{
    private readonly DbConnection _dbConnection;

    public PointFaibleService(DbConnection dbConnection)
    {
        _dbConnection = dbConnection;
        _dbConnection.Open();
    }
    public bool Insert(PointFaibleModel entity)
    {
        return 1 == _dbConnection.ExecuteNonQuery("INSERT INTO [dbo].[points_faibles] (nom, description) VALUES (@nom, @description)", false, new{ nom = entity.Nom, description = entity.Description });
    }

    public bool Update(int id, PointFaibleModel entity)
    {
        return 1 == _dbConnection.ExecuteNonQuery("UPDATE [dbo].[points_faibles] SET [dbo].[points_faibles].[nom] = @nom, [dbo].[points_faibles].[description] = @description WHERE [dbo].[points_faibles].[id] = @id ", false, entity);
    }

    public PointFaibleModel GetById(int id)
    {
        return _dbConnection.ExecuteReader("SELECT * FROM [dbo].[points_faibles] WHERE [dbo].[lieux].[id] = @Id", dr => dr.ToPointFaible(), false, new{ Id = id}).SingleOrDefault();
    }

    public IEnumerable<PointFaibleModel> GetAll()
    {
        return _dbConnection.ExecuteReader("SELECT * FROM [dbo].[points_faibles]", dr => dr.ToPointFaible());
    }
}