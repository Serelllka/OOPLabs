namespace Shops.Entities
{
    public class ShoppingListItem
    {
        private Shop _shop;
        public ShoppingListItem(Shop shop, Product product, uint price, uint count = 0)
        {
            _shop = shop;
            Item = product;
            Price = price;
            Count = count;
        }

        public Product Item { get; private set; }
        public string Name => Item.Name;
        public uint Count { get; set; }
        public uint Price { get; set; }

        public void BuyThisItem(Person person)
        {
            _shop.Buy(person, Item, Count);
            Count = 0;
        }
    }
}
