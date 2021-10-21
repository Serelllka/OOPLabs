using System;  
using System.Collections.Generic;
using System.ComponentModel;
using System.Net;  
using System.Net.Sockets;  
using System.IO;  
using System.Text;
using System.Text.Json;
using BackupsClient.ValueObject;

namespace BackupsServer
{  
    class Program  
    {
        static void Main(string[] args)
        {
            const string path = @"C:\Users\vprog\RiderProjects\Serelllka\BackupsServer\Backups\";
            var tcpListener = new TcpListener(IPAddress.Any, 1234);
            tcpListener.Start();

            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();

                byte[] bytes = new byte[sizeof(int)];
                tcpClient.GetStream().Read(bytes, 0, 4);
                
                int fileSize = BitConverter.ToInt32(bytes, 0);
                byte[] buffer = new byte[fileSize];
                
                int bytesReceived = 0;
                int bytesRead = 0;
                int maxBlockSize = 1024;
                
                while (bytesReceived < fileSize)  
                {  
                    int bytesRemaining = fileSize - bytesReceived;  
                    if (bytesRemaining < maxBlockSize)  
                    {  
                        maxBlockSize = bytesRemaining;  
                    }  
 
                    bytesRead = tcpClient.GetStream().Read(buffer, bytesReceived, maxBlockSize);  
                    bytesReceived += bytesRead;  
                }

                string json = Encoding.UTF8.GetString(buffer);
                Console.WriteLine(json);
                FileContent fileContent = JsonSerializer.Deserialize<FileContent>(buffer);
                fileContent.CreateFile(path);
            }
        }
    }  
} 