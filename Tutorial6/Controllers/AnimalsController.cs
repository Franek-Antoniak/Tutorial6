using Microsoft.AspNetCore.Mvc;
using Tutorial6.Models;

namespace Tutorial6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimalsController : ControllerBase
{

    [HttpGet]
    public IActionResult GetAnimals([FromQuery] string? name = null)
    {
        var animals = Database.Animals.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(name))
            animals = animals.Where(a => a.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

        return Ok(animals);
    }


    [HttpGet("{id:int}", Name = "GetAnimal")]
    public IActionResult GetAnimal(int id)
    {
        var animal = Database.Animals.FirstOrDefault(a => a.Id == id);
        return animal is null
            ? NotFound($"Animal with id {id} not found.")
            : Ok(animal);
    }


    [HttpPost]
    public IActionResult AddAnimal([FromBody] Animal animal)
    {
        animal.Id = Database.GetNextAnimalId();
        Database.Animals.Add(animal);

        return CreatedAtRoute("GetAnimal", new { id = animal.Id }, animal);
    }


    [HttpPut("{id:int}")]
    public IActionResult UpdateAnimal(int id, [FromBody] Animal updated)
    {
        var animal = Database.Animals.FirstOrDefault(a => a.Id == id);
        if (animal is null)
            return NotFound($"Animal with id {id} not found.");

        animal.Name = updated.Name;
        animal.Category = updated.Category;
        animal.Weight = updated.Weight;
        animal.FurColor = updated.FurColor;

        return NoContent();
    }


    [HttpDelete("{id:int}")]
    public IActionResult DeleteAnimal(int id)
    {
        var animal = Database.Animals.FirstOrDefault(a => a.Id == id);
        if (animal is null)
            return NotFound($"Animal with id {id} not found.");

        Database.Visits.RemoveAll(v => v.AnimalId == id);
        Database.Animals.Remove(animal);

        return NoContent();
    }
}