using Ferreteria.DataAccess.Repositorio.IRepositorio;
using Ferreteria.Models;
using Ferreteria.Models.ViewModels;
using Ferreteria.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ferreteria.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TrabajoController : Controller
    {
        public TrabajoController(IControlador controlador)
        {
            _controlador = controlador;
        }

        readonly IControlador _controlador;

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upsert(int id = 0)
        {
            TrabajoViewModel modelo = new TrabajoViewModel
            {
                Trabajo = new Trabajo(),
                Cliente = _controlador.Cliente.Listar().Select
                (
                    s => new SelectListItem
                    {
                        Text = s.NombreCliente,
                        Value = s.IdCliente.ToString()
                    }
                )
            };

            if (id == 0)
            {
                return View(modelo);
            }
            else
            {
                modelo.Trabajo = _controlador.Trabajo.Buscar(id);
                if (modelo.Trabajo == null)
                {
                    return NotFound();
                }

                return View(modelo);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(TrabajoViewModel modelo)
        {
            if (ModelState.IsValid)
            {
                if (modelo.Trabajo.IdTrabajo != 0)
                {
                    _controlador.Trabajo.Actualizar(modelo.Trabajo);
                }
                else
                {
                    _controlador.Trabajo.Agregar(modelo.Trabajo);
                }
                _controlador.Guardar();
                return Json(new { success = true, message = "Se agregó correctamente." });
            }
            else
            {
                modelo.Cliente = _controlador.Cliente.Listar().Select
                (
                    s => new SelectListItem
                    {
                        Text = s.NombreCliente,
                        Value = s.IdCliente.ToString()
                    }
                );
                if (modelo.Trabajo.IdTrabajo != 0)
                {
                    modelo.Trabajo = _controlador.Trabajo.Buscar(modelo.Trabajo.IdTrabajo);
                }
                return Json(new { success = false, message = "Ocurrió un error guardando el trabajo." });
            }
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return Json(new { data = _controlador.Trabajo.Listar(propiedades: "Cliente") });
        }

        // POST: Banco/Delete/5
        [HttpDelete]
        public IActionResult Borrar(int id)
        {
            var t = _controlador.Trabajo.Buscar(id);
            if (t == null)
            {
                return Json(new { success = false, message = "No se encontró el trabajo." });
            }
            _controlador.Trabajo.Remover(t);
            _controlador.Guardar();
            return Json(new { success = true, message = "El precio ha sido eliminado." });

        }
    }
}
