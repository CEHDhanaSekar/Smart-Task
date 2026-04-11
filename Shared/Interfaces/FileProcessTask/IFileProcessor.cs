using System;
using System.Collections.Generic;

namespace SmartTask.Shared.Interfaces.FileProcessTask;

public interface IFileProcessor
{
    void Process(IEnumerable<string> content, Action<string> statusUpdater);
}