using System.Collections.Generic;
using Backups.Models;

namespace Backups.Tools
{
    public interface IMergeMethod
    {
        void Merge(
            RestorePoint sourcePoint,
            RestorePoint destPoint);
    }
}