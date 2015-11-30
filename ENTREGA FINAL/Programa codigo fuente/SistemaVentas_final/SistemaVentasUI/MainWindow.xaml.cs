/*
* NombreClase: MainWindow.xaml.cs
* Autores: Maria Alejandra Pabon - 1310263,  Roger Andres Fernandez - 1310229
* Fecha: 10/May/2015
* Descripcion: Clase que se encarga de realizar la interfaz grafica del programa y traer o enviar los datos a la base de datos.
* Es la clase code behind de MainWindow.xaml.
*/

/*
* Métodos de la clase:
* public MainWindow()
* void bt_BuscarNegocio_Click(object sender, RoutedEventArgs e)
* void bt_listarPedido_Click(object sender, RoutedEventArgs e)
* void bt_abrirDetalle_Click(object sender, RoutedEventArgs e)
* void bt_volver_Click(object sender, RoutedEventArgs e)
* void consultarProductos(string nombreProducto, string nombreCategoria)  
* void bt_crearPedido_Click(object sender, RoutedEventArgs e)  
* void bt_agregarProducto_Click(object sender, RoutedEventArgs e)  
* void borrarProducto_Click(object sender, RoutedEventArgs e)
* void bt_BuscarProducto_Click(object sender, RoutedEventArgs e)
* void tb_IngresarNombreProducto_KeyUp(object sender, KeyEventArgs e)
* void registrarPedido(object sender, RoutedEventArgs e)
* void cancelarPedido(object sender, RoutedEventArgs e)
* void seleccionarCantidad_Click(object sender, SelectionChangedEventArgs e)
* void actualizarCantidad_Click(object sender, SelectionChangedEventArgs e)
* void controlRegistroPedidos() 
* void bt_gestionarPedido_Click(object sender, RoutedEventArgs e) 
* void bt_sincronizar_Click(object sender, RoutedEventArgs e) 
* void seleccionNegocio_Click(object sender, SelectionChangedEventArgs e) 
* void seleccionProductosEnPedido(object sender, SelectionChangedEventArgs e)
* lv_ListadoPedidos_SelectionChanged(object sender, SelectionChangedEventArgs e) 
*/

using SistemaVentasBL;
using SistemaVentasEntidades;
using SistemaVentasUI.ServicioSincronizacion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SistemaVentasUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        BL bl = new BL();

        ObservableCollection<PedidoEntidad> pedidos = new ObservableCollection<PedidoEntidad>();       
        ObservableCollection<PedidoProductoEntidad> pedidosProducto = new ObservableCollection<PedidoProductoEntidad>();
        ObservableCollection<ProductoEntidad> productos = new ObservableCollection<ProductoEntidad>();
        ObservableCollection<NegocioEntidad> listanegocios;
        ObservableCollection<ProductoEntidad> listaproductos;
        ObservableCollection<ProductoEntidad> listaproductosParaPedido;
        ObservableCollection<PedidoProductoEntidad> pedidoCompleto;
        ProductoEntidad producto = new ProductoEntidad();
        PedidoEntidad pedido;
        PedidoProductoEntidad pedidoProducto = new PedidoProductoEntidad();
        NegocioEntidad negocio = new NegocioEntidad();
        UsuarioEntidad usuario = new UsuarioEntidad();
        int totalPedido;


        /*
         *Propósito: Método constructor que inicia la pantalla principal y carga al vendedor
         *Entradas:
         *Salidas: 
         */
        //#Metodo: MainWindow       
        public MainWindow()
        {
            try
            {
                InitializeComponent();

                //Cargar info del vendedor
                //Por defecto esta el primer vendedor de la bd           
                usuario = bl.consultarVendedorPorCodigo(1);
                //this.im_FotoVendedor.Source = new BitmapImage(new Uri(@"\ImagenesProducto\doritos.jpg",  UriKind.Relative));                                 
                this.im_FotoVendedor.Source = new BitmapImage(new Uri(@"" + usuario.RutaFoto, UriKind.Relative));
                this.tb_NombreVendedor.Text = usuario.Nombre;
                listaproductos = new ObservableCollection<ProductoEntidad>();
                listaproductosParaPedido = new ObservableCollection<ProductoEntidad>();
                listanegocios = new ObservableCollection<NegocioEntidad>();
                pedidoCompleto = new ObservableCollection<PedidoProductoEntidad>();
                totalPedido = 0;
            }
            catch (Exception e)
            {
                MessageBox.Show("Debe correr el script de la base de datos local. Además debe asegurarse de que\n"
                    + "el nombre del servidor de la base de datos local sea el de su maquina.");
                Console.WriteLine(e);
                this.Close();
            }


        }

        /*
        *Propósito: Método que se encarga de gestionar el evento del boton que busca un negocio por su nombre
        *Entradas: object sender, RoutedEventArgs e
        *Salidas: void
        */
        //#Metodo: bt_BuscarNegocio_Click
        private void bt_BuscarNegocio_Click(object sender, RoutedEventArgs e)
        {
            /*
            negocio = bl.consultarNegocioPorNombre(this.tb_IngresarNombreNegocio.Text);

            if (negocio != null)
            {
                this.tb_NombreNeg.Text = negocio.NombreNegocio;
                this.tb_DireccionNeg.Text = negocio.Direccion;
                this.tb_NitNeg.Text = negocio.NitNegocio;
                this.tb_CiudadNeg.Text = negocio.Ciudad;
            }
            else {

                MessageBox.Show("Debe ingresar un nombre correcto del negocio");

            
            }*/

        }

        /*
        *Propósito: Método que se encargar de gestionar el evento del boton que lista a los pedidos de un negocio
        *Entradas: object sender, RoutedEventArgs e
        *Salidas: void
        */
        //#Metodo: bt_listarPedido_Click
        private void bt_listarPedido_Click(object sender, RoutedEventArgs e)
        {

            int seleccion = this.lv_listadoNegocios.SelectedIndex;
            this.gr_CrearPedido.Visibility = System.Windows.Visibility.Hidden;
            this.gr_productos.Visibility = System.Windows.Visibility.Hidden;
            this.gr_DetallePedido.Visibility = System.Windows.Visibility.Hidden;
            this.gr_ListarPedidos.Visibility = System.Windows.Visibility.Visible;


            ;



            if (seleccion != -1)
            {
                NegocioEntidad negocio = listanegocios[seleccion];
                ObservableCollection<PedidoEntidad> pedidos = bl.consultarPedidoPorNegocio(negocio);
                this.lv_ListadoPedidos.ItemsSource = pedidos;
                //Por binding se envia fecha y valor total de cada pedido
                //por codebehind se enviar el total de pedidos calculado           
                this.tb_TotalPedidos.Text = pedidos.Count.ToString();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un negocio");
            }

        }

        /*
        *Propósito: Método que se encargar de gestionar el evento del boton que abre el detalle de un pedido
        *Entradas: object sender, RoutedEventArgs e
        *Salidas: void
        */
        //#Metodo: bt_abrirDetalle_Click
        private void bt_abrirDetalle_Click(object sender, RoutedEventArgs e)
        {
            int seleccionNegocio = lv_listadoNegocios.SelectedIndex;
            this.gr_DetallePedido.Visibility = System.Windows.Visibility.Visible;
            this.gr_productos.Visibility = System.Windows.Visibility.Hidden;
            this.gr_CrearPedido.Visibility = System.Windows.Visibility.Hidden;
            this.gr_ListarPedidos.Visibility = System.Windows.Visibility.Hidden;

            pedidos=bl.consultarPedidoPorNegocio(listanegocios[seleccionNegocio]);

            int seleccionPedidos = this.lv_ListadoPedidos.SelectedIndex;
            if (seleccionPedidos != -1)
            {
                pedidosProducto = pedidos[seleccionPedidos].PedidoProducto;
                this.tb_TotalPedidoDetalle.Text = pedidos[seleccionPedidos].TotalPedido.ToString();
            }
            else
            {

                MessageBox.Show("Debe seleccionar un pedido");
            }

            for (int i = 0; i < pedidosProducto.Count; i++)
            {

                productos.Add(pedidosProducto[i].Producto);

            }

            this.lv_ListadoProductosDePedido.ItemsSource = productos;
            this.lv_CantidadProductoPedido.ItemsSource = pedidosProducto;
        }

        /*
        *Propósito: Método que se encargar de gestionar el evento del boton que se devuelve de detalle pedido a la lista de pedidos
        *Entradas: object sender, RoutedEventArgs e
        *Salidas: void
        */
        //#Metodo: void bt_volver_Click
        private void bt_volver_Click(object sender, RoutedEventArgs e)
        {
            this.gr_CrearPedido.Visibility = System.Windows.Visibility.Hidden;
            this.gr_productos.Visibility = System.Windows.Visibility.Hidden;
            this.gr_DetallePedido.Visibility = System.Windows.Visibility.Hidden;
            this.gr_ListarPedidos.Visibility = System.Windows.Visibility.Visible;

        }



        /*
        *Propósito: Método que se encarga de gestionar el evento del boton consulta a un producto de acuerdo a un filtro
        *Entradas: object sender, RoutedEventArgs e
        *Salidas: void
        */
        //#Metodo: consultarProductos      
        private void consultarProductos(string nombreProducto, string nombreCategoria)
        {

            if (!nombreCategoria.Equals("Categoria del producto") && !nombreProducto.Equals(""))
                listaproductos = bl.consultarProductoDisponiblePorNombreYCategoria(nombreCategoria, nombreProducto);
            else
                if (nombreCategoria.Equals("Categoria del producto") && !nombreProducto.Equals(""))
                    listaproductos = bl.consultarProductoDisponiblePorNombre(nombreProducto);
                else
                    if (!nombreCategoria.Equals("Categoria del producto") && nombreProducto.Equals(""))
                        listaproductos = bl.consultarProductoDisponiblePorCategoria(nombreCategoria);
                    else
                        listaproductos.Clear();
            this.lv_listadoProductos.ItemsSource = listaproductos;

        }


        /*
        *Propósito: Método que se encarga de gestionar el evento del boton que crea a un pedido
        *Entradas: object sender, RoutedEventArgs e
        *Salidas: void
        */
        //#Metodo: void bt_crearPedido_Click
        private void bt_crearPedido_Click(object sender, RoutedEventArgs e)
        {

            this.gr_CrearPedido.Visibility = System.Windows.Visibility.Visible;
            this.gr_productos.Visibility = System.Windows.Visibility.Visible;
            this.gr_ListarPedidos.Visibility = System.Windows.Visibility.Hidden;
            this.gr_DetallePedido.Visibility = System.Windows.Visibility.Hidden;
            pedidos.Clear();
            pedidosProducto.Clear();
            pedidoCompleto.Clear();
            List<string> categoriasItem = new List<string>();
            int seleccionNegocio = this.lv_listadoNegocios.SelectedIndex;
            if (listanegocios[seleccionNegocio] != null)
            {
                ObservableCollection<Categoria_ProductoEntidad> categorias = bl.listarCategorias();
                categoriasItem.Add("Categoria del producto");
                for (int i = 0; i < categorias.Count; i++)
                    categoriasItem.Add(categorias[i].NombreCatProducto);
                this.cb_CategoriasProductos.ItemsSource = categoriasItem;

            }
            this.cb_CategoriasProductos.SelectedIndex = 0;
        }


        /*
        *Propósito: Método que se encargar de gestionar el evento del boton que agrega un producto a un pedido
        *Entradas: object sender, RoutedEventArgs e
        *Salidas: void
        */
        //#Metodo: bt_agregarProducto_Click
        private void bt_agregarProducto_Click(object sender, RoutedEventArgs e)
        {
            int cantidad = 0;
            int codProducto = 0;

            int seleccion = this.lv_listadoProductos.SelectedIndex;
            if (seleccion != -1)
            {


                if (!listaproductosParaPedido.Contains(listaproductos[seleccion]) && !cb_seleCantidadProducto.SelectedValue.Equals("Cantidad"))
                {
                    codProducto = listaproductos[seleccion].CodProducto;
                    cantidad = Convert.ToInt32(cb_seleCantidadProducto.SelectedValue);
                    listaproductos[seleccion].Cantidad = cantidad;
                    listaproductosParaPedido.Add(listaproductos[seleccion]);
                    pedidoCompleto.Add(new PedidoProductoEntidad { Cantidad = cantidad, CodProducto = codProducto, Producto = listaproductos[seleccion] });
                    totalPedido += pedidoCompleto[pedidoCompleto.Count - 1].Cantidad * pedidoCompleto[pedidoCompleto.Count - 1].Producto.Precio;
                    this.tb_totalPedido.Text = "" + totalPedido;
                    this.lv_listadoPedido.ItemsSource = listaproductosParaPedido;
                    //this.sp_selecionCantidadProducto.Visibility = Visibility.Hidden;
                    this.lv_listadoProductos.SelectedItems.Clear();
                    controlRegistroPedidos();
                }
                else
                    MessageBox.Show("Debe seleccionar una cantidad para el producto");
            }
            else
                MessageBox.Show("Debe seleccionar un producto");

        }

        /*
        *Propósito: Método que se encargar de gestionar el evento del boton que borra un producto de un pedido
        *Entradas: object sender, RoutedEventArgs e
        *Salidas: void
        */
        //#Metodo:  borrarProducto_Click
        private void borrarProducto_Click(object sender, RoutedEventArgs e)
        {
            ////MessageBox.Show("" + lv_listadoPedido.SelectedIndex);
            int seleccion = this.lv_listadoPedido.SelectedIndex;
            if (seleccion != -1)
            {
                totalPedido -= listaproductosParaPedido[seleccion].Cantidad * listaproductosParaPedido[seleccion].Precio;
                listaproductosParaPedido.RemoveAt(seleccion);
                pedidoCompleto.RemoveAt(seleccion);
                this.tb_totalPedido.Text = "" + totalPedido;
                this.lv_listadoPedido.ItemsSource = listaproductosParaPedido;
                controlRegistroPedidos();
            }
            else
                MessageBox.Show("Debe seleccionar un producto");
        }

        /*
        *Propósito: Método que se encargar de gestionar el evento del boton que busca a un produco de acuerdo a un filtro
        *Entradas: object sender, RoutedEventArgs e
        *Salidas: void
        */
        //#Metodo:  bt_BuscarProducto_Clickt
        private void bt_BuscarProducto_Click(object sender, RoutedEventArgs e)
        {

            if (this.cb_CategoriasProductos == null)
            {
                //ObservableCollection<ProductoEntidad> productos = bl.ConsultarProductoPorNombre(this.tb_IngresarNombreProducto.Text);
                //this.lv_ListadoProductos.ItemsSource = productos;
            }
            else
            {
                //ObservableCollection<ProductoEntidad> productos = bl.ConsultarProductoPorCategoria(this.cb_CategoriasProductos.SelectedItem);
                //this.lv_ListadoProductos.ItemsSource = productos;

            }

        }


        /*
        *Propósito: Método que se encarga de consultar los productos cuadno se teclea dentro de la caja de texto
        *Entradas: object sender, RoutedEventArgs e
        *Salidas: void
        */
        //#Metodo:  tb_IngresarNombreProducto_KeyUp
        private void tb_IngresarNombreProducto_KeyUp(object sender, KeyEventArgs e)
        {


            consultarProductos(this.tb_IngresarNombreProducto.Text, (string)this.cb_CategoriasProductos.SelectedValue);
        }


        /*
        *Propósito: Método que se encarga de crear un pedido en la base de datos
        *Entradas: object sender, RoutedEventArgs e
        *Salidas: void
        */
        //#Metodo:  registrarPedido
        private void registrarPedido_Click(object sender, RoutedEventArgs e)
        {
            pedido = new PedidoEntidad();
            pedido.PedidoProducto = new ObservableCollection<PedidoProductoEntidad>();

            pedido.CodNegocio = negocio.CodNegocio;
            pedido.CodUsuario = usuario.CodUsuario;
            pedido.TotalPedido = totalPedido;
            pedido.Fecha = DateTime.Now;
            pedido.PedidoProducto = pedidoCompleto;
            pedido.Estado = "Creado";

            bl.crearPedido(pedido);
            int seleccionNegocio = this.lv_listadoNegocios.SelectedIndex;
            pedidoCompleto.Clear();
            listaproductosParaPedido.Clear();
            listaproductos.Clear();
            listanegocios.Clear();
            this.gr_CrearPedido.Visibility = System.Windows.Visibility.Hidden;
            this.gr_productos.Visibility = System.Windows.Visibility.Hidden;
            this.tb_IngresarNombreNegocio.Text = "";
            totalPedido = 0;
            tb_totalPedido.Text = "" + totalPedido;

            controlRegistroPedidos();
            MessageBox.Show("El pedido se registro exitosamente.");
        }


        /*
        *Propósito: Método que se encarga de cancelar un pedido
        *Entradas: object sender, RoutedEventArgs e
        *Salidas: void
        */
        //#Metodo: cancelarPedido
        private void cancelarPedido(object sender, RoutedEventArgs e)
        {
            listaproductosParaPedido.Clear();
            pedidoCompleto.Clear();
            totalPedido = 0;
            this.tb_totalPedido.Text = "" + totalPedido;
            this.sp_selecionCantidadProducto.Visibility = Visibility.Hidden;
        }


        /*
        *Propósito: Método que se encarga de capturar el evento para seleccionar la cantidad de un producto de un pedido
        *Entradas: object sender, RoutedEventArgs e
        *Salidas: void
        */
        //#Metodo:  seleccionarCantidad_Click
        private void seleccionarCantidad_Click(object sender, SelectionChangedEventArgs e)
        {

            consultarProductos(this.tb_IngresarNombreProducto.Text, (string)this.cb_CategoriasProductos.SelectedValue);
        }


        /*
        *Propósito: Método que se encarga de capturar el evento para actualizar la cantidad del pedido
        *Entradas: object sender, RoutedEventArgs e
        *Salidas: void
        */
        //#Metodo: actualizarCantidad_Click
        private void seleccionProducto_Click(object sender, SelectionChangedEventArgs e)
        {

            int seleccion = this.lv_listadoProductos.SelectedIndex;

            if (seleccion != -1)
            {
                if (!listaproductosParaPedido.Contains(listaproductos[seleccion]))
                {
                    List<string> itemCantidad = new List<string>();
                    itemCantidad.Add("Cantidad");
                    cb_seleCantidadProducto.SelectedItem = "Cantidad";

                   
                    
                    for (int i = 1; i <= listaproductos[seleccion].Cantidad; i++)
                        itemCantidad.Add("" + i);

                    cb_seleCantidadProducto.ItemsSource = itemCantidad;
                    this.sp_selecionCantidadProducto.Visibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("El producto seleccionado ya se encuentra en el pedido");
                    this.sp_selecionCantidadProducto.Visibility = Visibility.Hidden;
                }
            }
            else
                this.sp_selecionCantidadProducto.Visibility = Visibility.Hidden;
        }

        /*
        *Propósito: Método que se encarga de habilitar los botones aceptar y cancelar de crear pedido
        *Entradas: void
        *Salidas: void
        */
        //#Metodo: controlRegistroPedidos
        private void controlRegistroPedidos()
        {
            if (listaproductosParaPedido.Count == 0)
            {
                bt_aceptarPedido.IsEnabled = false;
                tb_cancelarPedido.IsEnabled = false;

            }
            else
            {
                bt_aceptarPedido.IsEnabled = true;
                tb_cancelarPedido.IsEnabled = true;
            }

        }


        /*
        *Propósito: Método que se encargar de gestionar el evento del boton que abre las opciones de gstionar un pedido: crearlo o listarlos
        *Entradas: object sender, RoutedEventArgs e
        *Salidas: void
        */
        //#Metodo: bt_gestionarPedido_Click
        private void bt_gestionarPedido_Click(object sender, RoutedEventArgs e)
        {
            this.gr_Negocio.Visibility = System.Windows.Visibility.Visible;

        }

        /*
        *Propósito: Método que se encargar de gestionar el evento de sincronizacion de subida y de bajada de los prodctos, negocios y pedidos entre el cliente y el servidor
        *Entradas: object sender, RoutedEventArgs e
        *Salidas: void
        */
        //#Metodo:  bt_sincronizar_Click
        private void bt_sincronizar_Click(object sender, RoutedEventArgs e)
        {
            DateTime now = DateTime.Now;
            string msj = "";
            bool todoVaBien = true;

            if (bl.haSincronizadoEnEstaFecha(now))
            {
                msj += "- Ya se ha realizado una sincronizacion el día de hoy\n";
                todoVaBien = false;
            }
            if (!(DateTime.Compare(now, Convert.ToDateTime("17:00:00")) >= 0) ||
                !(DateTime.Compare(now, Convert.ToDateTime("17:30:00")) <= 0))
            {
                todoVaBien = false;
                msj += "- Solo puede realizar la sincronización entre las 5:00 pm y las 5:30 pm";
            }
            if (todoVaBien)
            {

                //1.crear el proxy (puerta en la que yo acceso al servicio)
                SincronizacionClient proxy = new SincronizacionClient();

                BL blCliente = new BL();
                AuditoriaEntidad auditoria = new AuditoriaEntidad();


                // 2.usar el servicio 

                //Sincronizacion de bajada
                string cadenaComprimidaBajada = proxy.sincronizacionBajada(blCliente.ultimoProducto().CodProducto,
                    blCliente.ultimoNegocio().CodNegocio, usuario.CodUsuario);
                string cadenaDescifrada = blCliente.descifrar(blCliente.descomprimirString(cadenaComprimidaBajada));
                blCliente.guardarNuevosProductosYNegocios(cadenaDescifrada);


                auditoria.CodUsuario = usuario.CodUsuario;
                auditoria.Descripcion = "Bajada de nuevos productos y nuevos negocios";
                auditoria.Fecha = now;
                blCliente.crearAuditoria(auditoria);

                //Sincronizacion de subida
                string cadenaXMLSubida = blCliente.generarXmlNuevosPedidos();
                string cadenaCifrada = blCliente.cifrar(cadenaXMLSubida);
                proxy.sincronizacionSubida(blCliente.comprimirString(cadenaCifrada), usuario.CodUsuario);


                auditoria = new AuditoriaEntidad();
                auditoria.CodUsuario = usuario.CodUsuario;
                auditoria.Descripcion = "Subida de nuevos pedidos";
                auditoria.Fecha = now;
                blCliente.crearAuditoria(auditoria);

                //Console.WriteLine("Cadena bajada", cadenaComprimidaBajada);
                //Console.WriteLine("Cadena subida", cadenaComprimidaSubida);
                Console.ReadLine();


                //3.cerrar el proxy
                proxy.Close();
                msj = "Se realizo la sincronizacion exitosamente";
            }
            MessageBox.Show(msj);

        }


        /*
        *Propósito: Método que se encargar de ingresar el nombre del negocio
        *Entradas: object sender, RoutedEventArgs e
        *Salidas: void
        */
        //#Metodo:  tb_IngresarNombreNegocio_KeyUp
        private void tb_IngresarNombreNegocio_KeyUp(object sender, KeyEventArgs e)
        {
            pedidoCompleto.Clear();
            listaproductosParaPedido.Clear();
            listaproductos.Clear();
            this.tb_IngresarNombreProducto.Text = "";
            totalPedido = 0;
            this.tb_totalPedido.Text = "" + 0;
            this.gr_CrearPedido.Visibility = System.Windows.Visibility.Hidden;
            this.gr_productos.Visibility = System.Windows.Visibility.Hidden;
            this.gr_ListarPedidos.Visibility=System.Windows.Visibility.Hidden;
            this.gr_DetallePedido.Visibility = System.Windows.Visibility.Hidden;
            if (!this.tb_IngresarNombreNegocio.Text.Equals(""))
            {
                listanegocios = bl.consultarNegocioPorNombre(this.tb_IngresarNombreNegocio.Text);


            }
            else
            {

                listanegocios.Clear();
                this.btns_gestionarPedido.Visibility = System.Windows.Visibility.Hidden;
            }

            this.lv_listadoNegocios.ItemsSource = listanegocios;
        }

        /*
        *Propósito: Método que se encargar de seleccionar negocio
        *Entradas: object sender, RoutedEventArgs e
        *Salidas: void
        */
        //#Metodo:  seleccionNegocio_Click
        private void seleccionNegocio_Click(object sender, SelectionChangedEventArgs e)
        {
            pedidos.Clear();
            pedidosProducto.Clear();
            pedidoCompleto.Clear();
            listaproductosParaPedido.Clear();
            listaproductos.Clear();
            this.tb_IngresarNombreProducto.Text = "";
            totalPedido = 0;
            this.tb_totalPedido.Text = "" + 0;
            this.gr_CrearPedido.Visibility = System.Windows.Visibility.Hidden;
            this.gr_productos.Visibility = System.Windows.Visibility.Hidden;
            this.gr_ListarPedidos.Visibility = System.Windows.Visibility.Hidden;
            this.gr_DetallePedido.Visibility = System.Windows.Visibility.Hidden;
            



            int seleccionNegocio = this.lv_listadoNegocios.SelectedIndex;

            if (seleccionNegocio != -1)
            {
                negocio = listanegocios[seleccionNegocio];
                negocio.Pedido = bl.consultarPedidoPorNegocio(negocio);

                if (negocio.Pedido.Count != 0)
                {

                    if (!bl.haHechoPedido(negocio.CodNegocio, DateTime.Now))
                    {
                        this.btns_gestionarPedido.Visibility = Visibility.Visible;
                        this.bt_crearPedido.IsEnabled = true;
                    }
                    else
                    {
                        MessageBox.Show("El negocio seleccionado ya tiene un pedido agregado en la fecha " + DateTime.Now.Date);
                        this.btns_gestionarPedido.Visibility = Visibility.Visible;
                        this.bt_crearPedido.IsEnabled = false;
                    }
                }
                else
                {
                    this.btns_gestionarPedido.Visibility = Visibility.Visible;
                    this.bt_crearPedido.IsEnabled = true;
                }
            }
            else
                this.btns_gestionarPedido.Visibility = Visibility.Hidden;

        }

        /*
        *Propósito: Método que se encargar de seleccionar los prodcutos de un pedido
        *Entradas: object sender, RoutedEventArgs e
        *Salidas: void
        */
        //#Metodo:  seleccionProductosEnPedido
        private void seleccionProductosEnPedido(object sender, SelectionChangedEventArgs e)
        {
            int seleccionProductoPedido = lv_listadoPedido.SelectedIndex;
            if (seleccionProductoPedido != -1)
                this.bt_eliminarProducto.Visibility = Visibility.Visible;
            else
                this.bt_eliminarProducto.Visibility = Visibility.Hidden;

        }

        /*
        *Propósito: Método que se encargar de seleccionar los prodcutos de un pedido
        *Entradas: object sender, RoutedEventArgs e
        *Salidas: void
        */
        //#Metodo:  lv_ListadoPedidos_SelectionChanged
        private void lv_ListadoPedidos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int seleccionPedido = lv_ListadoPedidos.SelectedIndex;
            if (seleccionPedido != -1)
                this.bt_abrirDetalle.Visibility = Visibility.Visible;
            else
                this.bt_abrirDetalle.Visibility = Visibility.Hidden;
        }



    }
}
