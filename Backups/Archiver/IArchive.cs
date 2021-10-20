using System.IO;

namespace Backups.Archiver
{
    public interface IArchive
    {
        void CreateFromStream(Stream stream, string filePath);
    }
}