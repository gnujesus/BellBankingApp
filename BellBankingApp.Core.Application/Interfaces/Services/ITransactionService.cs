using BellBankingApp.Core.Application.ViewModels.Transaction;
using BellBankingApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Application.Interfaces.Services
{
    public interface ITransactionService : IGenericService<SaveTransactionViewModel, TransactionViewModel, Transaction>
    {
        Task<SaveTransactionViewModel> ExpressPayment(SaveTransactionViewModel transaction);
        Task<SaveTransactionViewModel> CreditCardPayment(SaveTransactionViewModel transaction);
        Task<SaveTransactionViewModel> LoanPayment(SaveTransactionViewModel transaction);
        Task<SaveTransactionViewModel> ValidateExpressPayment(SaveTransactionViewModel transaction);

    }
}
