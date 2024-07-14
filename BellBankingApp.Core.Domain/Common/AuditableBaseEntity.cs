using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Domain.Common
{
    public class AuditableBaseEntity
    {
        public virtual int Id { get; set; }
        public DateTime? DateCreated { get; set; } 
        public string? CreatedBy { get; set; }
        public DateTime? DateUpdated { get; set; } 
        public string? UpdatedBy { get; set; }
    }
}
