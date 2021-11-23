using System.Collections.Generic;
using System.Linq;
using Backups.Archiver;
using Backups.FileSaver;
using Backups.Models;
using Backups.Storage;
using Backups.Tools;
using Newtonsoft.Json;

namespace Backups.Entities
{
    public class BackupJob
    {
        [JsonProperty]
        private List<JobObject> _jobObjects;
        [JsonProperty]
        private List<RestorePoint> _restorePoints;
        [JsonProperty]
        private int _restorePointCounter;

        public BackupJob(IArchiver archiver, int restorePointCounter)
        {
            if (restorePointCounter == 0)
            {
                throw new BackupsException("restorePointsCounter must be positive");
            }

            _restorePointCounter = restorePointCounter;
            Archiver = archiver ?? throw new BackupsException("archiver can't be null");
            _restorePoints = new List<RestorePoint>();
            _jobObjects = new List<JobObject>();
        }

        [JsonConstructor]
        private BackupJob()
        {
        }

        public IArchiver Archiver { get; }
        [JsonIgnore]
        public IReadOnlyList<JobObject> JobObjects => _jobObjects;
        [JsonIgnore]
        public IReadOnlyList<RestorePoint> RestorePoints => _restorePoints;

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
            fileSaver.SaveFiles(Archiver, restorePointName, storage, _jobObjects);

            _restorePoints.Add(new RestorePoint(restorePointName));
        }

        public void RegisterRestorePoint(RestorePoint restorePoint)
        {
            if (restorePoint is null)
            {
                throw new BackupsException("Restore point can't be null");
            }

            if (_restorePoints.Any(point => point.PointName == restorePoint.PointName))
            {
                throw new BackupsException("Restore with this name already registered");
            }

            _restorePoints.Add(restorePoint);
        }

        public RestorePoint GetRestorePoint(string restorePointName)
        {
            if (_restorePoints.All(point => point.PointName != restorePointName))
            {
                throw new BackupsException("Restore point with this name doesn't exists");
            }

            return _restorePoints.First(point => point.PointName == restorePointName);
        }

        public void RemoveRestorePoint(RestorePoint restorePoint)
        {
            if (restorePoint is null)
            {
                throw new BackupsException("Restore point can't be null");
            }

            if (!_restorePoints.Contains(restorePoint))
            {
                throw new BackupsException("This restore point is not implemented");
            }

            _restorePoints.Remove(restorePoint);
        }

        private void UpdateState()
        {
            if (_restorePoints.Count > _restorePointCounter)
            {
                RemoveRestorePoint(_restorePoints[0]);
            }
        }
    }
}