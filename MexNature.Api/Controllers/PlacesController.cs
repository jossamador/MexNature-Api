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

    // ENDPOINT 1: GET /api/places?category=Cascada
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlaceDto>>> GetPlaces([FromQuery] string? category)
    {
        var query = _context.Places.AsQueryable();

        // Filtro opcional por categoría
        if (!string.IsNullOrEmpty(category))
        {
            query = query.Where(p => p.Category.ToLower() == category.ToLower());
        }

        // Proyectamos a DTOs para no exponer todo el modelo
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

    // ENDPOINT 2: GET /api/places/1
    [HttpGet("{id}")]
    public async Task<ActionResult<Place>> GetPlaceDetail(int id)
    {
        var place = await _context.Places
            .Include(p => p.Trails) // Incluir senderos
            .Include(p => p.Photos)  // Incluir fotos
            .Include(p => p.PlaceAmenities)
                .ThenInclude(pa => pa.Amenity) // Incluir amenidades
            .FirstOrDefaultAsync(p => p.Id == id);

        if (place == null)
        {
            return NotFound(); // Retorna un error 404 si no se encuentra
        }

        return Ok(place);
    }

    // ENDPOINT 3: POST /api/places
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
            Accessible = false // Asignamos un valor por defecto
        };

        _context.Places.Add(place);
        await _context.SaveChangesAsync();

        // Retornamos una respuesta 201 Created con la ubicación del nuevo recurso
        return CreatedAtAction(nameof(GetPlaceDetail), new { id = place.Id }, place);
    }
    
    // Dentro de PlacesController.cs

    [HttpGet("trails")] // Ruta: GET /api/places/trails
    public async Task<ActionResult<IEnumerable<Trail>>> GetAllTrails()
    {
        var trails = await _context.Trails.ToListAsync();
        return Ok(trails);
    }
}