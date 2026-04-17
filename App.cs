using SmartTask.Factories;
using SmartTask.Shared.Constants;
using SmartTask.Shared.Interfaces;
using SmartTask.Shared.Interfaces.FileProcessTask;
using SmartTask.Tasks;
using MyTaskFactory = SmartTask.Factories.TaskFactory;

namespace SmartTask;

public class App
{
    private readonly IEmailService _emailService;
    private readonly IFileValidator _fileValidator;

    public App(IEmailService emailService, IFileValidator fileValidator)
    {
        _emailService = emailService;
        _fileValidator = fileValidator;
    }

    public async void Run()
    {
        int choice;

        do
        {
            Console.WriteLine("Select Task:");
            Console.WriteLine("1. Email Task");
            Console.WriteLine("2. File Task");
            Console.WriteLine("3. Calculation Task");
            Console.WriteLine("0. Exit");
            Console.Write("Enter choice: ");

            choice = int.Parse(Console.ReadLine() ?? "0");

            if (choice == 0)
                break;

            if (!Enum.IsDefined(typeof(TaskType), choice))
            {
                Console.WriteLine("Invalid choice");
                continue;
            }

            ITaskFactory factory = new MyTaskFactory(_emailService, _fileValidator);

            BaseTask task = factory.CreateTask((TaskType)choice);

            await task.Execute();

        } while (choice != 0);
    }
}
