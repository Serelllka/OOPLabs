using System.Collections.Generic;
using Backups.Models;

namespace BackupsExtra.Services
{
    public interface IRestorePointCountManager
    {
        void HandleOverflow(List<RestorePoint> restorePoints);
    }
}