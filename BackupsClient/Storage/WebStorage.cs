using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using Backups.Storage;
using Backups.ValueObject;

namespace BackupsClient.Storage
{
    public class WebStorage : IStorage
    {
        private TcpClient _client;
        public WebStorage(TcpClient client)
        {
            _client = client;
        }

        public void SaveFromByteArray(string archivePath, byte[] bytes)
        {
            var fileContent = new FileContent(
                Path.GetFileName(archivePath),
                Path.GetFileName(Path.GetDirectoryName(archivePath)),
                bytes);

            string json = JsonSerializer.Serialize(fileContent);
            byte[] data = Encoding.UTF8.GetBytes(json);
            _client.GetStream().Write(BitConverter.GetBytes(data.Length));
            _client.GetStream().Write(data);
        }

        public string GetFolderPath()
        {
            return "";
        }
    }
}