using API.Aplicacion;
using API.Aplicacion.DTOs;
using API.Dominio;
using API.Persistencia;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace APITienda.Controllers
{
    [Route("api/subcategorias")]
    [ApiController]
    public class SubcategoriasController : ControllerBase
    {
        private TiendaContext _context;
        private IMapper _mapper;
        public SubcategoriasController(TiendaContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }

        [HttpGet("ListaSubcategorias")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ListaCategorias()
        {
            var categorias = _context.Categorias
                .FromSqlInterpolated($"EXEC dbo.ListaSubCategorias")
                .AsAsyncEnumerable();
            var lista = new List<SubcategoriasVM>();
            await foreach (var item in categorias)
            {
                lista.Add(_mapper.Map<SubcategoriasVM>(item));
            }

            //var root = lista.GenerateTree(c => c.Id, c => c.Padre);

            return Ok(lista);
        }

        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //public async   JsonResult ListaCategorias()
        //{
        //    var categorias = _context.Categorias
        //        .FromSqlInterpolated($"EXEC dbo.ListaSubCategorias")
        //        .AsAsyncEnumerable();
        //    var lista = new List<SubcategoriasVM>();
        //    var listaArbol = new List<SubcategoriasArbol>();
        //    await foreach (var item in categorias)
        //    {
        //        lista.Add(_mapper.Map<SubcategoriasVM>(item));
        //    }

        //    listaArbol = lista.OrderBy(x => x.Id)
        //        .Select(x => new SubcategoriasArbol
        //    {
        //            Id=x.Id,
        //            Nombre=x.Nombre,
        //            Padre=x.Padre,
        //            Hijo = GetChildren(lista,x.Id)


        //        }).ToList();

        //    return Ok(listaArbol);
        //}

        //private List<SubcategoriasArbol> GetChildren(List<SubcategoriasVM> locations, int parentId)
        //{
        //    return locations.Where(l => Convert.ToInt32(l.Id) == parentId).OrderBy(l => l.Padre)
        //        .Select(l => new SubcategoriasArbol
        //        {
        //            Id =  l.Id,
        //           Nombre = l.Nombre,
        //           Padre = l.Padre,
        //           Hijo = GetChildren(locations,l.Id)
        //        }).ToList();
        //}


        [HttpPost("AgregarSubcategoria")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> AgregarSubcateoria([FromBody] SubcategoriasVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var subcategoria = model.Id.Replace(".","");
            var padre = model.Padre.Replace(".", "");
            bool IsNumeric = subcategoria.All(char.IsDigit);
            bool IsNumerico = padre.All(char.IsDigit);
            if (!IsNumeric || !IsNumerico)
            {
                ModelState.AddModelError("", $"Ingrese Correctamente la subcategoria");
                return BadRequest(ModelState);
            }
            int id = Convert.ToInt32(subcategoria);
            int padres = Convert.ToInt32(padre);
            var existe = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
            if (existe != null)
            {
                ModelState.AddModelError("", $"La categoria {model.Id} ya existe");
                return BadRequest(ModelState);
            }
            try
            {

				await _context.Database
					  .ExecuteSqlInterpolatedAsync($@"EXEC AgregarCategoria
                                                        @nombre={model.Nombre},@padre={padres},@id={id} OUTPUT");
				return Ok(id);
			}
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPatch("ActualizaSubcategoria/{SubcategoriaId:int}")]
        [ProducesResponseType(200, Type = typeof(SubcategoriasVM))]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizaCategoria(int SubcategoriaId, [FromBody] SubcategoriasVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.Database
                .ExecuteSqlInterpolatedAsync($"EXEC ActualizaCategorias @nombre = {model.Nombre}, @padre = {model.Padre},@id = {SubcategoriaId},@nuevoid={model.Id}");

            return Ok();
        }

        [HttpDelete("EliminarSubcategoria/{SubcategoriaId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        public async Task<IActionResult> EliminarSubcategoria(int SubcategoriaId)
        {
            var Existe = await _context.Categorias.FindAsync(SubcategoriaId);
            if (Existe == null)
            {
                return NotFound();
            }

            _context.Remove(Existe);
            _context.SaveChanges();
            return NoContent();
        }
    }
    
    

}
