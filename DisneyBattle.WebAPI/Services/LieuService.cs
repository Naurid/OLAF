using System.Data.Common;
using BStorm.Tools.Database;
using DisneyBattle.WebAPI.Mappers;
using DisneyBattle.WebAPI.Models;
using DisneyBattle.WebAPI.Repos;

namespace DisneyBattle.WebAPI.Services;

public class LieuService:ILieuRepository
{
    private readonly DbConnection _dbConnection;

    public LieuService(DbConnection dbConnection)
    {
        _dbConnection = dbConnection;
        _dbConnection.Open();
    }
    public bool Insert(LieuModel entity)
    {
        return 1 == _dbConnection.ExecuteNonQuery("INSERT INTO [dbo].[lieux] (nom, description) VALUES @nom, @description ", false, new{ nom = entity.Name, description = entity.Description });
    }

    public bool Update(int id, LieuModel entity)
    {
        return 1 == _dbConnection.ExecuteNonQuery("UPDATE [dbo].[lieux] SET [dbo].[lieux].[nom] = @nom, [dbo].[lieux].[description] = @description WHERE [dbo].[lieux].[id] = @id ", false, entity);
    }

    public LieuModel GetById(int id)
    {
        return _dbConnection.ExecuteReader("SELECT * FROM [dbo].[lieux] WHERE [dbo].[lieux].[id] = @Id", dr => dr.ToLieu(), false, new{ Id = id}).SingleOrDefault();
    }

    public IEnumerable<LieuModel> GetAll()
    {
        return _dbConnection.ExecuteReader("SELECT * FROM [dbo].[lieux]", dr => dr.ToLieu());
    }
}