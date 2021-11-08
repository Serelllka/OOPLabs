using Banks.BuisnessLogic.Entities;
using Banks.BuisnessLogic.Tools;

namespace Banks.BuisnessLogic.Builders
{
    public class ClientBuilder
    {
        private string _clientName;
        private string _clientSurname;
        private string _passport;

        public void Reset()
        {
            _clientName = null;
            _clientSurname = null;
            _passport = null;
        }

        public void SetClientName(string name)
        {
            _clientName = name;
        }

        public void SetClientSurname(string surname)
        {
            _clientSurname = surname;
        }

        public void SetPassport(string passport)
        {
            _passport = passport;
        }

        public Client Build()
        {
            if (_clientName == null || _clientSurname == null)
            {
                throw new BanksException("Client must have the name and surname");
            }

            return new Client(_clientName, _clientSurname, _passport);
        }
    }
}