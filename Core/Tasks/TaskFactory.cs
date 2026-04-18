using SmartTask.Core.Constants;
using SmartTask.Core.Helpers;
using SmartTask.Core.Interfaces;
using SmartTask.Tasks.Email;
using SmartTask.Tasks.Calculation;
using SmartTask.Tasks.FileProcessing;
using SmartTask.Tasks.FileProcessing.Interfaces;

namespace SmartTask.Core.Tasks;

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
            { TaskType.File, CreateFileTask },
            { TaskType.Calculation, CreateCalculationTask }
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

        return new FileProcessTask(
            "Dynamic File Task",
            filePath,
            _fileValidator
        );
    }

    private BaseTask CreateCalculationTask()
    {
        Console.Write("Enter number: ");
        long number = long.Parse(Console.ReadLine() ?? "0");
        return new CalculationTask("Dynamic Calculation Task", number);
    }
}
