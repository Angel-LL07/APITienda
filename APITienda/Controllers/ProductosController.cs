using API.Aplicacion.DTOs;
using API.Dominio;
using API.Persistencia;
using API.Persistencia.Migrations;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;

namespace APITienda.Controllers
{
    [Route("api/productos")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private TiendaContext _context;
        private IMapper _mapper;
        private IWebHostEnvironment _webHostEnvironment;
        public ProductosController(TiendaContext context,IWebHostEnvironment webHostEnvironment,IMapper mapper)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _mapper = mapper;
        }
        [HttpGet("ProductosCategoria")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ProductosCategoria(string Categoria)
        {
            var categoriaid =    Categoria.Replace(".", "");
            bool IsNumeric = categoriaid.All(char.IsDigit);
            if (!IsNumeric || Categoria == null || Categoria =="")
            {
                ModelState.AddModelError("", $"Ingrese Correctamente la categoria");
                return BadRequest(ModelState);
            }
            var productos = _context.Productos
                .FromSqlInterpolated($"EXEC dbo.ListaProducCategoria @numero={categoriaid}")
                .AsAsyncEnumerable();
            var lista = new List<ProductoVM>();
            await foreach (var item in productos)
            {
                lista.Add(_mapper.Map<ProductoVM>(item));
            }
            //var root = lista.GenerateTree(c => c.Id, c => c.Padre);
            return Ok(lista);
        }

        [HttpGet("ListaProductos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ListaProductos()
        {
            var productos = _context.Productos
                .FromSqlInterpolated($"EXEC dbo.ListaProductos")
                .AsAsyncEnumerable();
            var lista = new List<ProductoVM>();
            await foreach (var item in productos)
            {
                lista.Add(_mapper.Map<ProductoVM>(item));
            }
            //var root = lista.GenerateTree(c => c.Id, c => c.Padre);
            return Ok(lista);
        }

        ///// GET: api/ListaProductosRangoInventario/5
        /////<summary>
        ///// Obtener listado de productos que se encuentren dentro de un rango de inventario.
        /////</summary>
        /////<returns></returns>
        [HttpGet("ProductosPorRango/{PrimerValor:int},{SegundoValor:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> ListaProductosRangoInventario(int PrimerValor, int SegundoValor)
        {
            var productos = _context.Productos
                .FromSqlInterpolated($"EXEC dbo.ListaProducInventario @primerValor={PrimerValor},@segundoValor={SegundoValor}")
                .AsAsyncEnumerable();
            var lista = new List<ProductoVM>();
            await foreach (var item in productos)
            {
                lista.Add(_mapper.Map<ProductoVM>(item));
            }

            //var root = lista.GenerateTree(c => c.Id, c => c.Padre);

            return Ok(lista);
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        [Route("CargarInventario")]
        public async Task<IActionResult> CargarInventario(IFormFile file)
        {

			if (file == null || file.Length <= 0)
				return BadRequest("Archivo Vacio");
			if (Path.GetExtension(file.FileName) != ".xlsx")
				return BadRequest("El documento debe ser un excel .xlsx");

            using (var stream = new MemoryStream())
            {
                List<Producto> lista = new();
                List<CargarProductosVM> listaSP = new();
                await file.CopyToAsync(stream);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.First(); //first worksheet
                    var endRow = worksheet.Dimension.End.Row; //total no of Rows
                    var endCol = worksheet.Dimension.End.Column; //total no of Column

                    var RegistrosNoInsertados = 0;
                    
                    for (int row = 2; row <= endRow; row++)
                    {
                        
                        var valorceldacategoria = worksheet.Cells[row, 4].Value.ToString().Replace(".", "");
                        var categoriaid = Convert.ToInt32(valorceldacategoria);
                        //verifica si la categoria es un entero
                        if (!valorceldacategoria.All(char.IsDigit)) { RegistrosNoInsertados++; continue; }
                        var existeCategoria = await _context.Categorias.FindAsync(categoriaid);
                        //verifica si la categoria existe, si no salta ala siguiente fila
                        if (existeCategoria == null) { RegistrosNoInsertados++; continue; }
                        if (row > endRow)
                            break;
                        lista.Add(new Producto
                        {
                            Sku = worksheet.Cells[row, 1].Value.ToString(),
                            Nombre = worksheet.Cells[row, 2].Value.ToString(),
                            NumMaterial = worksheet.Cells[row, 3].Value.ToString(),
                            CategoriaId = categoriaid,
                            Inventario = Convert.ToInt32(worksheet.Cells[row, 5].Value.ToString())
                        });
                    }

                    foreach (var item in lista)
                    {
                        await _context.Database
                        .ExecuteSqlInterpolatedAsync($"EXEC CargaInventario @sku={item.Sku}, @nombre = {item.Nombre},@numMaterial ={item.NumMaterial},@categoria={item.CategoriaId},@inventario={item.Inventario}");
                    }

					return StatusCode(200, new { mensaje = $"Documento cargado {RegistrosNoInsertados} productos no registrados" });
				}
			}
		}

        [HttpPatch("ActualizaProducto/{ProductoId:int}")]
        [ProducesResponseType(200, Type = typeof(ProductosVM))]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizaProducto(int ProductoId, [FromBody] ProductosVM model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _context.Database
                .ExecuteSqlInterpolatedAsync($"EXEC ActualizaProducto @id={ProductoId},@sku={model.Sku},@nombre = {model.Nombre}, @numMaterial={model.NumMaterial},@categoria={model.CategoriaId},@inventario={model.Inventario}");

            return Ok();
        }

        [HttpDelete("{ProductoId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> EliminarSubcategoria(int ProductoId)
        {
            var Existe = await _context.Productos.FindAsync(ProductoId);
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
