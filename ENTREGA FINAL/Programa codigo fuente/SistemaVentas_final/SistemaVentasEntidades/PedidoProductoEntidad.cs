/*
* NombreClase: PedidoProductoEntidad
* Autores: Edwin Gamboa - 1310233
* Fecha: 08/May/2015
* Descripcion: Clase que representa un producto que pertenece a un pedido.
*/

/*
* Métodos de la clase:
* PedidoProductoEntidad generarPedidoProducto(int cod_pedido, int cod_producto, int cantidad)
*/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentasEntidades
{
    public class PedidoProductoEntidad
    {
 
		/*
         *Propósito: Método que permite crear un objeto de tipo PedidoProductoEntidad
         *Entradas: cod_pedido, cod_producto, cantidad
         *Salidas: PedidoProductoEntidad
         */
        //#Metodo: generarPedidoProducto 
        public static PedidoProductoEntidad generarPedidoProducto(int cod_pedido, int cod_producto, int cantidad)
        {
            return new PedidoProductoEntidad
            {
                CodPedido = cod_pedido,
                CodProducto = cod_producto,
                Cantidad = cantidad,
                
                
            };
        }

        [Display(Name = "Código de Pedido")]
        public int CodPedido { get; set; }

        [Display(Name = "Producto")]
        public int CodProducto { get; set; }

        [Display(Name = "Cantidad")]
        public int Cantidad { get; set; }
        
        public PedidoEntidad Pedido { get; set; }
        public ProductoEntidad Producto { get; set; }
        

    }

}