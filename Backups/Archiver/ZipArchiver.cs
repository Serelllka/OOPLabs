using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Entities;
using Backups.Models;
using Backups.Tools;

namespace Backups.Archiver
{
    public class ZipArchiver : IArchiver
    {
        private readonly string _postfix;
        public ZipArchiver()
        {
            _postfix = ".zip";
        }

        public string GetPostfix()
        {
            return _postfix;
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
            return $"{fileName}{_postfix}";
        }

        public void AddFilesToArchive(Stream archiveStream, params Data[] filesStream)
        {
            using var zip = new ZipArchive(archiveStream, ZipArchiveMode.Update, leaveOpen: true);

            foreach (Data data in filesStream)
            {
                ZipArchiveEntry archiveEntry = zip.CreateEntry(data.Name);
                using Stream stream = archiveEntry.Open();
                data.Stream.CopyTo(stream);
            }
        }

        public Data[] GetFromStream(Stream archiveStream, params string[] names)
        {
            using var zip = new ZipArchive(archiveStream, ZipArchiveMode.Read, leaveOpen: true);
            var datass = new List<Data>();

            foreach (string name in names)
            {
                ZipArchiveEntry archiveEntry = zip.GetEntry(name) ??
                                               throw new BackupsException("can't get entry");
                Stream stream = archiveEntry.Open();
                datass.Add(new Data(name, stream));
            }

            return datass.ToArray();
        }
    }
}