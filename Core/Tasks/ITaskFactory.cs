using SmartTask.Core.Constants;
using SmartTask.Tasks;

namespace SmartTask.Core.Tasks;

public interface ITaskFactory
{
    BaseTask CreateTask(TaskType type);
}
