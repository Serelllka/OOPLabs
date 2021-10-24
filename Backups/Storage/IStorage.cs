using System.IO;

namespace Backups.Storage
{
    public interface IStorage
    {
        void SaveFromByteArray(string archivePath, byte[] bytes);
    }
}