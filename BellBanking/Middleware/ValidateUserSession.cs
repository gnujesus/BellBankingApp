using BellBankingApp.Core.Application.DTOs.Account;
using BellBankingApp.Core.Application.Enums;
using BellBankingApp.Core.Application.Helpers;
using Microsoft.AspNetCore.Http;

namespace BellBanking.Middleware
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasAdmin()
        {
            AuthenticationResponse response = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>(Roles.Admin.ToString());

            if (response == null)
            {
                return false;
            }
            return true;
        }

        public bool HasCustomer()
        {
            AuthenticationResponse response = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>(Roles.Customer.ToString());

            if (response == null)
            {
                return false;
            }
            return true;
        }

        public bool HasSession()
        {

            if (!this.HasAdmin() && !this.HasCustomer())
            {
                return false;
            }
            return true;
        }
    }
}
