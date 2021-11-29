using System;

namespace BackupsExtra.Tools
{
    public class BackupsExtraExceptions : Exception
    {
        public BackupsExtraExceptions()
        {
        }

        public BackupsExtraExceptions(string message)
            : base(message)
        {
        }

        public BackupsExtraExceptions(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}