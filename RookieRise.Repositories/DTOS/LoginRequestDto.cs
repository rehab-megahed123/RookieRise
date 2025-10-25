using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RookieRise.Data.Attributes;

namespace RookieRise.Services.DTOS
{
    public class LoginRequestDto
    {
        [Required]
        [CustomEmailAddress(ErrorMessage = "The email is not in a valid format.")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
