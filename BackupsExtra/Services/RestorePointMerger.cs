using System.Collections.Generic;
using System.Linq;
using Backups.Models;
using Backups.Tools;

namespace BackupsExtra.Services
{
    public class RestorePointMerger : IRestorePointCountManager
    {
        private readonly int _restorePointsLimit;
        private readonly IMergeMethod _mergeMethod;
        private readonly string _restorePointPath;

        public RestorePointMerger(int restorePointsLimit, string pointPath, IMergeMethod mergeMethod)
        {
            _restorePointPath = pointPath;
            _restorePointsLimit = restorePointsLimit;
            _mergeMethod = mergeMethod;
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
                _mergeMethod.Merge(
                    _restorePointPath,
                    restorePoints);
            }
        }
    }
}