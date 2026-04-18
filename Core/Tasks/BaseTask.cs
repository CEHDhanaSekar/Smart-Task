using SmartTask.Core.Constants;
using SmartTask.Core.Helpers;
using SmartTask.Core.Interfaces;

namespace SmartTask.Core.Tasks;

public abstract class BaseTask
{
    public string Id { get; private set; }
    public string Name { get; protected set; }
    public string Type { get; protected set; }
    private static long _count;
    protected static long GetNextCount() => Interlocked.Increment(ref _count);
    protected StatusTrackHelper Status;

    public abstract Task Execute();
    public abstract ValidationResult Validate();

    protected string GenerateTaskId()
    {
        return $"ST-{Type}-{GetNextCount():D4}";
    }

    public BaseTask(string name, string type)
    {
        Name = name;
        Type = type;
        Id = GenerateTaskId();
        Status = new StatusTrackHelper(Id);
    }
}
