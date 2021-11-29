using System.Collections.Generic;
using System.IO;
using Backups.Entities;
using Backups.Models;

namespace Backups.Archiver
{
    public interface IArchiver
    {
        Stream Archive(IReadOnlyList<JobObject> jobObjects);
        Stream Archive(JobObject jobObject);
        string GetArchiveNameFromFileName(string fileName);
        void AddFilesToArchive(Stream archiveStream, params Data[] filesStream);
        Data[] GetFromStream(Stream archiveStream, params string[] names);
        string GetPostfix();
    }
}