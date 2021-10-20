using System;
using System.IO;
using System.Net.Sockets;

namespace BackupsClient.Services
{
    public class Client : IDisposable
    {
        private string _hostName;
        private int _port;
        private TcpClient _tcpClient;
        private StreamWriter _streamWriter;
        
        public Client(string hostName, int port)
        {
            _hostName = hostName;
            _port = port;
            _tcpClient = new TcpClient(_hostName, _port);
            _streamWriter = new StreamWriter(_tcpClient.GetStream());
        }

        public StreamWriter Writer => _streamWriter;
        public TcpClient Tcp => _tcpClient;
        
        public void Dispose()
        {
            _tcpClient?.Dispose();
        }
    }
}