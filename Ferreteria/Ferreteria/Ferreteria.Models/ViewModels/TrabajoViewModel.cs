using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ferreteria.Models.ViewModels
{
    public class TrabajoViewModel
    {
        public TrabajoViewModel()
        {
            Cliente = new List<SelectListItem>();
        }

        public Trabajo Trabajo { get; set; }
        public IEnumerable<SelectListItem> Cliente { get; set; }
    }
}
