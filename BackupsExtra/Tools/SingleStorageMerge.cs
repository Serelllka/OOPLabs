﻿using System.Collections.Generic;
using System.IO;
using Backups.Models;

namespace Backups.Tools
{
    public class SingleStorageMerge : IMergeMethod
    {
        public void Merge(
            RestorePoint sourcePoint,
            RestorePoint destPoint)
        {
        }
    }
}