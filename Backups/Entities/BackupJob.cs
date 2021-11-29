using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Archiver;
using Backups.FileSaver;
using Backups.Logger;
using Backups.Models;
using Backups.Storage;
using Backups.Tools;
using BackupsExtra.Services;
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
        private IRestorePointCountManager _restorePointCounter;
        [JsonProperty]
        private IStorage _storage;
        [JsonProperty]
        private IFileSaver _fileSaver;
        [JsonProperty]
        private ILogger _logger;

        public BackupJob(
            IArchiver archiver,
            IFileSaver fileSaver,
            IStorage storage,
            IRestorePointCountManager restorePointCounter,
            ILogger logger)
        {
            _logger = logger ?? throw new BackupsException("logger can't be null");
            _restorePointCounter = restorePointCounter ?? throw new BackupsException(
                "restore point can't be null");
            _storage = storage ?? throw new BackupsException("storage can't be null");
            _fileSaver = fileSaver ?? throw new BackupsException("fileSaver can't be null");
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
            _logger.Log("new JobObject added");
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
            _logger.Log("JobObject deleted");
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

        public void CreateRestorePoint(string restorePointName)
        {
            _logger.Log("new RestorePoint created");
            _fileSaver.SaveFiles(Archiver, restorePointName, _storage, _jobObjects);

            _restorePoints.Add(new RestorePoint(
                Path.Combine(_storage.GetFolderPath(), restorePointName),
                Archiver));
            _restorePointCounter.HandleOverflow(_restorePoints);
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
    }
}