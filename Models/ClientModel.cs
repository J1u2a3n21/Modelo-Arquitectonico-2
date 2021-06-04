using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace McNutsAPI.Models
{
    public class ClientModel
    {
        public long? Id { get; set; }
        [Required]
        public long Ci { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int? Celular { get; set; }
        public string LugarCompra { get; set; }
        public DateTime? FechaCompra { get; set; }
        public long PeanutId { get; set; }
        public long? CantidadCompra { get; set; }
    }
}
