using System.Collections.Generic;
using System.IO;
using Backups.Models;

namespace Backups.Tools
{
    public class SplitStorageMerge : IMergeMethod
    {
        public void Merge(
            RestorePoint sourcePoint,
            RestorePoint destPoint)
        {
            foreach (string file in Directory.GetFiles(
                Path.Combine(Path.GetDirectoryName(sourcePoint.PointPath) ?? string.Empty)))
            {
                string path = Path.Combine(
                    Path.GetDirectoryName(destPoint.PointPath) ?? string.Empty,
                    Path.GetFileName(file));
                File.Copy(file, path);
            }
        }
    }
}