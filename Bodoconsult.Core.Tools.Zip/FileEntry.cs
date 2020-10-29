namespace Bodoconsult.Core.Tools.Zip
{
    /// <summary>
    /// Represents a file in a ZIP file
    /// </summary>
    public struct FileEntry
    {
        /// <summary>
        /// Full path of the file
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// File name of the file
        /// </summary>
        public string FileName { get; set; }

    }
}