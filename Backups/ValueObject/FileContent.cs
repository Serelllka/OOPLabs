using System.IO;
using System.Text.Json.Serialization;

namespace Backups.ValueObject
{
    public class FileContent
    {
        [JsonConstructor]
        public FileContent(string jobObjectName, string restorePointName, byte[] data) =>
            (JobObjectName, RestorePointName, Data) = (jobObjectName, restorePointName, data);

        public FileContent(string archivePath)
        {
            RestorePointName = Path.GetFileName(archivePath);
            JobObjectName = Path.GetFileName(Path.GetDirectoryName(archivePath));
        }

        public string JobObjectName { get; }
        public string RestorePointName { get; }
        public byte[] Data { get; }

        public void CreateFile(string pathToCreate)
        {
            using Stream stream = new FileStream(
                Path.Combine(pathToCreate, RestorePointName),
                FileMode.Create);
            stream.Write(Data);
        }
    }
}