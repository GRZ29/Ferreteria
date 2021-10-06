using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Models.ViewModels
{
    public class PrecioViewModel
    {
        public PrecioViewModel()
        {
            Trabajo = new List<SelectListItem>();
        }

        public Precio Precio { get; set; }
        public IEnumerable<SelectListItem> Trabajo { get; set; }
    }
}
