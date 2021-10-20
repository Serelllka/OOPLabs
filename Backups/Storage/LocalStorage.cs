using System.IO;

namespace Backups.Storage
{
    public class LocalStorage : IStorage
    {
        private string _root;

        public LocalStorage(string rootDir)
        {
            _root = rootDir;
        }

        public Stream GetStream(string archivePath)
        {
            return File.OpenWrite(archivePath);
        }
    }
}