using System.IO;
using System.IO.Compression;
using Backups.Archiver;
using Backups.Entities;
using Backups.FileSaver;
using Backups.Services;
using Backups.Storage;
using Backups.Tools;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupTests
    {
        private IArchiver _archiver;
        private string _srcPath;
        private BackupManager _backupManager;
        
        [SetUp]
        public void Setup()
        {
            _srcPath = @"C:\Users\vprog\RiderProjects\Serelllka\Backups.Tests\Src\";
            _archiver = new ZipArchiver();
            _backupManager = new BackupManager();
        }

        [Test]
        public void CreateBackupJobCreateJobCreateRestorePoint()
        {
            var storage = new LocalStorage("");
            var fileSaver = new SingleFileSaver();
            
            var backupJob = new BackupJob(_archiver, _srcPath + @"Backups\BackupJob1\");
            var backupObject1 = new JobObject(_srcPath + @"FilesToBackup\lol.txt");
            var backupObject2 = new JobObject(_srcPath + @"FilesToBackup\kek.txt");
            backupJob.AddJobObject(backupObject1);
            backupJob.AddJobObject(backupObject2);
            
            backupJob.CreateRestorePoint("RestorePoint1.zip", fileSaver, storage);
            
            var backupObject3 = new JobObject(_srcPath + @"FilesToBackup\cheburek.txt");
            backupJob.AddJobObject(backupObject3);
            backupJob.CreateRestorePoint("RestorePoint2.zip", fileSaver, storage);
            
            backupJob.RemoveJobObject(backupObject3);
            backupJob.CreateRestorePoint("RestorePoint3.zip", fileSaver, storage);
        }

        [Test]
        public void CreateJobObjectWithNonExistingFile_ThrowsException()
        {
            Assert.Catch<BackupsException>(() =>
            {
                var backupJob = new BackupJob(_archiver, null);
            });
        }

        [Test]
        public void ArchivesFolderThenExtract_FilesInSourceAndDestinationDirectoriesTheSame()
        {
            ZipFile.ExtractToDirectory(
                _srcPath + "zip.zip",
                _srcPath + "Extract");
            Assert.True(File.Exists(_srcPath + "Extract/lol.txt "));
        }
    }
}