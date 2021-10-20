using System.Collections.Generic;
using Backups.Archiver;
using Backups.Entities;
using Backups.Storage;

namespace Backups.FileSaver
{
    public interface IFileSaver
    {
        void SaveFiles(
            IArchiver archiver,
            string archivePath,
            IStorage storage,
            IReadOnlyList<JobObject> jobObjects);
    }
}