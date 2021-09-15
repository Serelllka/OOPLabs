namespace Shops.Entities
{
    public class ShoppingListItem
    {
        public ShoppingListItem(Product product, uint price, uint count = 0)
        {
            Item = product;
            Price = price;
            Count = count;
        }

        public Product Item { get; private set; }
        public string Name => Item.Name;
        public uint Count { get; set; }
        public uint Price { get; set; }
    }
}