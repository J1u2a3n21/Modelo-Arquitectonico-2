using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace McNutsAPI.Data.Entities
{
    public class PeanutEntity
    {
        [Key]
        [Required]
        public long? Id { get; set; }
        public string Name { get; set; }
        public DateTime? ElaborationDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public long? UnitCost { get; set; }
        public long? WholesalePrice { get; set; }
        public long? Amount { get; set; }
        public bool? ProductionStatus { get; set; }
        public DateTime? DiscontinuationDate { get; set; }
        public DateTime? ProductionStartDate { get; set; }
        public IEnumerable<ClientEntity> Clients { get; set; }
    }
}
