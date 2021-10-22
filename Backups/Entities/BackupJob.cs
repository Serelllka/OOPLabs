using System.Collections.Generic;
using System.IO;
using Backups.Archiver;
using Backups.FileSaver;
using Backups.Models;
using Backups.Storage;
using Backups.Tools;

namespace Backups.Entities
{
    public class BackupJob
    {
        private List<JobObject> _jobObjects;
        private List<RestorePoint> _restorePoints;

        public BackupJob(IArchiver archiver)
        {
            Archiver = archiver ?? throw new BackupsException("archiver can't be null");
            _restorePoints = new List<RestorePoint>();
            _jobObjects = new List<JobObject>();
        }

        private IReadOnlyList<JobObject> JobObjects => _jobObjects;
        private IArchiver Archiver { get; }

        public void AddJobObject(JobObject jobObject)
        {
            if (jobObject is null)
            {
                throw new BackupsException("jobObject doesn't exist");
            }

            if (_jobObjects.Contains(jobObject))
            {
                throw new BackupsException("this jobObject already implemented");
            }

            _jobObjects.Add(jobObject);
        }

        public void RemoveJobObject(JobObject jobObject)
        {
            if (jobObject is null)
            {
                throw new BackupsException("jobObject doesn't exist");
            }

            if (!_jobObjects.Contains(jobObject))
            {
                throw new BackupsException("this jobObject isn't exist in this BackupJob");
            }

            _jobObjects.Remove(jobObject);
        }

        public void CreateRestorePoint(string restorePointName, IFileSaver fileSaver, IStorage storage)
        {
            _restorePoints.Add(new RestorePoint(
                Archiver,
                restorePointName,
                JobObjects,
                fileSaver,
                storage));
        }
    }
}