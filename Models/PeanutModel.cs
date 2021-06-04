using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace McNutsAPI.Models
{
    public class PeanutModel
    {
        public long? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? ElaborationDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? ExpirationDate { get; set; }
        [Required]
        public long? UnitCost { get; set; }
        public long? WholesalePrice { get; set; }
        public long? Amount { get; set; }
        public bool? ProductionStatus { get; set; }
        public DateTime? DiscontinuationDate { get; set; }
        public DateTime? ProductionStartDate { get; set; }
    }
}
