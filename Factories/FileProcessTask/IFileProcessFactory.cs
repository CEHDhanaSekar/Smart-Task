using SmartTask.Shared.FileProcessTask.Processors;
using SmartTask.Shared.Helpers;

namespace SmartTask.Factories.FileProcessTask;

public interface IFileProcessFactory
{
    BaseFileProcessor CreateProcessor(FileTaskHelpers.FileType type);
}
