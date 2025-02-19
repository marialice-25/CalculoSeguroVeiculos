using CalculoSeguroVeiculos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculoSeguroVeiculos.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Veiculo> Veiculo { get; set; }
        public DbSet<Segurado> Segurado { get; set; }
        public DbSet<Seguro> Seguro { get; set; }
    }
}
