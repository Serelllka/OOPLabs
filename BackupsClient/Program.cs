﻿using System;  
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;  
using System.Net.Sockets;  
using System.IO;  
using System.Text;
using System.Text.Json;
using Backups.Archiver;
using Backups.Entities;
using Backups.FileSaver;
using Backups.Models;
using BackupsClient.Storage;
using BackupsExtra.Logger;
using BackupsExtra.PointFilter;
using BackupsExtra.Services;
using FileInfo = System.IO.FileInfo;

namespace BackupsClient
{  
    class Program  
    {
        static void Main(string[] args)
        {
            const int restorePointCounter = 2;
            const string srcPath = @"C:\Users\vprog\RiderProjects\Serelllka\BackupsClient\Src\";
            var storage = new WebStorage(new TcpClient("127.0.0.1", 1234));
            var restorePointDeleter = new RestorePointDeleter(new FilterByCount(restorePointCounter));
            var fileSaver = new SingleFileSaver();
            var archiver = new ZipArchiver();
            var logger = new FileLogger("logger.log");    
            
            var backupJob = new BackupJob(
                archiver,
                fileSaver,
                storage,
                restorePointDeleter,
                logger);
            var backupObject1 = new JobObject(srcPath + @"FilesToBackup\lol.txt");
            var backupObject2 = new JobObject(srcPath + @"FilesToBackup\kek.txt");
            backupJob.AddJobObject(backupObject1);
            backupJob.AddJobObject(backupObject2);
            
            backupJob.CreateRestorePoint("RestorePoint1.zip");
            
            var backupObject3 = new JobObject(srcPath + @"FilesToBackup\cheburek.txt");
            backupJob.AddJobObject(backupObject3);
            backupJob.CreateRestorePoint("RestorePoint2.zip");
            
            backupJob.RemoveJobObject(backupObject3);
            backupJob.CreateRestorePoint("RestorePoint3.zip");
            
        }  
    }  
}