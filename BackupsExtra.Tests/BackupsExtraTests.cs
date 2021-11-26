using System.IO;
using System.Linq;
using Backups.Archiver;
using Backups.Entities;
using Backups.FileSaver;
using Backups.Models;
using Backups.Storage;
using Backups.Tools;
using BackupsExtra.Configuration;
using BackupsExtra.Services;
using BackupsExtra.Tools;
using Newtonsoft.Json;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class BackupsExtraTests
    {
        private ZipArchiver _archiver;
        private FileJsonStateService _stateService;

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
            _stateService = new FileJsonStateService();
            _stateService.SetFilename("config.cfg");
            _stateService.SetJsonSettings(new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All,
                ContractResolver = new PrivateContractResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
            });
        }

        [Test]
        public void CreateBackupJobCreateJobCreateRestorePoint_CheckFilesForExisting()
        {
            const int restorePointCounter = 2;
            var storage = new LocalStorage(@"Backs");
            var fileSaver = new SplitFileSaver();
            
            var backupJob = new BackupJob(
                _archiver,
                new RestorePointDeleter(restorePointCounter));
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
        public void CreateBackupJobSaveItThenLoad_BackupJobLoaded()
        {
            const int restorePointCounter = 2;
            var storage = new LocalStorage(@"Backs");
            var fileSaver = new SplitFileSaver();
            
            var backupJob = new BackupJob(
                _archiver, 
                new RestorePointDeleter(restorePointCounter));
            var backupObject1 = new JobObject(Path.Combine("FilesToBackup","test1.txt"));
            var backupObject2 = new JobObject(Path.Combine("FilesToBackup","test2.txt"));
            backupJob.AddJobObject(backupObject1);
            backupJob.AddJobObject(backupObject2);
            backupJob.CreateRestorePoint("RestorePoint1", fileSaver, storage);
            
            _stateService.Save(backupJob);
            BackupJob backupJob1 = _stateService.Load();

            Assert.AreEqual(backupJob1.JobObjects.Count, backupJob.JobObjects.Count);
            Assert.AreEqual(backupJob1.RestorePoints.Count, backupJob.RestorePoints.Count);
        }

        [Test]
        public void CreateRestorePointsMoreThenLimit_RestorePointsCountsEqualsLimit()
        {
            const int restorePointCounter = 2;
            var storage = new LocalStorage(@"Backs");
            var fileSaver = new SplitFileSaver();
            
            var backupJob = new BackupJob(
                _archiver,
                new RestorePointDeleter(2));
            var backupObject1 = new JobObject(Path.Combine("FilesToBackup","test1.txt"));
            var backupObject2 = new JobObject(Path.Combine("FilesToBackup","test2.txt"));
            backupJob.AddJobObject(backupObject1);
            backupJob.AddJobObject(backupObject2);
            backupJob.CreateRestorePoint("point1", fileSaver, storage);
            backupJob.CreateRestorePoint("point2", fileSaver, storage);
            backupJob.CreateRestorePoint("point3", fileSaver, storage);
            Assert.AreEqual(backupJob.RestorePoints.Count, 2);
        }
        
        [Test]
        public void CreateRestorePointsMoreThenLimit_RestorePointsCountsEqualsLimit_PointsMerged()
        {
            const int restorePointCounter = 2;
            var storage = new LocalStorage(@"Backs");
            var fileSaver = new SplitFileSaver();
            
            var backupJob = new BackupJob(
                _archiver,
                new RestorePointMerger(
                    2,
                    "Backs",
                    new SplitStorageMerge()));
            var backupObject1 = new JobObject(Path.Combine("FilesToBackup","test1.txt"));
            var backupObject2 = new JobObject(Path.Combine("FilesToBackup","test2.txt"));
            backupJob.AddJobObject(backupObject1);
            backupJob.AddJobObject(backupObject2);
            
            backupJob.CreateRestorePoint(Path.Combine("point1", "1file"), fileSaver, storage);
            backupJob.CreateRestorePoint(Path.Combine("point2", "2file"), fileSaver, storage);
            backupJob.CreateRestorePoint(Path.Combine("point3", "3file"), fileSaver, storage);
            
            Assert.AreEqual(backupJob.RestorePoints.Count, 2);
        }

        [TearDown]
        public void TearDown()
        {
            Directory.Delete("Backs", true);
            Directory.Delete("FilesToBackup", true);
        }
    }
}