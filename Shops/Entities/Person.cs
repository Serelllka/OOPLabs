namespace Shops.Entities
{
    public class Person
    {
        Person(string name, uint moneyAmount)
        {
            Name = name;
            Balance = moneyAmount;
        }

        public string Name { get; }

        public uint Balance { get; set; }
    }
}