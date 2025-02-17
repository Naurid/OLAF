using DisneyBattle.Interfaces;
using DisneyBattle.WebAPI.Models;
using System.Data.Common;

namespace DisneyBattle.WebAPI.Repos.IRepository
{
    public interface IEquipeService : ICRUInterface<EquipeModel, int>
    {      
    }
}
