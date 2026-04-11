using SmartTask.Shared.Constants;

namespace SmartTask.Shared.Interfaces.FileProcessTask;

public interface IFileValidator
{
    ValidationResult Validate(string filePath);
}
