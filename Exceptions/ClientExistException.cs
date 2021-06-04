using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MCNutsAPI.Exceptions
{
    public class ClientExistException : Exception
    {
        public ClientExistException(string message) : base(message)
        {

        }
    }
}

