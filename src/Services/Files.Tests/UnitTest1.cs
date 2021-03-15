using System.IO;
using System.Threading.Tasks;
using Files.API.Controllers;
using Files.API.Repositories;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;

namespace Files.Tests
{
    public class Tests
    {
        private const string Path = "../../..";
        
        [SetUp]
        public void Setup()
        {
            if (!File.Exists(Path + "/Files/NewFile1.txt"))
            {
                File.Create(Path + "/Files/NewFile1.txt");
            } 
            if (!File.Exists(Path + "/Files/NewFile2.txt"))
            {
                File.Create(Path + "/Files/NewFile2.txt");
            } 
            if (!File.Exists(Path + "/Files/NewFile3.txt"))
            {
                File.Create(Path + "/Files/NewFile3.txt");
            } 
            if (File.Exists(Path + "/Files/FileFromClient.txt"))
            {
                File.Delete(Path + "/Files/FileFromClient.txt");
            }
        }

        [TestCase("1", "NewFile1.txt")]
        [TestCase("2", "NewFile2.txt")]
        [TestCase("3", "NewFile3.txt")]
        public async Task GetFileAsync_ReturnsSpecificFile(string id, string name)
        {
            var controller = new FilesController(new FileRepositoryForTests());
            var file = await controller.GetFileAsync(id);
            Assert.AreEqual(name, file.FileDownloadName);
        }

        [Test]
        public async Task CreateFileAsync_CreatesSpecificFile()
        {
            await using (var stream = new FileStream(Path + "/FileFromClient.txt", FileMode.Open))
            {
                var file = new FormFile(stream, 1, stream.Length, stream.Name, "FileFromClient.txt");
                var controller = new FilesController(new FileRepositoryForTests());
                await controller.CreateFileAsync(file);
            }

            using (var stream = new StreamReader(Path + "/Files/FileFromClient.txt"))
            {
                var text = await stream.ReadToEndAsync();
                Assert.AreEqual("TextFromClient", text);
            }
        }
    }
}