namespace Shops.Entities
{
    public class Person
    {
        public Person(string name, uint moneyAmount)
        {
            Name = name;
            Balance = moneyAmount;
        }

        public string Name { get; }
        public uint Balance { get; set; }
    }
}