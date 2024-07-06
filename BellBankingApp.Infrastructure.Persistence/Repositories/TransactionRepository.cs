using BellBanking.Core.Application.Interfaces.Repositories;
using BellBanking.Core.Domain.Entities;
using BellBanking.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBanking.Infrastructure.Persistence.Repositories
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
