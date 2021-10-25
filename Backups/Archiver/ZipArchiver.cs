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
            var archiveStream = new MemoryStream();
            using var zip = new ZipArchive(archiveStream, ZipArchiveMode.Create, leaveOpen: true);

            foreach (JobObject jobObject in jobObjects)
            {
                ZipArchiveEntry archiveEntry = zip.CreateEntry(jobObject.FileName);
                using Stream archiveEntryStream = archiveEntry.Open();
                jobObject.CopyFileToStream(archiveEntryStream);
            }

            return archiveStream;
        }

        public Stream Archive(JobObject jobObject)
        {
            var list = new List<JobObject> { jobObject };
            return Archive(list);
        }

        public string GetArchiveNameFromFileName(string fileName)
        {
            return fileName + _postfix;
        }
    }
}