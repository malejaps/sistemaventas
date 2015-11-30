/*
* NombreClase: DAL
* Autores: David Zuluaga - 1310294, Edwin Gamboa -1310233
* Fecha: 12/May/2015
* Descripcion: Clase que contiene los metodos de consultas  y mapeos de la base de datos a entidades, y de entidades a BD 
* que se requieren para mostrar información de pedidos, negocios y productos en el desconectado.
*/


/*
* Pedido CrearPedido(PedidoEntidad pedidos)
* ObservableCollection<PedidoEntidad> consultarPedidoPorNegocio(NegocioEntidad negocio)
* ObservableCollection<NegocioEntidad> consultarNegocioPorNombre(string nombre_negocio)
* ObservableCollection<ProductoEntidad> consultarProductoPorNombre(string nombre_producto)
* ObservableCollection<ProductoEntidad> consultarProductoPorCategoria(string nombreCategoria)
* ObservableCollection<ProductoEntidad> consultarProductoPorNombreYCategoria(string nombreCategoria, string nombre_producto)
* ObservableCollection<UsuarioEntidad> consultarVendedorPorCodigo(int cod_usuario)
* ObservableCollection<Categoria_ProductoEntidad> listarCategorias()
* Auditoria mapearAuditoriaDeEntidadesAeF(AuditoriaEntidad item)
* Categoria_Producto mapearCategoriaProductoDeEntidadesAeF(CategoriaProductoEntidad item)
* Negocio mapearNegocioDeEntidadesAef(NegocioEntidad item)
* Pedido mapearPedidoDeEntidadesAEf(PedidoEntidad item)
* PedidoProducto mapearPedidoProductoDeEntidadesAeF(PedidoProductoEntidad item)
* Perfil mapearPerfilDeEntidadesAeF(PerfilEntidad item)
* Producto mapearProductoDeEntidadesAef(ProductoEntidad item)
* Usuario mapearUsuarioDeEntidadesAeF(UsuarioEntidad item)
* AuditoriaEntidad mapearAuditoriaDeEfAEntidades(Auditoria item)
* Categoria_ProductoEntidad mapearCategoriaProductoDeEfAEntidades(Categoria_Producto item)
* NegocioEntidad mapearNegocioDeEntidadesAef(Negocio item)
* PedidoEntidad mapearPedidoDeEfAEntidades(Pedido item)
* PedidoProductoEntidad mapearPedidoProductoDeAeFAEntidades(PedidoProducto item)
* PerfilEntidad mapearPerfilDeEfAEntidades(Perfil item)
* ProductoEntidad mapearProductoDeEntidadesAef(Producto item)
* UsuarioEntidad mapearUsuarioDeEfAEntidades(Usuario item)
* ObservableCollection<ProductoEntidad> consultarNuevosProductos(int codigoUltimoProducto)
* ObservableCollection<NegocioEntidad> consultarNuevosNegocios(int codigoUltimoNegocio)
* ObservableCollection<PedidoEntidad> consultarNuevosPedidos()
* Producto crearProducto(ProductoEntidad producto)
* Negocio crearNegocio(NegocioEntidad negocio)
* ProductoEntidad ultimoProducto()
* NegocioEntidad ultimoNegocio()
* void cambiarEstadoPedido(PedidoEntidad pedido)
* Auditoria crearAuditoria(AuditoriaEntidad auditoria)
* AuditoriaEntidad mapearAuditoriaDeEfAEntidades(Auditoria item)
* Auditoria mapearAuditoriaDeEntidadesAEF(AuditoriaEntidad item)
* bool haSincronizadoEnEstaFecha(DateTime fecha)
* List<string> leerArchivo(string rutaArchivo)
* ObservableCollection<PedidoEntidad> consultarPedidoPorIntervaloDeTiempo(DateTime inicio, DateTime fin)
* ObservableCollection<PedidoEntidad> consultarPedidoPorFecha(DateTime fecha)
* void editarProducto(ProductoEntidad producto)
* Usuario crearUsuario(UsuarioEntidad usuario)
* void editarUsuario(UsuarioEntidad usuario)
* Perfil crearPerfil(PerfilEntidad perfil)
* void editarPerfil(PerfilEntidad perfil)
* UsuarioEntidad autenticarUsuario(string nombreUsuario, string contrasenia)
* bool haHechoPedido(int codNegocio, DateTime fecha)
* void editarPrecioProducto(ProductoEntidad producto)
* ObservableCollection<UsuarioEntidad> consultarUsuarioPorNombre(string nombre_usuario)
* ObservableCollection<UsuarioEntidad> consultarTodosLosUsuarios()
* ObservableCollection<PerfilEntidad> consultarPerfilPorNombre(string nombre_perfil)
* ObservableCollection<PerfilEntidad> consultarTodosLosPerfiles()
* PerfilEntidad consultarPerfilPorCodigo(int cod_perfil)
* ObservableCollection<PerfilEntidad> consultarTodosLosPerfiles()
* PerfilEntidad consultarPerfilPorCodigo(int cod_perfil)
* UsuarioEntidad consultarUsuarioPorCodigo(int cod_usuario)
* ProductoEntidad consultarProductoPorCodigo(int cod_producto)
* ObservableCollection<ProductoEntidad> consultarInventario()
* ObservableCollection<UsuarioEntidad> consultarVendedoresNoSincronizados(DateTime fecha)
* List<string> consultarCantidadPedidoPorProducto(DateTime inicio, DateTime fin)
* ObservableCollection<PedidoEntidad> consultarPedidoPorCiudad(string ciudad, DateTime inicio, DateTime fin)
* ObservableCollection<PedidoEntidad> consultarPedidoDeUnaCiudad(string ciudad, DateTime inicio, DateTime fin)
* ObservableCollection<ProductoEntidad> consultarProductoDisponiblePorNombre(string nombre_producto)
* ObservableCollection<ProductoEntidad> consultarProductoDisponiblePorCategoria(string nombreCategoria)
* ObservableCollection<ProductoEntidad> consultarProductoDisponiblePorNombreYCategoria(string nombreCategoria, string nombre_producto)
* List<String> consultarCantidadPedidosPorDiasSemana(DateTime inicio, DateTime fin)
*/

using SistemaVentasEntidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Objects;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentasDAL
{
    public class DAL
    {

        /*
         *Propósito: Permite crear un pedido a la base de datos.
         *Entradas: PedidoEntidad 
         *Salidas: Pedido
         */
        //#Metodo: crearPedido
        public Pedido crearPedido(PedidoEntidad pedidos)
        {
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                //pedidos.Estado = "creado";
                Pedido ped = new Pedido();
                ped = mapearPedidoDeEntidadesAEf(pedidos);


                foreach (var productoPedido in pedidos.PedidoProducto)
                {
                    var pro = contexto.Producto.Where(p => p.cod_producto == productoPedido.CodProducto).FirstOrDefault();
                    if (pro != null)
                    {
                        pro.cantidad = pro.cantidad - productoPedido.Cantidad;
                    }

                }

                contexto.Pedido.Add(ped);
                contexto.SaveChanges();

                return ped;
            }

        }


        /*
         *Propósito: Permite consultar un pedido por el nombre de un negocio a la base de datos.
         *Entradas: NegocioEntidad 
         *Salidas: ObservableCollection<PedidoEntidad>
         */
        //#Metodo: consultarPedidoPorNegocio
        public ObservableCollection<PedidoEntidad> consultarPedidoPorNegocio(NegocioEntidad negocio)
        {
            ObservableCollection<PedidoEntidad> respuesta = new ObservableCollection<PedidoEntidad>();
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                //los traigo de la base de datos
                var pedidosNegocio = from ped in contexto.Pedido
                                     where ped.Negocio.cod_negocio == negocio.CodNegocio
                                     select ped;
                //mapeo lo que me devuelve la base de datos a entidades
                foreach (var item in pedidosNegocio)
                {
                    PedidoEntidad actual = mapearPedidoDeEfAEntidades(item);
                    respuesta.Add(actual);
                }
            }
            return respuesta;

        }

        
        /*
       *Propósito: Permite consultar un negocio por su nombre a la base de datos.
       *Entradas: string nombre_negocio
       *Salidas: ObservableCollection<NegocioEntidad>
       */
        //#Metodo: consultarNegocioPorNombre
        public ObservableCollection<NegocioEntidad> consultarNegocioPorNombre(string nombre_negocio)
        {
            ObservableCollection<NegocioEntidad> respuesta = new ObservableCollection<NegocioEntidad>();
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                var almacen = from neg in contexto.Negocio
                              where neg.nombre_negocio.Contains(nombre_negocio)
                              select neg;
                foreach (var item in almacen)
                {
                    NegocioEntidad actual = mapearNegocioDeEfAEntidades(item);
                    respuesta.Add(actual);
                }
            }
            return respuesta;

        }


        /*
       *Propósito: Permite consultar un producto por su nombre a la base de datos.
       *Entradas: string nombre_producto
       *Salidas: ObservableCollection<ProductoEntidad>
       */
        //#Metodo: ConsultarProductoPorNombre
        public ObservableCollection<ProductoEntidad> ConsultarProductoPorNombre(string nombre_producto)
        {
            ObservableCollection<ProductoEntidad> respuesta = new ObservableCollection<ProductoEntidad>();
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                var producto = from pro in contexto.Producto
                              where pro.nombre_producto.Contains(nombre_producto)
                              select pro;
                foreach (var item in producto)
                {
                    ProductoEntidad actual = mapearProductoDeEfAEntidades(item);
                    respuesta.Add(actual);
                }
            }
            return respuesta;

        }

        /*
         *Propósito: Permite consultar un producto por su categoria a la base de datos.
         *Entradas: Categoria_ProductoEntidad categoria
         *Salidas: ObservableCollection<ProductoEntidad>
        */
        //#Metodo: consultarProductoPorCategoria
        public ObservableCollection<ProductoEntidad> consultarProductoPorCategoria(string nombreCategoria)
        {
            ObservableCollection<ProductoEntidad> respuesta = new ObservableCollection<ProductoEntidad>();
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                var producto = from pro in contexto.Producto
                               where pro.Categoria_Producto.nombre_cat_producto == nombreCategoria
                               select pro;
                foreach (var item in producto)
                {
                    ProductoEntidad actual = mapearProductoDeEfAEntidades(item);
                    respuesta.Add(actual);
                }
            }
            return respuesta;

        }

        /*
         *Propósito: Permite consultar un producto por nombre y categoria a la base de datos.
         *Entradas: Categoria_ProductoEntidad categoria, string nombre_producto
         *Salidas: ObservableCollection<ProductoEntidad>
        */
        //#Metodo: consultarProductoPorNombreYCategoria
        public ObservableCollection<ProductoEntidad> consultarProductoPorNombreYCategoria(string nombreCategoria, string nombre_producto)
        {
            ObservableCollection<ProductoEntidad> respuesta = new ObservableCollection<ProductoEntidad>();
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                var producto = from pro in contexto.Producto
                               where pro.nombre_producto.Contains(nombre_producto )
                               && pro.Categoria_Producto.nombre_cat_producto == nombreCategoria
                               select pro;
                foreach (var item in producto)
                {
                    ProductoEntidad actual = mapearProductoDeEfAEntidades(item);
                    respuesta.Add(actual);
                }
            }
            return respuesta;

        }
        
        /*
       *Propósito: Permite consultar un vendedor por su codigo a la base de datos.
       *Entradas: int cod_usuario
       *Salidas: ObservableCollection<UsuarioEntidad>
       */
        //#Metodo: consultarVendedorPorCodigo
        public ObservableCollection<UsuarioEntidad> consultarVendedorPorCodigo(int cod_usuario)
        {
            ObservableCollection<UsuarioEntidad> respuesta = new ObservableCollection<UsuarioEntidad>();
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                var vendedor = from pro in contexto.Usuario
                               where pro.cod_usuario == cod_usuario
                               select pro;
                foreach (var item in vendedor)
                {
                    UsuarioEntidad actual = mapearUsuarioDeEfAEntidades(item);
                    respuesta.Add(actual);
                }
            }
            return respuesta;

        }

        /*
       *Propósito: Permite listar las categorias que exisen en la base de datos.
       *Entradas:
       *Salidas: ObservableCollection<Categoria_ProductoEntidad>
       */
        //#Metodo: listarCategorias
        public ObservableCollection<Categoria_ProductoEntidad> listarCategorias()
        {
            ObservableCollection<Categoria_ProductoEntidad> respuesta = new ObservableCollection<Categoria_ProductoEntidad>();
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                var categorias = from cat in contexto.Categoria_Producto
                               select cat;
                foreach (var item in categorias)
                {
                    Categoria_ProductoEntidad actual = mapearCategoriaProductoDeEfAEntidades(item);
                    respuesta.Add(actual);
                }
            }
            return respuesta;

        }

        
        //Metodos de Entidades a Entities Framework

        /*
         *Propósito: Permite mapear Auditoria a la BD.
         *Entradas: AuditoriaEntidad item
         *Salidas: Auditoria
        */
        //#Metodo: mapearAuditoriaDeEntidadesAeF
        private Auditoria mapearAuditoriaDeEntidadesAeF(AuditoriaEntidad item)
        {

            Auditoria aud = new Auditoria();
            aud.cod_auditoria = item.CodAuditoria;
            aud.cod_usuario = item.CodUsuario;
            aud.descripcion = item.Descripcion;
            
            if(item.Usuario != null){
                aud.Usuario = mapearUsuarioDeEntidadesAeF(item.Usuario);
            }             

            return aud;
        }


        /*
         *Propósito: Permite mapear categoria_producto a la BD.
         *Entradas: Categoria_ProductoEntidad item
         *Salidas: Categoria_Producto
        */
        //#Metodo: mapearCategoriaProductoDeEntidadesAeF
        private Categoria_Producto mapearCategoriaProductoDeEntidadesAeF(Categoria_ProductoEntidad item)
        {

            Categoria_Producto cat = new Categoria_Producto();
            cat.cod_cat_producto = item.CodCatProducto;
            cat.nombre_cat_producto = item.NombreCatProducto;

            return cat;

        }


        /*
        *Propósito: Permite mapear negocio a la BD.
        *Entradas: NegocioEntidad item
        *Salidas: Negocio
       */
        //#Metodo: mapearNegocioDeEntidadesAef
        private Negocio mapearNegocioDeEntidadesAef(NegocioEntidad item)
        {

            Negocio neg = new Negocio();
            neg.ciudad = item.Ciudad;
            neg.cod_negocio = item.CodNegocio;
            neg.direccion = item.Direccion;
            neg.nit_negocio = item.NitNegocio;
            neg.nombre_negocio = item.NombreNegocio;

            return neg;

        }


        /*
        *Propósito: Permite mapear pedido a la BD.
        *Entradas: NegocioEntidad item
        *Salidas: Negocio
       */
        //#Metodo: mapearPedidoDeEntidadesAEf
        private Pedido mapearPedidoDeEntidadesAEf(PedidoEntidad item)
        {
            Pedido ped = new Pedido();
            
            ped.cod_negocio = item.CodNegocio;
            ped.cod_pedido = item.CodPedido;
            ped.cod_usuario = item.CodUsuario;
            ped.fecha = item.Fecha;
            ped.total_pedido = (int)item.TotalPedido;
            ped.estado = item.Estado;
            

            if (item.Negocio != null)
            {
              ped.Negocio = mapearNegocioDeEntidadesAef(item.Negocio);
            }


            if (item.Usuario != null)
            {
                ped.Usuario = mapearUsuarioDeEntidadesAeF(item.Usuario);
            }
           
            if (item.PedidoProducto != null || item.PedidoProducto.Count != 0)
            {

                foreach (var pedidoProducto in item.PedidoProducto)
                {
                    ped.PedidoProducto.Add(mapearPedidoProductoDeEntidadesAeF(pedidoProducto));
                }
            }
                  
            return ped;
        }


        /*
        *Propósito: Permite mapear pedido_producto a la BD.
        *Entradas: PedidoProductoEntidad item
        *Salidas: PedidoProducto
       */
        //#Metodo: mapearPedidoProductoDeEntidadesAeF
        private PedidoProducto mapearPedidoProductoDeEntidadesAeF(PedidoProductoEntidad item)
        {

            PedidoProducto ped = new PedidoProducto();
            ped.cantidad = item.Cantidad;
            ped.cod_pedido = item.CodPedido;
            ped.cod_producto = item.CodProducto;
            

            /*if (item.Pedido != null)
            {
                ped.Pedido = MapearPedidoDeEntidadesAEf(item.Pedido);                
            }
            if(item.Producto != null)
            {
                ped.Producto = MapearProductoDeEntidadesAef(item.Producto);
            } */
            
            return ped;

        }

        /*
        *Propósito: Permite mapear perfil a la BD.
        *Entradas: PerfilEntidad item
        *Salidas: Perfil
       */
        //#Metodo: mapearPerfilDeEntidadesAeF
        private Perfil mapearPerfilDeEntidadesAeF(PerfilEntidad item)
        {

            Perfil per = new Perfil();
            per.cod_perfil = item.CodPerfil;
            per.nombre = item.Nombre;


            return per;


        }


        /*
        *Propósito: Permite mapear producto a la BD.
        *Entradas: ProductoEntidad item
        *Salidas: Producto
       */
        //#Metodo: mapearProductoDeEntidadesAef
        private Producto mapearProductoDeEntidadesAef(ProductoEntidad item)
        {


            Producto pro = new Producto();

            pro.cantidad = item.Cantidad;
            pro.cod_cat_producto = item.CodCatProducto;
            pro.cod_usuario = item.CodUsuario;
            pro.cod_producto = item.CodProducto;
            pro.descripcion_producto = item.DescripcionProducto;
            pro.nombre_producto = item.NombreProducto;
            pro.precio = item.Precio;
            pro.ruta_foto = item.RutaFoto;

            if(item.Categoria_Producto != null)
            {
                pro.Categoria_Producto = mapearCategoriaProductoDeEntidadesAeF(item.Categoria_Producto);                
            }
            if (item.Usuario != null)
            {
                pro.Usuario = mapearUsuarioDeEntidadesAeF(item.Usuario);
            } 
            

            return pro;
        }
        

        /*
         *Propósito: Permite mapear usuario a la BD.
         *Entradas: UsuarioEntidad item
         *Salidas: Usuario
        */
        //#Metodo: mapearUsuarioDeEntidadesAeF
        private Usuario mapearUsuarioDeEntidadesAeF(UsuarioEntidad item)
        { 
        
            Usuario user = new Usuario();
            user.cod_perfil = item.CodPerfil;
            user.cod_usuario = item.CodUsuario;
            user.contrasena_usuario = item.ContrasenaUsuario;
            user.correo = item.Correo;
            user.nombre = item.Nombre;
            user.nombre_usuario = item.NombreUsuario;
            user.telefono = item.Telefono;
            user.ruta_foto = item.RutaFoto;

            if(item.Perfil != null)
            {
                user.Perfil = mapearPerfilDeEntidadesAeF(item.Perfil);
            }
                       

            return user;
        }

        
        //Metodos de Entities Framework a Entidades
        
        /*
         *Propósito: Permite mapear usuario de la BD a entidades.
         *Entradas: Auditoria item
         *Salidas: AuditoriaEntidad
        */
        //#Metodo: mapearAuditoriaDeEfAEntidades
        private AuditoriaEntidad mapearAuditoriaDeEfAEntidades(Auditoria item)
        {

            AuditoriaEntidad aud = new AuditoriaEntidad();
            aud.CodAuditoria = item.cod_auditoria;
            aud.CodUsuario = item.cod_usuario;
            aud.Descripcion = item.descripcion;

            if (item.Usuario != null)
            {
                aud.Usuario = mapearUsuarioDeEfAEntidades(item.Usuario); 
            }

            return aud;
        }

        /*
         *Propósito: Permite mapear categoria_producto de la BD a entidades.
         *Entradas: Categoria_Producto item
         *Salidas: Categoria_ProductoEntidad
        */
        //#Metodo: mapearCategoriaProductoDeEfAEntidades
        private Categoria_ProductoEntidad mapearCategoriaProductoDeEfAEntidades(Categoria_Producto item)
        {

            Categoria_ProductoEntidad cat = new Categoria_ProductoEntidad();
            cat.CodCatProducto = item.cod_cat_producto;
            cat.NombreCatProducto = item.nombre_cat_producto; 



            return cat;

        }

        /*
         *Propósito: Permite mapear negocio de la BD a entidades.
         *Entradas: Negocio item
         *Salidas: NegocioEntidad
        */
        //#Metodo: mapearNegocioDeEfAEntidades
        private NegocioEntidad mapearNegocioDeEfAEntidades(Negocio item)
        {

            NegocioEntidad neg = new NegocioEntidad();
            neg.Ciudad = item.ciudad;
            neg.CodNegocio = item.cod_negocio;
            neg.Direccion = item.direccion;
            neg.NitNegocio = item.nit_negocio;
            neg.NombreNegocio = item.nombre_negocio; 



            return neg;

        }


        /*
         *Propósito: Permite mapear pedido de la BD a entidades.
         *Entradas: Pedido item
         *Salidas: PedidoEntidad
        */
        //#Metodo: mapearPedidoDeEfAEntidades
        private PedidoEntidad mapearPedidoDeEfAEntidades(Pedido item)
        {
            PedidoEntidad ped = new PedidoEntidad();

            ped.CodNegocio = item.cod_negocio;
            ped.CodPedido = item.cod_pedido;
            ped.CodUsuario = item.cod_usuario;
            ped.Fecha = item.fecha;
            ped.TotalPedido = (int)item.total_pedido;
            ped.Estado = item.estado;

            ped.PedidoProducto = new ObservableCollection<PedidoProductoEntidad>();

                foreach (var pedidoProducto in item.PedidoProducto)
                {
                    ped.PedidoProducto.Add(mapearPedidoProductoDeAeFAEntidades(pedidoProducto));
                }
            

            ped.Negocio = mapearNegocioDeEfAEntidades(item.Negocio);
            ped.Usuario = mapearUsuarioDeEfAEntidades(item.Usuario);
            


            return ped;
        }

        /*r
         *Propósito: Permite mapear pedido_producto de la BD a entidades.
         *Entradas: PedidoProducto item
         *Salidas: PedidoProductoEntidad
        */
        //#Metodo: mapearPedidoProductoDeAeFAEntidades
        private PedidoProductoEntidad mapearPedidoProductoDeAeFAEntidades(PedidoProducto item)
        {

            PedidoProductoEntidad ped = new PedidoProductoEntidad();
            ped.Cantidad = item.cantidad;
            ped.CodPedido = item.cod_pedido;
            ped.CodProducto = item.cod_producto;
            


            //ped.Pedido = MapearPedidoDeEfAEntidades(item.Pedido);
            ped.Producto = mapearProductoDeEfAEntidades(item.Producto);



            return ped;

        }

        /*
         *Propósito: Permite mapear perfil de la BD a entidades.
         *Entradas: Perfil item
         *Salidas: PerfilEntidad
        */
        //#Metodo: mapearPerfilDeEfAEntidades
        private PerfilEntidad mapearPerfilDeEfAEntidades(Perfil item)
        {

            PerfilEntidad per = new PerfilEntidad();
            per.CodPerfil = item.cod_perfil;
            per.Nombre = item.nombre;


            return per;


        }

        /*
         *Propósito: Permite mapear producto de la BD a entidades.
         *Entradas: Producto item
         *Salidas: ProductoEntidad
        */
        //#Metodo: mapearProductoDeEfAEntidades
        private ProductoEntidad mapearProductoDeEfAEntidades(Producto item)
        {


            ProductoEntidad pro = new ProductoEntidad();

            pro.Cantidad = item.cantidad;
            pro.CodCatProducto = item.cod_cat_producto;
            pro.CodUsuario = item.cod_usuario;
            pro.CodProducto = item.cod_producto;
            pro.DescripcionProducto = item.descripcion_producto;
            pro.NombreProducto = item.nombre_producto;
            pro.Precio = item.precio;
            pro.RutaFoto = item.ruta_foto;

            pro.Categoria_Producto = mapearCategoriaProductoDeEfAEntidades(item.Categoria_Producto);
            pro.Usuario = mapearUsuarioDeEfAEntidades(item.Usuario);
            

            return pro;
        }

        /*
         *Propósito: Permite mapear usuario de la BD a entidades.
         *Entradas: Usuario item
         *Salidas: UsuarioEntidad
        */
        //#Metodo: mapearUsuarioDeEfAEntidades
        private UsuarioEntidad mapearUsuarioDeEfAEntidades(Usuario item)
        {

            UsuarioEntidad user = new UsuarioEntidad();
            user.CodPerfil = item.cod_perfil;
            user.CodUsuario = item.cod_usuario;
            user.ContrasenaUsuario = item.contrasena_usuario;
            user.Correo = item.correo;
            user.Nombre = item.nombre;
            user.NombreUsuario = item.nombre_usuario;
            user.Telefono = item.telefono;
            user.RutaFoto = item.ruta_foto;

            user.Perfil = mapearPerfilDeEfAEntidades(item.Perfil);

            return user;
        }

        //******ENTREA2EDWIN******

        /*
         *Propósito: Permite consultar los productos con codigo mayor a codigoUltimoProducto.
         *Entradas: codigoUltimoProducto
         *Salidas: ObservableCollection<ProductoEntidad>
         */
        //#Metodo: consultarNuevosProductos
        public ObservableCollection<ProductoEntidad> consultarNuevosProductos(int codigoUltimoProducto)
        {
            ObservableCollection<ProductoEntidad> respuesta = new ObservableCollection<ProductoEntidad>();
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                //los traigo de la base de datos
                var nuevosProductos = from pro in contexto.Producto
                                     where pro.cod_producto > codigoUltimoProducto
                                     select pro;
                //mapeo lo que me devuelve la base de datos a entidades
                foreach (var item in nuevosProductos)
                {
                    ProductoEntidad actual = mapearProductoDeEfAEntidades(item);
                    respuesta.Add(actual);
                }
            }
            return respuesta;
        }

        /*
         *Propósito: Permite consultar los negocios con codigo mayor a codigoUltimoNegocio.
         *Entradas: codigoUltimoNegocio
         *Salidas: ObservableCollection<NegocioEntidad>
         */
        //#Metodo: consultarNuevosNegocios
        public ObservableCollection<NegocioEntidad> consultarNuevosNegocios(int codigoUltimoNegocio)
        {
            ObservableCollection<NegocioEntidad> respuesta = new ObservableCollection<NegocioEntidad>();
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                //los traigo de la base de datos
                var nuevosNegocios = from neg in contexto.Negocio
                                      where neg.cod_negocio > codigoUltimoNegocio
                                      select neg;
                //mapeo lo que me devuelve la base de datos a entidades
                foreach (var item in nuevosNegocios)
                {
                    NegocioEntidad actual = mapearNegocioDeEfAEntidades(item);
                    respuesta.Add(actual);
                }
            }
            return respuesta;
        }

        /*
         *Propósito: Permite consultar los pedidos con estado igual a creado, es decir
         *pedidos que no se ha sincronizado.
         *Entradas:
         *Salidas: ObservableCollection<PedidoEntidad>
         */
        //#Metodo: consultarNuevosPedidos
        public ObservableCollection<PedidoEntidad> consultarNuevosPedidos()
        {
            ObservableCollection<PedidoEntidad> respuesta = new ObservableCollection<PedidoEntidad>();
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                //los traigo de la base de datos
                var nuevosPedidos = from ped in contexto.Pedido
                                     where ped.estado == "creado"
                                     select ped;
                //mapeo lo que me devuelve la base de datos a entidades
                foreach (var item in nuevosPedidos)
                {
                    PedidoEntidad actual = mapearPedidoDeEfAEntidades(item);
                    respuesta.Add(actual);
                }
            }
            return respuesta;
        }

        /*
         *Propósito: Permite crear un producto en la base de datos.
         *Entradas: ProductoEntidad 
         *Salidas: Producto
         */
        //#Metodo: crearProducto
        public Producto crearProducto(ProductoEntidad producto)
        {
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                Producto pro = new Producto();
                pro = mapearProductoDeEntidadesAef(producto);
                contexto.Producto.Add(pro);
                contexto.SaveChanges();
                return pro;
            }
        }

        /*
         *Propósito: Permite crear un negocio en la base de datos.
         *Entradas: NegocioEntidad 
         *Salidas: Negocio
         */
        //#Metodo: crearNegocio
        public Negocio crearNegocio(NegocioEntidad negocio)
        {
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                Negocio neg = new Negocio();
                neg = mapearNegocioDeEntidadesAef(negocio);
                contexto.Negocio.Add(neg);
                contexto.SaveChanges();
                return neg;
            }
        }

        /*
         *Propósito: Permite consultar el ultimo producto presente en la BD.
         *Entradas:  
         *Salidas: ProductoEntidad
         */
        //#Metodo: ultimoProducto
        public ProductoEntidad ultimoProducto()
        {
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                ProductoEntidad pro = new ProductoEntidad();
                pro = mapearProductoDeEfAEntidades(contexto.Producto.OrderByDescending(p => p.cod_producto).FirstOrDefault());
                return pro;
            }
        }

        /*
         *Propósito: Permite consultar el ultimo negocio presente en la BD.
         *Entradas:  
         *Salidas: NegocioEntidad
         */
        //#Metodo: ultimoNegocio
        public NegocioEntidad ultimoNegocio()
        {
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                NegocioEntidad neg = new NegocioEntidad();
                neg = mapearNegocioDeEfAEntidades(contexto.Negocio.OrderByDescending(n => n.cod_negocio).FirstOrDefault());
                return neg;
            }
        }

        /*
        *Propósito: Permite modificar el estado de un pedido en la BD
        *Entradas:  
        *Salidas: PedidoEntidad
        */
        //#Metodo: cambiarEstadoPedido
        public void cambiarEstadoPedido(PedidoEntidad pedido)
        {
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                var ped = contexto.Pedido.Where(p => p.cod_pedido == pedido.CodPedido).FirstOrDefault();

                if (ped != null)
                {
                    ped.estado = pedido.Estado;
                    contexto.SaveChanges();
                }
            }
        }

        /*
         *Propósito: Permite crear un registro en la tabla Auditoria a la base de datos.
         *Entradas: AuditoriaEntidad 
         *Salidas: Auditoria
         */
        //#Metodo: crearAuditoria
        public Auditoria crearAuditoria(AuditoriaEntidad auditoria)
        {
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                //pedidos.Estado = "creado";
                Auditoria aud = new Auditoria();
                aud = mapearAuditoriaDeEntidadesAEF(auditoria);
                contexto.Auditoria.Add(aud);
                contexto.SaveChanges();

                return aud;
            }

        }

        /*
         *Propósito: Permite mapear audotoria de entidades a la BD.
         *Entradas: AuditoriaEntidad
         *Salidas: Auditoria
        */
        //#Metodo: mapearAuditoriaDeEntidadesAEF
        private Auditoria mapearAuditoriaDeEntidadesAEF(AuditoriaEntidad item)
        {

            Auditoria aud = new Auditoria();
            aud.cod_auditoria = item.CodAuditoria;
            aud.descripcion = item.Descripcion;
            aud.cod_usuario = item.CodUsuario;
            aud.fecha = item.Fecha; 

            if (item.Usuario != null)
            {
                aud.Usuario = mapearUsuarioDeEntidadesAeF(item.Usuario);
            }

            return aud;
        }

        /*
         *Propósito: Permite sber si se ha ralizado una sincrnizacio en la fecha dada
         *Entradas: DateTime
         *Salidas: bool
        */
        //#Metodo: haSincronizadoEnEstaFecha
        public bool haSincronizadoEnEstaFecha(DateTime fecha){
            DateTime fechaUndiaDepues = fecha.Date.AddDays(1);
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                //los traigo de la base de datos
                var auditorias = from aud in contexto.Auditoria
                                 where aud.fecha >= fecha.Date && aud.fecha < fechaUndiaDepues
                                 select aud;
                int res = auditorias.Count();
                return auditorias.Count() != 0;
            }
        }

        //+++++entrega final Edwin ++++++

        /*
         *Propósito: Permite leer un archivo de texto plano y guardar las líneas en un arreglo
         *Entradas: string
         *Salidas: List<string>
        */
        //#Metodo: leerArchivo
        public List<string> leerArchivo(string rutaArchivo)
        {
            List<string> lineas = new List<string>();
            String line;
            using (System.IO.TextReader reader = new System.IO.StreamReader(rutaArchivo))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    lineas.Add(line);
                }
            }
            return lineas;
        }

        /*
         *Propósito: Permite consultar los pedidos que se han realizado en un intervalo de tiempo
         *Entradas: DateTime, DateTime
         *Salidas: ObservableCollection<PedidoEntidad>
        */
        //#Metodo: consultarPedidoPorIntervaloDeTiempo
        public ObservableCollection<PedidoEntidad> consultarPedidoPorIntervaloDeTiempo(DateTime inicio, DateTime fin)
        {
            ObservableCollection<PedidoEntidad> respuesta =new  ObservableCollection <PedidoEntidad> ();
            DateTime fechaFinUnDiaDepues = fin.Date.AddDays(1);
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                //los traigo de la base de datos
                var pedidos = from ped in contexto.Pedido
                              where ped.fecha >= inicio.Date
                                    && ped.fecha < fechaFinUnDiaDepues
                              select ped;
                //mapeo lo que me devuelve la base de datos a entidades
                foreach (var item in pedidos)
                {
                    PedidoEntidad actual = mapearPedidoDeEfAEntidades(item);
                    respuesta.Add(actual);
                }
            }
            return respuesta;

        }

        /*
         *Propósito: Permite consultar los pedidos que se han realizado en una fecha espcifica
         *Entradas: DateTime
         *Salidas: ObservableCollection<PedidoEntidad>
        */
        //#Metodo: consultarPedidoPorFecha
        public ObservableCollection<PedidoEntidad>  consultarPedidoPorFecha(DateTime fecha){
            ObservableCollection<PedidoEntidad> respuesta = new ObservableCollection<PedidoEntidad>();
            DateTime fechaUnDiaDespues = fecha.Date.AddDays(1);
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                //los traigo de la base de datos
                var pedidos = from ped in contexto.Pedido
                              where ped.fecha >= fecha.Date && ped.fecha < fechaUnDiaDespues
                              select ped;
                //mapeo lo que me devuelve la base de datos a entidades
                foreach (var item in pedidos)
                {
                    PedidoEntidad actual = mapearPedidoDeEfAEntidades(item);
                    respuesta.Add(actual);
                }
            }
            return respuesta;
        }

        /*
         *Propósito: Permite editar un producto en la BD
         *Entradas: ProductoEntidad
         *Salidas: 
        */
        //#Metodo: editarProducto
        public void editarProducto(ProductoEntidad producto){
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                var pro = contexto.Producto.Where(p => p.cod_producto == producto.CodProducto).FirstOrDefault();
           
                if (pro != null)
                {                    
                    pro.cantidad = producto.Cantidad;
                    pro.cod_cat_producto = producto.CodCatProducto;
                    pro.cod_usuario = producto.CodUsuario;
                    pro.cod_producto = producto.CodProducto;
                    pro.descripcion_producto = producto.DescripcionProducto;
                    pro.nombre_producto = producto.NombreProducto;
                    pro.precio = producto.Precio;
                    pro.ruta_foto = producto.RutaFoto;
                    contexto.SaveChanges();
                }
            }
        }

        /*
         *Propósito: Permite editar el precio de un producto en la BD
         *Entradas: ProductoEntidad
         *Salidas: 
        */
        //#Metodo: editarPrecioProducto
        public void editarPrecioProducto(ProductoEntidad producto)
        {
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                var pro = contexto.Producto.Where(p => p.cod_producto == producto.CodProducto).FirstOrDefault();

                if (pro != null)
                {
                    pro.precio = producto.Precio;
                    contexto.SaveChanges();
                }
            }
        }
        
        /*
        *Propósito: Permite crear un usuario en la base de datos
        *Entradas: UsuarioEntidad
        *Salidas: Usuario
       */
        //#Metodo: crearUsuario
        public Usuario crearUsuario(UsuarioEntidad usuario){
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {                
                Usuario usu = new Usuario();

                usu = mapearUsuarioDeEntidadesAeF(usuario);
                contexto.Usuario.Add(usu);
                contexto.SaveChanges();

                return usu;
            }
        }

        /*
        *Propósito: Permite modificar los datos un usuario en la base de datos
        *Entradas: UsuarioEntidad
        *Salidas: 
        */
        //#Metodo: editarUsuario
        public void editarUsuario(UsuarioEntidad usuario){
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                var usu = contexto.Usuario.Where(u => u.cod_usuario == usuario.CodUsuario).FirstOrDefault();

                if (usu != null)
                {
                    usu.cod_perfil = usuario.CodPerfil;
                    usu.cod_usuario = usuario.CodUsuario;
                    usu.contrasena_usuario = usuario.ContrasenaUsuario;
                    usu.correo = usuario.Correo;
                    usu.nombre = usuario.Nombre;
                    usu.nombre_usuario = usuario.NombreUsuario;
                    usu.telefono = usuario.Telefono;
                    usu.ruta_foto = usuario.RutaFoto;
                    contexto.SaveChanges();
                }
            }
        }

        /*
        *Propósito: Permite crear un perfil en la base de datos
        *Entradas: PerfilEntidad
        *Salidas: Usuario
       */
        //#Metodo: crearPerfil
        public Perfil crearPerfil(PerfilEntidad perfil){
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                Perfil per = new Perfil();

                per = mapearPerfilDeEntidadesAeF(perfil);
                contexto.Perfil.Add(per);
                contexto.SaveChanges();

                return per;
            }
        }

        /*
        *Propósito: Permite modificar los datos un perfil en la base de datos
        *Entradas: PerfilEntidad
        *Salidas: 
        */
        //#Metodo: editarPerfil
        public void editarPerfil(PerfilEntidad perfil){
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                var per = contexto.Perfil.Where(p => p.cod_perfil == perfil.CodPerfil).FirstOrDefault();

                if (per != null)
                {
                    per.cod_perfil = perfil.CodPerfil;
                    per.nombre = perfil.Nombre;
                    contexto.SaveChanges();
                }
            }
        }

        /*
        *Propósito: Permite autenticar un usuario de acuerdo con su nombre de usuario y contrasenia,, 
        * si el usuario no existe se dvuelve null.
        *Entradas: string, string
        *Salidas: UsuarioEntidad
        */
        //#Metodo: autenticarUsuario
        public UsuarioEntidad autenticarUsuario(string nombreUsuario, string contrasenia){
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                var usuario = from usu in contexto.Usuario
                              where usu.nombre_usuario == nombreUsuario && usu.contrasena_usuario == contrasenia
                              select usu;
                if (usuario.First() != null)
                {
                    return mapearUsuarioDeEfAEntidades(usuario.First());
                }
                else
                {
                    return null;
                }
            }
        }

        /*
        *Propósito: Permite determinar si se ha hecho un pedido para un negocio en una fecha despecifica.
        *Entradas: int, DateTime
        *Salidas: 
        */
        //#Metodo: haHechoPedido
        public bool haHechoPedido(int codNegocio, DateTime fecha)
        {
            DateTime fechaUnDiaDespues = fecha.Date.AddDays(1);
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                //los traigo de la base de datos
                var pedido = from ped in contexto.Pedido
                             where ped.fecha >= fecha.Date && ped.fecha < fechaUnDiaDespues
                             && ped.cod_negocio == codNegocio
                             select ped;
                int res = pedido.Count();
                return pedido.Count() != 0;
            }

        }

        //++++++Entrega Edwin Gestión Usuarios+++++++++++

        /*
       *Propósito: Permite consultar un usuario por su nombre en la base de datos.
       *Entradas: string
       *Salidas: ObservableCollection<UsuarioEntidad>
       */
        //#Metodo: consultarUsuarioPorNombre
        public ObservableCollection<UsuarioEntidad> consultarUsuarioPorNombre(string nombre_usuario)
        {
            ObservableCollection<UsuarioEntidad> respuesta = new ObservableCollection<UsuarioEntidad>();
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                var almacen = from usu in contexto.Usuario
                              where usu.nombre.Contains(nombre_usuario)
                              select usu;
                foreach (var item in almacen)
                {
                    UsuarioEntidad actual = mapearUsuarioDeEfAEntidades(item);
                    respuesta.Add(actual);
                }
            }
            return respuesta;

        }

        /*
       *Propósito: Permite consultar todos los usuarios de la base de datos.
       *Entradas: 
       *Salidas: ObservableCollection<UsuarioEntidad>
       */
        //#Metodo: consultarTodosLosUsuarios
        public ObservableCollection<UsuarioEntidad> consultarTodosLosUsuarios()
        {
            ObservableCollection<UsuarioEntidad> respuesta = new ObservableCollection<UsuarioEntidad>();
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                var almacen = from usu in contexto.Usuario
                              select usu;
                foreach (var item in almacen)
                {
                    UsuarioEntidad actual = mapearUsuarioDeEfAEntidades(item);
                    respuesta.Add(actual);
                }
            }
            return respuesta;

        }

        /*
        *Propósito: Permite consultar un pefil por su nombre en la base de datos.
        *Entradas: string
        *Salidas: ObservableCollection<PerfilEntidad>
        */
        //#Metodo: consultarPerfilPorNombre
        public ObservableCollection<PerfilEntidad> consultarPerfilPorNombre(string nombre_perfil)
        {
            ObservableCollection<PerfilEntidad> respuesta = new ObservableCollection<PerfilEntidad>();
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                var almacen = from per in contexto.Perfil
                              where per.nombre.Contains(nombre_perfil)
                              select per;
                foreach (var item in almacen)
                {
                    PerfilEntidad actual = mapearPerfilDeEfAEntidades(item);
                    respuesta.Add(actual);
                }
            }
            return respuesta;

        }

        /*
       *Propósito: Permite consultar todos los perfiles de la base de datos.
       *Entradas: 
       *Salidas: ObservableCollection<PerfilEntidad>
       */
        //#Metodo: consultarTodosLosPerfiles
        public ObservableCollection<PerfilEntidad> consultarTodosLosPerfiles()
        {
            ObservableCollection<PerfilEntidad> respuesta = new ObservableCollection<PerfilEntidad>();
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                var almacen = from per in contexto.Perfil
                              select per;
                foreach (var item in almacen)
                {
                    PerfilEntidad actual = mapearPerfilDeEfAEntidades(item);
                    respuesta.Add(actual);
                }
            }
            return respuesta;

        }

        /*
        *Propósito: Permite consultar un pefil por su codigo en la base de datos.
        *Entradas: int
        *Salidas: PerfilEntidad
        */
        //#Metodo: consultarPerfilPorCodigo
        public PerfilEntidad consultarPerfilPorCodigo(int cod_perfil)
        {            
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                return mapearPerfilDeEfAEntidades(contexto.Perfil.Find(cod_perfil));
            }
        }

        /*
        *Propósito: Permite consultar un usuario por su codigo en la base de datos.
        *Entradas: int
        *Salidas: UsuarioEntidad
        */
        //#Metodo: consultarUsuarioPorCodigo
        public UsuarioEntidad consultarUsuarioPorCodigo(int cod_usuario)
        {
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                return mapearUsuarioDeEfAEntidades(contexto.Usuario.Find(cod_usuario));
            }
        }

        /*
        *Propósito: Permite consultar un producto por su codigo en la base de datos.
        *Entradas: int
        *Salidas: ProductoEntidad
        */
        //#Metodo: consultarProductoPorCodigo
        public ProductoEntidad consultarProductoPorCodigo(int cod_producto)
        {
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                return mapearProductoDeEfAEntidades(contexto.Producto.Find(cod_producto));
            }
        }
        
        /*
        *Propósito: Permite consultar el inventario (cantidad diposnible) de los pruductos.
        *Entradas: 
        *Salidas: ObservableCollection<ProductoEntidad>
        */
        //#Metodo: consultarInventario
        public ObservableCollection<ProductoEntidad> consultarInventario()
        {
            ObservableCollection<ProductoEntidad> respuesta = new ObservableCollection<ProductoEntidad>();
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                //los traigo de la base de datos
                var inventario = from inv in contexto.Producto
                                 select inv;

                //mapeo lo que me devuelve la base de datos a entidades

                foreach (var item in inventario)
                {
                    ProductoEntidad actual = mapearProductoDeEfAEntidades(item);
                    respuesta.Add(actual);
                }
            }

            return respuesta;
        }
        
        /*
        *Propósito: Permite consultar los vendedores que no han sincronizado en un día determinado.
        *Entradas: DateTime
        *Salidas: ObservableCollection<UsuarioEntidad>
        */
        //#Metodo: consultarVendedoresNoSincronizados
        public ObservableCollection<UsuarioEntidad> consultarVendedoresNoSincronizados(DateTime fecha)
        {
            ObservableCollection<UsuarioEntidad> respuesta = new ObservableCollection<UsuarioEntidad>();
            DateTime fechaUnDiaDespues = fecha.Date.AddDays(1);
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                var usuarios = from usuNosin in contexto.Usuario
                               where 
                               usuNosin.cod_perfil == 2 &&
                               !(from usuSin in contexto.Auditoria
                                       where usuSin.fecha >= fecha.Date && usuSin.fecha < fechaUnDiaDespues
                                       select usuSin.cod_usuario)
                                         .Contains(usuNosin.cod_usuario)
                               select usuNosin;


                foreach (var item in usuarios)
                {
                    UsuarioEntidad actual = mapearUsuarioDeEfAEntidades(item);
                    respuesta.Add(actual);
                }
            }

            return respuesta;
        }

        /*
        *Propósito: Permite consultar la cantidad de pedidos por cada producto en un intervalo de tiempo
        *Entradas: DateTime, DateTime
        *Salidas: List<string>
        */
        //#Metodo: consultarCantidadPedidoPorProducto
        public List<string> consultarCantidadPedidoPorProducto(DateTime inicio, DateTime fin)
        {
            List<string> respuesta = new List<string>();
            DateTime fechaUnDiaDespuesFin = fin.Date.AddDays(1);
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {                             
                //los traigo de la base de datos
                var cantidadPedidosPorPorducto = from ped in contexto.PedidoProducto
                                                 where ped.Pedido.fecha >= inicio.Date
                                                && ped.Pedido.fecha < fechaUnDiaDespuesFin.Date
                                                 group ped by new 
                                                 { 
                                                     codProducto = ped.Producto.cod_producto,
                                                     nombreProducto = ped.Producto.nombre_producto,
                                                     cantidadPedidad = ped.Producto.cantidad
                                                 }
                                                 into grupo
                                                 orderby grupo.Sum(p => p.cantidad) descending
                                                 select new 
                                                 { 
                                                     key = grupo.Key, cnt = grupo.Sum(p => p.cantidad) 
                                                 };
                foreach (var producto in cantidadPedidosPorPorducto)
                {
                    respuesta.Add(producto.key.codProducto.ToString());
                    respuesta.Add(producto.key.nombreProducto);
                    respuesta.Add(producto.cnt.ToString());
                }

                return respuesta;
            } 
        }

        /*
        *Propósito: Permite consultar la cantidad de pedidos por cada categoria en un intervalo de tiempo
        *Entradas: DateTime, DateTime
        *Salidas: List<string>
        */
        //#Metodo: consultarCantidadPedidoPorCategoria
        public List<string> consultarCantidadPedidoPorCategoria(DateTime inicio, DateTime fin)
        {
            List<string> respuesta = new List<string>();
            DateTime fechaDiaDepuesFin = fin.AddDays(1);
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                //los traigo de la base de datos
                var cantidadPedidosPorCategoria = from ped in contexto.PedidoProducto
                                                  where ped.Pedido.fecha >= inicio.Date
                                                 && ped.Pedido.fecha < fechaDiaDepuesFin
                                                  group ped by new 
                                                  { 
                                                      codCategoria = ped.Producto.Categoria_Producto.cod_cat_producto,
                                                      nombreCategoria = ped.Producto.Categoria_Producto.nombre_cat_producto 
                                                  }
                                                  into grupo
                                                  orderby grupo.Count() descending
                                                  select new { key = grupo.Key, cnt = grupo.Count() };
                foreach (var categoria in cantidadPedidosPorCategoria)
                {
                    respuesta.Add(categoria.key.codCategoria.ToString());
                    respuesta.Add(categoria.key.nombreCategoria);
                    respuesta.Add(categoria.cnt.ToString());
                }

                return respuesta;
            }             
        }

        /*
        *Propósito: Permite consultar la cantidad de pedidos hechos por ciudad en un intervalo de tiempo
        *Entradas: DateTime, DateTime
        *Salidas: List<string>
        */
        //#Metodo: consultarCantidadPedidosPorCiudad
        public List<String> consultarCantidadPedidosPorCiudad(DateTime inicio, DateTime fin)
        {
            List<string> respuesta = new List<string>();
            DateTime fechaDiaDepuesFin = fin.AddDays(1);
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                //los traigo de la base de datos
                var pedidosCiudad = from ped in contexto.Pedido
                                    where ped.fecha >= inicio.Date
                                   && ped.fecha < fechaDiaDepuesFin.Date
                                   group ped by ped.Negocio.ciudad into grupo
                                   orderby grupo.Count() descending
                                   select new { key = grupo.Key, cnt = grupo.Count()};
                foreach(var ciudad in pedidosCiudad){
                    respuesta.Add(ciudad.key);
                    respuesta.Add(ciudad.cnt.ToString());
                }

                return respuesta;
            }            

        }

        /*
        *Propósito: Permite consultar la cantidad de pedidos hechos para una ciudad en un intervalo de tiempo
        *Entradas: DateTime, DateTime
        *Salidas: List<string>
        */
        //#Metodo: consultarPedidoDeUnaCiudad
        public ObservableCollection<PedidoEntidad> consultarPedidoDeUnaCiudad(string ciudad, DateTime inicio, DateTime fin)
        {
            ObservableCollection<PedidoEntidad> respuesta = new ObservableCollection<PedidoEntidad>();
            DateTime fechaDiaDepuesFin = fin.AddDays(1);
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                //los traigo de la base de datos
                var pedidosCiudad = from ped in contexto.Pedido
                                    where ped.Negocio.ciudad == ciudad &&
                                    ped.fecha >= inicio.Date
                                    && ped.fecha < fechaDiaDepuesFin
                                    select ped;
                //mapeo lo que me devuelve la base de datos a entidades
                foreach (var item in pedidosCiudad)
                {
                    PedidoEntidad actual = mapearPedidoDeEfAEntidades(item);
                    respuesta.Add(actual);
                }
            }
            return respuesta;

        }


        /*
        *Propósito: Permite crear una categoria en la base de datos.
        *Entradas: Categoria_ProductoEntidad 
        *Salidas: Producto
        */
        //#Metodo: crearCategoriaProducto
        public Categoria_Producto crearCategoriaProducto(Categoria_ProductoEntidad categoria)
        {
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                Categoria_Producto cat = new Categoria_Producto();
                cat = mapearCategoriaProductoDeEntidadesAeF(categoria);
                contexto.Categoria_Producto.Add(cat);
                contexto.SaveChanges();
                return cat;
            }
        }

        /*
        *Propósito: Permite modificar los datos un categoria de producto en la base de datos
        *Entradas: Categoria_ProductoEntidad
        *Salidas: 
        */
        //#Metodo: editarCategoria_ProductoEntidad
        public void editarCategoria_ProductoEntidad(Categoria_ProductoEntidad categoria)
        {
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                var cat = contexto.Categoria_Producto.Where(c => c.cod_cat_producto 
                    == categoria.CodCatProducto).FirstOrDefault();

                if (cat != null)
                {
                    cat.nombre_cat_producto = categoria.NombreCatProducto;
                    contexto.SaveChanges();
                }
            }
        }



        /*
         *Propósito: Permite consultar un producto por su nombre a la base de datos y lo retorna si hay al menos 1
         * en el inventario.
         *Entradas: string nombre_producto
         *Salidas: ObservableCollection<ProductoEntidad>
        */
        //#Metodo: ConsultarProductoDisponiblePorNombre
        public ObservableCollection<ProductoEntidad> consultarProductoDisponiblePorNombre(string nombre_producto)
        {
            ObservableCollection<ProductoEntidad> respuesta = new ObservableCollection<ProductoEntidad>();
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                var producto = from pro in contexto.Producto
                               where pro.nombre_producto.Contains(nombre_producto) && pro.cantidad > 0
                               select pro;
                foreach (var item in producto)
                {
                    ProductoEntidad actual = mapearProductoDeEfAEntidades(item);
                    respuesta.Add(actual);
                }
            }
            return respuesta;

        }

        /*
         *Propósito: Permite consultar un producto por su categoria a la base de datos cuando el inventario es mayor a 0.
         *Entradas: Categoria_ProductoEntidad categoria
         *Salidas: ObservableCollection<ProductoEntidad>
        */
        //#Metodo: consultarProductoDisponiblePorCategoria
        public ObservableCollection<ProductoEntidad> consultarProductoDisponiblePorCategoria(string nombreCategoria)
        {
            ObservableCollection<ProductoEntidad> respuesta = new ObservableCollection<ProductoEntidad>();
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                var producto = from pro in contexto.Producto
                               where pro.Categoria_Producto.nombre_cat_producto == nombreCategoria && pro.cantidad > 0
                               select pro;
                foreach (var item in producto)
                {
                    ProductoEntidad actual = mapearProductoDeEfAEntidades(item);
                    respuesta.Add(actual);
                }
            }
            return respuesta;

        }

        /*
         *Propósito: Permite consultar un producto por nombre y categoria a la base de datos cuando el inventario es mayor a 0.
         *Entradas: Categoria_ProductoEntidad categoria, string nombre_producto
         *Salidas: ObservableCollection<ProductoEntidad>
        */
        //#Metodo: consultarProductoPorNombreYCategoria
        public ObservableCollection<ProductoEntidad> consultarProductoDisponiblePorNombreYCategoria(string nombreCategoria, string nombre_producto)
        {
            ObservableCollection<ProductoEntidad> respuesta = new ObservableCollection<ProductoEntidad>();
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                var producto = from pro in contexto.Producto
                               where pro.nombre_producto.Contains(nombre_producto)
                               && pro.Categoria_Producto.nombre_cat_producto == nombreCategoria && pro.cantidad > 0
                               select pro;
                foreach (var item in producto)
                {
                    ProductoEntidad actual = mapearProductoDeEfAEntidades(item);
                    respuesta.Add(actual);
                }
            }
            return respuesta;

        }


        /*
        *Propósito: Permite consultar la cantidad de pedidos hechos por dias de la semana
        *Entradas: DateTime, DateTime
        *Salidas: List<string>
        */
        //#Metodo: consultarCantidadPedidosPorDiasSemana
        public List<String> consultarCantidadPedidosPorDiasSemana(DateTime inicio, DateTime fin)
        {
            List<string> respuesta = new List<string>();
            DateTime fechaDiaDepuesFin = fin.AddDays(1);
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                //los traigo de la base de datos
                var pedidosCiudad = from ped in contexto.Pedido
                                    where ped.fecha >= inicio.Date
                                   && ped.fecha < fechaDiaDepuesFin.Date
                                    group ped by SqlFunctions.DatePart("weekday", ped.fecha) into grupo
                                    orderby grupo.Count() descending
                                    select new { key = grupo.Key, cnt = grupo.Count() };
                string[] dias = new string[7] { "Domingo", "Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado" };

                foreach (var ciudad in pedidosCiudad)
                {
                    respuesta.Add(dias[(int)ciudad.key - 1]);
                    respuesta.Add(ciudad.cnt.ToString());
                }

                return respuesta;
            }

        }

        /*
        *Propósito: Permite consultar una ctaegoria de producto por su codigo en la base de datos.
        *Entradas: int
        *Salidas: Categoria_ProductoEntidad
        */
        //#Metodo: consultarCategoriaPorCodigo
        public Categoria_ProductoEntidad consultarCategoriaPorCodigo(int cod_categoria)
        {
            using (Sistema_ventasEntities contexto = new Sistema_ventasEntities())
            {
                return mapearCategoriaProductoDeEfAEntidades(contexto.Categoria_Producto.Find(cod_categoria));
            }
        }

    }
}
