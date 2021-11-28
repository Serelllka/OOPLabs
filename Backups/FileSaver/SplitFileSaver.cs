using System.Collections.Generic;
using System.IO;
using Backups.Archiver;
using Backups.Entities;
using Backups.Models;
using Backups.Storage;

namespace Backups.FileSaver
{
    public class SplitFileSaver : IFileSaver
    {
        public void SaveFiles(
            IArchiver archiver,
            string archivePath,
            IStorage storage,
            IReadOnlyList<JobObject> jobObjects)
        {
            uint counter = 1;
            foreach (JobObject jobObject in jobObjects)
            {
                using Stream stream = archiver.Archive(jobObject);
                stream.Position = 0;
                using var memoryStream = new MemoryStream();

                stream.CopyTo(memoryStream);
                storage.SaveFromByteArray(
                    archiver.GetArchiveNameFromFileName(archivePath + counter),
                    memoryStream.ToArray());
                ++counter;
            }
        }
    }
}