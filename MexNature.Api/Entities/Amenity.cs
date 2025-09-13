namespace MexNature.Api.Entities;

public class Amenity
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Propiedad de navegación
    public ICollection<PlaceAmenity> PlaceAmenities { get; set; } = new List<PlaceAmenity>();
}