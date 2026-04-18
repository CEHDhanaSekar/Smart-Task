using SmartTask.Core.Constants;

namespace SmartTask.Tasks.FileProcessing.Interfaces;

public interface IFileReader
{
    Task<IEnumerable<string>> Read(string filePath);
}
