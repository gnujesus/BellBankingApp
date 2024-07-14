using BellBankingApp.Core.Application.ViewModels.Beneficiary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Application.ViewModels.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string? AccountNumber { get; set; }
        public string? UserId { get; set; }
        public string? Type { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }

        public List<BeneficiaryViewModel>? Beneficiaries { get; set; }
    }
}
