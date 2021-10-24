using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using Backups.Archiver;
using Backups.FileSaver;
using Backups.Models;
using Backups.Storage;

namespace Backups.Entities
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