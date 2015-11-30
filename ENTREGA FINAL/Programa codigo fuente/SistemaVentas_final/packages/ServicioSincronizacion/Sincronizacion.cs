using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ServicioSincronizacion
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Sincronizacion" in both code and config file together.
    public class Sincronizacion : ISincronizacion
    {
        private string sincronizacion;

        //Prueba
        public double Sumar(double n1, double n2)
        {
            double resultado = n1 + n2;
            Console.WriteLine(string.Format("recibe sumar ({0},{1})", n1, n2));
            Console.WriteLine(string.Format("retorna {0}", resultado));
            return resultado;
        }



        public string sincronizacionBajada()
        {
          
            //firma cifrar que devuelve una cadena
            //string cadenacifrada = Intefaz.Cifrar...;

            string cadenaComprimida = "ensayo bajada";

            if (cadenaComprimida == null)
            {
                Console.WriteLine("Los datos de sincronizacion son nulos");
            }

           
            return cadenaComprimida;
        }

        public string sincronizacionSubida()
        {

            //firma cifrar que devuelve una cadena
            //string cadenacifrada = Intefaz.Cifrar...;

            string cadenaComprimida = "ensayo subida";

            if (cadenaComprimida == null)
            {
                Console.WriteLine("Los datos de sincronizacion son nulos");
            }


            return cadenaComprimida;
        }
    }
}
