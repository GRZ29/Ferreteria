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
    public class Precio
    {
        [Key]
        public int IdPrecio { get; set; }

        //primera foranea
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Trabajo")]
        public int IdTrabajo { get; set; }

        [ForeignKey("IdTrabajo")]
        public Trabajo Trabajo { get; set; }

        [Required]
        public double Costo { get; set; }

    }
}
