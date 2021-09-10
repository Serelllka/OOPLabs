namespace Shops.Entities
{
    public class ShopItem
    {
        public ShopItem(uint price, uint count)
        {
            Price = price;
            Count = count;
        }

        public uint Price { get; set; }

        public uint Count { get; set; }
    }
}