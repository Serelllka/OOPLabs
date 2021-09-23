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
        public uint Balance { get; private set; }

        public void ReduceMoney(uint delta)
        {
            if (delta > Balance)
            {
                throw new ShopException("not enough money");
            }

            Balance -= delta;
        }
    }
}