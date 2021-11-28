using System.Collections.Generic;
using Backups.Models;

namespace Backups.PointFilter
{
    public interface IPointFilter
    {
        IReadOnlyList<RestorePoint> Filter(IReadOnlyList<RestorePoint> restorePoints);
    }
}