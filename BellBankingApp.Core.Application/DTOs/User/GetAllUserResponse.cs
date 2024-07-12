using BellBankingApp.Core.Application.DTOs.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Application.DTOs.User
{
    public class GetAllUserResponse : BaseResponse
    {
        public List<GetUserResponse> users = new();
    }
}
