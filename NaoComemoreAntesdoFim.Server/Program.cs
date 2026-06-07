using NaoComemoreAntesdoFim.Server.Models;
using NaoComemoreAntesdoFim.Server.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<GcpTextToSpeechService>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy
            .WithOrigins(
                "http://localhost:5178",
                "https://localhost:7026",
                "http://localhost:63394",
                "https://localhost:44361",
                "https://meu-jogo-sud.web.app",
                "https://meu-jogo-sud.firebaseapp.com")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();

app.UseCors();

app.MapGet("/api/tts/health", async (GcpTextToSpeechService tts, CancellationToken cancellationToken) =>
{
    var health = await tts.GetHealthAsync(cancellationToken);
    return health.Ok
        ? Results.Ok(health)
        : Results.Json(health, statusCode: StatusCodes.Status503ServiceUnavailable);
});

app.MapGet("/api/tts/voices", async (GcpTextToSpeechService tts, CancellationToken cancellationToken) =>
{
    var voices = await tts.ListVoicesAsync(cancellationToken);
    return Results.Ok(voices.Select(v => new
    {
        name = v.Name,
        lang = v.Lang,
        displayName = v.DisplayName
    }));
});

app.MapPost("/api/tts/speak", async (SpeakRequest request, GcpTextToSpeechService tts, CancellationToken cancellationToken) =>
{
    if (string.IsNullOrWhiteSpace(request.Text))
        return Results.BadRequest(new { message = "Texto vazio." });

    var audio = await tts.SynthesizeAsync(request, cancellationToken);
    if (audio is null || audio.Length == 0)
    {
        return Results.Json(
            new { message = "Serviço de voz indisponível. Verifique credenciais GCP, projeto de quota e a API Text-to-Speech." },
            statusCode: StatusCodes.Status503ServiceUnavailable);
    }

    return Results.File(audio, "audio/mpeg");
});

app.Run();
