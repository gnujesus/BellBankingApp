using AutoMapper;
using BellBankingApp.Core.Application.DTOs.Account;
using BellBankingApp.Core.Application.Enums;
using BellBankingApp.Core.Application.Helpers;
using BellBankingApp.Core.Application.Interfaces.Repositories;
using BellBankingApp.Core.Application.Interfaces.Services;
using BellBankingApp.Core.Application.ViewModels.Product;
using BellBankingApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Application.Services
{
    public class ProductService : GenericService<SaveProductViewModel, ProductViewModel, Product>, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse currentUser;
        public ProductService(IProductRepository productRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(productRepository, mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            currentUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>(Roles.Customer.ToString());

        }

        public override Task<SaveProductViewModel> Create(SaveProductViewModel vm)
        {
            do
            {
                vm.AccountNumber = AccountNumberHelper.Generate();
            } while (_productRepository.Exist(p => p.AccountNumber == vm.AccountNumber)); 
            return base.Create(vm);
        }

        public async Task<bool> ExistAccountNumber(string AccountNumber)
        {
            return _productRepository.Exist(x => x.AccountNumber == AccountNumber);
        }

        public async Task<List<ProductViewModel>> GetAllbyUserId(string userId)
        {
            var entityList = await _productRepository.GetAllAsync();
            entityList = entityList.Where(x => x.UserId == userId).ToList();
            return _mapper.Map<List<ProductViewModel>>(entityList);
        }
    }
}
