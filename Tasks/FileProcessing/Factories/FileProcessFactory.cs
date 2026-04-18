using SmartTask.Tasks.FileProcessing;
using SmartTask.Tasks.FileProcessing.Processors;

namespace SmartTask.Tasks.FileProcessing.Factories;

public class FileProcessFactory : IFileProcessFactory
{
    private readonly Dictionary<FileTaskHelpers.FileType, Func<BaseFileProcessor>> _processMap;

    public FileProcessFactory()
    {
        _processMap = new Dictionary<FileTaskHelpers.FileType, Func<BaseFileProcessor>>
        {
            { FileTaskHelpers.FileType.Txt, CreateTextFileProcessor },
            { FileTaskHelpers.FileType.Json, CreateJsonFileProcessor },
            { FileTaskHelpers.FileType.Xml, CreateXmlFileProcessor }
        };
    }

    public BaseFileProcessor CreateProcessor(FileTaskHelpers.FileType type)
    {
        if (_processMap.TryGetValue(type, out var taskCreater))
        {
            return taskCreater();
        }
        throw new Exception($"Processor not found for file type: {type}");
    }

    private BaseFileProcessor CreateTextFileProcessor()
    {
        return new TextFileProcessor();
    }

    private BaseFileProcessor CreateJsonFileProcessor()
    {
        return new JsonFileProcessor();
    }

    private BaseFileProcessor CreateXmlFileProcessor()
    {
        return new XmlFileProcessor();
    }
}
