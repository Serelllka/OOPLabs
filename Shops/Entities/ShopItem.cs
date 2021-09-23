namespace Shops.Entities
{
    public class ShopItem
    {
        public ShopItem(uint price, uint count, Product product)
        {
            Price = price;
            Count = count;
            Item = product;
        }

        public Product Item { get; private set; }
        public uint Price { get; set; }
        public uint Count { get; set; }
    }
}