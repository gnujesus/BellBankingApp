using BellBankingApp.Core.Application.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Application.ViewModels.Transaction
{
    public class SaveTransactionViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must enter the amount")]
        public decimal Amount { get; set; }

        public int OriginProductId { get; set; }

        [Required(ErrorMessage = "You must enter the destination")]
        public int DestinationProductId { get; set; }

        public List<ProductViewModel>? OriginProduct { get; set; }
        public List<ProductViewModel>? DestinationProduct { get; set; }
    }
}
