using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace McNutsAPI.Data.Entities
{
    public class ClientEntity
    {
        [Key]
        [Required]
        public long? Id { get; set; }
        public long Ci { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int? Celular { get; set; }
        public string LugarCompra { get; set; }
        public DateTime? FechaCompra { get; set; }
        [ForeignKey("PeanutId")]
        public virtual PeanutEntity Peanut { get; set; }
        public long? CantidadCompra { get; set; }
    }
}
