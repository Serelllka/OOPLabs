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

        public void SaveFromStream(string localArchivePath, Stream stream)
        {
            stream.Position = 0;
            FileStream fileStream = File.Create(Path.Combine(_storagePath, localArchivePath));
            stream.CopyTo(fileStream);
            fileStream.Close();
            stream.Close();
        }
    }
}