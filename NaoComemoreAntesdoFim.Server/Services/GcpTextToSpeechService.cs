using Google.Cloud.TextToSpeech.V1;
using Grpc.Core;
using NaoComemoreAntesdoFim.Server.Models;

namespace NaoComemoreAntesdoFim.Server.Services;

public sealed class GcpTextToSpeechService
{
    private readonly TextToSpeechClient? _client;
    private readonly string _defaultVoiceName;
    private readonly int _maxTextLength;
    private readonly ILogger<GcpTextToSpeechService> _logger;
    private string? _lastError;

    private static readonly IReadOnlyList<VoiceDto> FallbackVoices =
    [
        new("pt-BR-Neural2-A", "pt-BR", "Feminina — Neural2 A"),
        new("pt-BR-Neural2-B", "pt-BR", "Masculina — Neural2 B"),
        new("pt-BR-Neural2-C", "pt-BR", "Feminina — Neural2 C"),
        new("pt-BR-Wavenet-A", "pt-BR", "Feminina — WaveNet A"),
        new("pt-BR-Wavenet-B", "pt-BR", "Masculina — WaveNet B"),
        new("pt-BR-Wavenet-C", "pt-BR", "Feminina — WaveNet C"),
        new("pt-BR-Wavenet-D", "pt-BR", "Feminina — WaveNet D"),
        new("pt-BR-Wavenet-E", "pt-BR", "Masculina — WaveNet E"),
        new("pt-BR-Standard-A", "pt-BR", "Feminina — Standard A"),
        new("pt-BR-Standard-B", "pt-BR", "Masculina — Standard B"),
        new("pt-BR-Standard-C", "pt-BR", "Feminina — Standard C"),
    ];

    public GcpTextToSpeechService(IConfiguration configuration, ILogger<GcpTextToSpeechService> logger)
    {
        _defaultVoiceName = configuration["Tts:DefaultVoiceName"] ?? "pt-BR-Neural2-C";
        _maxTextLength = configuration.GetValue("Tts:MaxTextLength", 5000);
        _logger = logger;

        try
        {
            var projectId = configuration["GoogleCloud:ProjectId"]
                ?? Environment.GetEnvironmentVariable("GOOGLE_CLOUD_PROJECT")
                ?? Environment.GetEnvironmentVariable("GCLOUD_PROJECT");

            var builder = new TextToSpeechClientBuilder();
            if (!string.IsNullOrWhiteSpace(projectId))
                builder.QuotaProject = projectId;

            _client = builder.Build();
        }
        catch (Exception ex)
        {
            _lastError = ex.Message;
            _logger.LogWarning(ex, "Google Cloud Text-to-Speech não configurado.");
            _client = null;
        }
    }

    public bool IsConfigured => _client is not null;

    public async Task<TtsHealthResponse> GetHealthAsync(CancellationToken cancellationToken)
    {
        if (_client is null)
        {
            return new TtsHealthResponse(
                false,
                "gcp",
                _lastError ?? "Credenciais GCP ausentes. Defina GOOGLE_APPLICATION_CREDENTIALS.");
        }

        try
        {
            await _client.ListVoicesAsync("pt-BR", cancellationToken);
            _lastError = null;
            return new TtsHealthResponse(true, "gcp", null);
        }
        catch (RpcException ex)
        {
            _lastError = DescribeRpcError(ex);
            _logger.LogWarning(ex, "Google Cloud Text-to-Speech indisponível.");
            return new TtsHealthResponse(false, "gcp", _lastError);
        }
        catch (Exception ex)
        {
            _lastError = ex.Message;
            return new TtsHealthResponse(false, "gcp", _lastError);
        }
    }

    public async Task<IReadOnlyList<VoiceDto>> ListVoicesAsync(CancellationToken cancellationToken)
    {
        if (_client is null)
            return FallbackVoices;

        try
        {
            var response = await _client.ListVoicesAsync("pt-BR", cancellationToken);
            var voices = response.Voices
                .Where(v => v.LanguageCodes.Contains("pt-BR"))
                .Select(v => new VoiceDto(
                    v.Name,
                    "pt-BR",
                    BuildDisplayName(v.Name)))
                .OrderByDescending(v => v.Name.Contains("Neural2", StringComparison.OrdinalIgnoreCase))
                .ThenByDescending(v => v.Name.Contains("Wavenet", StringComparison.OrdinalIgnoreCase))
                .ThenBy(v => v.DisplayName, StringComparer.OrdinalIgnoreCase)
                .ToList();

            return voices.Count > 0 ? voices : FallbackVoices;
        }
        catch (RpcException ex)
        {
            _lastError = DescribeRpcError(ex);
            _logger.LogWarning(ex, "Falha ao listar vozes no GCP. Usando lista padrão.");
            return FallbackVoices;
        }
        catch (Exception ex)
        {
            _lastError = ex.Message;
            _logger.LogWarning(ex, "Falha ao listar vozes no GCP. Usando lista padrão.");
            return FallbackVoices;
        }
    }

    public async Task<byte[]?> SynthesizeAsync(SpeakRequest request, CancellationToken cancellationToken)
    {
        if (_client is null)
            return null;

        var text = (request.Text ?? string.Empty).Trim();
        if (string.IsNullOrEmpty(text))
            return null;

        if (text.Length > _maxTextLength)
            text = text[.._maxTextLength];

        var voiceName = string.IsNullOrWhiteSpace(request.VoiceName)
            ? _defaultVoiceName
            : request.VoiceName.Trim();

        var input = new SynthesisInput { Text = text };
        var voice = new VoiceSelectionParams
        {
            LanguageCode = "pt-BR",
            Name = voiceName
        };
        var audioConfig = new AudioConfig
        {
            AudioEncoding = AudioEncoding.Mp3,
            SpeakingRate = MapSpeakingRate(request.Rate),
            Pitch = MapPitchSemitones(request.Pitch),
            VolumeGainDb = MapVolumeGainDb(request.Volume)
        };

        try
        {
            var response = await _client.SynthesizeSpeechAsync(input, voice, audioConfig, cancellationToken);
            return response.AudioContent.ToByteArray();
        }
        catch (RpcException ex)
        {
            _lastError = DescribeRpcError(ex);
            _logger.LogWarning(ex, "Falha ao sintetizar fala no GCP.");
            return null;
        }
        catch (Exception ex)
        {
            _lastError = ex.Message;
            _logger.LogWarning(ex, "Falha ao sintetizar fala no GCP.");
            return null;
        }
    }

    internal static double MapSpeakingRate(double rate) =>
        Math.Clamp(rate, 0.6, 1.6);

    internal static double MapPitchSemitones(double pitchMultiplier) =>
        Math.Clamp((pitchMultiplier - 1.0) * 20.0, -20.0, 20.0);

    internal static double MapVolumeGainDb(double volume)
    {
        if (volume <= 0)
            return -96.0;

        return Math.Clamp(20.0 * Math.Log10(volume), -40.0, 6.0);
    }

    private static string BuildDisplayName(string voiceName)
    {
        var suffix = voiceName.Replace("pt-BR-", string.Empty, StringComparison.OrdinalIgnoreCase);
        var lastLetter = suffix.Length > 0 ? suffix[^1].ToString() : string.Empty;
        var gender = lastLetter.Equals("B", StringComparison.OrdinalIgnoreCase)
            || lastLetter.Equals("E", StringComparison.OrdinalIgnoreCase)
            ? "Masculina"
            : "Feminina";

        if (suffix.Contains("Neural2", StringComparison.OrdinalIgnoreCase))
            return $"{gender} — Neural2 {lastLetter}";

        if (suffix.Contains("Wavenet", StringComparison.OrdinalIgnoreCase))
            return $"{gender} — WaveNet {lastLetter}";

        if (suffix.Contains("Standard", StringComparison.OrdinalIgnoreCase))
            return $"{gender} — Standard {lastLetter}";

        return voiceName;
    }

    private static string DescribeRpcError(RpcException ex)
    {
        if (ex.StatusCode == StatusCode.PermissionDenied)
        {
            return "Permissão negada no GCP. Ative a API Text-to-Speech, configure GOOGLE_APPLICATION_CREDENTIALS "
                + "e defina GoogleCloud:ProjectId (ou GOOGLE_CLOUD_PROJECT) para o projeto de quota.";
        }

        return ex.Status.Detail ?? ex.Message;
    }
}
