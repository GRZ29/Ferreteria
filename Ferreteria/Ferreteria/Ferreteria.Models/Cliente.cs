using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Models
{
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }

        [DisplayName("Digite el nombre")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        public string NombreCliente { get; set; }

        /*HACE MAPPED DE NOMBRE Y APELLIDO*/
        [DisplayName("Digite el apellido")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        public string ApellidosCliente { get; set; }

        [DisplayName("Digite el número de teléfono")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        public string TelefonoCliente { get; set; }

        [DisplayName("Digite el correo electrónico")]
        [Required(ErrorMessage = "Este campo es requerido.")]
        [MaxLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        public string CorreoCliente { get; set; }
    }
}
