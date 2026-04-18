using System;
using System.Collections.Generic;

namespace SmartTask.Tasks.FileProcessing.Interfaces;

public interface IFileProcessor
{
    void Process(IEnumerable<string> content, Action<string> statusUpdater);
}
