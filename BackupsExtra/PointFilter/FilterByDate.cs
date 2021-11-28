using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Models;
using Backups.PointFilter;

namespace BackupsExtra.PointFilter
{
    public class FilterByDate : IPointFilter
    {
        private DateTime _filterDate;

        public FilterByDate(DateTime creationDate)
        {
            _filterDate = creationDate;
        }

        public IReadOnlyList<RestorePoint> Filter(IReadOnlyList<RestorePoint> restorePoints)
        {
            return restorePoints.Where(item => item.CreationDate > _filterDate).ToList();
        }
    }
}