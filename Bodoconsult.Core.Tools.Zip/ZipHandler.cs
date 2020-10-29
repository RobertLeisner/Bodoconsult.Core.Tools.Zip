using System;
using System.IO;
using Ionic.Zip;

namespace Bodoconsult.Core.Tools.Zip
{
    /// <summary>
    /// Class to generate ZIP files as file or as stream
    /// </summary>
    public class ZipHandler
    {
        private readonly string[] _files;

        private readonly string _password;

        /// <summary>
        /// Default ctor
        /// </summary>
        /// <param name="files">Files to pack in ZIP file</param>
        /// <param name="password">Password to save the ZIP file with. mMy be empty</param>
        public ZipHandler(string[] files, string password = null)
        {
            _files = files;
            _password = password;
        }

        /// <summary>
        /// Generate ZIP file and save it as file to disk
        /// </summary>
        /// <param name="fileName">File name</param>
        public void GenerateZip(string fileName)
        {
            if (File.Exists(fileName)) File.Delete(fileName);
            var zip = new ZipFile(fileName);
            GenerateZipBase(zip);
            zip.Save();

        }

        /// <summary>
        /// Generate ZIP file and save it into a stream
        /// </summary>
        /// <param name="stream">File name</param>
        public void GenerateZip(MemoryStream stream)
        {
            var zip = new ZipFile();
            GenerateZipBase(zip);
            zip.Save(stream);
            stream.Position = 0;
        }

        private ZipFile GenerateZipBase(ZipFile zip)
        {

            // Place password before adding files otherwise useless
            if (!string.IsNullOrEmpty(_password)) zip.Password = _password;
            foreach (var file in _files)
            {
                if (!File.Exists(file))
                {
                    throw new Exception("Not found: " + file);
                }

                zip.AddFile(file, "");
            }


            return zip;
        }


    }
}
