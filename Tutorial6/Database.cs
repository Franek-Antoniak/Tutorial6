using Tutorial6.Models;

namespace Tutorial6;

public static class Database
{
    public static List<Animal> Animals { get; set; } = new List<Animal>()
    {
        new Animal { Id = 1, Name = "Burek", Category = "Pies", Weight = 15.5, FurColor = "Brązowy" },
        new Animal { Id = 2, Name = "Mruczek", Category = "Kot", Weight = 4.2, FurColor = "Czarny" },
        new Animal { Id = 3, Name = "Fafik", Category = "Ptak", Weight = 8.0, FurColor = "Biały" }
    };

    public static List<Visit> Visits { get; set; } = new List<Visit>()
    {
        new Visit { Id = 1, AnimalId = 1, Date = DateTime.Now.AddDays(-10), Description = "Szczepienie", Price = new decimal(50) },
        new Visit { Id = 2, AnimalId = 2, Date = DateTime.Now.AddDays(-5), Description = "Kontrola ogólna", Price = new decimal(40) },
        new Visit { Id = 3, AnimalId = 1, Date = DateTime.Now.AddDays(-2), Description = "Badanie krwi", Price = new decimal(120) }
    };

    private static int _nextAnimalId = 4;
    private static int _nextVisitId = 4;

    public static int GetNextAnimalId() => _nextAnimalId++;
    public static int GetNextVisitId() => _nextVisitId++;
}

