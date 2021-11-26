using System.Collections.Generic;
using Backups.Models;

namespace Backups.Tools
{
    public interface IMergeMethod
    {
        void Merge(
            string restorePointPath,
            List<RestorePoint> restorePoints);
    }
}