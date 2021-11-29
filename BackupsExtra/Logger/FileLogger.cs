using System.IO;
using Backups.Logger;

namespace BackupsExtra.Logger
{
    public class FileLogger : ILogger
    {
        private string _fileName;

        public FileLogger(string fileName)
        {
            _fileName = fileName;
        }

        public void Log(string message)
        {
            File.AppendAllText(_fileName, message);
        }
    }
}