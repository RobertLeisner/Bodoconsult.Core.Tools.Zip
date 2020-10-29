using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ionic.Zip;

namespace Bodoconsult.Core.Tools.Zip
{
    /// <summary>
    /// Handles unzipping a ZIP file
    /// </summary>
    public class UnZipHandler
    {
        private readonly ZipFile _zipFile;


        public IList<FileEntry> Files { get; set; }


        public UnZipHandler(string fileName)
        {
            Files = new List<FileEntry>();
            _zipFile = ZipFile.Read(fileName);

            LoadFiles();
        }


        public UnZipHandler(byte[] data)
        {

            Files = new List<FileEntry>();
            Stream stream = new MemoryStream(data);
            _zipFile = ZipFile.Read(stream);

            LoadFiles();
        }

        private void LoadFiles()
        {
            foreach (var entry in _zipFile.Entries)
            {

                var fe = new FileEntry
                {
                    FileName = new FileInfo(entry.FileName.Replace("/", "\\")).Name,
                    Path = entry.FileName
                };

                Files.Add(fe);
            }
        }

        /// <summary>
        /// Save a file from <see cref="Files"/> in ZIP file to a target folder
        /// </summary>
        /// <param name="filePath">File name in ZIP file</param>
        /// <param name="targetPath">Target file. Existing files will be overwritten!</param>
        public void SaveFile(string filePath, string targetPath)
        {
            var entry = _zipFile.Entries.FirstOrDefault(x => x.FileName == filePath);

            if (entry == null) throw  new Exception($"File {filePath} not found in ZIP file!");

            try
            {
                if (File.Exists(targetPath)) File.Delete(targetPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            using (var fileStream = File.Create(targetPath))
            {
                entry.Extract(fileStream);
            }

            

        }

        /// <summary>
        /// Get a file from <see cref="Files"/> a byte arry
        /// </summary>
        /// <param name="filePath">File name in ZIP file</param>
        public byte[] GetFileData(string filePath)
        {
            var entry = _zipFile.Entries.FirstOrDefault(x => x.FileName == filePath);

            if (entry == null) throw new Exception($"File {filePath} not found in ZIP file!");


            using (var fs = new MemoryStream())
            {
                entry.Extract(fs);

                var bytes = new byte[fs.Length];

                fs.Position = 0;
                fs.Read(bytes, 0, bytes.Length);

                return bytes;
            }
        }
    }
}