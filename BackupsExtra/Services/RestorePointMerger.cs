using System.Collections.Generic;
using System.Linq;
using Backups.Models;
using Backups.PointFilter;
using Backups.Tools;

namespace BackupsExtra.Services
{
    public class RestorePointMerger : IRestorePointCountManager
    {
        private readonly IPointFilter _pointFilter;
        private readonly IMergeMethod _mergeMethod;
        private readonly string _restorePointPath;

        public RestorePointMerger(IPointFilter pointFilter, string pointPath, IMergeMethod mergeMethod)
        {
            _restorePointPath = pointPath;
            _pointFilter = pointFilter;
            _mergeMethod = mergeMethod;
        }

        public void HandleOverflow(List<RestorePoint> restorePoints)
        {
            IReadOnlyList<RestorePoint> filteredList = _pointFilter.Filter(restorePoints);
            if (filteredList.Count == 0)
            {
                throw new BackupsException("RestorePoint list can't be empty");
            }

            RestorePoint firstObtainedObject = filteredList[0];
            foreach (RestorePoint restorePoint in restorePoints)
            {
                if (!filteredList.Contains(restorePoint))
                {
                    _mergeMethod.Merge(restorePoint, firstObtainedObject);
                }
            }

            restorePoints.RemoveAll(item => !filteredList.Contains(item));
        }
    }
}