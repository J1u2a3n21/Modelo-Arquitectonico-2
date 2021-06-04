using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McNutsAPI.Exceptions
{
    public class InvalidOperationClientException : Exception
    {
        public InvalidOperationClientException(string message) : base(message)
        {

        }
    }
}
