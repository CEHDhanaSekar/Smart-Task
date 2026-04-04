using SmartTask.Shared.Interfaces;

namespace SmartTask.Shared.Helpers;

public class StatusTrackHelper : IStatusTracker
{
    public event Action<string>? OnStatusChanged;

    private readonly string _id;

    public StatusTrackHelper(string id)
    {
        _id = id;
        Console.WriteLine($"Task Created - {id}");
    }

    public void UpdateStatus(string status)
    {
        string message = $"Task : {_id} | {status}";

        Console.WriteLine(message);

        OnStatusChanged?.Invoke(message);
    }
}
