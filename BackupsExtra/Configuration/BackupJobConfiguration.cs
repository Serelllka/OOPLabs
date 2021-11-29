using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using Backups.Archiver;
using Backups.Entities;
using Backups.Models;
using Newtonsoft.Json;

namespace BackupsExtra.Configuration
{
    public class BackupJobConfiguration
    {
        public BackupJobConfiguration(BackupJob backupJob)
        {
            Archiver = backupJob.Archiver;
            JobObjects = backupJob.JobObjects;
            RestorePoints = backupJob.RestorePoints;
        }

        public IReadOnlyList<JobObject> JobObjects { get; }
        public IReadOnlyList<RestorePoint> RestorePoints { get; }
        public IArchiver Archiver { get; }
    }
}