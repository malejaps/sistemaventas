/*
* NombreClase: ProductoEntidad
* Autores: Edwin Gamboa - 1310233
* Fecha: 08/May/2015
* Descripcion: Clase que representa un producto.
*/

/*
* Métodos de la clase:
* ProductoEntidad generarProducto(int cod_producto, string nombre_producto, int cantidad,
            int precio, string descripcion_producto, string ruta_foto, int cod_usuario, 
            int cod_cat_producto)
*/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace SistemaVentasEntidades
{
    public class ProductoEntidad
    {
		/*
         *Propósito: Método que permite crear un objeto de tipo ProductoEntidad
         *Entradas: cod_producto, nombre_producto, cantidad, precio, descripcion_producto,
		 * ruta_foto, cod_usuario, cod_cat_producto.
         *Salidas: ProductoEntidad
         */
        //#Metodo: generarProducto 
        public static ProductoEntidad generarProducto(int cod_producto, string nombre_producto, int cantidad,
            int precio, string descripcion_producto, string ruta_foto, int cod_usuario, 
            int cod_cat_producto)
               {
                   return new ProductoEntidad
                   {
                       CodProducto = cod_producto,
                       NombreProducto = nombre_producto,
                       Cantidad = cantidad,
                       Precio = precio,
                       DescripcionProducto = descripcion_producto,
                       RutaFoto = ruta_foto,
                       CodUsuario= cod_usuario,
                       CodCatProducto = cod_cat_producto

                   };
               }
        [Display(Name = "Codigo del producto")]
        public int CodProducto { get; set; }

        [Display(Name = "Nombre del producto")]
        [Required]
        public string NombreProducto { get; set; }

        [Display(Name = "Cantidad")]
        [Required]
        public int Cantidad { get; set; }

        [Display(Name = "Precio")]
        [Required]
        public int Precio { get; set; }

        [Display(Name = "Descripcion del Producto")]
        [Required]
        public string DescripcionProducto { get; set; }

        [Display(Name = "Ruta actual de la foto")]
        public string RutaFoto { get; set; }

        //[Display(AutoGenerateField=false)]
        public int CodUsuario { get; set; }

        [Display(Name = "Categoria")]
        [Required]
        public int CodCatProducto { get; set; }




        public Categoria_ProductoEntidad Categoria_Producto { get; set; }
        public ObservableCollection<PedidoProductoEntidad> PedidoProducto { get; set; }
        public UsuarioEntidad Usuario { get; set; }


    }
}
