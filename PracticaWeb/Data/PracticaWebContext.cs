using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PracticaWeb.Models;

namespace PracticaWeb.Data
{
    public class PracticaWebContext : DbContext
    {
        public PracticaWebContext (DbContextOptions<PracticaWebContext> options)
            : base(options)
        {
        }

        public DbSet<Productos> ProductosDB { get; set; } = default!;
        public DbSet<Proveedores> ProveedoresDB { get; set; } = default!;
        public DbSet<TipoProductos> TipoProductosDB { get; set; } = default!;
        public DbSet<PrecioProveedores> PreciosProveedoresDB { get; set; } = default!;
    }
}
