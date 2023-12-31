USE [APITienda]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 29/08/2023 10:30:35 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 29/08/2023 10:30:35 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorias](
	[Id] [int] NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[Padre] [int] NULL,
 CONSTRAINT [PK_Categorias] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 29/08/2023 10:30:35 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Sku] [nvarchar](max) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[NumMaterial] [nvarchar](max) NOT NULL,
	[CategoriaId] [int] NOT NULL,
	[Inventario] [int] NOT NULL,
 CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230826174144_tienda', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230826175927_SPCategoria', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230827012402_listarCategoria', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230827021022_SPs', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230827172926_CargaInventario', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230827185348_listarProductos', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230827191424_actualizaproducto', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230827205042_SPRangoproducto', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230827224812_SP', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230828050824_ver', N'6.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230828051158_SPTable', N'6.0.0')
GO
INSERT [dbo].[Categorias] ([Id], [Nombre], [Padre]) VALUES (11, N'Tecnología2', NULL)
INSERT [dbo].[Categorias] ([Id], [Nombre], [Padre]) VALUES (12, N'Farmacia', NULL)
INSERT [dbo].[Categorias] ([Id], [Nombre], [Padre]) VALUES (13, N'Hogar', NULL)
INSERT [dbo].[Categorias] ([Id], [Nombre], [Padre]) VALUES (111, N'Computación', 11)
INSERT [dbo].[Categorias] ([Id], [Nombre], [Padre]) VALUES (112, N'Telefonía', 11)
INSERT [dbo].[Categorias] ([Id], [Nombre], [Padre]) VALUES (121, N'Medicamentos', 12)
INSERT [dbo].[Categorias] ([Id], [Nombre], [Padre]) VALUES (131, N'Baño', 13)
INSERT [dbo].[Categorias] ([Id], [Nombre], [Padre]) VALUES (1111, N'Computadora de Escritorio', 111)
INSERT [dbo].[Categorias] ([Id], [Nombre], [Padre]) VALUES (1112, N'Computadora portatil', 111)
INSERT [dbo].[Categorias] ([Id], [Nombre], [Padre]) VALUES (1113, N'Tablets', 111)
INSERT [dbo].[Categorias] ([Id], [Nombre], [Padre]) VALUES (1121, N'Celular', 112)
INSERT [dbo].[Categorias] ([Id], [Nombre], [Padre]) VALUES (1122, N'Accesorios', 112)
INSERT [dbo].[Categorias] ([Id], [Nombre], [Padre]) VALUES (1211, N'Analgésicos', 121)
INSERT [dbo].[Categorias] ([Id], [Nombre], [Padre]) VALUES (1212, N'Estomacal', 121)
INSERT [dbo].[Categorias] ([Id], [Nombre], [Padre]) VALUES (1311, N'Toallas', 131)
INSERT [dbo].[Categorias] ([Id], [Nombre], [Padre]) VALUES (1312, N'Batas', 131)
GO
SET IDENTITY_INSERT [dbo].[Productos] ON 

INSERT [dbo].[Productos] ([Id], [Sku], [Nombre], [NumMaterial], [CategoriaId], [Inventario]) VALUES (1073, N'1', N'Dell 4512', N'AX-4342FD', 1112, 6)
INSERT [dbo].[Productos] ([Id], [Sku], [Nombre], [NumMaterial], [CategoriaId], [Inventario]) VALUES (1074, N'2', N'Iphone X', N'AD-4332EE', 1121, 20)
INSERT [dbo].[Productos] ([Id], [Sku], [Nombre], [NumMaterial], [CategoriaId], [Inventario]) VALUES (1075, N'3', N'Correa', N'AC-5545Q', 1122, 0)
INSERT [dbo].[Productos] ([Id], [Sku], [Nombre], [NumMaterial], [CategoriaId], [Inventario]) VALUES (1076, N'4', N'Bata hombre', N'BN-18643', 1312, 2)
INSERT [dbo].[Productos] ([Id], [Sku], [Nombre], [NumMaterial], [CategoriaId], [Inventario]) VALUES (1077, N'5', N'Aspirina', N'MD-7456AS', 1211, 44)
SET IDENTITY_INSERT [dbo].[Productos] OFF
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD  CONSTRAINT [FK_Productos_Categorias_CategoriaId] FOREIGN KEY([CategoriaId])
REFERENCES [dbo].[Categorias] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Productos] CHECK CONSTRAINT [FK_Productos_Categorias_CategoriaId]
GO
/****** Object:  StoredProcedure [dbo].[ActualizaCategorias]    Script Date: 29/08/2023 10:30:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

                    CREATE PROCEDURE [dbo].[ActualizaCategorias]
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
            
GO
/****** Object:  StoredProcedure [dbo].[ActualizaProducto]    Script Date: 29/08/2023 10:30:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

            CREATE PROCEDURE [dbo].[ActualizaProducto]
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
            
GO
/****** Object:  StoredProcedure [dbo].[AgregarCategoria]    Script Date: 29/08/2023 10:30:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

                CREATE PROCEDURE [dbo].[AgregarCategoria]
                @nombre varchar(max),
                @padre int =NULL,
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
            
GO
/****** Object:  StoredProcedure [dbo].[CargaInventario]    Script Date: 29/08/2023 10:30:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

                CREATE PROCEDURE [dbo].[CargaInventario]
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
                                    INSERT INTO productos(Sku,Nombre,NumMaterial,CategoriaId,Inventario)
                                    VALUES(@sku,@nombre,@numMaterial,@categoria,@inventario);
                            END
                    ELSE
                        BEGIN
                                    UPDATE productos SET Inventario =(Inventario + @inventario) WHERE Nombre = @nombre AND NumMaterial =@numMaterial AND CategoriaId =@categoria AND Sku = @sku;
                        END
                   
                END

             
GO
/****** Object:  StoredProcedure [dbo].[ListaCategorias]    Script Date: 29/08/2023 10:30:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

                CREATE PROCEDURE [dbo].[ListaCategorias]
                AS
                BEGIN
                SELECT Id,Nombre,Padre FROM Categorias WHERE Padre IS NULL;
                END
            
GO
/****** Object:  StoredProcedure [dbo].[ListaProducCategoria]    Script Date: 29/08/2023 10:30:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

                CREATE PROCEDURE [dbo].[ListaProducCategoria]
                @numero nvarchar(max)
                AS
                BEGIN
                    SELECT * FROM Productos WHERE CategoriaId Like @numero +'%'
                END
            
GO
/****** Object:  StoredProcedure [dbo].[ListaProducInventario]    Script Date: 29/08/2023 10:30:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

                CREATE PROCEDURE [dbo].[ListaProducInventario]
                @primerValor int,
                @segundoValor int
                AS
                BEGIN
               SELECT * FROM PRODUCTOS WHERE Inventario BETWEEN @primerValor  and @segundoValor;
                END
            
GO
/****** Object:  StoredProcedure [dbo].[ListaProductos]    Script Date: 29/08/2023 10:30:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

                CREATE PROCEDURE [dbo].[ListaProductos]
                AS
                BEGIN
                SELECT * FROM Productos;
                END
            
GO
/****** Object:  StoredProcedure [dbo].[ListaSubcategorias]    Script Date: 29/08/2023 10:30:36 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

                CREATE PROCEDURE [dbo].[ListaSubcategorias]
                AS
                BEGIN
                SELECT Id,Nombre,Padre FROM Categorias WHERE Padre IS NOT NUll;
                END
            
GO
