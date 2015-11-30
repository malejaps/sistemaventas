/*
* NombreClase: HomeController.cs
* Autores: Maria Alejandra Pabon - 1310263
* Fecha: 13/Junio/2015
* Descripcion: 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaVentasWeb.Controllers
{
    public class HomeController : Controller
    {
        /*
       *Propósito: Muestra la vista de inicio
       *Entradas: void
       *Salidas: ActionResult
       */
        //#Metodo: Index
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}