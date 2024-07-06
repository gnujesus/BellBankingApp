﻿using BellBanking.Core.Application.Interfaces.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace BellBanking.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #region Services
            //services.AddTransient(typeof(IGenericService<,,>) typeof(GenericService<,,>));
            //services.AddTransient(IProductRepository, ProductService());
            //services.AddTransient(IBeneficiaryRepository, BeneficiaryService());
            //services.AddTransient(ITransactionRepository, TransactionService());
            #endregion
        }
    }
}
