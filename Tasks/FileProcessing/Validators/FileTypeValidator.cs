using SmartTask.Core.Constants;
using SmartTask.Core.Helpers;

namespace SmartTask.Tasks.FileProcessing.Validators;

public class FileTypeValidator : BaseFileValidator
{
    public override ValidationResult Validate(string filePath)
    {
        var res = new ValidationResult { IsValid = true };

        var allowedExtensions = FileTaskHelpers.AllowedFileTypes;

        var extension = Path.GetExtension(filePath);

        if (string.IsNullOrWhiteSpace(extension))
        {
            res.IsValid = false;
            res.Error = "File has no extension";
            return res;
        }

        if (!allowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
        {
            res.IsValid = false;
            res.Error = $"Invalid file type. Allowed: {string.Join(", ", allowedExtensions)}";
            return res;
        }

        return res;
    }
}
