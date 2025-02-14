using DisneyBattle.Interfaces;
using DisneyBattle.WebAPI.Models;
using System.Collections.Generic;

namespace DisneyBattle.WebAPI.Repositories
{
    public interface IPersonnageRepository : ICRUInterface<PersonnageModel, int>
    {
    }
}
