/*
* NombreClase: PerfilEntidad
* Autores: Edwin Gamboa - 1310233
* Fecha: 08/May/2015
* Descripcion: Clase que representa un perfil de usuario.
*/

/*
* Métodos de la clase:
* PerfilEntidad generarPerfil(int cod_perfil, string nombre)
*/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaVentasEntidades
{
    public class PerfilEntidad
    {
		/*
         *Propósito: Método que permite crear un objeto de tipo PerfilEntidad
         *Entradas: cod_perfil, nombre
         *Salidas: PerfilEntidad
         */
        //#Metodo: generarPerfil 
        public static PerfilEntidad generarPerfil(int cod_perfil, string nombre)
        {

            return new PerfilEntidad
            {
                CodPerfil = cod_perfil,
                Nombre = nombre,
                

            };
       
        }

        public int CodPerfil { get; set; }
        public string Nombre { get; set; }

        public ObservableCollection<UsuarioEntidad> Usuario { get; set; }
        }

    }

