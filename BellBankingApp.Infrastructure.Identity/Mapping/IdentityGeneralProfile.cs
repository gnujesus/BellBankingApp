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
                .ForMember(x => x.Rol, opt => opt.Ignore())
                .ForMember(x => x.Password, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(x => x.NormalizedEmail, opt => opt.Ignore())
                .ForMember(x => x.LockoutEnabled, opt => opt.Ignore())
                .ForMember(x => x.TwoFactorEnabled, opt => opt.Ignore())
                .ForMember(x => x.AccessFailedCount, opt => opt.Ignore())
                .ForMember(x => x.ConcurrencyStamp, opt => opt.Ignore())
                .ForMember(x => x.EmailConfirmed, opt => opt.Ignore())
                .ForMember(x => x.LockoutEnd, opt => opt.Ignore())
                .ForMember(x => x.NormalizedUserName, opt => opt.Ignore())
                .ForMember(x => x.PhoneNumber, opt => opt.Ignore())
                .ForMember(x => x.PhoneNumberConfirmed, opt => opt.Ignore())
                .ForMember(x => x.SecurityStamp, opt => opt.Ignore());

            CreateMap<ApplicationUser, CreateUserRequest>()
                .ReverseMap();

            CreateMap<ApplicationUser, UpdateUserRequest>()
                .ReverseMap();
        }
    }
}
