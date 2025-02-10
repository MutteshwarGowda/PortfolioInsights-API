using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwMetrics.Domain.Exceptions
{
    public class PortfolioNotValidException : NotValidException
    {
        internal PortfolioNotValidException() { }

        internal PortfolioNotValidException(string message) : base(message) { }

        internal PortfolioNotValidException(string message,  Exception innerException) : base(message, innerException) { }
    }
}
