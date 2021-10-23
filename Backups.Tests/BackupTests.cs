using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Archiver;
using Backups.Entities;
using Backups.FileSaver;
using Backups.Models;
using Backups.Storage;
using Backups.Tools;
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
                File.Create(Path.Combine("FilesToBackup", "test1.txt")).Close();
                File.Create(Path.Combine("FilesToBackup", "test2.txt")).Close();
                File.Create(Path.Combine("FilesToBackup", "test3.txt")).Close();
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
            var storage = new LocalStorage(@"Backs");
            var fileSaver = new SplitFileSaver();
            
            var backupJob = new BackupJob(_archiver);
            var backupObject1 = new JobObject(Path.Combine("FilesToBackup","test1.txt"));
            var backupObject2 = new JobObject(Path.Combine("FilesToBackup","test2.txt"));
            backupJob.AddJobObject(backupObject1);
            backupJob.AddJobObject(backupObject2);
            
            backupJob.CreateRestorePoint("RestorePoint1", fileSaver, storage);
            
            var backupObject3 = new JobObject(Path.Combine("FilesToBackup","test3.txt"));
            backupJob.AddJobObject(backupObject3);
            backupJob.CreateRestorePoint("RestorePoint2", fileSaver, storage);
            
            backupJob.RemoveJobObject(backupObject3);
            backupJob.CreateRestorePoint("RestorePoint3", fileSaver, storage);
            
            Assert.True(File.Exists(Path.Combine("Backs","RestorePoint11.zip")));
        }

        [Test]
        public void CreateJobObjectWithNonExistingArchiver_ThrowsException()
        {
            Assert.Catch<BackupsException>(() =>
            {
                var backupJob = new BackupJob(null);
            });
        }

        [TearDown]
        public void Teardown()
        {
            foreach (string file in Directory.GetFiles("Backs"))
            {
                File.Delete(file);
            }
            Directory.Delete("Backs");
            foreach (string file in Directory.GetFiles("FilesToBackup"))
            {
                File.Delete(file);
            }
            Directory.Delete("FilesToBackup");
        }
    }
}