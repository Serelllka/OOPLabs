using System.IO;
using Backups.Tools;

namespace Backups.Entities
{
    public class JobObject
    {
        public JobObject(string filePath)
        {
            FilePath = filePath;
            if (!File.Exists(FilePath))
            {
                throw new BackupsException("This file is not exists!");
            }
        }

        public string FileName => Path.GetFileName(FilePath);
        public string FilePath { get; }

        public Stream ConvertFileIntoStream()
        {
            return File.OpenRead(FilePath);
        }
    }
}