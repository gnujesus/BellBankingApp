using BellBanking.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBanking.Core.Domain.Entities
{
    public class Transaction : AuditableBaseEntity
    {
        public decimal Amount { get; set; }
        public string OriginAccount { get; set; }
        public string DestinationAccount { get; set; }


        // Navigation properties
        public Product? OriginProduct { get; set; }
        public Product? DestinationProduct { get; set; }
    }
}
