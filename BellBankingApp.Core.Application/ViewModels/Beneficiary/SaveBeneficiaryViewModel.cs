using BellBankingApp.Core.Application.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Application.ViewModels.Beneficiary
{
    public class SaveBeneficiaryViewModel
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "You must enter the type of the product")]
        public int ProductId { get; set; }

        public List<ProductViewModel>? Products { get; set; }
        public string? UserId { get; set; }
    }
}
