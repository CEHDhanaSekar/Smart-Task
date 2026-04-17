using SmartTask.Factories.FileProcessTask;
using SmartTask.Shared.Constants;
using SmartTask.Shared.Helpers;
using SmartTask.Shared.Interfaces.FileProcessTask;

namespace SmartTask.Tasks.Categories;

public class FileProcessTask : BaseTask
{
    private readonly string _filePath;
    private readonly IFileValidator _fileValidator;

    public FileProcessTask(string name, string filePath, IFileValidator fileValidator) : base(name, "FILE_PROCESS_TASK")
    {
        _filePath = filePath;
        _fileValidator = fileValidator;
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
            Status.UpdateStatus(res.Error);
            return;
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
}

