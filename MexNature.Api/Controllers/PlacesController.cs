using MexNature.Api.Data;
using MexNature.Api.DTOs;
using MexNature.Api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MexNature.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlacesController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PlacesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlaceDto>>> GetPlaces([FromQuery] string? category)
    {
        var query = _context.Places.AsQueryable();

        if (!string.IsNullOrEmpty(category))
        {
            query = query.Where(p => p.Category.ToLower() == category.ToLower());
        }

        // --- ✅ CORRECCIÓN APLICADA AQUÍ ---
        // Rellenamos las propiedades del DTO con los datos de la base de datos.
        var places = await query
            .Select(p => new PlaceDto
            {
                Id = p.Id,
                Name = p.Name,
                Category = p.Category,
                Latitude = p.Latitude,
                Longitude = p.Longitude
            })
            .ToListAsync();

        return Ok(places);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PlaceDetailDto>> GetPlaceDetail(int id)
    {
        var place = await _context.Places
            .Include(p => p.Photos)
            .Include(p => p.PlaceAmenities)
                .ThenInclude(pa => pa.Amenity)
            .Include(p => p.Trails)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (place == null)
        {
            return NotFound();
        }

        var placeDto = new PlaceDetailDto
        {
            Id = place.Id,
            Name = place.Name,
            Description = place.Description,
            Category = place.Category,
            Latitude = place.Latitude,
            Longitude = place.Longitude,
            ElevationMeters = place.ElevationMeters,
            Accessible = place.Accessible,
            EntryFee = place.EntryFee,
            OpeningHours = place.OpeningHours,
            Photos = place.Photos.Select(photo => new PhotoDto { Id = photo.Id, Url = photo.Url }).ToList(),
            Trails = place.Trails.Select(trail => new TrailDto { Id = trail.Id, Name = trail.Name, Difficulty = trail.Difficulty, DistanceKm = trail.DistanceKm, EstimatedTimeMinutes = trail.EstimatedTimeMinutes }).ToList(),
            Amenities = place.PlaceAmenities.Select(pa => new AmenityDto { Id = pa.Amenity.Id, Name = pa.Amenity.Name }).ToList()
        };

        return Ok(placeDto);
    }

    [HttpPost]
    public async Task<ActionResult<Place>> CreatePlace(CreatePlaceDto createPlaceDto)
    {
        var place = new Place
        {
            Name = createPlaceDto.Name,
            Description = createPlaceDto.Description,
            Category = createPlaceDto.Category,
            Latitude = createPlaceDto.Latitude,
            Longitude = createPlaceDto.Longitude,
            EntryFee = createPlaceDto.EntryFee,
            OpeningHours = createPlaceDto.OpeningHours,
            CreatedAt = DateTime.UtcNow,
            Accessible = false
        };

        _context.Places.Add(place);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPlaceDetail), new { id = place.Id }, place);
    }

    [HttpGet("trails")]
    public async Task<ActionResult<IEnumerable<TrailDto>>> GetAllTrails()
    {
        var trails = await _context.Trails
            .Select(trail => new TrailDto
            {
                Id = trail.Id,
                Name = trail.Name,
                Difficulty = trail.Difficulty,
                DistanceKm = trail.DistanceKm,
                EstimatedTimeMinutes = trail.EstimatedTimeMinutes
            }).ToListAsync();
        
        return Ok(trails);
    }
}