using MexNature.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace MexNature.Api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Place> Places { get; set; }
    public DbSet<Trail> Trails { get; set; }
    public DbSet<Photo> Photos { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Amenity> Amenities { get; set; }
    public DbSet<PlaceAmenity> PlaceAmenities { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configurar la clave primaria compuesta para la tabla puente PlaceAmenity
        modelBuilder.Entity<PlaceAmenity>()
            .HasKey(pa => new { pa.PlaceId, pa.AmenityId });

        // Configurar la relación Muchos a Muchos entre Place y Amenity
        modelBuilder.Entity<PlaceAmenity>()
            .HasOne(pa => pa.Place)
            .WithMany(p => p.PlaceAmenities)
            .HasForeignKey(pa => pa.PlaceId);

        modelBuilder.Entity<PlaceAmenity>()
            .HasOne(pa => pa.Amenity)
            .WithMany(a => a.PlaceAmenities)
            .HasForeignKey(pa => pa.AmenityId);

        // --- INICIO DEL SEED DE DATOS ---
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // 1. Seed Amenidades (Servicios)
        modelBuilder.Entity<Amenity>().HasData(
            new Amenity { Id = 1, Name = "Estacionamiento" },
            new Amenity { Id = 2, Name = "Baños" },
            new Amenity { Id = 3, Name = "Mirador" },
            new Amenity { Id = 4, Name = "Zona de Comida" },
            new Amenity { Id = 5, Name = "Acceso para Sillas de Ruedas" }
        );

        // 2. Seed Lugares (Places)
        modelBuilder.Entity<Place>().HasData(
            new Place
            {
                Id = 1,
                Name = "Cascadas de Agua Azul",
                Description = "Impresionantes cascadas de un intenso color azul turquesa en Chiapas, formadas por los afluentes del río Otulún, Shumuljá y Tulijá.",
                Category = "Cascada",
                Latitude = 17.2586,
                Longitude = -92.1158,
                ElevationMeters = 200,
                Accessible = true,
                EntryFee = 70.0,
                OpeningHours = "08:00-17:30",
                CreatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
            },
            new Place
            {
                Id = 2,
                Name = "Parque Nacional Nevado de Toluca",
                Description = "Volcán inactivo con dos hermosas lagunas en su cráter (El Sol y La Luna), un destino popular para el senderismo y el alpinismo.",
                Category = "Parque",
                Latitude = 19.1083,
                Longitude = -99.7583,
                ElevationMeters = 4680,
                Accessible = false,
                EntryFee = 50.0,
                OpeningHours = "08:00-15:00",
                CreatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        // 3. Seed Fotos
        modelBuilder.Entity<Photo>().HasData(
            new Photo { Id = 1, PlaceId = 1, Url = "https://images.unsplash.com/photo-1629885375531-157154536544" },
            new Photo { Id = 2, PlaceId = 2, Url = "https://images.unsplash.com/photo-1579979119561-14bc7423233c" }
        );

        // 4. Seed Senderos (Trails)
        modelBuilder.Entity<Trail>().HasData(
            new Trail { Id = 1, PlaceId = 2, Name = "Ascenso al Pico del Fraile", DistanceKm = 8.5, EstimatedTimeMinutes = 240, Difficulty = "Difícil", IsLoop = false, Path = "{}" }
        );

        // 5. Seed Relación Place <-> Amenity
        modelBuilder.Entity<PlaceAmenity>().HasData(
            new PlaceAmenity { PlaceId = 1, AmenityId = 1 }, // Agua Azul tiene Estacionamiento
            new PlaceAmenity { PlaceId = 1, AmenityId = 2 }, // Agua Azul tiene Baños
            new PlaceAmenity { PlaceId = 1, AmenityId = 4 }, // Agua Azul tiene Zona de Comida
            new PlaceAmenity { PlaceId = 2, AmenityId = 1 }, // Nevado de Toluca tiene Estacionamiento
            new PlaceAmenity { PlaceId = 2, AmenityId = 3 }  // Nevado de Toluca tiene Mirador
        );
    }
}