# What does the library

Bodoconsult.Core.Zip library simplifies the handling of ZIP files in C#. With the ZipHandler class you can add one or more files to a ZIP archive. 
With the UnZipHandler class you can unzipped the files contained in a ZIP archive.


# How to use the library

The source code contain a NUnit test classes, the following source code is extracted from. The samples below show the most helpful use cases for the library.

## Create a ZIP files

Use ZipHandler class to build a ZIP Archive from one or more files:

            var toZip1 = Path.Combine(TestHelper.TestDataPath, @"logo.jpg");
            var toZip2 = Path.Combine(TestHelper.TestDataPath, @"logo1.jpg");
			
            var zipFileName = Path.Combine(TestHelper.TempPath, @"zipFile3.zip");

            // Zip files with a password
            var zh = new ZipHandler(new[] { toZip1, toZip2 }, "Test123!");
            zh.GenerateZip(zipFileName);

## Open an existing ZIP fileS

Use the UnZipHandler class to unzip the files contained in a ZIP archive:

            var zipFile = Path.Combine(TestHelper.TestDataPath, @"Rechnung_10.04.2019.zip");
            var targetPath = TestHelper.TempPath;

            var uh = new UnZipHandler(zipFile);
            foreach (var f in uh.Files)
            {
                var fileName = Path.Combine(targetPath, f.FileName);

                if (File.Exists(fileName)) File.Delete(fileName);

                // Get file data and save it
                var data = uh.GetFileData(f.Path);

                File.WriteAllBytes(fileName, data);
            }

# About us

Bodoconsult <http://www.bodoconsult.de> is a Munich based software development company from Germany.

Robert Leisner is senior software developer at Bodoconsult. See his profile on <http://www.bodoconsult.de/Curriculum_vitae_Robert_Leisner.pdf>.

