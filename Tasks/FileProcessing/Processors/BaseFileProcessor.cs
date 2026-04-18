using SmartTask.Tasks.FileProcessing.Interfaces;

namespace SmartTask.Tasks.FileProcessing.Processors;

public abstract class BaseFileProcessor : IFileReader, IFileProcessor
{
    public async Task<IEnumerable<string>> Read(string filePath)
    {
        return await Task.FromResult(File.ReadLines(filePath));
    }

    public abstract void Process(IEnumerable<string> content, Action<string> statusUpdater);
}
