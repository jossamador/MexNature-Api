using System.ComponentModel.DataAnnotations;

namespace MexNature.Api.DTOs;

public class CreatePlaceDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    [Required]
    public string Category { get; set; } = string.Empty;

    [Range(-90.0, 90.0, ErrorMessage = "La latitud debe estar entre -90 y 90.")]
    public double Latitude { get; set; }

    [Range(-180.0, 180.0, ErrorMessage = "La longitud debe estar entre -180 y 180.")]
    public double Longitude { get; set; }

    public double EntryFee { get; set; }
    public string OpeningHours { get; set; } = string.Empty;
}