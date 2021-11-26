using System.IO;

namespace Backups.Models
{
    public class RestorePoint
    {
        public RestorePoint(string archivePath, string archivePostfix)
        {
            ArchivePostfix = archivePostfix;
            PointPath = archivePath;
            PointName = Path.GetFileName(PointPath);
        }

        public string PointPath { get; }
        public string PointName { get; }
        public string ArchivePostfix { get; }
    }
}