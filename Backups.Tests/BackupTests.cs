using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Archiver;
using Backups.Entities;
using Backups.FileSaver;
using Backups.Storage;
using Backups.Tools;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupTests
    {
        private IArchiver _archiver;
        private string _srcPath;

        [SetUp]
        public void Setup()
        {
            Directory.CreateDirectory("FilesToBackup");
            File.Create("FilesToBackup/test1.txt").Close();
            File.Create("FilesToBackup/test2.txt").Close();
            File.Create("FilesToBackup/test3.txt").Close();
            Directory.CreateDirectory("Backups");
            _archiver = new ZipArchiver();
        }

        [Test]
        public void CreateBackupJobCreateJobCreateRestorePoint()
        {
            var storage = new LocalStorage(@"Backups");
            var fileSaver = new SplitFileSaver();
            
            var backupJob = new BackupJob(_archiver);
            var backupObject1 = new JobObject(@"FilesToBackup\test1.txt");
            var backupObject2 = new JobObject(@"FilesToBackup\test2.txt");
            backupJob.AddJobObject(backupObject1);
            backupJob.AddJobObject(backupObject2);
            
            backupJob.CreateRestorePoint("RestorePoint1", fileSaver, storage);
            
            var backupObject3 = new JobObject(@"FilesToBackup\test3.txt");
            backupJob.AddJobObject(backupObject3);
            backupJob.CreateRestorePoint("RestorePoint2", fileSaver, storage);
            
            backupJob.RemoveJobObject(backupObject3);
            backupJob.CreateRestorePoint("RestorePoint3", fileSaver, storage);
            
            Assert.True(File.Exists(@"Backups\RestorePoint11.zip"));
            foreach (string file in Directory.GetFiles("Backups"))
            {
                File.Delete(file);
            }
            Directory.Delete("Backups");
            foreach (string file in Directory.GetFiles("FilesToBackup"))
            {
                File.Delete(file);
            }
            Directory.Delete("FilesToBackup");
        }

        [Test]
        public void CreateJobObjectWithNonExistingArchiver_ThrowsException()
        {
            Assert.Catch<BackupsException>(() =>
            {
                var backupJob = new BackupJob(null);
            });
        }
    }
}