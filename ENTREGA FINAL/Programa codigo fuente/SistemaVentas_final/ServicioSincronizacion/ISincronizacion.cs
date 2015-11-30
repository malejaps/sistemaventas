/*
* NombreClase: ISincronizacion.cs
* Autores: 
* Maria Alejandra Pabon - 1310263
* Fecha: 28/May/2015
* Descripcion: Clase que sirve de interfaz para la clase Sincronizacion.cs
*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServicioSincronizacion
{
    //Interfaz para la gestion del servicio. No cuenta como metodo.
    [ServiceContract]
    public interface ISincronizacion
    {
        [OperationContract]
        string sincronizacionBajada(int codigoUltimoProducto, int codigoUltimoNegocio, int codigoUsuario);

        [OperationContract]
        void sincronizacionSubida(string cadenaComprimidaSubida, int codigoUsuario);

    }
}
