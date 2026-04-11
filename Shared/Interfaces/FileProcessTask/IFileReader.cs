using SmartTask.Shared.Constants;

namespace SmartTask.Shared.Interfaces.FileProcessTask;

public interface IFileReader
{
    Task<IEnumerable<string>> Read(string filePath);
}
