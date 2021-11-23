using Backups.Entities;
using BackupsExtra.Configuration;

namespace BackupsExtra.Services
{
    public interface IStateService
    {
        void Save(BackupJob backupJob);
        BackupJob Load();
    }
}