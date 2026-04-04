using SmartTask.Shared.Constants;
using SmartTask.Shared.Helpers;
using SmartTask.Shared.Interfaces;
using SmartTask.Tasks;

namespace SmartTask.Factories;

public class TaskFactory : ITaskFactory
{
    private readonly IEmailService _emailService;
    private readonly Dictionary<TaskType, Func<BaseTask>> _taskMap;

    public TaskFactory(IEmailService emailService)
    {
        _emailService = emailService;

        _taskMap = new Dictionary<TaskType, Func<BaseTask>>
        {
            { TaskType.Email, CreateEmailTask }
        };
    }

    public BaseTask CreateTask(TaskType choice)
    {
        if(_taskMap.TryGetValue(choice, out var taskCreater))
        {
            return taskCreater();
        }
        throw new Exception("Invalid task type");
    }

    private BaseTask CreateEmailTask()
    {
        Console.Write("Enter recipient email: ");
        string to = Console.ReadLine() ?? "";

        Console.Write("Enter subject: ");
        string subject = Console.ReadLine() ?? "";

        string body = CommonHelper.ReadMultilineInput("Enter body content: ");

        return new EmailTask(
            "Dynamic Email Task",
            to,
            subject,
            body,
            _emailService
        );
    }
}
