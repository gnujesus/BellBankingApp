using BellBankingApp.Core.Application.DTOs.Account;
using BellBankingApp.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Application.Interfaces.Services
{
    public interface ILoginService
    {
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<RegisterResponse> RegisterAsync(SaveUserViewModel vm);
        Task<UpdateResponse> UpdateAsync(SaveUserViewModel vm);
        Task SignOutAsync();
    }
}
