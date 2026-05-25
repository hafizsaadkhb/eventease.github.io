using System.Text.Json;
using Microsoft.JSInterop;

namespace EventEase.Services;

public class SessionService
{
    private readonly IJSRuntime _js;
    private const string Key = "EventEase.Session";

    public UserSession? Current { get; private set; }
    public event Action? OnChange;

    public SessionService(IJSRuntime js)
    {
        _js = js;
    }

    public async Task InitializeAsync()
    {
        try
        {
            var json = await _js.InvokeAsync<string>("localStorage.getItem", Key);
            if (!string.IsNullOrEmpty(json))
            {
                Current = JsonSerializer.Deserialize<UserSession>(json);
            }
        }
        catch
        {
            // ignore
        }
    }

    public async Task SetCurrentAsync(string name, string email)
    {
        Current = new UserSession { Name = name, Email = email, LastSeenUtc = DateTime.UtcNow };
        var json = JsonSerializer.Serialize(Current);
        await _js.InvokeVoidAsync("localStorage.setItem", Key, json);
        OnChange?.Invoke();
    }

    public async Task ClearAsync()
    {
        Current = null;
        await _js.InvokeVoidAsync("localStorage.removeItem", Key);
        OnChange?.Invoke();
    }
}

public class UserSession
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime LastSeenUtc { get; set; }
}
