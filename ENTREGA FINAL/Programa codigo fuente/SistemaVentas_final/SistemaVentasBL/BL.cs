/*
* NombreClase: BL
* Autores: Edwin Gamboa - 1310233, David Zuluaga - 1310294, Roger Fernandez - 1310229
* Fecha: 12/May/2015
* Descripcion: Clase que permite conectar la capa UI con la capa DAL del SistemaVentas, para realizar las consultas que 
* se requieren para mostrar información de pedidos, negocios y productos en el desconectado.
*/

/*
* Métodos de la clase:
* void crearPedido(PedidoEntidad ped)
* Auditoria crearAuditoria(AuditoriaEntidad auditoria) 
* ObservableCollection<PedidoEntidad> consultarPedidoPorNegocio(NegocioEntidad neg)
* NegocioEntidad consultarNegocioPorNombre(string nombre_negocio)
* ObservableCollection<ProductoEntidad> consultarProductoPorNombre(string nombre_producto)
* ObservableCollection<ProductoEntidad> consultarProductoPorCategoria(Categoria_ProductoEntidad categoria)
* ObservableCollection<ProductoEntidad> consultarProductoPorNombreYCategoria(Categoria_ProductoEntidad categoria, string nombre)
* ObservableCollection<Categoria_ProductoEntidad> ListarCategorias()
* string generarXmlProductosNegocios(int codUltimoProducto, int codUltimoNegocio)
* void guardarNuevosProductosYNegocios(string xmlString)
* string generarXmlNuevosPedidos()
* void guardarNuevosPedidos(string xmlString)
* string cifrar(string texto)
* string descifrar(string texto)
* NegocioEntidad ultimoNegocio()
* ProductoEntidad ultimoProducto()
* string descomprimirString(string cadenaComprimida)
* string comprimirString(string cadena)
* bool haSincronizadoEnEstaFecha(DateTime fecha)
* void editarPreciosProductosDesdeArchivos(string rutaArchivo)
* ObservableCollection<PedidoEntidad> consultarPedidoPorIntervaloDeTiempo(DateTime inicio, DateTime fin)
* ObservableCollection<PedidoEntidad> consultarPedidoPorFecha(DateTime fecha)
* void editarProducto(ProductoEntidad producto)
* void crearUsuario(UsuarioEntidad usuario)
* void editarUsuario(UsuarioEntidad usuario)
* void crearPerfil(PerfilEntidad perfil)
* void editarPerfil(PerfilEntidad perfil)
* UsuarioEntidad autenticarUsuario(string nombreUsuario, string contrasenia)
* bool haHechoPedido(int codNegocio, DateTime fecha)
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



using SistemaVentasDAL;
using SistemaVentasEntidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SistemaVentasBL
{
    public class BL
    {
        private string key = "tIIy1PBcV3ipmPocAN8GCuAHrdSnB2Zvi2BRRf8";

        /*
         *Propósito: Método que permite crear un pedido, utiliza la clase DAL para guardarlo en la BD.
         *Entradas: PedidoEntidad 
         *Salidas:
         */
        //#Metodo: crearPedido 
        public void crearPedido(PedidoEntidad pedido)
        {
            DAL contexto = new DAL();
            contexto.crearPedido(pedido);
        }

        /*
         *Propósito: Permite crear un registro en la tabla Auditoria a la base de datos.
         *Entradas: AuditoriaEntidad 
         *Salidas: Auditoria
         */
        //#Metodo: crearAuditoria
        public void crearAuditoria(AuditoriaEntidad auditoria)
        {
            DAL contexto = new DAL();
            contexto.crearAuditoria(auditoria);

        }

        /*
         *Propósito: Método que consultar los pedidos que se han realizado para un negocio.
         * utiliza la clase DAL para para consultar la BD.
         *Entradas: NegocioEntidad 
         *Salidas: ObservableCollection<PedidoEntidad>
         */
        //#Metodo: consultarPedidoPorNegocio
        public ObservableCollection<PedidoEntidad> consultarPedidoPorNegocio(NegocioEntidad negocio)
        {
            DAL contexto = new DAL();
            ObservableCollection<PedidoEntidad> pedidosPorNegocio = contexto.consultarPedidoPorNegocio(negocio);
            return pedidosPorNegocio;

        }

        /*
         *Propósito: Método que permite consultar un negocio con el nombre del mismo.
         * utiliza la clase DAL para para consultar la BD.
         *Entradas: string 
         *Salidas: NegocioEntidad
         */
        //#Metodo: consultarNegocioPorNombre
        public ObservableCollection<NegocioEntidad> consultarNegocioPorNombre(string nombre_negocio)
        {
            DAL contexto = new DAL();
            return contexto.consultarNegocioPorNombre(nombre_negocio);
        }

        /*
         *Propósito: Método que permite consultar productos con coincidan con un nombre.
         * utiliza la clase DAL para para consultar la BD.
         *Entradas: string 
         *Salidas: ObservableCollection<ProductoEntidad>
         */
        //#Metodo: consultarProductoPorNombre
        public ObservableCollection<ProductoEntidad> consultarProductoPorNombre(string nombre_producto)
        {
            DAL contexto = new DAL();
            ObservableCollection<ProductoEntidad> productos = contexto.ConsultarProductoPorNombre(nombre_producto);
            return productos;
        }

        /*
         *Propósito: Método que permite consultar productos que ertenecen a una Categoria_ProductoEntidad.
         * utiliza la clase DAL para para consultar la BD.
         *Entradas: Categoria_ProductoEntidad 
         *Salidas: ObservableCollection<ProductoEntidad>
         */
        //#Metodo: consultarProductoPorCategoria
        public ObservableCollection<ProductoEntidad> consultarProductoPorCategoria(string nombreCategoria)
        {
            DAL contexto = new DAL();
            ObservableCollection<ProductoEntidad> productos = contexto.consultarProductoPorCategoria(nombreCategoria);
            return productos;
        }

        /*
         *Propósito: Método que permite consultar productos que pertenecen a una Categoria_ProductoEntidad
         * y que coinciden con un nombre, utiliza la clase DAL para para consultar la BD.
         *Entradas: string,  string
         *Salidas: ObservableCollection<ProductoEntidad>
         */
        //#Metodo: consultarProductoPorNombreYCategoria
        public ObservableCollection<ProductoEntidad> consultarProductoPorNombreYCategoria(string nombreCategoria,
            string nombreProducto)
        {
            DAL contexto = new DAL();
            ObservableCollection<ProductoEntidad> productos = contexto.consultarProductoPorNombreYCategoria(nombreCategoria, nombreProducto);
            return productos;
        }

        /*
       *Propósito:Metodo que Permite consultar un vendedor por su codigo, utiliza la clase DAL para la BD
       *Entradas: int
       *Salidas: UsuarioEntidad
       */
        //#Metodo: consultarVendedorPorCodigo
        public UsuarioEntidad consultarVendedorPorCodigo(int cod_vendedor)
        {
            DAL contexto = new DAL();
            ObservableCollection<UsuarioEntidad> vendedor = contexto.consultarVendedorPorCodigo(cod_vendedor);
            return vendedor.FirstOrDefault();
        }

        /*
        *Propósito: Permite listar las categorias que exisen en la base de datos.
        *Entradas:
        *Salidas: ObservableCollection<Categoria_ProductoEntidad>
        */
        //#Metodo: listarCategorias
        public ObservableCollection<Categoria_ProductoEntidad> listarCategorias()
        {
            DAL contexto = new DAL();
            ObservableCollection<Categoria_ProductoEntidad> categoria = contexto.listarCategorias();
            return categoria;
        }
        
        /*
         *Propósito: Permite consultar el ultimo producto presente en la BD.
         *Entradas:  
         *Salidas: ProductoEntidad
         */
        //#Metodo: ultimoProducto
        public ProductoEntidad ultimoProducto()
        {
            DAL contexto = new DAL();
            return contexto.ultimoProducto();
        }

        /*
         *Propósito: Permite consultar el ultimo negocio presente en la BD.
         *Entradas:  
         *Salidas: NegocioEntidad
         */
        //#Metodo: ultimoNegocio
        public NegocioEntidad ultimoNegocio()
        {
            DAL contexto = new DAL();
            return contexto.ultimoNegocio();
        }

        /*
        *Propósito: Permite generar un string en formato XML con los productos y negocios
        *cuyo coidigo es mayor a codUltimoProducto y codUltimoNegocio
        *Entradas: codUltimoProducto, codUltimoNegocio
        *Salidas: string
        */
        //#Metodo: generarXmlProductosNegocios
        public string generarXmlProductosNegocios(int codUltimoProducto, int codUltimoNegocio)
        {
            DAL contexto = new DAL();
            ObservableCollection<ProductoEntidad> nuevosProductos = contexto.consultarNuevosProductos(codUltimoProducto);

            XmlDocument xml = new XmlDocument();
            xml.LoadXml("<SincronizacionBajada></SincronizacionBajada>");

            XmlElement productos = xml.CreateElement("Productos");
            foreach (var itemProducto in nuevosProductos)
            {
                XmlElement producto = xml.CreateElement("Producto");

                XmlElement CodigoProducto = xml.CreateElement("CodigoProducto");
                XmlText CodigoProductoTxt = xml.CreateTextNode(itemProducto.CodProducto.ToString());
                CodigoProducto.AppendChild(CodigoProductoTxt);
                producto.AppendChild(CodigoProducto);

                XmlElement NombreProducto = xml.CreateElement("NombreProducto");
                XmlText NombreProductoTxt = xml.CreateTextNode(itemProducto.NombreProducto.ToString());
                NombreProducto.AppendChild(NombreProductoTxt);
                producto.AppendChild(NombreProducto);

                XmlElement Cantidad = xml.CreateElement("Cantidad");
                XmlText CantidadTxt = xml.CreateTextNode(itemProducto.Cantidad.ToString());
                Cantidad.AppendChild(CantidadTxt);
                producto.AppendChild(Cantidad);

                XmlElement Precio = xml.CreateElement("Precio");
                XmlText PrecioTxt = xml.CreateTextNode(itemProducto.Precio.ToString());
                Precio.AppendChild(PrecioTxt);
                producto.AppendChild(Precio);

                XmlElement Descripcion = xml.CreateElement("Descripcion");
                XmlText DescripcionTxt = xml.CreateTextNode(itemProducto.DescripcionProducto.ToString());
                Descripcion.AppendChild(DescripcionTxt);
                producto.AppendChild(Descripcion);

                XmlElement RutaFoto = xml.CreateElement("RutaFoto");
                XmlText RutaFotoTxt = xml.CreateTextNode(itemProducto.RutaFoto.ToString());
                RutaFoto.AppendChild(RutaFotoTxt);
                producto.AppendChild(RutaFoto);

                XmlElement CodigoUsuario = xml.CreateElement("CodigoUsuario");
                XmlText CodigoUsuarioTxt = xml.CreateTextNode(itemProducto.CodUsuario.ToString());
                CodigoUsuario.AppendChild(CodigoUsuarioTxt);
                producto.AppendChild(CodigoUsuario);

                XmlElement CodigoCategoriaProducto = xml.CreateElement("CodigoCategoriaProducto");
                XmlText CodigoCategoriaProductoTxt = xml.CreateTextNode(itemProducto.CodCatProducto.ToString());
                CodigoCategoriaProducto.AppendChild(CodigoCategoriaProductoTxt);
                producto.AppendChild(CodigoCategoriaProducto);

                productos.AppendChild(producto);
            }
            xml.DocumentElement.AppendChild(productos);

            ObservableCollection<NegocioEntidad> nuevosNegocios = contexto.consultarNuevosNegocios(codUltimoNegocio);
            XmlElement negocios = xml.CreateElement("Negocios");
            foreach (var itemNegocio in nuevosNegocios)
            {
                XmlElement negocio = xml.CreateElement("Negocio");

                XmlElement CodigoNegocio = xml.CreateElement("CodigoNegocio");
                XmlText CodigoNegocioTxt = xml.CreateTextNode(itemNegocio.CodNegocio.ToString());
                CodigoNegocio.AppendChild(CodigoNegocioTxt);
                negocio.AppendChild(CodigoNegocio);

                XmlElement Nit = xml.CreateElement("Nit");
                XmlText NitTxt = xml.CreateTextNode(itemNegocio.NitNegocio.ToString());
                Nit.AppendChild(NitTxt);
                negocio.AppendChild(Nit);

                XmlElement NombreNegocio = xml.CreateElement("Nombre");
                XmlText NombreNegocioTxt = xml.CreateTextNode(itemNegocio.NombreNegocio.ToString());
                NombreNegocio.AppendChild(NombreNegocioTxt);
                negocio.AppendChild(NombreNegocio);

                XmlElement CiudadNegocio = xml.CreateElement("Ciudad");
                XmlText CiudadNegocioTxt = xml.CreateTextNode(itemNegocio.Ciudad.ToString());
                CiudadNegocio.AppendChild(CiudadNegocioTxt);
                negocio.AppendChild(CiudadNegocio);

                XmlElement DireccionNegocio = xml.CreateElement("Direccion");
                XmlText DireccionNegocioTxt = xml.CreateTextNode(itemNegocio.Direccion.ToString());
                DireccionNegocio.AppendChild(DireccionNegocioTxt);
                negocio.AppendChild(DireccionNegocio);

                negocios.AppendChild(negocio);
            }
            xml.DocumentElement.AppendChild(negocios);

            return xml.OuterXml;
        }

        /*
        *Propósito: Permite leer un string en formato XML con productos y negocios
        *y los llama a DAL para gurardarlos en la BD
        *Entradas: xmlString
        *Salidas: 
        */
        //#Metodo: guardarNuevosProductosYNegocios
        public void guardarNuevosProductosYNegocios(string xmlString)
        {
            DAL contexto = new DAL();
            //Crear el documento XML
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlString);

            XmlNodeList productosNegocios = xml.FirstChild.ChildNodes;
            //Productos
            foreach (XmlNode productoTag in productosNegocios.Item(0).ChildNodes)
            {
                XmlNodeList atributosProducto = productoTag.ChildNodes;
                ProductoEntidad producto = new ProductoEntidad();
                producto.CodProducto = Convert.ToInt32(atributosProducto.Item(0).InnerText);
                producto.NombreProducto = atributosProducto.Item(1).InnerText;
                producto.Cantidad = Convert.ToInt32(atributosProducto.Item(2).InnerText);
                producto.Precio = Convert.ToInt32(atributosProducto.Item(3).InnerText);
                producto.DescripcionProducto = atributosProducto.Item(4).InnerText;
                producto.RutaFoto = atributosProducto.Item(5).InnerText;
                producto.CodUsuario = Convert.ToInt32(atributosProducto.Item(6).InnerText);
                producto.CodCatProducto = Convert.ToInt32(atributosProducto.Item(7).InnerText);

                contexto.crearProducto(producto);
            }

            //Negocios
            foreach (XmlNode negocioTag in productosNegocios.Item(1).ChildNodes)
            {
                XmlNodeList atributosProducto = negocioTag.ChildNodes;
                NegocioEntidad negocio = new NegocioEntidad();
                negocio.CodNegocio = Convert.ToInt32(atributosProducto.Item(0).InnerText);
                negocio.NitNegocio = atributosProducto.Item(1).InnerText;
                negocio.NombreNegocio = atributosProducto.Item(2).InnerText;
                negocio.Ciudad = atributosProducto.Item(3).InnerText;
                negocio.Direccion = atributosProducto.Item(4).InnerText;

                contexto.crearNegocio(negocio);
            }
        }

        /*
        *Propósito: Permite generar un string en formato XML con los pedidos que 
        *no han sido sincronizados
        *Entradas:
        *Salidas: string
        */
        //#Metodo: generarXmlNuevosPedidos
        public string generarXmlNuevosPedidos()
        {
            DAL contexto = new DAL();
            ObservableCollection<PedidoEntidad> nuevosPedidos = contexto.consultarNuevosPedidos();

            XmlDocument xml = new XmlDocument();
            xml.LoadXml("<Pedidos></Pedidos>");

            foreach (var itemPedido in nuevosPedidos)
            {

                //Cambiar estado de pedidos
                itemPedido.Estado = "sincronizado";
                contexto.cambiarEstadoPedido(itemPedido);

                XmlElement pedido = xml.CreateElement("Pedido");


                XmlElement CodigoPedido = xml.CreateElement("CodigoPedido");
                XmlText CodigoPedidoTxt = xml.CreateTextNode(itemPedido.CodPedido.ToString());
                CodigoPedido.AppendChild(CodigoPedidoTxt);
                pedido.AppendChild(CodigoPedido);

                XmlElement CodigoNegocio = xml.CreateElement("CodigoNegocio");
                XmlText CodigoNegocioTxt = xml.CreateTextNode(itemPedido.CodNegocio.ToString());
                CodigoNegocio.AppendChild(CodigoNegocioTxt);
                pedido.AppendChild(CodigoNegocio);

                XmlElement CodigoUsuario = xml.CreateElement("CodigoUsuario");
                XmlText CodigoUsuarioTxt = xml.CreateTextNode(itemPedido.CodUsuario.ToString());
                CodigoUsuario.AppendChild(CodigoUsuarioTxt);
                pedido.AppendChild(CodigoUsuario);

                XmlElement TotalPedido = xml.CreateElement("TotalPedido");
                XmlText TotalPedidoTxt = xml.CreateTextNode(itemPedido.TotalPedido.ToString());
                TotalPedido.AppendChild(TotalPedidoTxt);
                pedido.AppendChild(TotalPedido);

                XmlElement FechaPedido = xml.CreateElement("FechaPedido");
                XmlText FechaPedidoTxt = xml.CreateTextNode(itemPedido.Fecha.ToString());
                FechaPedido.AppendChild(FechaPedidoTxt);
                pedido.AppendChild(FechaPedido);

                XmlElement EstadoPedido = xml.CreateElement("EstadoPedido");
                XmlText EstadoPedidoTxt = xml.CreateTextNode(itemPedido.Estado);
                EstadoPedido.AppendChild(EstadoPedidoTxt);
                pedido.AppendChild(EstadoPedido);

                XmlElement PedidoProducto = xml.CreateElement("PedidoProducto");
                foreach (var producto in itemPedido.PedidoProducto)
                {
                    XmlElement Producto = xml.CreateElement("Producto");

                    XmlElement CodigoProducto = xml.CreateElement("CodigoProducto");
                    XmlText CodigoProductoTxt = xml.CreateTextNode(producto.CodProducto.ToString());
                    CodigoProducto.AppendChild(CodigoProductoTxt);
                    Producto.AppendChild(CodigoProducto);

                    XmlElement Cantidad = xml.CreateElement("Cantidad");
                    XmlText CantidadTxt = xml.CreateTextNode(producto.Cantidad.ToString());
                    Cantidad.AppendChild(CantidadTxt);
                    Producto.AppendChild(Cantidad);

                    PedidoProducto.AppendChild(Producto);
                }
                pedido.AppendChild(PedidoProducto);

                xml.DocumentElement.AppendChild(pedido);
            }

            return xml.OuterXml;
        }

        /*
         *Propósito: Permite leer un string en formato XML con pedidos
         *y los llama a DAL para gurardarlos en la BD
         *Entradas: xmlString
         *Salidas: 
        */
        //#Metodo: guardarNuevosPedidos
        public void guardarNuevosPedidos(string xmlString)
        {
            DAL contexto = new DAL();
            //Crear el documento XML
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlString);

            XmlNodeList pedidos = xml.FirstChild.ChildNodes;
            //Productos
            foreach (XmlNode pedidoTag in pedidos)
            {
                XmlNodeList atributosPedido = pedidoTag.ChildNodes;
                PedidoEntidad pedido = new PedidoEntidad();
                pedido.CodPedido = Convert.ToInt32(atributosPedido.Item(0).InnerText);
                pedido.CodNegocio = Convert.ToInt32(atributosPedido.Item(1).InnerText);
                pedido.CodUsuario = Convert.ToInt32(atributosPedido.Item(2).InnerText);
                pedido.TotalPedido = Convert.ToInt32(atributosPedido.Item(3).InnerText);
                pedido.Fecha = Convert.ToDateTime(atributosPedido.Item(4).InnerText);
                pedido.Estado = atributosPedido.Item(5).InnerText;

                ObservableCollection<PedidoProductoEntidad> pedidoCompleto = new ObservableCollection<PedidoProductoEntidad>();
                foreach (XmlNode productoTag in atributosPedido.Item(6).ChildNodes)
                {
                    XmlNodeList atributosProducto = productoTag.ChildNodes;
                    PedidoProductoEntidad pedidoProducto = new PedidoProductoEntidad();
                    pedidoProducto.CodProducto = Convert.ToInt32(atributosProducto.Item(0).InnerText); ;
                    pedidoProducto.Cantidad = Convert.ToInt32(atributosProducto.Item(1).InnerText);
                    pedidoCompleto.Add(pedidoProducto);

                }
                pedido.PedidoProducto = pedidoCompleto;
                contexto.crearPedido(pedido);
            }
        }
        
        /*
          *Propósito: cifra el texto indicado usando las claves en formato XML
          *Entradas: string 
          *Salidas: string
          */
        //#Metodo cifrar
        public string cifrar(string texto)
        {
            string valorRGBSalt = "Frase_Encriptado";
            string algoritmoEncriptacionHASH = "MD5";
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(valorRGBSalt);
            byte[] parametroIV = UTF8Encoding.ASCII.GetBytes("1234567891234567");
            PasswordDeriveBytes password =
                    new PasswordDeriveBytes(key, saltValueBytes,
                        algoritmoEncriptacionHASH, 10);
            //clave de 128 bits. se divide la clave entre 8
            byte[] clave = password.GetBytes(128 / 8);
            //int keySize = 128;//clave de 128 bits
            //int ivSize = 16;//parametroIV de 16 bits
            //Array.Resize(ref clave, keySize);
            //Array.Resize(ref parametroIV, ivSize);

            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor =
                    symmetricKey.CreateEncryptor(clave, parametroIV);

            // Crear una instancia del algoritmo de Rijndael

            //Rijndael RijndaelAlg = Rijndael.Create();

            // Establecer un flujo en memoria para el cifrado

            MemoryStream memoryStream = new MemoryStream();

            // Crear un flujo de cifrado basado en el flujo de los datos

            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                         encryptor,
                                                         CryptoStreamMode.Write);

            // Obtener la representación en bytes de la información a cifrar
            byte[] plainMessageBytes = UTF8Encoding.UTF8.GetBytes(texto);

            // Cifrar los datos enviándolos al flujo de cifrado
            cryptoStream.Write(plainMessageBytes, 0, plainMessageBytes.Length);
            cryptoStream.FlushFinalBlock();

            // Obtener los datos datos cifrados como un arreglo de bytes
            byte[] cipherMessageBytes = memoryStream.ToArray();

            // Cerrar los flujos utilizados
            memoryStream.Close();
            cryptoStream.Close();

            // Retornar la representación de texto de los datos cifrados
            return Convert.ToBase64String(cipherMessageBytes);
        }
        
        /*
          *Propósito: Descifra el array de bytes usando las claves en formato XML
          *Entradas: string 
          *Salidas: string
          */
        //#Metodo descifrar
        public string descifrar(string texto)
        {
            string valorRGBSalt = "Frase_Encriptado";
            string algoritmoEncriptacionHASH = "MD5";
            byte[] saltValueBytes = Encoding.ASCII.GetBytes(valorRGBSalt);
            byte[] parametroIV = UTF8Encoding.ASCII.GetBytes("1234567891234567");
            byte[] cipherTextBytes = Convert.FromBase64String(texto);
            PasswordDeriveBytes password =
                    new PasswordDeriveBytes(key, saltValueBytes,
                        algoritmoEncriptacionHASH, 10);
            //clave de 128 bits. se divide la clave entre 8
            byte[] clave = password.GetBytes(128 / 8);
            // Obtener la representación en bytes del texto cifrado

            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform decryptor =
                    symmetricKey.CreateDecryptor(clave, parametroIV);

            MemoryStream memoryStream = new MemoryStream(cipherTextBytes);

            // Crear un flujo de descifrado basado en el flujo de los datos

            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                                                         decryptor,
                                                         CryptoStreamMode.Read);

            // Obtener los datos descifrados obteniéndolos del flujo de descifrado

            byte[] plainTextBytes = new byte[cipherTextBytes.Length];

            int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);

            memoryStream.Close();
            cryptoStream.Close();

            string textoDescifradoFinal = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);

            return textoDescifradoFinal;

            // Retornar la representación de texto de los datos descifrados

            // return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
        }

        /*
        *Propósito: Comprimir una cadena de texto
        *Entradas: string 
        *Salidas: string
        */
        //#Metodo comprimirString
        public string comprimirString(string cadena)
        {

            //Arreglo de Bytes que recibe un string
            byte[] buffer = Encoding.UTF8.GetBytes(cadena);


            var memoryStream = new MemoryStream();

            //Creo una variable GZipStream
            using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gZipStream.Write(buffer, 0, buffer.Length);
            }

            memoryStream.Position = 0;

            //Variable para comprimir
            var compressedData = new byte[memoryStream.Length];

            memoryStream.Read(compressedData, 0, compressedData.Length);

            var gZipBuffer = new byte[compressedData.Length + 4];
            Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);

            return Convert.ToBase64String(gZipBuffer);
        }
        
        /*
        *Propósito: descomprimir una cadena de texto
        *Entradas: string 
        *Salidas: string
        */
        //#Metodo descomprimirString
        public string descomprimirString(string cadenaComprimida)
        {
            byte[] gZipBuffer = Convert.FromBase64String(cadenaComprimida);
            using (var memoryStream = new MemoryStream())
            {
                int dataLength = BitConverter.ToInt32(gZipBuffer, 0);
                memoryStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);

                var buffer = new byte[dataLength];

                memoryStream.Position = 0;
                using (var gZipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gZipStream.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }

        /*
         *Propósito: Permite sber si se ha ralizado una sincrnizacio en la fecha dada
         *Entradas: DateTime
         *Salidas: bool
        */
        //#Metodo: haSincronizadoEnEstaFecha
        public bool haSincronizadoEnEstaFecha(DateTime fecha)
        {
            DAL contexto = new DAL();
            return contexto.haSincronizadoEnEstaFecha(fecha);
        }
        
        //+++++entrega final Edwin ++++++
        /*
         *Propósito: Permite editar los precios de productos usando un archivo de texto, que contiene 
         *parejas "codProducto, precio" en cada linea
         *Entradas: string
         *Salidas:
        */
        //#Metodo: editarPreciosProductos
        public void editarPreciosProductosDesdeArchivo(string rutaArchivo)
        {
            DAL contexto = new DAL();
            List<string> productos = contexto.leerArchivo(rutaArchivo);
            foreach (string linea in productos)
            {
                ProductoEntidad producto = new ProductoEntidad();
                var values = linea.Split(',');
                producto.CodProducto = Convert.ToInt32(values[0]);
                producto.Precio = Convert.ToInt32(values[1]);
                contexto.editarPrecioProducto(producto);
            }

        }

        /*
         *Propósito: Permite consultar los pedidos que se han realizado en un intervalo de tiempo
         *Entradas: DateTime, DateTime
         *Salidas: ObservableCollection<PedidoEntidad>
        */
        //#Metodo: consultarPedidoPorIntervaloDeTiempo
        public ObservableCollection<PedidoEntidad> consultarPedidoPorIntervaloDeTiempo(DateTime inicio, DateTime fin)
        {
            DAL contexto = new DAL();
            return contexto.consultarPedidoPorIntervaloDeTiempo(inicio, fin);

        }

        /*
         *Propósito: Permite consultar los pedidos que se han realizado en una fecha espcifica
         *Entradas: DateTime
         *Salidas: ObservableCollection<PedidoEntidad>
        */
        //#Metodo: consultarPedidoPorFecha
        public ObservableCollection<PedidoEntidad> consultarPedidoPorFecha(DateTime fecha)
        {
            DAL contexto = new DAL();
            return contexto.consultarPedidoPorFecha(fecha);
        }

        /*
         *Propósito: Permite editar un producto en la BD
         *Entradas: ProductoEntidad
         *Salidas: 
        */
        //#Metodo: editarProducto
        public void editarProducto(ProductoEntidad producto)
        {
            DAL contexto = new DAL();
            contexto.editarProducto(producto);
        }
        
        /*
        *Propósito: Permite crear un usuario en la base de datos
        *Entradas: UsuarioEntidad
        *Salidas:
       */
        //#Metodo: crearUsuario
        public void crearUsuario(UsuarioEntidad usuario)
        {
            DAL contexto = new DAL();
            contexto.crearUsuario(usuario);
        }

        /*
        *Propósito: Permite modificar los datos un usuario en la base de datos
        *Entradas: UsuarioEntidad
        *Salidas: 
        */
        //#Metodo: editarUsuario
        public void editarUsuario(UsuarioEntidad usuario)
        {
            DAL contexto = new DAL();
            contexto.editarUsuario(usuario);
        }

        /*
        *Propósito: Permite crear un perfil en la base de datos
        *Entradas: 
        *Salidas: Usuario
       */
        //#Metodo: crearPerfil
        public void crearPerfil(PerfilEntidad perfil)
        {
            DAL contexto = new DAL();
            contexto.crearPerfil(perfil);
        }

        /*
        *Propósito: Permite modificar los datos un perfil en la base de datos
        *Entradas: PerfilEntidad
        *Salidas: 
        */
        //#Metodo: editarPerfil
        public void editarPerfil(PerfilEntidad perfil)
        {
            DAL contexto = new DAL();
            contexto.editarPerfil(perfil);
        }

        /*
        *Propósito: Permite autenticar un usuario de acuerdo con su nombre de usuario y contrasenia,, 
        * si el usuario no existe se dvuelve null.
        *Entradas: string, string
        *Salidas: UsuarioEntidad
        */
        //#Metodo: autenticarUsuario
        public UsuarioEntidad autenticarUsuario(string nombreUsuario, string contrasenia)
        {
            DAL contexto = new DAL();
            return contexto.autenticarUsuario(nombreUsuario, contrasenia);
        }

        /*
        *Propósito: Permite determinar si se ha hecho un pedido para un negocio en una fecha despecifica.
        *Entradas: int, DateTime
        *Salidas: 
        */
        //#Metodo: haHechoPedido
        public bool haHechoPedido(int codNegocio, DateTime fecha)
        {
            DAL contexto = new DAL();
            return contexto.haHechoPedido(codNegocio, fecha);

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
            DAL contexto = new DAL();
            return contexto.consultarUsuarioPorNombre(nombre_usuario);

        }

        /*
       *Propósito: Permite consultar todos los usuarios de la base de datos.
       *Entradas: 
       *Salidas: ObservableCollection<UsuarioEntidad>
       */
        //#Metodo: consultarTodosLosUsuarios
        public ObservableCollection<UsuarioEntidad> consultarTodosLosUsuarios()
        {
            DAL contexto = new DAL();
            return contexto.consultarTodosLosUsuarios();

        }

        /*
       *Propósito: Permite consultar un pefil por su nombre en la base de datos.
       *Entradas: string
       *Salidas: ObservableCollection<PerfilEntidad>
       */
        //#Metodo: consultarPerfilPorNombre
        public ObservableCollection<PerfilEntidad> consultarPerfilPorNombre(string nombre_perfil)
        {
            DAL contexto = new DAL();
            return contexto.consultarPerfilPorNombre(nombre_perfil);

        }

        /*
       *Propósito: Permite consultar todos los perfiles de la base de datos.
       *Entradas: 
       *Salidas: ObservableCollection<PerfilEntidad>
       */
        //#Metodo: consultarTodosLosPerfiles
        public ObservableCollection<PerfilEntidad> consultarTodosLosPerfiles()
        {
            DAL contexto = new DAL();
            return contexto.consultarTodosLosPerfiles();
        }

        /*
        *Propósito: Permite consultar un pefil por su codigo en la base de datos.
        *Entradas: int
        *Salidas: PerfilEntidad
        */
        //#Metodo: consultarPerfilPorCodigo
        public PerfilEntidad consultarPerfilPorCodigo(int cod_perfil)
        {
            DAL contexto = new DAL();
            return contexto.consultarPerfilPorCodigo(cod_perfil);
        }

        /*
        *Propósito: Permite consultar un usuario por su codigo en la base de datos.
        *Entradas: int
        *Salidas: UsuarioEntidad
        */
        //#Metodo: consultarUsuarioPorCodigo
        public UsuarioEntidad consultarUsuarioPorCodigo(int cod_usuario)
        {
            DAL contexto = new DAL();
            return contexto.consultarUsuarioPorCodigo(cod_usuario);
        }

        /*
        *Propósito: Permite consultar un producto por su codigo en la base de datos.
        *Entradas: int
        *Salidas: ProductoEntidad
        */
        //#Metodo: consultarProductoPorCodigo
        public ProductoEntidad consultarProductoPorCodigo(int cod_producto)
        {
            DAL contexto = new DAL();
            return contexto.consultarProductoPorCodigo(cod_producto);
        }

        /*
        *Propósito: Permite consultar el inventario (cantidad diposnible) de los pruductos.
        *Entradas: 
        *Salidas: ObservableCollection<ProductoEntidad>
        */
        //#Metodo: consultarInventario        
        public ObservableCollection<ProductoEntidad> consultarInventario()
        {
            DAL contexto = new DAL();
            ObservableCollection<ProductoEntidad> inventario = contexto.consultarInventario();
            return inventario;
        }

        /*
        *Propósito: Permite consultar la cantidad de pedidos por cada producto en un intervalo de tiempo
        *Entradas: DateTime, DateTime
        *Salidas: List<string>
        */
        //#Metodo: consultarCantidadPedidoPorProducto
        public List<string> consultarCantidadPedidoPorProducto(DateTime inicio, DateTime fin)
        {
            DAL contexto = new DAL();
            return contexto.consultarCantidadPedidoPorProducto(inicio, fin);
        
        }

        /*
        *Propósito: Permite consultar la cantidad de pedidos por cada categoria en un intervalo de tiempo
        *Entradas: DateTime, DateTime
        *Salidas: List<string>
        */
        //#Metodo: consultarCatnidadPedidoPorCategoria
        public List<string> consultarCantidadPedidoPorCategoria(DateTime inicio, DateTime fin)
        {
            DAL contexto = new DAL();
            return contexto.consultarCantidadPedidoPorCategoria(inicio, fin);

        }

        /*
        *Propósito: Permite consultar la cantidad de pedidos hechos para una ciudad en un intervalo de tiempo
        *Entradas: DateTime, DateTime
        *Salidas: List<string>
        */
        //#Metodo: consultarPedidoDeUnaCiudad
        public ObservableCollection<PedidoEntidad> consultarPedidoDeUnaCiudad(string ciudad, DateTime inicio, DateTime fin)
        {
            DAL contexto = new DAL();
            return contexto.consultarPedidoDeUnaCiudad(ciudad, inicio, fin); ;
        }

        /*
        *Propósito: Permite consultar los vendedores que no han sincronizado en un día determinado.
        *Entradas: DateTime
        *Salidas: ObservableCollection<UsuarioEntidad>
        */
        //#Metodo: consultarVendedoresNoSincronizados
        public ObservableCollection<UsuarioEntidad> consultarVendedoresNoSincronizados(DateTime fechaYHora)
        {
            DAL contexto = new DAL();
            return contexto.consultarVendedoresNoSincronizados(fechaYHora);
        }

        /*
        *Propósito: Permite guardar productos cargando un archivo, donde cad linea es un producto
        *con los atributos separados por coma así: 
        *    NombreProducto,Cantidad,Precio,DescripcónPorducto,RutaFoto,CodUsuario, CodCatProducto.
        *Entradas: string
        *Salidas: 
        */
        //#Metodo: guardarNuevosProductosDesdeArchivo
        public void guardarNuevosProductosDesdeArchivo(string rutaArchivo)
        {
            DAL contexto = new DAL();
            List<string> productos = contexto.leerArchivo(rutaArchivo);
            foreach(string linea in productos)
            {
                ProductoEntidad pro = new ProductoEntidad();
                var values = linea.Split(',');
                pro.NombreProducto = values[0];
                pro.Cantidad = Convert.ToInt32(values[1]);
                pro.Precio = Convert.ToInt32(values[2]);
                pro.DescripcionProducto = (values[3]);
                pro.RutaFoto = values[4];
                pro.CodUsuario = Convert.ToInt32(values[5]);
                pro.CodCatProducto = Convert.ToInt32(values[6]);
                contexto.crearProducto(pro);

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
            DAL contexto = new DAL();
            return contexto.consultarCantidadPedidosPorCiudad(inicio, fin);
        }

        /*
        *Propósito: Permite mandar un correo eletrónico
        *Entradas: DateTime, DateTime
        *Salidas: List<string>
        */
        //#Metodo: consultarCantidadPedidosPorCiudad
        public void MandarMensajeCorreo(string direccionCorreo)
        {

            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Timeout = 20000;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;

            smtp.Credentials = new NetworkCredential("zuluaaristi@gmail.com", "zuluaga-17");

            string from = "zuluaaristi@gmail.com";
            MailAddress to = new MailAddress(direccionCorreo);
            MailMessage message = new MailMessage(from, direccionCorreo, "Sincronizar", "Hola joven sincronize las ventas");

            message.BodyEncoding = UTF8Encoding.UTF8;
            message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            try
            {
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateCopyMessage(): {0}",
                            ex.ToString());
            }
        }
		
		
	/*Propósito: Permite cambiar la contraseña de un usuario.
        *Entradas: string, string, string
        *Salidas: UsuarioEntidad
        */
        //#Metodo: consultarUsuarioPorCodigo
        public bool cambiarContraseniaUsuario(string nombreUsuario, string contrasenaActual, string contrasenaNueva)
        {
            DAL contexto = new DAL();
            UsuarioEntidad usuario = contexto.autenticarUsuario(nombreUsuario, contrasenaActual);

            if (usuario != null)
            {
                usuario.ContrasenaUsuario = contrasenaNueva;
                contexto.editarUsuario(usuario);
                return true;
            }
            return false;
        }


         /*
        *Propósito: Permite crear una categoria en la base de datos.
        *Entradas: Categoria_ProductoEntidad 
        *Salidas: void
        */
        //#Metodo: crearCategoriaProducto
        public void crearCategoriaProducto(Categoria_ProductoEntidad categoria)
        {
            DAL contexto = new DAL();
            contexto.crearCategoriaProducto(categoria);
        }

        /*
        *Propósito: Permite modificar los datos un categoria de producto en la base de datos
        *Entradas: Categoria_ProductoEntidad
        *Salidas: 
        */
        //#Metodo: editarCategoria_ProductoEntidad
        public void editarCategoria_ProductoEntidad(Categoria_ProductoEntidad categoria)
        {
            DAL contexto = new DAL();
            contexto.editarCategoria_ProductoEntidad(categoria);
        }

        /*
         *Propósito: Permite crear un producto en la base de datos.
         *Entradas: ProductoEntidad 
         *Salidas:
         */
        //#Metodo: crearProducto
        public void crearProducto(ProductoEntidad producto)
        {
            DAL contexto = new DAL();
            contexto.crearProducto(producto);
        }

        /*
         *Propósito: Permite consultar un producto por su nombre a la base de datos y lo retorna si hay al menos 1
         * en el inventario.
         *Entradas: string nombre_producto
         *Salidas: ObservableCollection<ProductoEntidad>
        */
        //#Metodo: ConsultarProductoDisponiblePorNombre
      public  ObservableCollection<ProductoEntidad> consultarProductoDisponiblePorNombre(string nombre_producto){
            DAL contexto = new DAL();
            return contexto.consultarProductoDisponiblePorNombre(nombre_producto);
        }

        /*
         *Propósito: Permite consultar un producto por su categoria a la base de datos cuando el inventario es mayor a 0.
         *Entradas: Categoria_ProductoEntidad categoria
         *Salidas: ObservableCollection<ProductoEntidad>
        */
        //#Metodo: consultarProductoDisponiblePorCategoria
       public ObservableCollection<ProductoEntidad> consultarProductoDisponiblePorCategoria(string nombreCategoria){
            DAL contexto = new DAL();
            return contexto.consultarProductoDisponiblePorCategoria(nombreCategoria);
        }

        /*
         *Propósito: Permite consultar un producto por nombre y categoria a la base de datos cuando el inventario es mayor a 0.
         *Entradas: Categoria_ProductoEntidad categoria, string nombre_producto
         *Salidas: ObservableCollection<ProductoEntidad>
        */
        //#Metodo: consultarProductoPorNombreYCategoria
       public ObservableCollection<ProductoEntidad> consultarProductoDisponiblePorNombreYCategoria(string nombreCategoria, string nombre_producto)
        {
            DAL contexto = new DAL();
            return contexto.consultarProductoDisponiblePorNombreYCategoria(nombreCategoria, nombre_producto);
        }

       /*
       *Propósito: Permite consultar la cantidad de pedidos hechos por dias de la semana
       *Entradas: DateTime, DateTime
       *Salidas: List<string>
       */
       //#Metodo: consultarCantidadPedidosPorDiasSemana
       public List<String> consultarCantidadPedidosPorDiasSemana(DateTime inicio, DateTime fin)
       {
           DAL contexto = new DAL();
           return contexto.consultarCantidadPedidosPorDiasSemana(inicio, fin);
       }

       /*Propósito: Permite consultar una ctaegoria de producto por su codigo en la base de datos.
        *Entradas: int
        *Salidas: Categoria_ProductoEntidad
        */
        //#Metodo: consultarCategoriaPorCodigo
        public Categoria_ProductoEntidad consultarCategoriaPorCodigo(int cod_categoria)
        {
            DAL contexto = new DAL();
            return contexto.consultarCategoriaPorCodigo(cod_categoria);
        }


    }

     
}
