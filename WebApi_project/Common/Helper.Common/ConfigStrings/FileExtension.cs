namespace Helper.Common.ConfigStrings
{
    /// <summary>
    /// Extensions of file types, in format ".TYPE"
    /// </summary>
    public static class FileExtension
    {
        private const char _separator = '.';

        /// <summary>
        /// .xml
        /// </summary>
        public static string Xml => _separator + "xml";
    }
}
