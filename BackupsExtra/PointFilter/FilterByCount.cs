using System.Collections.Generic;
using Backups.Models;
using Backups.PointFilter;

namespace BackupsExtra.PointFilter
{
    public class FilterByCount : IPointFilter
    {
        private int _countLimit;

        public FilterByCount(int limit)
        {
            _countLimit = limit;
        }

        public IReadOnlyList<RestorePoint> Filter(IReadOnlyList<RestorePoint> restorePoints)
        {
            int index = 0;
            var toReturn = new List<RestorePoint>();
            foreach (RestorePoint restorePoint in restorePoints)
            {
                if (index < _countLimit)
                {
                    toReturn.Add(restorePoint);
                }

                ++index;
            }

            return toReturn;
        }
    }
}