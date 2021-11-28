using System;
using System.IO;
using Backups.Archiver;

namespace Backups.Models
{
    public class RestorePoint
    {
        private IArchiver _archiver;
        public RestorePoint(string archivePath, IArchiver archiver)
        {
            _archiver = archiver;
            PointPath = archivePath;
            PointName = Path.GetFileName(PointPath);
            CreationDate = DateTime.Today;
        }

        public DateTime CreationDate { get; }
        public string PointPath { get; }
        public string PointName { get; }
    }
}