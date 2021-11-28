using System.IO;
using Backups.Tools;
using Newtonsoft.Json;

namespace Backups.Storage
{
    public class LocalStorage : IStorage
    {
        [JsonProperty]
        private string _storagePath;
        public LocalStorage(string storagePath)
        {
            if (!Directory.Exists(storagePath))
            {
                throw new BackupsException("This directory is not exists");
            }

            _storagePath = storagePath;
        }

        [JsonConstructor]
        private LocalStorage()
        {
        }

        public void SaveFromByteArray(string archivePath, byte[] bytes)
        {
            if (!Directory.Exists(Path.GetDirectoryName(Path.Combine(_storagePath, archivePath))))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(
                    Path.Combine(_storagePath, archivePath)) ?? string.Empty);
            }

            using FileStream fileStream = File.Create(Path.Combine(_storagePath, archivePath));
            using var memoryStream = new MemoryStream(bytes);
            memoryStream.CopyTo(fileStream);
            }
    }
}