using System.Collections.Generic;
using System.IO;
using Backups.Entities;

namespace Backups.Archiver
{
    public interface IArchiver
    {
        void Archive(IReadOnlyList<JobObject> jobObjects, Stream archiveStream);
        void Archive(JobObject jobObject, Stream archiveStream);
    }
}