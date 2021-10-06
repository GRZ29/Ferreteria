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
    public class Trabajo
    {
        [Key]
        public int IdTrabajo { get; set; }

        //primera foranea
        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Cliente")]
        public int IdCliente { get; set; }

        [ForeignKey("IdCliente")]
        public Cliente Cliente { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        public string NombreTrabajo { get; set; }

    }
}
