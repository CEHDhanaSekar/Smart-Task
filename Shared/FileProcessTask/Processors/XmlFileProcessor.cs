using System.Xml.Linq;

namespace SmartTask.Shared.FileProcessTask.Processors;

public class XmlFileProcessor : BaseFileProcessor
{
    public override void Process(IEnumerable<string> content, Action<string> statusUpdater)
    {
        string xmlContent = string.Join(Environment.NewLine, content);
        if (string.IsNullOrWhiteSpace(xmlContent))
        {
            statusUpdater("Element count: 0, Attributes count: 0");
            return;
        }

        try
        {
            XDocument doc = XDocument.Parse(xmlContent);
            int elementCount = doc.Descendants().Count();
            int attributeCount = doc.Descendants().SelectMany(e => e.Attributes()).Count();

            statusUpdater($"Element count: {elementCount}, Attributes count: {attributeCount}");
        }
        catch (Exception ex)
        {
            statusUpdater($"XML parsing failed: {ex.Message}");
        }
    }
}
