using System.Xml;

namespace Helper.Common.Files
{
    /// <summary>
    /// Helper methods regarding files in .xml format.
    /// </summary>
    public static class Xml
    {
        public static XmlWriterSettings GetXmlWriterSettings()
        {
            return new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "\t",
                Async = true
            };
        }
    }
}
