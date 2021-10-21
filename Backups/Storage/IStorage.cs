using System.IO;

namespace Backups.Storage
{
    public interface IStorage
    {
        void SaveFromStream(string archivePath, Stream stream);
    }
}