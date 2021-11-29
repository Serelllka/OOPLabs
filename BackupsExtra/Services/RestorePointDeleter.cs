using System.Collections.Generic;
using System.Linq;
using Backups.Models;
using Backups.PointFilter;
using Backups.Tools;

namespace BackupsExtra.Services
{
    public class RestorePointDeleter : IRestorePointCountManager
    {
        private IPointFilter _pointFilter;

        public RestorePointDeleter(IPointFilter pointFilter)
        {
            _pointFilter = pointFilter;
        }

        public void HandleOverflow(List<RestorePoint> restorePoints)
        {
            IReadOnlyList<RestorePoint> obtainedList = _pointFilter.Filter(restorePoints);
            restorePoints.RemoveAll(item => !obtainedList.Contains(item));
        }
    }
}