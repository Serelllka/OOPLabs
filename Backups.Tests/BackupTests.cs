using System.IO;
using System.IO.Compression;
using Backups.Archiver;
using Backups.Entities;
using Backups.FileSaver;
using Backups.Models;
using Backups.Storage;
using Backups.Tools;
using BackupsExtra.Services;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupTests
    {
        private IArchiver _archiver;

        [SetUp]
        public void Setup()
        {
            if (!Directory.Exists("FilesToBackup"))
            {
                Directory.CreateDirectory("FilesToBackup");
                File.Create(Path.Combine("FilesToBackup", "test1.txt")).Dispose();
                File.Create(Path.Combine("FilesToBackup", "test2.txt")).Dispose();
                File.Create(Path.Combine("FilesToBackup", "test3.txt")).Dispose();
            }

            if (!Directory.Exists("Backs"))
            {
                Directory.CreateDirectory("Backs");
            }

            _archiver = new ZipArchiver();
        }

        [Test]
        public void CreateBackupJobCreateJobCreateRestorePoint_CheckFilesForExisting()
        {
            const int restorePointCounter = 2;
            var storage = new LocalStorage(@"Backs");
            var fileSaver = new SplitFileSaver();

            var backupJob = new BackupJob(
                _archiver,
                fileSaver,
                storage,
                new RestorePointDeleter(restorePointCounter));
            var backupObject1 = new JobObject(Path.Combine("FilesToBackup","test1.txt"));
            var backupObject2 = new JobObject(Path.Combine("FilesToBackup","test2.txt"));
            backupJob.AddJobObject(backupObject1);
            backupJob.AddJobObject(backupObject2);
            
            backupJob.CreateRestorePoint(Path.Combine("RestorePoint1", "file"));
            
            var backupObject3 = new JobObject(Path.Combine("FilesToBackup","test3.txt"));
            backupJob.AddJobObject(backupObject3);
            backupJob.CreateRestorePoint(Path.Combine("RestorePoint2", "file"));
            
            backupJob.RemoveJobObject(backupObject3);
            backupJob.CreateRestorePoint(Path.Combine("RestorePoint3", "file"));
            
            Assert.True(File.Exists(Path.Combine("Backs",Path.Combine("RestorePoint1", "file1.zip"))));
        }

        [Test]
        public void CreateJobObjectWithNonExistingArchiver_ThrowsException()
        {
            const int restorePointCounter = 2;
            var storage = new LocalStorage(@"Backs");
            var fileSaver = new SplitFileSaver();
            var restorePointDeleter = new RestorePointDeleter(restorePointCounter);

            Assert.Catch<BackupsException>(() =>
            {
                var backupJob = new BackupJob(
                    null, 
                    fileSaver,
                    storage,
                    restorePointDeleter);
            });

            Assert.Catch<BackupsException>(() =>
            {
                var backupJob = new BackupJob(
                    _archiver, 
                    null,
                    storage,
                    restorePointDeleter);
            });
            
            Assert.Catch<BackupsException>(() =>
            {
                var backupJob = new BackupJob(
                    _archiver, 
                    fileSaver,
                    null,
                    restorePointDeleter);
            });
            
            Assert.Catch<BackupsException>(() =>
            {
                var backupJob = new BackupJob(
                    _archiver, 
                    fileSaver,
                    storage,
                    null);
            });
        }
        
        [Test]
        public void TriesCreateJobObjectWithNonExistingFile_ThrowsException()
        {
            Assert.Catch<BackupsException>(() =>
            {
                var backupObject1 = new JobObject(Path.Combine("FilesToBackup", "aboba.txt"));
            });
        }
        
        [TearDown]
        public void TearDown()
        {
            Directory.Delete("Backs", true);
            Directory.Delete("FilesToBackup", true);
        }
    }
}