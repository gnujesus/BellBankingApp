using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Application.ViewModels.Product
{
    public class SaveProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must enter the account number")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "You must enter the product type")]
        public string Type { get; set; }

        public string? UserId { get; set; }
    }
}
