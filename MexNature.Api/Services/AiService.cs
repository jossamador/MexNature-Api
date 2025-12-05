using OpenAI.Chat;

namespace MexNature.Api.Services;

public class AiService
{
    private readonly string _apiKey;

    public AiService(IConfiguration configuration)
    {
        _apiKey = configuration["OpenAI:ApiKey"] ?? string.Empty;
    }

    public async Task<string> GetPlaceDescription(string placeName, string category)
    {
        if (string.IsNullOrEmpty(_apiKey)) return "Sin clave de IA configurada.";

        ChatClient client = new(model: "gpt-4o", apiKey: _apiKey);

        ChatCompletion completion = await client.CompleteChatAsync(
            $"Escribe una descripción breve, atractiva y turística (máximo 150 caracteres) para un lugar llamado '{placeName}' que es de categoría '{category}' en México."
        );

        return completion.Content[0].Text;
    }
}