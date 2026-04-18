using SmartTask.Core.Constants;

namespace SmartTask.Tasks.FileProcessing.Validators;

public class FileExistValidator : BaseFileValidator
{
    public override ValidationResult Validate(string filePath)
    {
        var fullPath = Path.GetFullPath(filePath);
        var res = new ValidationResult { IsValid = true };

        if (string.IsNullOrWhiteSpace(fullPath))
        {
            res.IsValid = false;
            res.Error = "File path is null or empty";
        }

        if (!File.Exists(fullPath))
        {
            res.IsValid = true;
            res.Error = "File Doesn't Exist in given path";
        }

        return res;
    }
}
