using DisneyBattle.WebAPI.Models;
using DisneyBattle.WebAPI.Repos;
using Microsoft.AspNetCore.Mvc;

namespace DisneyBattle.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class LieuController(ILieuRepository lr):ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(lr.GetAll());
    }
    
    [HttpGet("Get/{id}")]
    public IActionResult Get(int id)
    {
        return Ok(lr.GetById(id));
    }
    
    [HttpPut("Put/{id}")]
    public IActionResult Put(int id, [FromBody] LieuModel entity)
    {
        return Ok(lr.Update(id, entity));
    }
    
    [HttpPost("Post")]
    public IActionResult Post([FromBody] LieuModel entity)
    {
        return Ok(lr.Insert(entity));
    }
}