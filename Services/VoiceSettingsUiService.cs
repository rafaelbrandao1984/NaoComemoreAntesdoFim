namespace NaoComemoreAntesdoFim.Services;

public class VoiceSettingsUiService
{
    public bool IsOpen { get; private set; }

    public event Action? OnChange;

    public void Open()
    {
        IsOpen = true;
        OnChange?.Invoke();
    }

    public void SetOpen(bool isOpen)
    {
        IsOpen = isOpen;
        OnChange?.Invoke();
    }
}
