using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace McNutsAPI.Models
{
    public class PeanutWithClientModel:PeanutModel
    {
        public IEnumerable<ClientModel> Clients { get; set; }
    }
}
