using BellBankingApp.Core.Application.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Application.ViewModels.User
{
    public class UpdateUserViewModel
    {

        public string? Id { get; set; }
        [Required(ErrorMessage = "Must enter a Firstname")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Must enter a Lastname")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Must enter an Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Must Enter an Username")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Must Enter a Amount")]
        public double Amount { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Must Enter a National ID")]
        [DataType(DataType.Text)]
        public string NationalId { get; set; }
        public bool IsActive { get; set; }
        public Roles Rol { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
