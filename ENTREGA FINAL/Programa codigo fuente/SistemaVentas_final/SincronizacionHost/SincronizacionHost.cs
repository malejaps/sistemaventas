/*
* NombreClase: Program.cs
* Autores: 
* Maria Alejandra Pabon - 1310263
* Fecha: 28/May/2015
* Descripcion: Clase que se encarga de levantar el host y postear. Ayuda a verificar que el servicio este corriendo.
*/

/*
* Métodos de la clase:
* string Main()
*/

using ServicioSincronizacion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace SincronizacionHost
{
    class SincronizacionHost
    {

        /*
         *Propósito: Método principal que se encargar de ejecutar el host
         *Entradas: string[] args
         *Salidas: void 
         */
        //#Metodo: Main() 
        static void Main(string[] args)
        {
            //Levantando un host y postear
            //1. Crear una uri
            Uri direccionBase = new Uri("http://localhost:8010/ServicioSincronizacion");
            //2. Crear la instancia del host
            ServiceHost autoHost = new ServiceHost(typeof(Sincronizacion), direccionBase);
            try
            {
                //3. crear el endpoint . direccion que se le pega a la direccion base
                autoHost.AddServiceEndpoint(typeof(ISincronizacion), new WSHttpBinding(), "ServicioSincronizacion");
                //4. habilitar  metadatos
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                autoHost.Description.Behaviors.Add(smb);
                //5. iniciar el servicio
                autoHost.Open();
                //notificamos que el servicio esta corriendo
                Console.WriteLine("Servicio corriendo...");
                Console.WriteLine("Presone <ENTER> para finalizar");
                Console.ReadLine();
                //6. cerrar el servicio
                autoHost.Close();
            }
            catch (CommunicationException ce)
            {

                Console.WriteLine("Ha ocurrido un error: {0}", ce.Message);
                autoHost.Abort();
                Console.ReadLine();


            }
        }
    }
}
