namespace MexNature.Api.Entities;

public class Place
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int ElevationMeters { get; set; }
    public bool Accessible { get; set; }
    public double EntryFee { get; set; }
    public string OpeningHours { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    // Propiedades de navegación para las relaciones
    public ICollection<Trail> Trails { get; set; } = new List<Trail>();
    public ICollection<Photo> Photos { get; set; } = new List<Photo>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<PlaceAmenity> PlaceAmenities { get; set; } = new List<PlaceAmenity>();
}