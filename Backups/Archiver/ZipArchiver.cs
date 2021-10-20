using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Entities;

namespace Backups.Archiver
{
    public class ZipArchiver : IArchiver
    {
        public void Archive(IReadOnlyList<JobObject> jobObjects, Stream archiveStream)
        {
            using var zip = new ZipArchive(archiveStream, ZipArchiveMode.Create, leaveOpen: true);

            foreach (JobObject jobObject in jobObjects)
            {
                ZipArchiveEntry archiveEntry = zip.CreateEntry(jobObject.FileName);
                using Stream archiveEntryStream = archiveEntry.Open();
                jobObject.ConvertFileIntoStream().CopyTo(archiveEntryStream);
            }
        }

        public void Archive(JobObject jobObject, Stream archiveStream)
        {
            using var zip = new ZipArchive(archiveStream, ZipArchiveMode.Create, leaveOpen: true);

            ZipArchiveEntry zipArchiveEntry = zip.CreateEntry(jobObject.FileName);
            using Stream archiveEntryStream = zipArchiveEntry.Open();
            jobObject.ConvertFileIntoStream().CopyTo(archiveEntryStream);
        }
    }
}