using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Application.ViewModels.User
{
    public class UserViewModel
    {
        public string? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string NationalId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
