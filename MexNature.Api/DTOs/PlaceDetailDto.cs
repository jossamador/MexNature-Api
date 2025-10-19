namespace MexNature.Api.DTOs;

public class PlaceDetailDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public int ElevationMeters { get; set; }
    public bool Accessible { get; set; }
    public double EntryFee { get; set; }
    public string OpeningHours { get; set; }
    public List<PhotoDto> Photos { get; set; } = new();
    public List<TrailDto> Trails { get; set; } = new();
    public List<AmenityDto> Amenities { get; set; } = new();
}