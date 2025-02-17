
using DisneyBattle.Services;
using DisneyBattle.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DisneyBattle.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonnageController : Controller
    {
        private readonly PersonnageService _personnageService;

        public PersonnageController(PersonnageService personnageService)
        {
            _personnageService = personnageService;
        }

        [HttpPost]
        public IActionResult Insert(PersonnageModel personnage)
        {
            if (_personnageService.Insert(personnage))
                return Ok(new { message = "Personnage ajouté avec succès !" });

            return BadRequest(new { message = "Erreur lors de l'ajout du personnage." });
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, PersonnageModel personnage)
        {
            if (_personnageService.Update(id, personnage))
                return Ok(new { message = "Personnage mis à jour avec succès !" });

            return NotFound(new { message = "Personnage non trouvé." });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var personnage = _personnageService.GetById(id);
            if (personnage != null)
                return Ok(personnage);

            return NotFound(new { message = "Personnage non trouvé." });
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_personnageService.GetAll());
        }
    }
}
