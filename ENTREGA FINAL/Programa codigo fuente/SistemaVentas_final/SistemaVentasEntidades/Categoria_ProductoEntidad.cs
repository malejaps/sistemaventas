/*
* NombreClase: Categoria_ProductoEntidad
* Autores: Edwin Gamboa - 1310233
* Fecha: 08/May/2015
* Descripcion: Clase que representa una Categoria de un producto.
*/

/*
* Métodos de la clase:
* Categoria_ProductoEntidad generarCategoriaProducto(int cod_cat_producto, string nombre_cat_producto)
*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentasEntidades
{
    public class Categoria_ProductoEntidad
    {
		/*
         *Propósito: Método que permite crear un objeto de tipo Categoria_ProductoEntidad
         *Entradas: cod_cat_producto, nombre_cat_producto
         *Salidas: Categoria_ProductoEntidad
         */
        //#Metodo: generarCategoriaProducto 
        public static Categoria_ProductoEntidad generarCategoriaProducto(int cod_cat_producto, string nombre_cat_producto)
        {
            return new Categoria_ProductoEntidad { CodCatProducto = cod_cat_producto, NombreCatProducto = nombre_cat_producto };
        }
        [Display(Name = "Codigo de Categoria")]
        public int CodCatProducto { get; set; }

        [Display(Name = "Nombre de Categoria")]
        [StringLength(50)]
        public string NombreCatProducto { get; set; }

        public ObservableCollection<ProductoEntidad> Producto { get; set; }

    }
}
