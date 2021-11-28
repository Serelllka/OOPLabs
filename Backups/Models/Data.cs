using System.IO;

namespace Backups.Models
{
    public class Data
    {
        public Data(string name, Stream stream)
        {
            Stream = stream;
            Name = name;
        }

        public Stream Stream { get; }
        public string Name { get; }
    }
}