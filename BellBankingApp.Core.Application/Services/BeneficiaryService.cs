using AutoMapper;
using BellBankingApp.Core.Application.DTOs.Account;
using BellBankingApp.Core.Application.Enums;
using BellBankingApp.Core.Application.Helpers;
using BellBankingApp.Core.Application.Interfaces.Repositories;
using BellBankingApp.Core.Application.Interfaces.Services;
using BellBankingApp.Core.Application.ViewModels.Beneficiary;
using BellBankingApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Application.Services
{
    public class BeneficiaryService : GenericService<SaveBeneficiaryViewModel, BeneficiaryViewModel, Beneficiary>, IBeneficiaryService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IProductRepository _productRepository;
        private readonly AuthenticationResponse currentUser;
        public BeneficiaryService(IBeneficiaryRepository beneficiaryRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, IProductRepository productRepository)
            : base(beneficiaryRepository, mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _productRepository = productRepository;
            currentUser = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>(Roles.Customer.ToString());
        }

        public override async Task<List<BeneficiaryViewModel>> GetAll()
        {            
            var beneficiariesList = await base.GetAll();
            return beneficiariesList.Where(user => user.UserId == currentUser.Id).ToList();
        }

        public override Task<SaveBeneficiaryViewModel> Create(SaveBeneficiaryViewModel vm)
        {
            //vm.UserId = currentUser.Id;
            vm.BeneficiaryUserId = "Creo que no es Necesario";
            return base.Create(vm);
        }
    }
}
