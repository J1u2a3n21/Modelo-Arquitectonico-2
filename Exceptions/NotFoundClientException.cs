using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McNutsAPI.Exceptions
{
    public class NotFoundClientException : Exception
    {
        public NotFoundClientException(string message) : base(message)
        {

        }
    }
}
