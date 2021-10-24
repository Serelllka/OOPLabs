using System.IO;

namespace Backups.Models
{
    public class RestorePoint
    {
        public RestorePoint(string archivePath)
        {
            PointPath = archivePath;
            PointName = Path.GetFileName(PointPath);
        }

        public string PointPath { get; }
        public string PointName { get; }
    }
}