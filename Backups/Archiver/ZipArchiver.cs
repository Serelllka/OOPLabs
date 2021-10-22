using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Entities;
using Backups.Models;

namespace Backups.Archiver
{
    public class ZipArchiver : IArchiver
    {
        private string _postfix;

        public ZipArchiver()
        {
            _postfix = ".zip";
        }

        public Stream Archive(IReadOnlyList<JobObject> jobObjects)
        {
            using var archiveStream = new MemoryStream();
            using var zip = new ZipArchive(archiveStream, ZipArchiveMode.Create, leaveOpen: true);

            foreach (JobObject jobObject in jobObjects)
            {
                ZipArchiveEntry archiveEntry = zip.CreateEntry(jobObject.FileName);
                using Stream archiveEntryStream = archiveEntry.Open();
                jobObject.ConvertFileIntoStream().CopyTo(archiveEntryStream);
                jobObject.CloseStream();
            }

            return archiveStream;
        }

        public Stream Archive(JobObject jobObject)
        {
            var archiveStream = new MemoryStream();
            using var zip = new ZipArchive(archiveStream, ZipArchiveMode.Create, leaveOpen: true);

            ZipArchiveEntry zipArchiveEntry = zip.CreateEntry(jobObject.FileName);
            using Stream archiveEntryStream = zipArchiveEntry.Open();
            jobObject.ConvertFileIntoStream().CopyTo(archiveEntryStream);
            jobObject.CloseStream();
            return archiveStream;
        }

        public string GetArchiveNameFromFileName(string fileName)
        {
            return fileName + _postfix;
        }
    }
}