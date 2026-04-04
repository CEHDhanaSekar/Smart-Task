using SmartTask.Factories;
using SmartTask.Shared.Constants;
using SmartTask.Shared.Interfaces;
using SmartTask.Tasks;
using MyTaskFactory = SmartTask.Factories.TaskFactory;

namespace SmartTask;

public class App
{
    private readonly IEmailService _emailService;

    public App(IEmailService emailService)
    {
        _emailService = emailService;
    }

    public async void Run()
    {
        int choice;

        do
        {
            Console.WriteLine("Select Task:");
            Console.WriteLine("1. Email Task");
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

            ITaskFactory factory = new MyTaskFactory(_emailService);

            BaseTask task = factory.CreateTask((TaskType)choice);

            await task.Execute();

        } while (choice != 0);
    }
}
