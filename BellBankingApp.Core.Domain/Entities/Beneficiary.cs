using BellBankingApp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Domain.Entities
{
    public class Beneficiary : AuditableBaseEntity
    {
        public int ProductId { get; set; }
        public string UserId { get; set; }
        public string BeneficiaryUserId { get; set; }

        //Navigation properties
        public Product? Product { get; set; }
        //public User User { get; set; }
    }
}
