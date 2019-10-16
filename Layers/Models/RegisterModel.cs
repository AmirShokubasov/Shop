using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Layers.Models
{
    public class RegisterModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Compare("Pasword", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }


        [Required]
        public string Login { get; set; }
        
        
    }
}