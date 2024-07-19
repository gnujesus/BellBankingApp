using BellBankingApp.Core.Application.Enums;
using BellBankingApp.Core.Application.ViewModels.Product;
using BellBankingApp.Core.Application.ViewModels.User;
using BellBankingApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Application.Interfaces.Services
{
    public interface IProductService : IGenericService<SaveProductViewModel, ProductViewModel, Product>
    {
        Task<List<ProductViewModel>> GetAllbyUserId(string userId);
        Task<bool> ExistAccountNumber(string AccountNumber);
        Task<List<ProductViewModel>> GetProductTypebyUserId(string userId, ProductType productType);
        Task<ProductViewModel> GetProductByAccount(string account);
        Task<ProductViewModel> GetVMById(int id);
    }
}
