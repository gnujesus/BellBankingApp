using BellBanking.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBanking.Core.Domain.Entities
{
    public class Product : AuditableBaseEntity
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Type { get; set; }


        //Navigation properties
        //public User User { get; set; }
        public List<Beneficiary>? Beneficiaries { get; set; }
    }
}

