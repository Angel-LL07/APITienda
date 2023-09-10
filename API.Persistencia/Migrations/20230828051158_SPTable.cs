using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Persistencia.Migrations
{
    public partial class SPTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE dbo.InsertaProductos
                (
                @lst ListasProductos READONLY
                )
                AS 
                BEGIN
                INSERT INTO Productos( Sku,Nombre,NumMaterial,CategoriaId,Inventario)
                SELECT  Sku,Nombre,NumMaterial,CategoriaId,Inventario FROM  @lst
                END
             ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE dbo.InsertarProductos");
        }
    }
}
