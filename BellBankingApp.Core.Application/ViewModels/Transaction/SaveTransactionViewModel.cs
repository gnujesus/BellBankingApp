using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Application.ViewModels.Transaction
{
    public class SaveTransactionViewModel
    {
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
