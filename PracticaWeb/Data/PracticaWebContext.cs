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

        public DbSet<PracticaWeb.Models.Productos> Productos { get; set; } = default!;
    }
}
