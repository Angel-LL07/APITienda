using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Aplicacion.DTOs
{
    public class SubcategoriasArbol
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Padre { get; set; }
        public object Hijo { get; set; }
    }
}
