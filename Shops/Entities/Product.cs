using Shops.Tools;

namespace Shops.Entities
{
    public class Product
    {
        public Product(string name)
        {
            Count = 0;
            Name = name;
        }

        public string Name { get; set; }

        public int Count { get; private set; }

        public void AddProduct(int delta)
        {
            Count += delta;
        }

        public void ReserveProduct(int delta)
        {
            if (delta > Count)
            {
                throw new ShopException("Invalid value of count");
            }

            Count -= delta;
        }
    }
}