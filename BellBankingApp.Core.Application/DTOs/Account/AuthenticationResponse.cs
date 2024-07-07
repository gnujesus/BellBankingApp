using BellBankingApp.Core.Application.DTOs.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Application.DTOs.Account
{
    public class AuthenticationResponse : BaseResponse
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsVerified { get; set; }
        public string Role { get; set; }
    }
}
