using BellBankingApp.Core.Application.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Application.ViewModels.Beneficiary
{
    public class BeneficiaryViewModel
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string? UserId { get; set; }

        public ProductViewModel? Products { get; set; }
    }
}

