using System.Diagnostics;
using System.IO;
using Bodoconsult.Core.Tools.Zip.Test.Helpers;
using NUnit.Framework;

namespace Bodoconsult.Core.Tools.Zip.Test
{
    [TestFixture]
    public class UnitTestUnZipHandler
    {
        [Test]
        public void TestReadContentOfZipFile()
        {

            var zipFile = Path.Combine(TestHelper.TestDataPath, @"Rechnung_10.04.2019.zip");

            var uh = new UnZipHandler(zipFile);

            Assert.IsTrue(uh.Files.Count>0);

            foreach (var f in uh.Files)
            {
                Debug.Print(f.FileName);
            }
        }


        [Test]
        public void TestSaveFile()
        {

            var zipFile = Path.Combine(TestHelper.TestDataPath, @"Rechnung_10.04.2019.zip");
            var  targetPath = TestHelper.TempPath;

            var uh = new UnZipHandler(zipFile);

            Assert.IsTrue(uh.Files.Count > 0);

            foreach (var f in uh.Files)
            {
                Debug.Print(f.FileName);

                var fileName = Path.Combine(targetPath, f.FileName);

                if (File.Exists(fileName)) File.Delete(fileName);

                uh.SaveFile(f.Path, fileName);

                Assert.IsTrue(File.Exists(fileName));
            }
        }

        [Test]
        public void TestGetFileData()
        {

            var zipFile = Path.Combine(TestHelper.TestDataPath, @"Rechnung_10.04.2019.zip");
            var targetPath = TestHelper.TempPath;

            var uh = new UnZipHandler(zipFile);

            Assert.IsTrue(uh.Files.Count > 0);

            foreach (var f in uh.Files)
            {
                Debug.Print(f.FileName);

                var fileName = Path.Combine(targetPath, f.FileName);

                if (File.Exists(fileName)) File.Delete(fileName);

                var data = uh.GetFileData(f.Path);

                File.WriteAllBytes(fileName, data);

                Assert.IsTrue(data.Length>0);

                Assert.IsTrue(File.Exists(fileName));

                TestHelper.StartFile(fileName);
            }
        }
    }
}