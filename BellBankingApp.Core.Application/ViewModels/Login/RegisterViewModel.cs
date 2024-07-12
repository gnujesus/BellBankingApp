using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BellBankingApp.Core.Application.ViewModels.Login
{
    public class RegisterViewModel
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

        [Required(ErrorMessage = "Must enter a Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Both Password must match")]
        [Required(ErrorMessage = "Must enter a Password")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Must Enter a National ID")]
        [DataType(DataType.Text)]
        public string NationalId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsAdmin { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
