namespace NaoComemoreAntesdoFim.Server.Models;

public sealed record SpeakRequest(
    string Text,
    string? VoiceName,
    double Rate,
    double Pitch,
    double Volume);

public sealed record VoiceDto(string Name, string Lang, string DisplayName);

public sealed record TtsHealthResponse(bool Ok, string Provider, string? Message);
