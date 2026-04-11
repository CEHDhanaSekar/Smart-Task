using System.Text.Json;

namespace SmartTask.Shared.FileProcessTask.Processors;

public class JsonFileProcessor : BaseFileProcessor
{
    public override void Process(IEnumerable<string> content, Action<string> statusUpdater)
    {
        string jsonContent = string.Join(Environment.NewLine, content);
        if (string.IsNullOrWhiteSpace(jsonContent))
        {
            statusUpdater("Item count: 0, Properties count: 0");
            return;
        }

        try
        {
            using JsonDocument doc = JsonDocument.Parse(jsonContent);

            int itemCount = 0;
            int propertyCount = 0;

            if (doc.RootElement.ValueKind == JsonValueKind.Array)
            {
                itemCount = doc.RootElement.GetArrayLength();
                foreach (var element in doc.RootElement.EnumerateArray())
                {
                    if (element.ValueKind == JsonValueKind.Object)
                    {
                        propertyCount += element.EnumerateObject().Count();
                    }
                }
            }
            else if (doc.RootElement.ValueKind == JsonValueKind.Object)
            {
                itemCount = 1;
                propertyCount = doc.RootElement.EnumerateObject().Count();
            }

            statusUpdater($"Item count: {itemCount}, Properties count: {propertyCount}");
        }
        catch (JsonException ex)
        {
            statusUpdater($"JSON parsing failed: {ex.Message}");
        }
    }
}
