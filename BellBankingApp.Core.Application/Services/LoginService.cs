using AutoMapper;
using BellBankingApp.Core.Application.DTOs.Account;
using BellBankingApp.Core.Application.Interfaces.Services;
using BellBankingApp.Core.Application.ViewModels.Login;
using BellBankingApp.Core.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Application.Services
{
    public class LoginService : ILoginService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public LoginService(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }
        public async Task<AuthenticationResponse> LoginAsync(LoginViewModel vm)
        {
            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse userResponse = await _accountService.AuthenticateAsync(loginRequest);
            return userResponse;
        }

        public async Task<RegisterResponse> RegisterAsync(RegisterViewModel vm)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            return await _accountService.RegisterUserAsync(registerRequest);
        }

        public async Task SignOutAsync()
        {
            await _accountService.SignOutAsync();
        }

        public async Task<UpdateResponse> UpdateAsync(RegisterViewModel vm)
        {
            UpdateRequest updateRequest = _mapper.Map<UpdateRequest>(vm);
            return await _accountService.UpdateUserAsync(updateRequest);
        }
    }
}
