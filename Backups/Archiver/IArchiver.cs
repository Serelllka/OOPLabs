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
        public string GetArchiveNameFromFileName(string fileName);
    }
}