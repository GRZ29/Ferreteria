using Ferreteria.DataAccess.Repositorio.IRepositorio;
using Ferreteria.Models;
using Ferreteria.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ferreteria.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MaterialController : Controller
    {
        public MaterialController(IControlador controlador)
        {
            _controlador = controlador;
        }

        readonly IControlador _controlador;

        public IActionResult Index()
        {
            IEnumerable<Material> material = _controlador.Material.Listar();
            return View(material);
        }

        public IActionResult Index2()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Upsert(int id = 0)
        {
            if (id == 0)
            {
                return View(new Material());
            }
            else
            {
                var t = _controlador.Material.Buscar(id);
                if (t == null)
                {
                    return NotFound();
                }

                return View(t);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(int id, Material material)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    _controlador.Material.Agregar(material);
                    _controlador.Guardar();
                }
                //Update
                else
                {
                    try
                    {
                        _controlador.Material.Actualizar(material);
                        _controlador.Guardar();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (_controlador.Material.Buscar(material.IdMaterial) == null)
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                return Json(new { success = true, message = "Se agregó correctamente." });
            }

            return Json(new { success = false, message = "Ocurrió un error guardando el material." });
        }

        [HttpGet]
        public IActionResult Listar()
        {
            //return Json(new { success = true, data = _controlador.Transaccion.Listar() });

            return Json(new { success = true, data = _controlador.Material.Listar() });
        }

        [HttpDelete]
        public IActionResult Borrar(int id)
        {
            var t = _controlador.Material.Buscar(id);
            if (t == null)
            {
                return Json(new { success = false, message = "No se encuentran materiales." });
            }
            _controlador.Material.Remover(t);
            _controlador.Guardar();
            return Json(new { success = true, message = "El material ha sido eliminado." });
        }

    }
}
