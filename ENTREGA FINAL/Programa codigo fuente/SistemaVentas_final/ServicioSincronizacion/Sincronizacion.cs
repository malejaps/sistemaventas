/*
* NombreClase: Sincronizacion.cs
* Autores: 
* Maria Alejandra Pabon - 1310263
* Fecha: 28/May/2015
* Descripcion: Clase que se encarga de realizar la sincronizacion de bajada y de subida entre las bases de datos del cliente y del servidor
*/

/*
* Métodos de la clase:
* string sincronizacionBajada()
* string sincronizacionSubida()
*/
using SistemaVentasBL;
using SistemaVentasEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServicioSincronizacion
{
    
    public class Sincronizacion : ISincronizacion
    {
  
       BL blServidor = new BL();
       AuditoriaEntidad auditoria;


        /*
        *Propósito: Metodo que se encarga de gestionar la sincronizacion de bajada
        *Entradas: int codigoUltimoProducto, int codigoUltimoNegocio 
        *Salidas: string
        */
       //#Metodo: sincronizacionBajada
        public string sincronizacionBajada(int codigoUltimoProducto, int codigoUltimoNegocio, int codigoUsuario)
        {
          
            string cadenaComprimidaBajada = "";
            string XMLGenerado = blServidor.generarXmlProductosNegocios(codigoUltimoProducto, codigoUltimoProducto);

            cadenaComprimidaBajada=blServidor.comprimirString(blServidor.cifrar(XMLGenerado));

            if (cadenaComprimidaBajada == null)
            {
                Console.WriteLine("Los datos de sincronizacion de bajada son nulos");
            }
            auditoria = new AuditoriaEntidad();
            auditoria.CodUsuario = codigoUsuario;
            auditoria.Descripcion = "Bajada de nuevos productos y nuevos negocios";
            auditoria.Fecha = DateTime.Now;
            blServidor.crearAuditoria(auditoria);
           
           
            return cadenaComprimidaBajada;
        }

        /*
        *Propósito: Metodo que se encarga de gestionar la sincronizacion de subida
        *Entradas: string cadenaComprimidaSubida 
        *Salidas: void
        */
        //#Metodo: sincronizacionSubida
        public void sincronizacionSubida(string cadenaComprimidaSubida, int codigoUsuario)
        {
            if (cadenaComprimidaSubida == null)
            {
                Console.WriteLine("Los datos de sincronizacion de subida son nulos");
            }

            blServidor.guardarNuevosPedidos(blServidor.descifrar(blServidor.descomprimirString(cadenaComprimidaSubida)));

            auditoria = new AuditoriaEntidad();
            auditoria.CodUsuario = codigoUsuario;
            auditoria.Descripcion = "Subida de nuevos pedidos";
            auditoria.Fecha = DateTime.Now;
            blServidor.crearAuditoria(auditoria);
          
           

        }
    }
}
