using System.Collections.Generic;
using System.IO;
using Backups.Archiver;
using Backups.Entities;
using Backups.Models;
using Backups.Storage;

namespace Backups.FileSaver
{
    public class SingleFileSaver : IFileSaver
    {
        public void SaveFiles(
            IArchiver archiver,
            string archivePath,
            IStorage storage,
            IReadOnlyList<JobObject> jobObjects)
        {
            using Stream stream = archiver.Archive(jobObjects);
            using var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            storage.SaveFromByteArray(archivePath, memoryStream.ToArray());
        }
    }
}