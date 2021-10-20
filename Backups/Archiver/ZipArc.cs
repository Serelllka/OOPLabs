using System.IO;

namespace Backups.Archiver
{
    public class ZipArc : IArchive
    {
        private string _filePath;

        public void CreateFromStream(Stream stream, string filePath)
        {
            _filePath = filePath;
            var streamWriter = new StreamWriter(File.Create(filePath));
            streamWriter.Write(stream);
            streamWriter.Flush();
        }
    }
}