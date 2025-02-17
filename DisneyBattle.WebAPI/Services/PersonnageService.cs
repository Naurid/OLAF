using DisneyBattle.Interfaces;
using DisneyBattle.WebAPI.Models;
using DisneyBattle.WebAPI.Repositories;

namespace DisneyBattle.Services
{
    public class PersonnageService
    {
        private readonly IPersonnageRepository _personnageRepository;

        public PersonnageService(IPersonnageRepository personnageRepository)
        {
            _personnageRepository = personnageRepository;
        }

        public bool Insert(PersonnageModel personnage)
        {
            return _personnageRepository.Insert(personnage);
        }

        public bool Update(int id, PersonnageModel personnage)
        {
            return _personnageRepository.Update(id, personnage);
        }

        public PersonnageModel GetById(int id)
        {
            return _personnageRepository.GetById(id);
        }

        public IEnumerable<PersonnageModel> GetAll()
        {
            return _personnageRepository.GetAll();
        }
    }
}
