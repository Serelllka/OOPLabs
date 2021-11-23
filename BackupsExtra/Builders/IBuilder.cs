using Backups.Entities;
using Backups.Models;

namespace BackupsExtra.Builders
{
    public interface IBuilder
    {
        BackupJob Build();
    }
}