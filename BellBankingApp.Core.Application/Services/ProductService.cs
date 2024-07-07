using AutoMapper;
using BellBankingApp.Core.Application.Interfaces.Repositories;
using BellBankingApp.Core.Application.Interfaces.Services;
using BellBankingApp.Core.Application.ViewModels.Product;
using BellBankingApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Application.Services
{
    public class ProductService : GenericService<SaveProductViewModel, ProducViewModel, Product>, IProductService
    {
        public ProductService(IProductRepository productRepository, IMapper mapper)
            : base(productRepository, mapper)
        {

        }
    }
}
