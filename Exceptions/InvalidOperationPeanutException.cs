using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McNutsAPI.Exceptions
{
    public class InvalidOperationPeanutException : Exception
    {
        public InvalidOperationPeanutException(string message) : base(message)
        {

        }
    }
}
