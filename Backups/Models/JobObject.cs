using System.IO;
using Backups.Tools;
using Newtonsoft.Json;

namespace Backups.Models
{
    public class JobObject
    {
        public JobObject(string filePath)
        {
            FilePath = filePath;
            FileName = Path.GetFileName(FilePath);
            if (!File.Exists(FilePath))
            {
                throw new BackupsException("This file is not exists!");
            }
        }

        [JsonConstructor]
        private JobObject()
        { }

        public string FileName { get; }
        private string FilePath { get; }

        public void CopyFileToStream(Stream stream)
        {
            using Stream fileStream = File.OpenRead(FilePath);
            fileStream.CopyTo(stream);
        }
    }
}