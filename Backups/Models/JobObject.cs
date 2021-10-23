using System.IO;
using Backups.Tools;

namespace Backups.Models
{
    public class JobObject
    {
        private Stream _stream;
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

        public Stream ConvertFileIntoStream()
        {
            _stream = File.OpenRead(FilePath);
            return _stream;
        }

        public void CloseStream()
        {
            _stream.Close();
        }
    }
}