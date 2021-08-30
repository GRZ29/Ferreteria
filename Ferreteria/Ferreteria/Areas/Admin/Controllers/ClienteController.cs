using Ferreteria.DataAccess.Repositorio.IRepositorio;
using Ferreteria.Models;
using Ferreteria.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ferreteria.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ClienteController : Controller
    {
        public ClienteController(IControlador controlador)
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
            if (id == 0)
            {
                return View(new Cliente());
            }
            else
            {
                var t = _controlador.Cliente.Buscar(id);
                if (t == null)
                {
                    return NotFound();
                }

                return View(t);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(int id, Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (id == 0)
                {
                    _controlador.Cliente.Agregar(cliente);
                    _controlador.Guardar();
                }
                //Update
                else
                {
                    try
                    {
                        _controlador.Cliente.Actualizar(cliente);
                        _controlador.Guardar();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (_controlador.Cliente.Buscar(cliente.IdCliente) == null)
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

            return Json(new { success = false, message = "Ocurrió un error guardando la información." });
        }

        [HttpGet]
        public IActionResult Listar()
        {
            
            return Json(new { success = true, data = _controlador.Cliente.Listar() });
        }

        [HttpDelete]
        public IActionResult Borrar(int id)
        {
            var t = _controlador.Cliente.Buscar(id);
            if (t == null)
            {
                return Json(new { success = false, message = "No se encuentra el cliente." });
            }
            _controlador.Cliente.Remover(t);
            _controlador.Guardar();
            return Json(new { success = true, message = "El cliente ha sido eliminado." });
        }

    }
}
