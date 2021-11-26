using System.Collections.Generic;
using Backups.Models;

namespace Backups.Tools
{
    public class SingleStorageMerge : IMergeMethod
    {
        public void Merge(
            string restorePointPath,
            List<RestorePoint> restorePoints)
        {
            restorePoints.Remove(restorePoints[0]);
        }
    }
}