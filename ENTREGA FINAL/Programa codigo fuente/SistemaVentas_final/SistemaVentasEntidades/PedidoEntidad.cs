/*
* NombreClase: PedidoEntidad
* Autores: Edwin Gamboa - 1310233
* Fecha: 08/May/2015
* Descripcion: Clase que representa un Pedido.
*/

/*
* Métodos de la clase:
* PedidoEntidad generarPedido(int cod_negocio, int cod_usuario, int cod_pedido, DateTime fecha, int total_pedido)
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
   public class PedidoEntidad
    {
		/*
         *Propósito: Método que permite crear un objeto de tipo PedidoEntidad
         *Entradas: cod_negocio, cod_usuario, cod_pedido, fecha, total_pedido
         *Salidas: PedidoEntidad
         */
        //#Metodo: GenerarPedido 
        public static PedidoEntidad generarPedido(int cod_negocio, int cod_usuario, int cod_pedido,
                DateTime fecha, int total_pedido, string estado)
        {
            return new PedidoEntidad
            {
                CodNegocio = cod_negocio,
                CodUsuario = cod_usuario,
                CodPedido = cod_pedido,
                Fecha = fecha,
                TotalPedido = total_pedido,
                Estado = estado
                
            };
        }


        [Display(Name = "Código de Negocio")]
        public int CodNegocio { get; set; }

        [Display(Name = "Código de Usuario")]
        public int CodUsuario { get; set; }

        [Display(Name = "Código de Pedido")]
        public int CodPedido { get; set; }

        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }

        [Display(Name = "Total Pedido")]
        public int TotalPedido { get; set; }

        [Display(Name = "Estado")]
        public string Estado { get; set; }


        public  NegocioEntidad Negocio { get; set; }
        public  UsuarioEntidad Usuario { get; set; }
        public  ObservableCollection<PedidoProductoEntidad> PedidoProducto { get; set; }

    }
}
