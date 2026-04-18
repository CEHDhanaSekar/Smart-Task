using SmartTask.Core.Constants;
using SmartTask.Core.Tasks;
using SmartTask.Tasks.FileProcessing.Factories;
using SmartTask.Tasks.FileProcessing.Interfaces;

namespace SmartTask.Tasks.FileProcessing;

public class FileProcessTask : BaseTask
{
    private string _filePath;
    private readonly IFileValidator _fileValidator;

    public FileProcessTask(string name, string filePath, IFileValidator fileValidator) : base(name, "FILE_PROCESS_TASK")
    {
        _filePath = filePath;
        _fileValidator = fileValidator;
    }

    public void SetFilePath(string filePath)
    {
        _filePath = filePath;
    }

    public override ValidationResult Validate()
    {
        var res = _fileValidator.Validate(_filePath);
        return res;
    }

    public override async Task Execute()
    {
        Status.UpdateStatus("Validating...");

        var res = Validate();

        await Task.Delay(1000);
        if (!res.IsValid)
        {
            Status.UpdateStatus($"Validation failed. {res.Error}");
            var fallbackRes = FallBack(ref res);
            if (!fallbackRes)
            {
                Status.UpdateStatus("Task Cancelled...");
                return;
            }
        }

        Status.UpdateStatus("Validation Completed...");

        try
        {
            Status.UpdateStatus("Processing File...");

            FileTaskHelpers.FileType extension = FileTaskHelpers.GetFileType(_filePath);

            FileProcessFactory fpf = new FileProcessFactory();
            var fileProcessor = fpf.CreateProcessor(extension);

            var content = await fileProcessor.Read(_filePath);
            fileProcessor.Process(content, msg => Status.UpdateStatus(msg));

            Status.UpdateStatus("File Successfully Processed...");
            await Task.Delay(1000);
            Status.UpdateStatus("Task Completed...");
        }
        catch (Exception ex)
        {
            Status.UpdateStatus($"Failed - {ex.Message}");
        }
    }

    private bool FallBack(ref ValidationResult res)
    {
        while (!res.IsValid)
        {
            Console.WriteLine("File path is invalid");
            Console.WriteLine("1. Want to Re-Enter File Path");
            Console.WriteLine("2. Want to Exit");
            Console.Write("Enter choice: ");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                choice = 0;
            }

            if (choice == 1)
            {
                Console.Write("Enter File Path: ");
                string filePath = Console.ReadLine() ?? "";
                SetFilePath(filePath);

                res = Validate();
                if (res.IsValid)
                {
                    return true;
                }
                else
                {
                    Console.WriteLine($"Validation failed. {res.Error}");
                }
            }
            else
            {
                res.IsValid = false;
                return false;
            }
        }
        return true;
    }
}
