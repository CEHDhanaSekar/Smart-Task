using SmartTask.Shared.Constants;
using SmartTask.Tasks;

namespace SmartTask.Factories;

public interface ITaskFactory
{
    BaseTask CreateTask(TaskType type);
}
