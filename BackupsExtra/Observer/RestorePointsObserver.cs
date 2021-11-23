using System.Linq;
using Backups.Entities;

namespace BackupsExtra.Observer
{
    public class RestorePointsObserver
    {
        private readonly int _restorePointsLimits;
        private readonly BackupJob _backupJobPublisher;

        public RestorePointsObserver(BackupJob backupJob, int restorePointsLimits)
        {
            _backupJobPublisher = backupJob;
            _restorePointsLimits = restorePointsLimits;
        }

        public void UpdateState()
        {
            if (_backupJobPublisher.RestorePoints.Count > _restorePointsLimits)
            {
                _backupJobPublisher.RemoveRestorePoint(
                    _backupJobPublisher.GetRestorePoint(_backupJobPublisher.RestorePoints[0].PointName));
            }
        }
    }
}