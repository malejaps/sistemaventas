/*
* NombreClase: NegocioEntidad
* Autores: Edwin Gamboa - 1310233
* Fecha: 08/May/2015
* Descripcion: Clase que representa una Categoria de un Negocio.
*/

/*
* Métodos de la clase:
* NegocioEntidad generarNegocio(int cod_negocio, string nit_negocio, string nombre_negocio, string ciudad, string direccion)
*/

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentasEntidades
{
    public class NegocioEntidad
    {

		/*
         *Propósito: Método que permite crear un objeto de tipo NegocioEntidad
         *Entradas: cod_negocio, nit_negocio, nombre_negocio, ciudad, direccion
         *Salidas: NegocioEntidad
         */
        //#Metodo: generarNegocio 
        public static NegocioEntidad generarNegocio(int cod_negocio, string nit_negocio, string nombre_negocio, string ciudad, string direccion)
        {
            return new NegocioEntidad
            {
                CodNegocio = cod_negocio,
                NitNegocio = nit_negocio,
                NombreNegocio = nombre_negocio,
                Ciudad = ciudad,
                Direccion = direccion
            };
        }

        public int CodNegocio { get; set; }
        public string NitNegocio { get; set; }
        public string NombreNegocio { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }

        public ObservableCollection<PedidoEntidad> Pedido { get; set; }



    }
}

