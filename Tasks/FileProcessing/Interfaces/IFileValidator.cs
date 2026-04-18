using SmartTask.Core.Constants;

namespace SmartTask.Tasks.FileProcessing.Interfaces;

public interface IFileValidator
{
    ValidationResult Validate(string filePath);
}
