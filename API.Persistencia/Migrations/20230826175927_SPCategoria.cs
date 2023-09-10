using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Persistencia.Migrations
{
    public partial class SPCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE PROCEDURE dbo.AgregarCategoria
                @nombre varchar(max),
                @padre int= NULL,
                @id int OUTPUT
                AS
                BEGIN
                        IF(@padre IS NULL)
                            BEGIN
                                SET NOCOUNT ON;
                                INSERT INTO categorias (Id,Nombre)
                                VALUES(@id,@nombre)
                                RETURN @id
                            END
                        ELSE
                            BEGIN
                                SET NOCOUNT ON;
                                INSERT INTO categorias (Id,Nombre,Padre)
                                VALUES(@id,@nombre,@padre)
                                RETURN @id
                            END
                END
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE dbo.ListaCategorias
                AS
                BEGIN
                SELECT Id,Nombre FROM Categorias WHERE Padre IS NULL;
                END
            ");

            migrationBuilder.Sql(@"
                CREATE PROCEDURE dbo.ListaSubcategorias
                AS
                BEGIN
                SELECT Id,Nombre FROM Categorias WHERE Padre IS NOT NUll;
                END
            ");

            migrationBuilder.Sql(@"
                    CREATE PROCEDURE dbo.ActualizaCategorias
                    @id int,
                    @padre int = NULL,
                    @nuevoid int,
                    @nombre nvarchar(max)
                    AS 
                    BEGIN
                        IF(@padre IS NULL)
                            BEGIN
                                UPDATE categorias SET Nombre = @nombre,Id=@nuevoid
                                WHERE Id =@id AND Padre IS NULL;
                            END
                        ELSE
                            BEGIN
                                 UPDATE categorias SET Nombre = @nombre,Id=@nuevoid,Padre=@padre
                                WHERE Id =@id;
                            END
                    END
            ");
            migrationBuilder.Sql(@"
                CREATE PROCEDURE dbo.CargaInventario
                @sku nvarchar(max),
                @nombre nvarchar(max),
                @numMaterial nvarchar(max),
                @categoria int,
                @inventario int
                AS 
                BEGIN
                    IF(SELECT COUNT(*) 
                                    FROM productos 
                                    WHERE Sku=@sku AND Nombre = @nombre AND NumMaterial =@numMaterial AND CategoriaId =@categoria) = 0
                            
                            BEGIN
                                    INSERT INTO productos(Nombre,NumMaterial,CategoriaId,Inventario)
                                    VALUES(@nombre,@numMaterial,@categoria,@inventario);
                            END
                    ELSE
                        BEGIN
                                    UPDATE productos SET Inventario =(Inventario + @inventario) WHERE Nombre = @nombre AND NumMaterial =@numMaterial AND CategoriaId =@categoria AND Sku = @sku;
                        END
                   
                END

             ");
            migrationBuilder.Sql(@"
                CREATE PROCEDURE dbo.ListaProductos
                AS
                BEGIN
                SELECT * FROM Productos;
                END
            ");
            migrationBuilder.Sql(@"
            CREATE PROCEDURE dbo.ActualizaProducto
                @id int,
                @sku nvarchar(max),
                @nombre nvarchar(max),
                @numMaterial nvarchar(max),
                @categoria int,
                @inventario int
               AS
               BEGIN
                UPDATE Productos 
                       SET Sku=@sku,Nombre=@nombre,NumMaterial=@numMaterial,CategoriaId=@categoria,Inventario=@inventario
                WHERE Id=@id
               END
            ");
            migrationBuilder.Sql(@"
                CREATE PROCEDURE dbo.ListaProducInventario
                @primerValor int,
                @segundoValor int
                AS
                BEGIN
               SELECT * FROM PRODUCTOS WHERE Inventario BETWEEN @primerValor  and @segundoValor;
                END
            ");
            migrationBuilder.Sql(@"
                CREATE PROCEDURE dbo.ListaProducCategoria
                @numero nvarchar(max)
                AS
                BEGIN
                    SELECT * FROM Productos WHERE CategoriaId Like @numero +'%'
                END
            ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROCEDURE dbo.AgregarCategoria");
            migrationBuilder.Sql(@"DROP PROCEDURE dbo.ListaCategorias");
            migrationBuilder.Sql(@"DROP PROCEDURE dbo.ListaSubcategorias");
            migrationBuilder.Sql("DROP PROCEDURE dbo.ActualizaCategorias");
            migrationBuilder.Sql(@"DROP PROCEDURE dbo.CargaInventario");
            migrationBuilder.Sql(@"DROP PROCEDURE dbo.ListaProductos");
            migrationBuilder.Sql(@"DROP PROCEDURE dbo.ActualizaProducto");
            migrationBuilder.Sql(@"DROP PROCEDURE dbo.ListaProducInventario");
            migrationBuilder.Sql(@"DROP PROCEDURE dbo.ListaProducCategoria");
        }
    }
}
