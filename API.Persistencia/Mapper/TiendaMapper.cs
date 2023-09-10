using API.Aplicacion.DTOs;
using API.Dominio;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Persistencia.Mapper
{
    public class TiendaMapper:Profile
    {
        public TiendaMapper()
        {
            CreateMap<Categoria,CategoriaVM>().ReverseMap();
            CreateMap<Categoria,CategoriaListar>().ReverseMap();
            CreateMap<Categoria,SubcategoriasVM>().ReverseMap();
            CreateMap<SubcategoriasVM,SubcategoriasArbol>().ReverseMap();
            CreateMap<Producto,ProductoVM>().ReverseMap();
		}
    }
}
