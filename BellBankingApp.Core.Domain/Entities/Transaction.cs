using BellBankingApp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Domain.Entities
{
    public class Transaction : AuditableBaseEntity
    {
        public decimal Amount { get; set; }
        public int OriginProductId { get; set; }
        public int DestinationProductId { get; set; }


        // Navigation properties
        public Product? OriginProduct { get; set; }
        public Product? DestinationProduct { get; set; }
    }
}
