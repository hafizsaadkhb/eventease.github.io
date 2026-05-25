using System.Text.Json;
using EventEase.Models;
using Microsoft.JSInterop;

namespace EventEase.Services;

public class AttendanceService
{
    private readonly IJSRuntime _js;
    private const string Key = "EventEase.Attendance";

    private List<RegistrationRecord> _records = new();

    public AttendanceService(IJSRuntime js)
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
                _records = JsonSerializer.Deserialize<List<RegistrationRecord>>(json) ?? new();
            }
        }
        catch
        {
            _records = new();
        }
    }

    public async Task AddAsync(RegistrationRecord registration)
    {
        registration.Id = _records.Count > 0 ? _records.Max(r => r.Id) + 1 : 1;
        _records.Add(registration);
        await SaveAsync();
    }

    private async Task SaveAsync()
    {
        var json = JsonSerializer.Serialize(_records);
        await _js.InvokeVoidAsync("localStorage.setItem", Key, json);
    }

    public Task<List<RegistrationRecord>> GetAllAsync() => Task.FromResult(_records);

    public Task<List<RegistrationRecord>> GetForEventAsync(int eventId) => Task.FromResult(_records.Where(r => r.EventId == eventId).ToList());

    public Task<int> GetCountForEventAsync(int eventId) => Task.FromResult(_records.Count(r => r.EventId == eventId));

    public async Task MarkAttendedAsync(int registrationId)
    {
        var rec = _records.FirstOrDefault(r => r.Id == registrationId);
        if (rec != null)
        {
            rec.Attended = true;
            await SaveAsync();
        }
    }
}
