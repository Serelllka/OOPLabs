using System;
using System.IO;
using System.Net.Sockets;

namespace BackupsClient.Entities
{
    public class Data
    {
        private int _length;
        private string _filePath;
        
        public Data(string filePath)
        {
            byte[] bytes = File.ReadAllBytes(filePath);
            _length = bytes.Length;
            _filePath = filePath;
        }

        public void WriteOnStream(StreamWriter streamWriter, TcpClient tcpClient)
        {
            streamWriter.WriteLine(_length.ToString());
            //streamWriter.Flush();
            streamWriter.WriteLine(_filePath);
            //streamWriter.Flush();
            streamWriter.Write(File.ReadAllBytes(_filePath));
            //streamWriter.Flush();
        }
    }
}