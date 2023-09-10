using API.Aplicacion.DTOs;
using API.Persistencia;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.RegularExpressions;

namespace APITienda.Controllers
{
    [Route("api/categorias")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private TiendaContext _context;
        private IMapper _mapper;
        public CategoriasController(TiendaContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("ListaCategorias")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ListaCategorias()
        {
            var categorias = _context.Categorias
                .FromSqlInterpolated($"EXEC dbo.ListaCategorias")
                .AsAsyncEnumerable();
            var lista = new List<CategoriaListar>();
            await foreach(var item in categorias)
            {
                lista.Add(_mapper.Map<CategoriaListar>(item));
            }
            return Ok(lista);
        }

        [HttpPost("AgregarCategoria")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<int>> AgregarCateoria([FromBody] CategoriaAgregarVM model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
				var categoria = model.Id.Replace(".", "");
				bool IsNumeric = categoria.All(char.IsDigit);
				if (!IsNumeric)
				{
					ModelState.AddModelError("", $"Ingrese Correctamente la categoria");
					return BadRequest(ModelState);
				}
				int numero = Convert.ToInt32(categoria);
				var existe = await _context.Categorias.FirstOrDefaultAsync(c => c.Id == numero);
				if (existe != null)
				{
					ModelState.AddModelError("", $"La categoria {model.Id} ya existe");
					return BadRequest(ModelState);
				}

				await _context.Database
					  .ExecuteSqlInterpolatedAsync($@"EXEC AgregarCategoria
                                                        @nombre={model.Nombre},@id={numero} OUTPUT");
				return Ok(numero);
			}
            catch(Exception ex)
            {
				return BadRequest(ex);
			}
          
           
        }

        [HttpPatch("ActualizaCategoria/{CategoriaId:int}")]
        [ProducesResponseType(200, Type = typeof(CategoriaVM))]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizaCategoria(int CategoriaId, [FromBody] CategoriaVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
				await _context.Database
				.ExecuteSqlInterpolatedAsync($"EXEC ActualizaCategorias @nombre = {model.Nombre}, @id = {CategoriaId},@nuevoid={model.Id}");

				return Ok();
			}
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("EliminarCategoria/{CategoriaId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]

        public async Task<IActionResult> EliminarCategoria(int CategoriaId)
        {
            var Existe = await _context.Categorias.FindAsync(CategoriaId);
            if (Existe == null)
            {
                return NotFound();
            }

            try 
            {
				_context.Remove(Existe);
				_context.SaveChanges();
				return NoContent();
			}
            catch(Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
   
}
