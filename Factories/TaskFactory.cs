using SmartTask.Shared.Constants;
using SmartTask.Shared.Helpers;
using SmartTask.Shared.Interfaces;
using SmartTask.Tasks;
using SmartTask.Tasks.Categories;
using SmartTask.Shared.Interfaces.FileProcessTask;

namespace SmartTask.Factories;

public class TaskFactory : ITaskFactory
{
    private readonly IEmailService _emailService;
    private readonly IFileValidator _fileValidator;
    private readonly Dictionary<TaskType, Func<BaseTask>> _taskMap;

    public TaskFactory(IEmailService emailService, IFileValidator fileValidator)
    {
        _emailService = emailService;
        _fileValidator = fileValidator;

        _taskMap = new Dictionary<TaskType, Func<BaseTask>>
        {
            { TaskType.Email, CreateEmailTask },
            { TaskType.File, CreateFileTask }
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

    private BaseTask CreateFileTask()
    {
        Console.Write("Enter file path: ");
        string filePath = Console.ReadLine() ?? "";

        return new SmartTask.Tasks.Categories.FileProcessTask(
            "Dynamic File Task",
            filePath,
            _fileValidator
        );
    }
}
