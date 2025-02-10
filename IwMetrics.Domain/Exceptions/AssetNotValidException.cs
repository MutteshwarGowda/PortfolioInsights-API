using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IwMetrics.Domain.Exceptions
{
    public class AssetNotValidException : NotValidException
    {
        internal AssetNotValidException() { }

        internal AssetNotValidException(string message) : base(message) { }

        internal AssetNotValidException(string message, Exception innerException) : base(message, innerException) { }
    }
}
