using Ferreteria.DataAccess.Repositorio.IRepositorio;
using Ferreteria.Models;
using Ferreteria.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Ferreteria.Utility;

namespace Ferreteria.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PrecioController : Controller
    {
        public PrecioController(IControlador controlador)
        {
            _controlador = controlador;
        }

        readonly IControlador _controlador;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Generated()
        {
            return View();
        }

        public IActionResult cart()
        {
            return View();

        }

        public IActionResult pdf()
        {
            return new ViewAsPdf("cart")
            {
            };
        }

        [HttpGet]
        public IActionResult Upsert(int id = 0)
        {
            PrecioViewModel modelo = new PrecioViewModel
            {
                Precio = new Precio(),
                Trabajo = _controlador.Trabajo.Listar().Select
                (
                    s => new SelectListItem
                    {
                        Text = s.NombreTrabajo,
                        Value = s.IdTrabajo.ToString()
                    }
                )
            };

            if (id == 0)
            {
                return View(modelo);
            }
            else
            {
                modelo.Precio = _controlador.Precio.Buscar(id);
                if (modelo.Precio == null)
                {
                    return NotFound();
                }

                return View(modelo);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(PrecioViewModel modeloo)
        {
            if (ModelState.IsValid)
            {
                if (modeloo.Precio.IdPrecio != 0)
                {
                    _controlador.Precio.Actualizar(modeloo.Precio);
                }
                else
                {
                    _controlador.Precio.Agregar(modeloo.Precio);
                }
                _controlador.Guardar();
                return Json(new { success = true, message = "Se agregó correctamente." });
            }
            else
            {
                modeloo.Trabajo = _controlador.Trabajo.Listar().Select
                (
                    s => new SelectListItem
                    {
                        Text = s.NombreTrabajo,
                        Value = s.IdTrabajo.ToString()
                    }
                );
                if (modeloo.Precio.IdPrecio != 0)
                {
                    modeloo.Precio = _controlador.Precio.Buscar(modeloo.Precio.IdPrecio);
                }
                return Json(new { success = false, message = "Ocurrió un error guardando el precio." });
            }
        }
        //-------------
        [HttpGet]
        public IActionResult Upsert2(int id = 0)
        {
            PrecioViewModel modelo = new PrecioViewModel
            {
                Precio = new Precio(),
                Trabajo = _controlador.Trabajo.Listar().Select
                (
                    s => new SelectListItem
                    {
                        Text = s.NombreTrabajo,
                        Value = s.IdTrabajo.ToString()
                    }
                )
            };

            if (id == 0)
            {
                return View(modelo);
            }
            else
            {
                modelo.Precio = _controlador.Precio.Buscar(id);
                if (modelo.Precio == null)
                {
                    return NotFound();
                }

                return View(modelo);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert2(PrecioViewModel modeloo)
        {
            if (ModelState.IsValid)
            {
                if (modeloo.Precio.IdPrecio != 0)
                {
                    _controlador.Precio.Actualizar(modeloo.Precio);
                }
                else
                {
                    _controlador.Precio.Agregar(modeloo.Precio);
                }
                _controlador.Guardar();
                return Json(new { success = true, message = "Se agregó correctamente." });
            }
            else
            {
                modeloo.Trabajo = _controlador.Trabajo.Listar().Select
                (
                    s => new SelectListItem
                    {
                        Text = s.NombreTrabajo,
                        Value = s.IdTrabajo.ToString()
                    }
                );
                if (modeloo.Precio.IdPrecio != 0)
                {
                    modeloo.Precio = _controlador.Precio.Buscar(modeloo.Precio.IdPrecio);
                }
                return Json(new { success = false, message = "Ocurrió un error guardando el precio." });
            }
        }
        //------------
        [HttpGet]
        public IActionResult Listar()
        {
            return Json(new { data = _controlador.Precio.Listar(propiedades: "Trabajo") });
        }

        // POST: Banco/Delete/5
        [HttpDelete]
        public IActionResult Borrar(int id)
        {
            var t = _controlador.Precio.Buscar(id);
            if (t == null)
            {
                return Json(new { success = false, message = "Los precios no se encontraron." });
            }
            _controlador.Precio.Remover(t);
            _controlador.Guardar();
            return Json(new { success = true, message = "El precio ha sido eliminado." });

        }
    }
}
