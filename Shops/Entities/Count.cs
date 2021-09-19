using System.Runtime.InteropServices;

namespace Shops.Entities
{
    public class Count
    {
        public Count(uint value)
        {
            Value = value;
        }

        public uint Value { get; private set; }
    }
}