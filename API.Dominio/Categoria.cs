using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Dominio
{
    public class Categoria
    {
        public Categoria()
        {
            Productos = new HashSet<Producto>();
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? Padre { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
