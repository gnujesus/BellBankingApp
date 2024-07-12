using BellBankingApp.Core.Application.DTOs.Account;
using BellBankingApp.Core.Application.ViewModels.Login;
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
        Task<RegisterResponse> RegisterAsync(RegisterViewModel vm);
        Task<UpdateResponse> UpdateAsync(RegisterViewModel vm);
        Task SignOutAsync();
    }
}
