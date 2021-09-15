namespace Shops.UI.Menu
{
    public interface IMenu
    {
        public void Show();
        public IMenu GenerateNextMenu();

        public void UpdateTable();
    }
}