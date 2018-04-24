using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleBlog.Views.Profile
{
    public class PasswordViewModel
    {
        [Required(ErrorMessage ="Wymagane nowe hasło")]
        [StringLength(255, MinimumLength = 4,ErrorMessage ="Minimum 4 znakowe musi być hasło")]
        public string newPaswword { get; set; }
        [Required(ErrorMessage = "Wymagane powtórzene hasła")]
        public string reNewPassword { get; set; }
        [Required(ErrorMessage = "Wymagane stare hasło")]
        public string oldPassword { get; set; }
    }
}