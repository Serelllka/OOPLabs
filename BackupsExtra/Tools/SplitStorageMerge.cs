using System.Collections.Generic;
using System.IO;
using Backups.Models;

namespace Backups.Tools
{
    public class SplitStorageMerge : IMergeMethod
    {
        public void Merge(
            string restorePointPath,
            List<RestorePoint> restorePoints)
        {
            foreach (string file in Directory.GetFiles(
                Path.Combine(
                    restorePointPath,
                    Path.GetDirectoryName(restorePoints[0].PointPath) ?? string.Empty)))
            {
                string path = Path.Combine(
                    restorePointPath,
                    Path.GetDirectoryName(restorePoints[1].PointPath) ?? string.Empty,
                    Path.GetFileName(file));
                File.Copy(file, path);
            }

            restorePoints.Remove(restorePoints[0]);
        }
    }
}