using Ferreteria.DataAccess.Repositorio.IRepositorio;
using Ferreteria.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReactVista.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        public ClientesController(IControlador controlador)
        {
            _controlador = controlador;
        }

        readonly IControlador _controlador;

        [HttpGet]
        public IActionResult GetClientes()
        {
            var clientes = _controlador.Cliente.Listar();
            return new JsonResult(new { success = true, data = clientes });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClientes(int id)
        {
            var t = _controlador.Cliente.Buscar(id);
            if (t == null)
            {
                return new JsonResult(new { success = false, message = "No se encuentra el cliente." });
            }
            _controlador.Cliente.Remover(t);
            _controlador.Guardar();
            return new JsonResult(new { success = true, message = "El cliente ha sido eliminado." });
        }

        [HttpGet("{id}")]
        public IActionResult Upsert(int id = 0)
        {
            if (id == 0)
            {
                return new JsonResult(new Cliente());
            }
            else
            {
                var t = _controlador.Cliente.Buscar(id);
                if (t == null)
                {
                    return NotFound();
                }

                return new JsonResult(t);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                //Insert
                if (cliente.IdCliente == 0)
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

                return new JsonResult(new { success = true, message = "Se agregó correctamente." });
            }

            return new JsonResult(new { success = false, message = "Ocurrió un error guardando la información." });
        }
    }
}
