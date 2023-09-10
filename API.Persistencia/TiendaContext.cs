using API.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Persistencia
{
    public class TiendaContext :DbContext
    {
        public TiendaContext(DbContextOptions<TiendaContext> options):base(options)
        {
            
        }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
    }
}
