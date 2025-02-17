using DisneyBattle.WebAPI.Models;
using DisneyBattle.WebAPI.Repos;
using Microsoft.AspNetCore.Mvc;

namespace DisneyBattle.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipementController : ControllerBase
    {
        private IEquipementServices _equipementServices;

        public EquipementController(IEquipementServices equipementServices)
        {
            _equipementServices = equipementServices;
        }

        // Récupérer tous les équipements - Get: api/equipement
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_equipementServices.GetAll());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur: {ex.Message}");
            }
        }

        // Récupérer un équipement par ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var equipement = _equipementServices.GetById(id);

                if (equipement == null)
                {
                    return NotFound($"Aucun équipement trouvé avec l'ID {id}");
                }
                return Ok(equipement);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur : {ex.Message}");
            }
        }

        // Insérer un nouvel équipement
        [HttpPost]
        public IActionResult Insert([FromBody] EquipementModel equipement)
        {
            try
            {
                if (equipement == null)
                {
                    return BadRequest("Les données de l'équipement sont invalides.");
                }

                bool success = _equipementServices.Insert(equipement);

                if (!success)
                {
                    return StatusCode(500, "Erreur lors de l'insertion de l'équipement.");
                }
                return CreatedAtAction(nameof(GetById), new { id = equipement.Id }, equipement);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur : {ex.Message}");
            }
        }

        // Mettre à jour un équipement existant
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] EquipementModel equipement)
        {
            try
            {
                if (equipement == null)
                {
                    return BadRequest("Les données de l'équipement sont invalides.");
                }

                bool success = _equipementServices.Update(id, equipement);

                if (!success)
                {
                    return NotFound($"Aucun équipement trouvé avec l'ID {id} ou mise à jour échouée.");
                }
                return NoContent(); // 204 No Content si succès
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erreur serveur : {ex.Message}");
            }
        }
    }
}
