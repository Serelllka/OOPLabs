using System.Collections.Generic;
using Backups.Models;
using Backups.Tools;

namespace BackupsExtra.Services
{
    public class RestorePointDeleter : IRestorePointCountManager
    {
        private int _restorePointsLimit;

        public RestorePointDeleter(int limit)
        {
            _restorePointsLimit = limit;
        }

        public bool IsOverflow(IReadOnlyList<RestorePoint> restorePoints)
        {
            if (restorePoints is null)
            {
                throw new BackupsException("Restore point can't be null");
            }

            return restorePoints.Count > _restorePointsLimit;
        }

        public void HandleOverflow(List<RestorePoint> restorePoints)
        {
            if (IsOverflow(restorePoints))
            {
                restorePoints.Remove(restorePoints[0]);
            }
        }
    }
}