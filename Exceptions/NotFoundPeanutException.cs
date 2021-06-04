using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McNutsAPI.Exceptions
{
    public class NotFoundPeanutException : Exception
    {
        public NotFoundPeanutException(string message) : base(message)
        {

        }
    }
}
