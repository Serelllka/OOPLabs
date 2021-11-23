using System.IO;
using System.Text;
using Backups.Entities;
using BackupsExtra.Configuration;
using Newtonsoft.Json;

namespace BackupsExtra.Services
{
    public class FileJsonStateService : IFileStateService, IJsonStateService
    {
        private string _filename;
        private JsonSerializerSettings _jsonSetting;

        public void Save(BackupJob backupJob)
        {
            File.WriteAllText(
                _filename,
                JsonConvert.SerializeObject(backupJob, Formatting.Indented, _jsonSetting));
        }

        public BackupJob Load()
        {
            BackupJob backupJob = JsonConvert.DeserializeObject<BackupJob>(
                File.ReadAllText(_filename),
                _jsonSetting);
            return backupJob;
        }

        public void SetJsonSettings(JsonSerializerSettings settings)
        {
            _jsonSetting = settings;
        }

        public void SetFilename(string filename)
        {
            _filename = filename;
        }
    }
}