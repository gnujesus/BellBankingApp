using BellBankingApp.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Domain.Entities
{
    public class Product : AuditableBaseEntity
    {
        public string AccountNumber { get; set; }
        public string UserId { get; set; }
        public double Amount { get; set; }
        public bool IsMainAccount { get; set; }
        public string Type { get; set; }


        //Navigation properties
        //public User User { get; set; }
        public List<Beneficiary>? Beneficiaries { get; set; }
    }
}

