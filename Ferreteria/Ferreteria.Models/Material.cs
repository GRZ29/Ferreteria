using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Models
{
    public class Material
    {
        [Key]
        public int IdMaterial { get; set; }

        [DisplayName("Digite el Material")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        public string NombreMaterial { get; set; }

        [Required]
        public double CostoMaterial { get; set; }
    }
}
