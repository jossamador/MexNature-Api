namespace MexNature.Api.Entities;

public class Photo
{
    public int Id { get; set; }
    public int PlaceId { get; set; } // Foreign Key
    public string Url { get; set; } = string.Empty;

    // Propiedad de navegación
    public Place Place { get; set; } = null!;
}