using Shops.Tools;

namespace Shops.Entities
{
    public class Person
    {
        public Person(string name, uint moneyAmount)
        {
            Name = name ?? throw new ShopException("name can't be null");
            Balance = moneyAmount;
        }

        public string Name { get; }
        public uint Balance { get; set; }
    }
}