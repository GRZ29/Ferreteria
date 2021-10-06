using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Models
{
    public class UserLogin
    {
        [Key]
        public int id { get; set; }

        public string Usuario { get; set; }

        public string Contra{ get; set; }
    }
}
