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
        throw new NotImplementedException();
    }

    public bool Update(int id, LieuModel entity)
    {
        throw new NotImplementedException();
    }

    public LieuModel GetById(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<LieuModel> GetAll()
    {
        return _dbConnection.ExecuteReader("SELECT * FROM [dbo].[lieux]", dr => dr.ToLieu());
    }
}