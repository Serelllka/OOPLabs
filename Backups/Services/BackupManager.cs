using System;
using System.Collections.Generic;
using Backups.Entities;
using Backups.Tools;

namespace Backups.Services
{
    public class BackupManager
    {
        private List<BackupJob> _backupJobs;
        public BackupManager()
        {
            _backupJobs = new List<BackupJob>();
            Id = Guid.NewGuid();
        }

        public Guid Id { get; }

        public void AddBackupJob(BackupJob backupJob)
        {
            if (backupJob is null)
            {
                throw new BackupsException("BackupJob doesn't exist");
            }

            if (_backupJobs.Contains(backupJob))
            {
                throw new BackupsException("This job is already implemented");
            }

            _backupJobs.Add(backupJob);
        }
    }
}