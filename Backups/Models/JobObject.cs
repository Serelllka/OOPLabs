using System;
using System.IO;
using Backups.Tools;

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

        public string FileName { get; }
        public string FilePath { get; }

        public void CopyFileToStream(Stream stream)
        {
            Stream fileStream = File.OpenRead(FilePath);
            fileStream.CopyTo(stream);
            fileStream.Close();
        }
    }
}