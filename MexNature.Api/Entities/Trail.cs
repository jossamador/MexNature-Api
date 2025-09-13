namespace MexNature.Api.Entities;

public class Trail
{
    public int Id { get; set; }
    public int PlaceId { get; set; } // Foreign Key
    public string Name { get; set; } = string.Empty;
    public double DistanceKm { get; set; }
    public int EstimatedTimeMinutes { get; set; }
    public string Difficulty { get; set; } = string.Empty;
    public string Path { get; set; } = string.Empty;
    public bool IsLoop { get; set; }

    // Propiedad de navegación
    public Place Place { get; set; } = null!;
}