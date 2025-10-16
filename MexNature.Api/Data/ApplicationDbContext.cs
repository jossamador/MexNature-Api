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

    // 2. Lugares
    modelBuilder.Entity<Place>().HasData(
    new Place
    {
        Id = 1,
        Name = "Cascadas de Agua Azul",
        Description = "Impresionantes cascadas de un intenso color azul turquesa en Chiapas.",
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
        Description = "Volcán inactivo con dos hermosas lagunas en su cráter (El Sol y La Luna).",
        Category = "Parque",
        Latitude = 19.1083,
        Longitude = -99.7583,
        ElevationMeters = 4680,
        Accessible = false,
        EntryFee = 50.0,
        OpeningHours = "08:00-15:00",
        CreatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
    },
    new Place
    {
        Id = 3,
        Name = "Bosque de la Primavera",
        Description = "Área de Protección de Flora y Fauna en la Zona Metropolitana de Guadalajara.",
        Category = "Bosque",
        Latitude = 20.6667,
        Longitude = -103.5833,
        ElevationMeters = 1600,
        Accessible = true,
        EntryFee = 40.0,
        OpeningHours = "07:00-18:00",
        CreatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
    },
    new Place
    {
        Id = 4,
        Name = "Cañón del Sumidero",
        Description = "Imponente cañón con altos acantilados, navegable por el río Grijalva en Chiapas.",
        Category = "Cañón",
        Latitude = 16.8315,
        Longitude = -93.0934,
        ElevationMeters = 1000,
        Accessible = true,
        EntryFee = 36.0,
        OpeningHours = "08:00-16:00",
        CreatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
    },
    new Place
    {
        Id = 5,
        Name = "Hierve el Agua",
        Description = "Cascadas petrificadas en Oaxaca, formadas por agua carbonatada.",
        Category = "Cascada",
        Latitude = 16.8642,
        Longitude = -96.2731,
        ElevationMeters = 1750,
        Accessible = true,
        EntryFee = 50.0,
        OpeningHours = "07:00-18:30",
        CreatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
    },
    new Place
    {
        Id = 6,
        Name = "Sótano de las Golondrinas",
        Description = "Una de las cuevas verticales más grandes y hermosas del mundo, en San Luis Potosí.",
        Category = "Cueva",
        Latitude = 21.6000,
        Longitude = -99.0000,
        ElevationMeters = 330,
        Accessible = false,
        EntryFee = 100.0,
        OpeningHours = "06:00-16:00",
        CreatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
    },
    new Place
    {
        Id = 7,
        Name = "Reserva de la Biósfera Ría Celestún",
        Description = "Humedal famoso por sus grandes poblaciones de flamencos rosados en Yucatán.",
        Category = "Reserva Natural",
        Latitude = 20.8583,
        Longitude = -90.3958,
        ElevationMeters = 5,
        Accessible = true,
        EntryFee = 200.0,
        OpeningHours = "08:00-17:00",
        CreatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
    },
    new Place
    {
        Id = 8,
        Name = "Parque Nacional Sierra de San Pedro Mártir",
        Description = "Parque en Baja California con bosques de coníferas y el Observatorio Astronómico Nacional.",
        Category = "Parque",
        Latitude = 30.9953,
        Longitude = -115.4642,
        ElevationMeters = 2830,
        Accessible = false,
        EntryFee = 64.0,
        OpeningHours = "07:00-20:00",
        CreatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
    },
    new Place
    {
        Id = 9,
        Name = "Parque Nacional Arrecifes de Cozumel",
        Description = "Parte del Gran Arrecife Maya, un destino de clase mundial para el buceo y snorkel.",
        Category = "Arrecife",
        Latitude = 20.3556,
        Longitude = -87.0019,
        ElevationMeters = 0,
        Accessible = true,
        EntryFee = 100.0,
        OpeningHours = "09:00-17:00",
        CreatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
    },
    new Place
    {
        Id = 10,
        Name = "Cascada de Basaseachi",
        Description = "La cascada permanente más alta de México, ubicada en la Sierra Tarahumara en Chihuahua.",
        Category = "Cascada",
        Latitude = 28.1697,
        Longitude = -108.2128,
        ElevationMeters = 2453,
        Accessible = true,
        EntryFee = 70.0,
        OpeningHours = "08:00-17:00",
        CreatedAt = new DateTime(2024, 1, 15, 0, 0, 0, DateTimeKind.Utc)
    }
);

    // 3. Seed Fotos
    modelBuilder.Entity<Photo>().HasData(
        new Photo { Id = 1, PlaceId = 1, Url = "https://images.unsplash.com/photo-1629885375531-157154536544" },
        new Photo { Id = 2, PlaceId = 2, Url = "https://images.unsplash.com/photo-1579979119561-14bc7423233c" },
        new Photo { Id = 3, PlaceId = 4, Url = "https://images.unsplash.com/photo-1603753254932-92e104b4c052" },
        new Photo { Id = 4, PlaceId = 10, Url = "https://upload.wikimedia.org/wikipedia/commons/4/47/Basaseachi_Chihuahua_M%C3%A9xico_por_Javier_Correa_Zabala_2018._%282%29.jpg" }
    );

    // 4. Seed Senderos (Trails)
    modelBuilder.Entity<Trail>().HasData(
        new Trail { Id = 1, PlaceId = 2, Name = "Ascenso al Pico del Fraile", DistanceKm = 8.5, EstimatedTimeMinutes = 240, Difficulty = "Difícil", IsLoop = false, Path = "{}" },
        new Trail { Id = 2, PlaceId = 4, Name = "Miradores del Cañón", DistanceKm = 4, EstimatedTimeMinutes = 120, Difficulty = "Fácil", IsLoop = true, Path = "{}" },
        new Trail { Id = 3, PlaceId = 8, Name = "Sendero al Observatorio", DistanceKm = 12, EstimatedTimeMinutes = 300, Difficulty = "Moderada", IsLoop = false, Path = "{}" },
        new Trail { Id = 4, PlaceId = 10, Name = "Mirador La Ventana", DistanceKm = 2.5, EstimatedTimeMinutes = 90, Difficulty = "Fácil", IsLoop = true, Path = "{}" }
    );

    // 5. Seed Relación Place <-> Amenity
    modelBuilder.Entity<PlaceAmenity>().HasData(
        new PlaceAmenity { PlaceId = 1, AmenityId = 1 }, // Agua Azul: Estacionamiento
        new PlaceAmenity { PlaceId = 1, AmenityId = 2 }, // Agua Azul: Baños
        new PlaceAmenity { PlaceId = 2, AmenityId = 1 }, // Nevado de Toluca: Estacionamiento
        new PlaceAmenity { PlaceId = 4, AmenityId = 3 }, // Cañón del Sumidero: Mirador
        new PlaceAmenity { PlaceId = 5, AmenityId = 2 }  // Hierve el Agua: Baños
    );
}

}