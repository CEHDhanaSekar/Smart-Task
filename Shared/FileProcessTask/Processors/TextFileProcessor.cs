using SmartTask.Shared.Interfaces.FileProcessTask;

namespace SmartTask.Shared.FileProcessTask.Processors;

public class TextFileProcessor : BaseFileProcessor
{
    public override void Process(IEnumerable<string> content, Action<string> statusUpdater)
    {
        long linesCount = 0;
        long wordsCount = 0;

        foreach (var line in content)
        {
            linesCount++;
            if (!string.IsNullOrWhiteSpace(line))
            {
                wordsCount += line.Split(new char[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length;
            }
        }

        statusUpdater($"Line count: {linesCount}, Word count: {wordsCount}");
    }
}
