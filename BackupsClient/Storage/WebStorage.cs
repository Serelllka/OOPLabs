using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Backups.Storage;
using BackupsClient.Services;
using BackupsClient.ValueObject;

namespace BackupsClient.Storage
{
    public class WebStorage : IStorage
    {
        private TcpClient _client;
        public WebStorage(TcpClient client)
        {
            _client = client;
        }
        
        public void SaveFromStream(string archivePath, Stream stream)
        {
            var fileContent = new FileContent(archivePath);
            var ms = new MemoryStream();
            stream.Position = 0;
            stream.CopyTo(ms);
            fileContent.Data = ms.ToArray();
            
            string json = JsonSerializer.Serialize(fileContent);
            byte[] data = Encoding.UTF8.GetBytes(json);
            _client.GetStream().Write(BitConverter.GetBytes(data.Length));
            _client.GetStream().Write(data);
        }
    }
}