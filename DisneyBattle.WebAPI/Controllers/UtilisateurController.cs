using DisneyBattle.WebAPI.Models;
using DisneyBattle.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DisneyBattle.WebAPI.Controllers;
[Route("api/")]
[ApiController]
public class UtilisateurController(IUtilisateurServices service) : ControllerBase
{
    [HttpGet]
    [Route("utilisateur/getall")]
    public IActionResult GetAll()
    {
        return Ok();
    }

    [HttpGet]
    [Route("utilisateur/get")]
    public IActionResult Get([FromQuery] int id)
    {
        return Ok(service.GetById(id));
    }

    [HttpPost]
    [Route("utilisateur/add")]
    public IActionResult Add([FromBody] UtilisateurInModel in_model)
    {
        UtilisateurModel model = new(
            0,
            in_model.Pseudo,
            in_model.Email,
            in_model.MotDePasse,
            DateTime.Now
        );
        return Ok(service.Insert(model));
    }
}