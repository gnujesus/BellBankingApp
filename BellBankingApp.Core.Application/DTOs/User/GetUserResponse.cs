﻿using BellBankingApp.Core.Application.DTOs.Commons;
using BellBankingApp.Core.Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Application.DTOs.User
{
    public class GetUserResponse : BaseResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string LastName { get; set; }
        public string NationalId { get; set; }
        public bool IsActive { get; set; }
        public string Rol { get; set; }

    }
}
