using SmartTask.Tasks.FileProcessing;
using SmartTask.Tasks.FileProcessing.Processors;

namespace SmartTask.Tasks.FileProcessing.Factories;

public interface IFileProcessFactory
{
    BaseFileProcessor CreateProcessor(FileTaskHelpers.FileType type);
}
