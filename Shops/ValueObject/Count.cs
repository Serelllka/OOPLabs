namespace Shops.ValueObject
{
    public class Count
    {
        public Count(uint value)
        {
            Value = value;
        }

        public uint Value { get; }
    }
}