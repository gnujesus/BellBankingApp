using AutoMapper;
using BellBankingApp.Core.Application.DTOs.Account;
using BellBankingApp.Core.Application.DTOs.User;
using BellBankingApp.Core.Application.ViewModels.Login;
using BellBankingApp.Infrastructure.Identity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Infrastructure.Identity.Mapping
{
    public class IdentityGeneralProfile : Profile
    {
        public IdentityGeneralProfile()
        {
            CreateMap<ApplicationUser, GetUserResponse>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ApplicationUser, CreateUserRequest>()
                .ReverseMap();

            CreateMap<ApplicationUser, UpdateUserRequest>()
                .ReverseMap();
        }
    }
}
