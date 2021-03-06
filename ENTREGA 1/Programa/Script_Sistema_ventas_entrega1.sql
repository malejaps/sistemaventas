USE [master]
GO
/****** Object:  Database [Sistema_ventas]    Script Date: 15/05/2015 14:45:35 ******/
CREATE DATABASE [Sistema_ventas]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Sistema_ventas', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\Sistema_ventas.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Sistema_ventas_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\Sistema_ventas_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Sistema_ventas] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Sistema_ventas].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Sistema_ventas] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Sistema_ventas] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Sistema_ventas] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Sistema_ventas] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Sistema_ventas] SET ARITHABORT OFF 
GO
ALTER DATABASE [Sistema_ventas] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Sistema_ventas] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Sistema_ventas] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Sistema_ventas] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Sistema_ventas] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Sistema_ventas] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Sistema_ventas] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Sistema_ventas] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Sistema_ventas] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Sistema_ventas] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Sistema_ventas] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Sistema_ventas] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Sistema_ventas] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Sistema_ventas] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Sistema_ventas] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Sistema_ventas] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Sistema_ventas] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Sistema_ventas] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Sistema_ventas] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Sistema_ventas] SET  MULTI_USER 
GO
ALTER DATABASE [Sistema_ventas] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Sistema_ventas] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Sistema_ventas] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Sistema_ventas] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [Sistema_ventas]
GO
/****** Object:  Table [dbo].[Auditoria]    Script Date: 15/05/2015 14:45:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Auditoria](
	[cod_auditoria] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nvarchar](50) NOT NULL,
	[cod_usuario] [int] NOT NULL,
 CONSTRAINT [PK_Auditoria] PRIMARY KEY CLUSTERED 
(
	[cod_auditoria] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Categoria_Producto]    Script Date: 15/05/2015 14:45:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria_Producto](
	[cod_cat_producto] [int] IDENTITY(1,1) NOT NULL,
	[nombre_cat_producto] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Categoria_Producto] PRIMARY KEY CLUSTERED 
(
	[cod_cat_producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Negocio]    Script Date: 15/05/2015 14:45:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Negocio](
	[cod_negocio] [int] IDENTITY(1,1) NOT NULL,
	[nit_negocio] [nvarchar](50) NOT NULL,
	[nombre_negocio] [nvarchar](50) NOT NULL,
	[ciudad] [nvarchar](50) NOT NULL,
	[direccion] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Negocio] PRIMARY KEY CLUSTERED 
(
	[cod_negocio] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Pedido]    Script Date: 15/05/2015 14:45:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedido](
	[fecha] [datetime] NOT NULL,
	[cod_pedido] [int] IDENTITY(1,1) NOT NULL,
	[cod_negocio] [int] NOT NULL,
	[cod_usuario] [int] NOT NULL,
	[total_pedido] [int] NULL,
 CONSTRAINT [PK_Pedido_1] PRIMARY KEY CLUSTERED 
(
	[cod_pedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PedidoProducto]    Script Date: 15/05/2015 14:45:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PedidoProducto](
	[cod_producto] [int] NOT NULL,
	[cod_pedido] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
 CONSTRAINT [PK_PedidoProducto] PRIMARY KEY CLUSTERED 
(
	[cod_producto] ASC,
	[cod_pedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Perfil]    Script Date: 15/05/2015 14:45:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Perfil](
	[cod_perfil] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Perfil] PRIMARY KEY CLUSTERED 
(
	[cod_perfil] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Producto]    Script Date: 15/05/2015 14:45:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Producto](
	[cod_producto] [int] IDENTITY(1,1) NOT NULL,
	[nombre_producto] [nvarchar](50) NOT NULL,
	[cantidad] [int] NOT NULL,
	[precio] [int] NOT NULL,
	[descripcion_producto] [nvarchar](50) NOT NULL,
	[ruta_foto] [nvarchar](max) NOT NULL,
	[cod_usuario] [int] NOT NULL,
	[cod_cat_producto] [int] NOT NULL,
 CONSTRAINT [PK_Producto_1] PRIMARY KEY CLUSTERED 
(
	[cod_producto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 15/05/2015 14:45:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[cod_usuario] [int] IDENTITY(1,1) NOT NULL,
	[nombre_usuario] [nvarchar](50) NOT NULL,
	[contrasena_usuario] [nvarchar](50) NOT NULL,
	[nombre] [nvarchar](50) NOT NULL,
	[correo] [nvarchar](50) NOT NULL,
	[telefono] [nvarchar](50) NOT NULL,
	[ruta_foto] [nvarchar](50) NOT NULL,
	[cod_perfil] [int] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[cod_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Categoria_Producto] ON 

INSERT [dbo].[Categoria_Producto] ([cod_cat_producto], [nombre_cat_producto]) VALUES (1, N'Dulces')
INSERT [dbo].[Categoria_Producto] ([cod_cat_producto], [nombre_cat_producto]) VALUES (2, N'Aseo')
INSERT [dbo].[Categoria_Producto] ([cod_cat_producto], [nombre_cat_producto]) VALUES (3, N'Belleza')
INSERT [dbo].[Categoria_Producto] ([cod_cat_producto], [nombre_cat_producto]) VALUES (4, N'Papeleria')
INSERT [dbo].[Categoria_Producto] ([cod_cat_producto], [nombre_cat_producto]) VALUES (5, N'Ferreteria')
SET IDENTITY_INSERT [dbo].[Categoria_Producto] OFF
SET IDENTITY_INSERT [dbo].[Negocio] ON 

INSERT [dbo].[Negocio] ([cod_negocio], [nit_negocio], [nombre_negocio], [ciudad], [direccion]) VALUES (1, N'1020304050', N'Surtimedellin', N'Cali', N'Calle 1 3-45')
INSERT [dbo].[Negocio] ([cod_negocio], [nit_negocio], [nombre_negocio], [ciudad], [direccion]) VALUES (2, N'6070809010', N'Mercaplaza', N'Cali', N'Calle 2 4-50')
INSERT [dbo].[Negocio] ([cod_negocio], [nit_negocio], [nombre_negocio], [ciudad], [direccion]) VALUES (3, N'1001101201', N'Exito', N'Cali', N'Calle 6 8-28')
INSERT [dbo].[Negocio] ([cod_negocio], [nit_negocio], [nombre_negocio], [ciudad], [direccion]) VALUES (4, N'2013014015', N'Super Esquina', N'Cali', N'Calle 13 4-67')
INSERT [dbo].[Negocio] ([cod_negocio], [nit_negocio], [nombre_negocio], [ciudad], [direccion]) VALUES (5, N'0160170180', N'El Regalaton', N'Jamundi', N'Calle 3 18-20')
INSERT [dbo].[Negocio] ([cod_negocio], [nit_negocio], [nombre_negocio], [ciudad], [direccion]) VALUES (6, N'1020304050', N'La Terraza', N'yumbo', N'Calle 34 100-50')
INSERT [dbo].[Negocio] ([cod_negocio], [nit_negocio], [nombre_negocio], [ciudad], [direccion]) VALUES (7, N'6070809010', N'El Trebol', N'Yumbo', N'Calle 22 10-9')
INSERT [dbo].[Negocio] ([cod_negocio], [nit_negocio], [nombre_negocio], [ciudad], [direccion]) VALUES (8, N'1001101201', N'Mercagrande', N'Cali', N'Calle 70 34-20')
INSERT [dbo].[Negocio] ([cod_negocio], [nit_negocio], [nombre_negocio], [ciudad], [direccion]) VALUES (9, N'2013014015', N'El Gran Mercado', N'Jamundi', N'Carrera 44 9-8')
INSERT [dbo].[Negocio] ([cod_negocio], [nit_negocio], [nombre_negocio], [ciudad], [direccion]) VALUES (10, N'0160170180', N'La Tiendita', N'Cali', N'Calle 21 7-60')
SET IDENTITY_INSERT [dbo].[Negocio] OFF
SET IDENTITY_INSERT [dbo].[Pedido] ON 

INSERT [dbo].[Pedido] ([fecha], [cod_pedido], [cod_negocio], [cod_usuario], [total_pedido]) VALUES (CAST(0x0000A49800E85894 AS DateTime), 1, 1, 1, 585300)
INSERT [dbo].[Pedido] ([fecha], [cod_pedido], [cod_negocio], [cod_usuario], [total_pedido]) VALUES (CAST(0x0000A49801094E14 AS DateTime), 2, 1, 1, 345300)
INSERT [dbo].[Pedido] ([fecha], [cod_pedido], [cod_negocio], [cod_usuario], [total_pedido]) VALUES (CAST(0x0000A4980130F750 AS DateTime), 3, 2, 1, 794300)
INSERT [dbo].[Pedido] ([fecha], [cod_pedido], [cod_negocio], [cod_usuario], [total_pedido]) VALUES (CAST(0x0000A4980130F750 AS DateTime), 4, 2, 1, 668000)
INSERT [dbo].[Pedido] ([fecha], [cod_pedido], [cod_negocio], [cod_usuario], [total_pedido]) VALUES (CAST(0x0000A4980130F750 AS DateTime), 5, 2, 1, 480000)
INSERT [dbo].[Pedido] ([fecha], [cod_pedido], [cod_negocio], [cod_usuario], [total_pedido]) VALUES (CAST(0x0000A4980130F750 AS DateTime), 6, 3, 1, 30000)
INSERT [dbo].[Pedido] ([fecha], [cod_pedido], [cod_negocio], [cod_usuario], [total_pedido]) VALUES (CAST(0x0000A4980130F750 AS DateTime), 7, 3, 1, 35000)
INSERT [dbo].[Pedido] ([fecha], [cod_pedido], [cod_negocio], [cod_usuario], [total_pedido]) VALUES (CAST(0x0000A4980130F750 AS DateTime), 8, 5, 1, 212500)
INSERT [dbo].[Pedido] ([fecha], [cod_pedido], [cod_negocio], [cod_usuario], [total_pedido]) VALUES (CAST(0x0000A4980130F750 AS DateTime), 9, 6, 1, 1164000)
INSERT [dbo].[Pedido] ([fecha], [cod_pedido], [cod_negocio], [cod_usuario], [total_pedido]) VALUES (CAST(0x0000A4980130F750 AS DateTime), 10, 7, 1, 1018500)
SET IDENTITY_INSERT [dbo].[Pedido] OFF
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (1, 1, 10)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (3, 1, 14)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (4, 1, 13)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (12, 1, 6)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (13, 1, 8)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (14, 1, 19)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (3, 2, 14)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (5, 2, 5)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (14, 2, 19)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (2, 3, 20)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (12, 3, 50)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (19, 3, 13)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (20, 3, 14)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (2, 4, 15)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (12, 4, 13)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (19, 4, 40)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (20, 4, 20)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (2, 5, 27)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (12, 5, 12)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (20, 5, 14)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (6, 6, 13)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (9, 6, 12)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (10, 6, 4)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (11, 6, 5)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (6, 7, 20)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (9, 7, 12)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (11, 7, 15)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (8, 8, 30)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (15, 8, 48)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (16, 8, 23)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (7, 9, 32)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (17, 9, 45)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (7, 10, 35)
INSERT [dbo].[PedidoProducto] ([cod_producto], [cod_pedido], [cantidad]) VALUES (17, 10, 28)

SET IDENTITY_INSERT [dbo].[Perfil] ON 

INSERT [dbo].[Perfil] ([cod_perfil], [nombre]) VALUES (1, N'Jefe de Produccion')
INSERT [dbo].[Perfil] ([cod_perfil], [nombre]) VALUES (2, N'Vendedor')
SET IDENTITY_INSERT [dbo].[Perfil] OFF
SET IDENTITY_INSERT [dbo].[Producto] ON 

INSERT [dbo].[Producto] ([cod_producto], [nombre_producto], [cantidad], [precio], [descripcion_producto], [ruta_foto], [cod_usuario], [cod_cat_producto]) VALUES (1, N'Colgate', 60, 2500, N'Crema dental', N'ImagenesProducto/1.jpg', 2, 2)
INSERT [dbo].[Producto] ([cod_producto], [nombre_producto], [cantidad], [precio], [descripcion_producto], [ruta_foto], [cod_usuario], [cod_cat_producto]) VALUES (2, N'Delineador', 100, 8000, N'Maquillaje', N'ImagenesProducto/2.jpg', 2, 3)
INSERT [dbo].[Producto] ([cod_producto], [nombre_producto], [cantidad], [precio], [descripcion_producto], [ruta_foto], [cod_usuario], [cod_cat_producto]) VALUES (3, N'Ariel', 50, 12000, N'Detergente en polvo', N'ImagenesProducto/3.jpg', 2, 2)
INSERT [dbo].[Producto] ([cod_producto], [nombre_producto], [cantidad], [precio], [descripcion_producto], [ruta_foto], [cod_usuario], [cod_cat_producto]) VALUES (4, N'Fab', 50, 11000, N'Detergente en polvo', N'ImagenesProducto/4.jpg', 2, 2)
INSERT [dbo].[Producto] ([cod_producto], [nombre_producto], [cantidad], [precio], [descripcion_producto], [ruta_foto], [cod_usuario], [cod_cat_producto]) VALUES (5, N'Top', 50, 10000, N'Detergente en polvo', N'ImagenesProducto/5.jpg', 2, 2)
INSERT [dbo].[Producto] ([cod_producto], [nombre_producto], [cantidad], [precio], [descripcion_producto], [ruta_foto], [cod_usuario], [cod_cat_producto]) VALUES (6, N'Doritos', 60, 1000, N'Mecato en paquete', N'ImagenesProducto/6.jpg', 2, 1)
INSERT [dbo].[Producto] ([cod_producto], [nombre_producto], [cantidad], [precio], [descripcion_producto], [ruta_foto], [cod_usuario], [cod_cat_producto]) VALUES (7, N'Alicate', 80, 19500, N'Herramienta de mano', N'ImagenesProducto/7.jpg', 2, 5)
INSERT [dbo].[Producto] ([cod_producto], [nombre_producto], [cantidad], [precio], [descripcion_producto], [ruta_foto], [cod_usuario], [cod_cat_producto]) VALUES (8, N'Cuaderno', 100, 2000, N'Util escolar', N'ImagenesProducto/8.jpg', 2, 4)
INSERT [dbo].[Producto] ([cod_producto], [nombre_producto], [cantidad], [precio], [descripcion_producto], [ruta_foto], [cod_usuario], [cod_cat_producto]) VALUES (9, N'Maragrita Pollo', 100, 1000, N'Papas fritas sabor a pollo', N'ImagenesProducto/9.jpg', 2, 1)
INSERT [dbo].[Producto] ([cod_producto], [nombre_producto], [cantidad], [precio], [descripcion_producto], [ruta_foto], [cod_usuario], [cod_cat_producto]) VALUES (10, N'Margarita Limon', 100, 1000, N'Papas fritas sabor a limon', N'ImagenesProducto/10.jpg', 2, 1)
INSERT [dbo].[Producto] ([cod_producto], [nombre_producto], [cantidad], [precio], [descripcion_producto], [ruta_foto], [cod_usuario], [cod_cat_producto]) VALUES (11, N'Bombones', 60, 200, N'Dulce', N'ImagenesProducto/11.jpg', 2, 1)
INSERT [dbo].[Producto] ([cod_producto], [nombre_producto], [cantidad], [precio], [descripcion_producto], [ruta_foto], [cod_usuario], [cod_cat_producto]) VALUES (12, N'Desodorante', 100, 8000, N'Producto de cuidado personal', N'ImagenesProducto/12.jpg', 2, 2)
INSERT [dbo].[Producto] ([cod_producto], [nombre_producto], [cantidad], [precio], [descripcion_producto], [ruta_foto], [cod_usuario], [cod_cat_producto]) VALUES (13, N'Jabon Azul', 70, 3000, N'Jabon en barra para la ropa', N'ImagenesProducto/13.jpg', 2, 2)
INSERT [dbo].[Producto] ([cod_producto], [nombre_producto], [cantidad], [precio], [descripcion_producto], [ruta_foto], [cod_usuario], [cod_cat_producto]) VALUES (14, N'Jabon Miel', 60, 6700, N'Jabon de baño', N'ImagenesProducto/14.jpg', 2, 2)
INSERT [dbo].[Producto] ([cod_producto], [nombre_producto], [cantidad], [precio], [descripcion_producto], [ruta_foto], [cod_usuario], [cod_cat_producto]) VALUES (15, N'Lapicero Pro', 60, 1500, N'Lapicero de punta fina', N'ImagenesProducto/15.jpg', 2, 4)
INSERT [dbo].[Producto] ([cod_producto], [nombre_producto], [cantidad], [precio], [descripcion_producto], [ruta_foto], [cod_usuario], [cod_cat_producto]) VALUES (16, N'Portaminas', 100, 3500, N'Lapiz de minas', N'ImagenesProducto/16.jpg', 2, 4)
INSERT [dbo].[Producto] ([cod_producto], [nombre_producto], [cantidad], [precio], [descripcion_producto], [ruta_foto], [cod_usuario], [cod_cat_producto]) VALUES (17, N'Martillo', 50, 12000, N'Herramientas de mano', N'ImagenesProducto/17.jpg', 2, 5)
INSERT [dbo].[Producto] ([cod_producto], [nombre_producto], [cantidad], [precio], [descripcion_producto], [ruta_foto], [cod_usuario], [cod_cat_producto]) VALUES (18, N'Borrador Nata', 80, 500, N'Borrador', N'ImagenesProducto/18.jpg', 2, 4)
INSERT [dbo].[Producto] ([cod_producto], [nombre_producto], [cantidad], [precio], [descripcion_producto], [ruta_foto], [cod_usuario], [cod_cat_producto]) VALUES (19, N'Labial Intenso', 70, 5100, N'Labial', N'ImagenesProducto/19.jpg', 2, 3)
INSERT [dbo].[Producto] ([cod_producto], [nombre_producto], [cantidad], [precio], [descripcion_producto], [ruta_foto], [cod_usuario], [cod_cat_producto]) VALUES (20, N'Sombras', 100, 12000, N'Sombras para el rostro', N'ImagenesProducto/20.jpg', 2, 3)

SET IDENTITY_INSERT [dbo].[Producto] OFF
SET IDENTITY_INSERT [dbo].[Usuario] ON 

INSERT [dbo].[Usuario] ([cod_usuario], [nombre_usuario], [contrasena_usuario], [nombre], [correo], [telefono], [ruta_foto], [cod_perfil]) VALUES (1, N'david_fza', N'1234542345', N'David Herrera', N'david@gmail.com', N'4376548', N'ImagenesUsuario/1.jpg', 2)
INSERT [dbo].[Usuario] ([cod_usuario], [nombre_usuario], [contrasena_usuario], [nombre], [correo], [telefono], [ruta_foto], [cod_perfil]) VALUES (2, N'jefe_prod', N'2146404580', N'Ernesto Lopez', N'ernesto@gmail.com', N'5378548', N'ImagenesUsuario/2.jpg', 1)
SET IDENTITY_INSERT [dbo].[Usuario] OFF
ALTER TABLE [dbo].[Auditoria]  WITH CHECK ADD  CONSTRAINT [FK_Auditoria_Usuario1] FOREIGN KEY([cod_usuario])
REFERENCES [dbo].[Usuario] ([cod_usuario])
GO
ALTER TABLE [dbo].[Auditoria] CHECK CONSTRAINT [FK_Auditoria_Usuario1]
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD  CONSTRAINT [FK_Pedido_Negocio] FOREIGN KEY([cod_negocio])
REFERENCES [dbo].[Negocio] ([cod_negocio])
GO
ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK_Pedido_Negocio]
GO
ALTER TABLE [dbo].[Pedido]  WITH CHECK ADD  CONSTRAINT [FK_Pedido_Usuario] FOREIGN KEY([cod_usuario])
REFERENCES [dbo].[Usuario] ([cod_usuario])
GO
ALTER TABLE [dbo].[Pedido] CHECK CONSTRAINT [FK_Pedido_Usuario]
GO
ALTER TABLE [dbo].[PedidoProducto]  WITH CHECK ADD  CONSTRAINT [FK_PedidoProducto_Pedido] FOREIGN KEY([cod_pedido])
REFERENCES [dbo].[Pedido] ([cod_pedido])
GO
ALTER TABLE [dbo].[PedidoProducto] CHECK CONSTRAINT [FK_PedidoProducto_Pedido]
GO
ALTER TABLE [dbo].[PedidoProducto]  WITH CHECK ADD  CONSTRAINT [FK_PedidoProducto_Producto] FOREIGN KEY([cod_producto])
REFERENCES [dbo].[Producto] ([cod_producto])
GO
ALTER TABLE [dbo].[PedidoProducto] CHECK CONSTRAINT [FK_PedidoProducto_Producto]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Categoria_Producto1] FOREIGN KEY([cod_cat_producto])
REFERENCES [dbo].[Categoria_Producto] ([cod_cat_producto])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_Categoria_Producto1]
GO
ALTER TABLE [dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Usuario] FOREIGN KEY([cod_usuario])
REFERENCES [dbo].[Usuario] ([cod_usuario])
GO
ALTER TABLE [dbo].[Producto] CHECK CONSTRAINT [FK_Producto_Usuario]
GO
ALTER TABLE [dbo].[Usuario]  WITH CHECK ADD  CONSTRAINT [FK_Usuario_Perfil] FOREIGN KEY([cod_perfil])
REFERENCES [dbo].[Perfil] ([cod_perfil])
GO
ALTER TABLE [dbo].[Usuario] CHECK CONSTRAINT [FK_Usuario_Perfil]
GO
USE [master]
GO
ALTER DATABASE [Sistema_ventas] SET  READ_WRITE 
GO
