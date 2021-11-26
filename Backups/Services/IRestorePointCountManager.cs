using System.Collections.Generic;
using Backups.Models;

namespace BackupsExtra.Services
{
    public interface IRestorePointCountManager
    {
        bool IsOverflow(IReadOnlyList<RestorePoint> restorePoints);
        void HandleOverflow(List<RestorePoint> restorePoints);
    }
}