using System.IO;
using Bodoconsult.Core.Tools.Zip.Test.Helpers;
using NUnit.Framework;

namespace Bodoconsult.Core.Tools.Zip.Test
{
    [TestFixture]
    public class UnitTestZipHandler
    {
        [Test]
        public void TestZipOneFileToFile()
        {
            var toZip = Path.Combine(TestHelper.TestDataPath, @"logo.jpg");
            var zipFileName = Path.Combine(TestHelper.TempPath, @"zipFile.zip");

            if (File.Exists(zipFileName)) File.Delete(zipFileName);

            var zh = new ZipHandler(new[] { toZip }, null);

            zh.GenerateZip(zipFileName);

            Assert.IsTrue(File.Exists(zipFileName));
        }

        [Test]
        public void TestZipTwoFilesToFile()
        {
            var toZip1 = Path.Combine(TestHelper.TestDataPath, @"logo.jpg");
            var toZip2 = Path.Combine(TestHelper.TestDataPath, @"logo1.jpg");
            var zipFileName = Path.Combine(TestHelper.TempPath, @"zipFile2.zip");

            if (File.Exists(zipFileName)) File.Delete(zipFileName);

            var zh = new ZipHandler(new[] { toZip1, toZip2 }, null);

            zh.GenerateZip(zipFileName);

            Assert.IsTrue(File.Exists(zipFileName));
        }

        [Test]
        public void TestZipTwoFilesToFileWithpassword()
        {
            var toZip1 = Path.Combine(TestHelper.TestDataPath, @"logo.jpg");
            var toZip2 = Path.Combine(TestHelper.TestDataPath, @"logo1.jpg");
            var zipFileName = Path.Combine(TestHelper.TempPath, @"zipFile3.zip");

            if (File.Exists(zipFileName)) File.Delete(zipFileName);

            var zh = new ZipHandler(new[] { toZip1, toZip2 }, "Test123!");

            zh.GenerateZip(zipFileName);

            Assert.IsTrue(File.Exists(zipFileName));
        }


        [Test]
        public void TestZipAsStream()
        {
            var toZip = Path.Combine(TestHelper.TestDataPath, @"logo.jpg");

            var stream = new MemoryStream();

            var zh = new ZipHandler(new[] { toZip }, null);

            zh.GenerateZip(stream);

            Assert.IsTrue(stream != null);
            Assert.IsTrue(stream.Position == 0);
            Assert.IsTrue(stream.Length > 0);
        }
    }
}