using MexNature.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- POLÍTICA DE CORS SIMPLIFICADA PARA DEPURACIÓN ---
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOriginPolicy",
        policyBuilder =>
        {
            policyBuilder.AllowAnyOrigin() // <-- Acepta cualquier origen
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// ... tus otros servicios (AddControllers, AddDbContext, etc.) ...
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 👇 1. REGISTRAR EL SERVICIO DE IA
builder.Services.AddScoped<MexNature.Api.Services.AiService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

// Registrar el servicio de IA
builder.Services.AddScoped<MexNature.Api.Services.AiService>();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


// --- ORDEN CORRECTO DE LA TUBERÍA ---

// 1. Habilita el enrutamiento para que la app sepa a dónde va la petición.
app.UseRouting(); 

// 2. Aplica la política de CORS.
app.UseCors("AllowAnyOriginPolicy");

// 3. (Opcional) Redirige a HTTPS.
app.UseHttpsRedirection();

// 4. Ejecuta los endpoints de los controladores.
app.MapControllers();


// 👇 2. CREAR EL ENDPOINT DE IA
app.MapGet("/api/ai/describe", async (string name, string category, MexNature.Api.Services.AiService aiService) =>
    {
        try 
        {
            var description = await aiService.GetPlaceDescription(name, category);
            return Results.Ok(new { description });
        }
        catch (Exception ex)
        {
            return Results.Problem($"Error con la IA: {ex.Message}");
        }
    })
    .WithOpenApi()
    .WithName("GetAiDescription");

app.Run();