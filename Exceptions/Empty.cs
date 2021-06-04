using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McNutsAPI.Exceptions
{
    public class Empty : Exception
    {
        public Empty(string message) : base(message)
        {

        }
    }
}
