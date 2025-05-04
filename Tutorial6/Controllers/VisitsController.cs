using Microsoft.AspNetCore.Mvc;
using Tutorial6.Models;

namespace Tutorial6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VisitsController : ControllerBase
{
    [HttpGet("~/api/animals/{animalId:int}/visits")]
    public IActionResult GetAnimalVisits(int animalId)
    {
        if (Database.Animals.All(a => a.Id != animalId))
            return NotFound($"Animal with id {animalId} not found.");

        var visits = Database.Visits.Where(v => v.AnimalId == animalId).ToList();
        return Ok(visits);
    }
    
    [HttpPost("~/api/animals/{animalId:int}/visits")]
    public IActionResult AddVisit(int animalId, [FromBody] Visit visit)
    {
        if (Database.Animals.All(a => a.Id != animalId))
            return NotFound($"Animal with id {animalId} not found. Cannot add visit.");

        visit.Id = Database.GetNextVisitId();
        visit.AnimalId = animalId;
        Database.Visits.Add(visit);
        
        return CreatedAtRoute("GetVisit", new { visitId = visit.Id }, visit);
    }
}