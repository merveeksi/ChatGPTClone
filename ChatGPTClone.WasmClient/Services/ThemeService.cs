namespace ChatGPTClone.WasmClient.Services;

public class ThemeService
{
    public event Action OnChange;

    private string _currentTheme = "light";

    public string CurrentTheme
    {
        get => _currentTheme;
        set
        {
            if (_currentTheme != value)
            {
                _currentTheme = value;
                NotifyThemeChanged();
            }
        }
    }

    private void NotifyThemeChanged() => OnChange?.Invoke();
}
