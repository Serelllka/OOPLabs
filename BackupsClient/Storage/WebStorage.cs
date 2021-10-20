using System.IO;
using Backups.Storage;
using BackupsClient.ValueObject;

namespace BackupsClient.Storage
{
    public class WebStorage : IStorage
    {
        public Stream GetStream(string archivePath)
        {
            var fileContent = new FileContent(archivePath);
            return new MemoryStream(fileContent.Data);
        }
    }
}