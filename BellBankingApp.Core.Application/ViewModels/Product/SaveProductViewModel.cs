
﻿using System;
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

        //[Required(ErrorMessage = "You must enter the account number")]
        //public string AccountNumber { get; set; }

        [Required(ErrorMessage = "You must enter the product type")]
        public string Type { get; set; }
        public string? AccountNumber { get; set; }
        public string? UserId { get; set; }
        public double? Amount { get; set; }
        public double? AmountLimit { get; set; }
        public bool IsMainAccount { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
