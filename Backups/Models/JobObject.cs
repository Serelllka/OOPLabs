using System.IO;
using Backups.Tools;

namespace Backups.Entities
{
    public class JobObject
    {
        private Stream _stream;
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
            _stream = File.OpenRead(FilePath);
            return _stream;
        }

        public void CloseStream()
        {
            _stream.Close();
        }
    }
}