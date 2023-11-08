using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSuite.Modules.Domain.Exceptions
{
    public abstract class BadResquestException : ApplicationException
    {
        protected BadResquestException(string message)
            : base("Bad Request", message)
        {
        }

    }
}
