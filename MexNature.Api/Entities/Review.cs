namespace MexNature.Api.Entities;

public class Review
{
    public int Id { get; set; }
    public int PlaceId { get; set; } // Foreign Key
    public string Author { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    // Propiedad de navegación
    public Place Place { get; set; } = null!;
}