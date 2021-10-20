using System.IO;

namespace Backups.Storage
{
    public interface IStorage
    {
        Stream GetStream(string archivePath);
    }
}