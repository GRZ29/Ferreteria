using Ferreteria.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ferreteria.DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Login> Login { get; set; }
        public DbSet<Material> Materiales { get; set; }
        public DbSet<Precio> Precios { get; set; }
        public DbSet<Trabajo> Trabajos { get; set; }
        public DbSet<UserLogin> UserLogin { get; set; }
    }

}
