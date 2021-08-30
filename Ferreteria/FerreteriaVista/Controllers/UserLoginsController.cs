using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ferreteria.DataAccess.Data;
using Ferreteria.Models;

namespace FerreteriaVista.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserLoginsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserLogins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserLogin>>> GetUserLogin()
        {
            return await _context.UserLogin.ToListAsync();
        }

        // GET: api/UserLogins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserLogin>> GetUserLogin(int id)
        {
            var userLogin = await _context.UserLogin.FindAsync(id);

            if (userLogin == null)
            {
                return NotFound();
            }

            return userLogin;
        }

        // PUT: api/UserLogins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserLogin(int id, UserLogin userLogin)
        {
            if (id != userLogin.id)
            {
                return BadRequest();
            }

            _context.Entry(userLogin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserLoginExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserLogins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserLogin>> PostUserLogin(UserLogin userLogin)
        {
            _context.UserLogin.Add(userLogin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserLogin", new { id = userLogin.id }, userLogin);
        }

        // DELETE: api/UserLogins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserLogin(int id)
        {
            var userLogin = await _context.UserLogin.FindAsync(id);
            if (userLogin == null)
            {
                return NotFound();
            }

            _context.UserLogin.Remove(userLogin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{usuario}/{contra}")]
        public  ActionResult<List<UserLogin>> GetIniciarSesion(string usuario, string contra)
        {
            var userLogin = _context.UserLogin.Where(UserLogin => UserLogin.Usuario.Equals(usuario) && UserLogin.Contra.Equals(contra)).ToList();

            if (userLogin == null)
            {
                return NotFound();
            }

            return userLogin;
        }

        private bool UserLoginExists(int id)
        {
            return _context.UserLogin.Any(e => e.id == id);
        }
    }
}
