using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bilbayt.Web.API.Dto.Request
{
    public class UserDto
    {
        [Required]
        [MaxLength(50)]
        [MinLength(5)]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,100}$",
            ErrorMessage = "Invalid Password. Password of the user must contain aleast 8 characters , 1 CAP, 1 special, 1 Number")]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        [Required]
        [MaxLength(500)]
        [MinLength(5)]
        public string FullName { get; set; }
    }
}
