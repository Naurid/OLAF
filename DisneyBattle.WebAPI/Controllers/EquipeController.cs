using DisneyBattle.WebAPI.Repos.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace DisneyBattle.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EquipeController : Controller
    {
        private readonly IEquipeService _equipeService;

        public EquipeController(IEquipeService personnageService)
        {
            _equipeService = personnageService;
        }

        [HttpGet]
        public IActionResult GetAllEquipes()
        {
            return Ok(_equipeService.GetAll());
        }

    }
}
