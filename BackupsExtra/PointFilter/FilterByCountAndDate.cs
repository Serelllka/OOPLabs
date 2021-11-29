using System.Collections.Generic;
using System.Linq;
using Backups.Models;
using Backups.PointFilter;

namespace BackupsExtra.PointFilter
{
    public class FilterByCountAndDate : IPointFilter
    {
        private FilterByCount _filterByCount;
        private FilterByDate _filterByDate;

        public FilterByCountAndDate(FilterByCount countFilter, FilterByDate dateFilter)
        {
            _filterByCount = countFilter;
            _filterByDate = dateFilter;
        }

        public IReadOnlyList<RestorePoint> Filter(IReadOnlyList<RestorePoint> restorePoints)
        {
            IReadOnlyList<RestorePoint> filteredByCount = _filterByCount.Filter(restorePoints);
            IReadOnlyList<RestorePoint> filteredByDate = _filterByDate.Filter(restorePoints);
            return filteredByCount.Intersect(filteredByDate).ToList();
        }
    }
}