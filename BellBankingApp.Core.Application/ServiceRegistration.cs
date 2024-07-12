﻿using BellBankingApp.Core.Application.Interfaces.Repositories;
using BellBankingApp.Core.Application.Interfaces.Services;
using BellBankingApp.Core.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace BellBankingApp.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #region Services
            services.AddTransient(typeof(IGenericService<,,>), typeof(GenericService<,,>));
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IBeneficiaryService, BeneficiaryService>();
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<ILoginService, LoginService>();
            #endregion
        }
    }
}
