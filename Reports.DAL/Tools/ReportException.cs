using System;

namespace Reports.DAL.Tools
{
    public class ReportException : Exception
    {
        public ReportException()
        {
        }

        public ReportException(string message)
            : base(message)
        {
        }

        public ReportException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}