using System.IO;
using Backups.Tools;

namespace Backups.Storage
{
    public class LocalStorage : IStorage
    {
        private string _storagePath;
        public LocalStorage(string storagePath)
        {
            if (storagePath is null)
            {
                throw new BackupsException("storage path can't be null");
            }

            if (!Directory.Exists(storagePath))
            {
                throw new BackupsException("This directory is not exists");
            }

            _storagePath = storagePath;
        }

        public void SaveFromByteArray(string archivePath, byte[] bytes)
        {
            using FileStream fileStream = File.Create(Path.Combine(_storagePath, archivePath));
            using var memoryStream = new MemoryStream(bytes);
            memoryStream.CopyTo(fileStream);
            }
    }
}