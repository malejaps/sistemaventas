/*
* NombreClase: AuditoriaEntidad
* Autores: Edwin Gamboa - 1310233
* Fecha: 08/May/2015
* Descripcion: Clase que representa una Auitoria (registro de una sincronizacion) hecha por un usuario.
*/

/*
* Métodos de la clase:
* AuditoriaEntidad generarAuditoria(int cod_auditoria, string descripcion, int cod_usuario)
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentasEntidades
{
    public class AuditoriaEntidad
    {
		/*
         *Propósito: Método que permite crear un objeto de tipo AuditoriaEntidad
         *Entradas: cod_auditoria, descripcion, cod_usuario
         *Salidas: AuditoriaEntidad
         */
        //#Metodo: generarAuditoria 
        public static AuditoriaEntidad generarAuditoria(int cod_auditoria, string descripcion, int cod_usuario, 
            DateTime fecha)
        {
            return new AuditoriaEntidad
            {
                CodAuditoria = cod_auditoria,
                Descripcion = descripcion,
                CodUsuario = cod_usuario,
                Fecha = fecha

            };
        }

        public int CodAuditoria { get; set; }
        public string Descripcion { get; set; }
        public int CodUsuario { get; set; }
        public DateTime Fecha { get; set; }




        public UsuarioEntidad Usuario { get; set; }

    }

}