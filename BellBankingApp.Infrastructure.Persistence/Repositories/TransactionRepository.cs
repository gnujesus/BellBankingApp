using BellBankingApp.Core.Application.Interfaces.Repositories;
using BellBankingApp.Core.Domain.Entities;
using BellBankingApp.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Infrastructure.Persistence.Repositories
{
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {

        private readonly ApplicationContext _dbContext;

        public TransactionRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
