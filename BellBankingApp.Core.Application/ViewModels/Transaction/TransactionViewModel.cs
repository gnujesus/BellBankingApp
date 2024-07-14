
﻿using BellBankingApp.Core.Application.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Application.ViewModels.Transaction
{
    public class TransactionViewModel
    {
        public DateTime? DateCreated { get; set; }
        public decimal Amount { get; set; }
        public int OriginProductId { get; set; }
        public int DestinationProductId { get; set; }
        public string? UserId { get; set; }

        // Navigation properties
        public ProductViewModel? OriginProduct { get; set; }
        public ProductViewModel? DestinationProduct { get; set; }
    }
}

