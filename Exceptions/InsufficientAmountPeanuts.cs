using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McNutsAPI.Exceptions
{
    public class InsufficientAmountPeanuts : Exception
    {
        public InsufficientAmountPeanuts(string message) : base(message)
        {

        }
    }
}
