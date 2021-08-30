using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Models
{
    public class Login : IdentityUser
    {
        [Required]
        public string Nombre { get; set; }

        [NotMapped]
        public string Role { get; set; }
    }
}
