using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MexNature.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddCompleteSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PlaceAmenities",
                keyColumns: new[] { "AmenityId", "PlaceId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "PlaceAmenities",
                keyColumns: new[] { "AmenityId", "PlaceId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.UpdateData(
                table: "Places",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Impresionantes cascadas de un intenso color azul turquesa en Chiapas.");

            migrationBuilder.UpdateData(
                table: "Places",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Volcán inactivo con dos hermosas lagunas en su cráter (El Sol y La Luna).");

            migrationBuilder.InsertData(
                table: "Places",
                columns: new[] { "Id", "Accessible", "Category", "CreatedAt", "Description", "ElevationMeters", "EntryFee", "Latitude", "Longitude", "Name", "OpeningHours" },
                values: new object[,]
                {
                    { 3, true, "Bosque", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Área de Protección de Flora y Fauna en la Zona Metropolitana de Guadalajara.", 1600, 40.0, 20.666699999999999, -103.58329999999999, "Bosque de la Primavera", "07:00-18:00" },
                    { 4, true, "Cañón", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Imponente cañón con altos acantilados, navegable por el río Grijalva en Chiapas.", 1000, 36.0, 16.831499999999998, -93.093400000000003, "Cañón del Sumidero", "08:00-16:00" },
                    { 5, true, "Cascada", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Cascadas petrificadas en Oaxaca, formadas por agua carbonatada.", 1750, 50.0, 16.8642, -96.273099999999999, "Hierve el Agua", "07:00-18:30" },
                    { 6, false, "Cueva", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Una de las cuevas verticales más grandes y hermosas del mundo, en San Luis Potosí.", 330, 100.0, 21.600000000000001, -99.0, "Sótano de las Golondrinas", "06:00-16:00" },
                    { 7, true, "Reserva Natural", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Humedal famoso por sus grandes poblaciones de flamencos rosados en Yucatán.", 5, 200.0, 20.8583, -90.395799999999994, "Reserva de la Biósfera Ría Celestún", "08:00-17:00" },
                    { 8, false, "Parque", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Parque en Baja California con bosques de coníferas y el Observatorio Astronómico Nacional.", 2830, 64.0, 30.9953, -115.46420000000001, "Parque Nacional Sierra de San Pedro Mártir", "07:00-20:00" },
                    { 9, true, "Arrecife", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Parte del Gran Arrecife Maya, un destino de clase mundial para el buceo y snorkel.", 0, 100.0, 20.355599999999999, -87.001900000000006, "Parque Nacional Arrecifes de Cozumel", "09:00-17:00" },
                    { 10, true, "Cascada", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "La cascada permanente más alta de México, ubicada en la Sierra Tarahumara en Chihuahua.", 2453, 70.0, 28.169699999999999, -108.2128, "Cascada de Basaseachi", "08:00-17:00" }
                });

            migrationBuilder.InsertData(
                table: "Photos",
                columns: new[] { "Id", "PlaceId", "Url" },
                values: new object[,]
                {
                    { 3, 4, "https://images.unsplash.com/photo-1603753254932-92e104b4c052" },
                    { 4, 10, "https://upload.wikimedia.org/wikipedia/commons/4/47/Basaseachi_Chihuahua_M%C3%A9xico_por_Javier_Correa_Zabala_2018._%282%29.jpg" }
                });

            migrationBuilder.InsertData(
                table: "PlaceAmenities",
                columns: new[] { "AmenityId", "PlaceId" },
                values: new object[,]
                {
                    { 3, 4 },
                    { 2, 5 }
                });

            migrationBuilder.InsertData(
                table: "Trails",
                columns: new[] { "Id", "Difficulty", "DistanceKm", "EstimatedTimeMinutes", "IsLoop", "Name", "Path", "PlaceId" },
                values: new object[,]
                {
                    { 2, "Fácil", 4.0, 120, true, "Miradores del Cañón", "{}", 4 },
                    { 3, "Moderada", 12.0, 300, false, "Sendero al Observatorio", "{}", 8 },
                    { 4, "Fácil", 2.5, 90, true, "Mirador La Ventana", "{}", 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Photos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PlaceAmenities",
                keyColumns: new[] { "AmenityId", "PlaceId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "PlaceAmenities",
                keyColumns: new[] { "AmenityId", "PlaceId" },
                keyValues: new object[] { 2, 5 });

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Trails",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Trails",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Trails",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Places",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.InsertData(
                table: "PlaceAmenities",
                columns: new[] { "AmenityId", "PlaceId" },
                values: new object[,]
                {
                    { 4, 1 },
                    { 3, 2 }
                });

            migrationBuilder.UpdateData(
                table: "Places",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Impresionantes cascadas de un intenso color azul turquesa en Chiapas, formadas por los afluentes del río Otulún, Shumuljá y Tulijá.");

            migrationBuilder.UpdateData(
                table: "Places",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Volcán inactivo con dos hermosas lagunas en su cráter (El Sol y La Luna), un destino popular para el senderismo y el alpinismo.");
        }
    }
}
