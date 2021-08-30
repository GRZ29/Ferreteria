using Ferreteria.DataAccess.Data;
using Ferreteria.DataAccess.Repositorio.IRepositorio;
using Ferreteria.Models;
using Microsoft.AspNetCore.Authorization;
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
        readonly ApplicationDbContext _context;
        public ClientesController(IControlador controlador, ApplicationDbContext context)
        {
            _controlador = controlador;
            _context = context;
        }

        readonly IControlador _controlador;

        [HttpGet]
        public ActionResult GetClientes()
        {
            var clientes = _controlador.Cliente.Listar();
            return new JsonResult(new { success = true, data = clientes });
        }

        // api/Clientes/0

        [HttpGet("{id}", Name = "GetCliente")]
        public IActionResult GetCliente(int id)
        {

            try
            {
                var clientes = _controlador.Cliente.Buscar(id);
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClientes(int id)
        {
            try
            {
                var t = _controlador.Cliente.Buscar(id);
                if (t == null)
                {
                    return BadRequest("No se encuentra el cliente." );
                }
                _controlador.Cliente.Remover(t);
                _controlador.Guardar();
                return Ok(id);
                //return Ok("se elimino");
            }
            catch (Exception)
            {

                return BadRequest("existe un problema con el servidor");
            }
            
        }

        [HttpPost]
        public ActionResult InsertCliente([FromBody]Cliente cliente)
        {
            try
            {
                _controlador.Cliente.Agregar(cliente);
                _controlador.Guardar();

                return CreatedAtRoute("GetCliente", new { id = cliente.IdCliente }, cliente);
                //return Ok("todo bien");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCliente (int id,Cliente cliente)
        {
            try
            {
                
                if (cliente.IdCliente == id)
                {


                    _controlador.Cliente.Actualizar(cliente);
                    _controlador.Guardar();

                    return CreatedAtRoute("GetCliente", new { id = cliente.IdCliente }, cliente);
                }
                else
                {
                    return BadRequest();
                }
              

            }
            catch (DbUpdateConcurrencyException)
            {
                if (_controlador.Cliente.Buscar(cliente.IdCliente) == null)
                {
                    return NotFound();
                }
                else
                {
                    throw ;
                }
            }

        }


        //API vs RESTfull API
        //[HttpPost]
        //public IActionResult InsertCliente(Cliente cliente)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //Insert
        //        if (cliente.IdCliente == 0)
        //        {
        //            _controlador.Cliente.Agregar(cliente);
        //            _controlador.Guardar();
        //        }
        //        //Update
        //        else
        //        {
        //            try
        //            {
        //                _controlador.Cliente.Actualizar(cliente);
        //                _controlador.Guardar();
        //            }
        //            catch (DbUpdateConcurrencyException)
        //            {
        //                if (_controlador.Cliente.Buscar(cliente.IdCliente) == null)
        //                {
        //                    return NotFound();
        //                }
        //                else
        //                {
        //                    throw;
        //                }
        //            }
        //        }

        //        return new JsonResult(new { success = true, message = "Se agregó correctamente." });
        //    }

        //    return new JsonResult(new { success = false, message = "Ocurrió un error guardando la información." });
        //}
    }
}
