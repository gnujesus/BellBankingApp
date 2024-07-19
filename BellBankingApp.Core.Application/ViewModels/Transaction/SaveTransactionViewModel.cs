
﻿using BellBankingApp.Core.Application.ViewModels.Product;
using BellBankingApp.Core.Application.ViewModels.User;
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
        public DateTime? DateCreated { get; set; } = DateTime.Now;

        public int Id { get; set; }

        [Required(ErrorMessage = "You must enter the amount")]
        public double Amount { get; set; }

        public int OriginProductId { get; set; }

        [Required(ErrorMessage = "You must enter the destination")]
        public int? DestinationProductId { get; set; }

        [Required(ErrorMessage = "You must enter the destination")]
        public string? DestinationAccount { get; set; }

        public string? UserId { get; set; }

        public List<ProductViewModel>? OriginProductList { get; set; } = new();
        public List<ProductViewModel>? DestinationProductList { get; set; } = new();
        public ProductViewModel? OriginProduct { get; set; }
        public ProductViewModel? DestinationProduct { get; set; }
        public UserViewModel? User { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}

