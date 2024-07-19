using AutoMapper;
using BellBankingApp.Core.Application.Interfaces.Repositories;
using BellBankingApp.Core.Application.Interfaces.Services;
using BellBankingApp.Core.Application.ViewModels.Product;
using BellBankingApp.Core.Application.ViewModels.Transaction;
using BellBankingApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Application.Services
{
    public class TransactionService : GenericService<SaveTransactionViewModel, TransactionViewModel, Transaction>, ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserManagerService _userManagerService;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper, IProductRepository productRepository, IUserManagerService userManagerService)
            : base(transactionRepository, mapper)
        {
            _transactionRepository = transactionRepository;
            _productRepository = productRepository;
            _userManagerService = userManagerService;
        }

        public async Task<SaveTransactionViewModel> CreditCardPayment(SaveTransactionViewModel transaction)
        {
            SaveTransactionViewModel response = new()
            {
                HasError = false
            };

            if (transaction.Amount >= transaction.DestinationProduct.AmountLimit)
            {
                transaction.Amount = transaction.Amount - (transaction.Amount - (double)transaction.DestinationProduct.AmountLimit);
            }

            return response;

        }

        public async Task<SaveTransactionViewModel> ValidateExpressPayment(SaveTransactionViewModel transaction)
        {
            SaveTransactionViewModel response = new()
            {
                HasError = false
            };

            if (transaction.DestinationProduct == null)
            {
                response.HasError = true;
                response.Error = $"The account {transaction.DestinationAccount} does not Exist";
                return response;
            }

            if (transaction.Amount >= transaction.OriginProduct.Amount) 
            {
                response.HasError = true;
                response.Error = $"You account {transaction.OriginProduct.AccountNumber} do not have enough balance";
                return response;
            }

            return response;
        }

        public async Task<SaveTransactionViewModel> LoanPayment(SaveTransactionViewModel transaction)
        {
            SaveTransactionViewModel response = new()
            {
                HasError = false
            };

            if (transaction.Amount >= transaction.DestinationProduct.AmountLimit)
            {
                transaction.Amount = transaction.Amount - (transaction.Amount - (double)transaction.DestinationProduct.AmountLimit);
            }

            return response;
        }

        public Task<SaveTransactionViewModel> ExpressPayment(SaveTransactionViewModel transaction)
        {
            throw new NotImplementedException();
        }

    }
}
