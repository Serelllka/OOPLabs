using System.IO;
using Backups.Entities;

namespace BackupsClient.ValueObject
{
    public class FileContent
    {
        public FileContent()
        {
        }

        public FileContent(string archivePath)
        {
            RestorePointName = Path.GetFileName(archivePath);
            JobObjectName = Path.GetFileName(Path.GetDirectoryName(archivePath));
        }

        public string JobObjectName { get; set; }
        public string RestorePointName { get; set; }
        public byte[] Data { get; set; }

        public void CreateFile(string pathToCreate)
        {
            Directory.CreateDirectory(Path.Combine(pathToCreate, JobObjectName));
            Stream stream = new FileStream(
                Path.Combine(pathToCreate, JobObjectName, RestorePointName),
                FileMode.Create);
            stream.Write(Data);
            stream.Close();
        }
    }
}