using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Aplicacion.DTOs
{
    public class SubcategoriaAgregarVM
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Padre { get; set; }   
    }
}
