using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Aplicacion.DTOs
{
    public class CargarProductosVM
    {
        public string Sku { get; set; }
        public string Nombre { get; set; }
        public string NumMaterial { get; set; }
        //public int CategoriaDocumento { get; set; }
        public int CategoriaId { get; set; }
        //public int SubcategoriaId { get; set; }
        public int Inventario { get; set; }
    }
}
