using BStorm.Tools.Database;
using DisneyBattle.WebAPI.Mappers;
using DisneyBattle.WebAPI.Models;
using DisneyBattle.WebAPI.Repos.IRepository;
using System.Data;
using System.Data.Common;

namespace DisneyBattle.WebAPI.Services
{
    public class EquipeService : IEquipeService
    {
        private DbConnection _connection;

        public EquipeService(DbConnection connection)
        {
            _connection = connection;
            _connection.Open();
        }

        public IEnumerable<EquipeModel> GetAll()
        {
            return _connection.ExecuteReader("SELECT * FROM [dbo].[equipes]", dr => dr.ToEquipe());
        }

        public EquipeModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(EquipeModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(int id, EquipeModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
