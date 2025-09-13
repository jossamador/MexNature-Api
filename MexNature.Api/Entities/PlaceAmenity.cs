namespace MexNature.Api.Entities;

public class PlaceAmenity
{
    public int PlaceId { get; set; }
    public int AmenityId { get; set; }

    // Propiedades de navegación
    public Place Place { get; set; } = null!;
    public Amenity Amenity { get; set; } = null!;
}