using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Persistencia.Migrations
{
    public partial class ver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE TYPE ListasProductos as Table(
                Sku nvarchar(max),
                Nombre  nvarchar(max),
                NumMaterial  nvarchar(max),
                CategoriaId int,
                Inventario int
                PRIMARY KEY(Id)
                )
             ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP TYPE ListasProductos");
        }
    }
}
