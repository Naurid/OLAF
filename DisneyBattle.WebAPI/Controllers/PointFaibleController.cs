using DisneyBattle.WebAPI.Models;
using DisneyBattle.WebAPI.Repos;
using Microsoft.AspNetCore.Mvc;


namespace DisneyBattle.WebAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class PointFaibleController(IPointFaibleRepository pfr):ControllerBase
{
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(pfr.GetAll());
    }
    
    [HttpGet("Get/{id}")]
    public IActionResult Get(int id)
    {
        return Ok(pfr.GetById(id));
    }
    
    [HttpPut("Put/{id}")]
    public IActionResult Put(int id, [FromBody] PointFaibleModel entity)
    {
        return Ok(pfr.Update(id, entity));
    }
    
    [HttpPost("Post")]
    public IActionResult Post([FromBody] PointFaibleModel entity)
    {
        return Ok(pfr.Insert(entity));
    }
}