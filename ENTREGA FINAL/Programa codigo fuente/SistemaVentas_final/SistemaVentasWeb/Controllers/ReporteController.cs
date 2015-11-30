/*
* NombreClase: ReporteController.cs
* Autores: Maria Alejandra Pabon - 1310263
* Fecha: 12/Junio/2015
* Descripcion: clase encargada de generar los reportes de ventas
*/

/*
* ActionResult Index()
 * ActionResult VentasPorProducto()
 * ActionResult VentasPorProducto(DateTime fecha_inicio, DateTime fecha_fin)
 * ActionResult ReporteProducto(string id, DateTime fechai, DateTime fechaf)
 * ActionResult VentasPorCategoria()
 * ActionResult VentasPorCategoria(DateTime fecha_inicio, DateTime fecha_fin)
 * ActionResult ReporteCategoria(string id, DateTime fechai, DateTime fechaf)
 * ActionResult VentasPorCiudad()
 * ActionResult VentasPorCiudad(DateTime fecha_inicio, DateTime fecha_fin)
 * ActionResult ReporteCiudad(string id, DateTime fechai, DateTime fechaf)
 * ActionResult VentasPorDiaSemana()
 * ActionResult VentasPorDiaSemana(DateTime fecha_inicio, DateTime fecha_fin)
 * ActionResult ReporteDiaSemana(string id, DateTime fechai, DateTime fechaf)

*/
using Microsoft.Reporting.WebForms;
using SistemaVentasBL;
using SistemaVentasEntidades;
using SistemaVentasWeb.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace SistemaVentasWeb.Controllers
{
    public class ReporteController : Controller
    {

        BL bl = new BL();


        /*
          *Propósito: Controla el inicio de los reportes
          *Entradas: void
          *Salidas: ActionResult
          */
        //#Metodo: Index
        // GET: Reporte
        [Authorize(Roles = "Area de Ventas, Administrador")]
        public ActionResult Index()
        {                
            return View();
        }

        /*
        *Propósito: Genera la lista de ventas por producto
        *Entradas: void
        *Salidas: ActionResult
        */
        //#Metodo:VentasPorProducto 
        [Authorize(Roles = "Area de Ventas, Administrador")]
        public ActionResult VentasPorProducto()
        {
            List<ReporteProducto> pedidos = new List<ReporteProducto>();
            return View(pedidos);
        }


        /*
       *Propósito: Genera la lista de ventas por producto
       *Entradas: DateTime fecha_inicio, DateTime fecha_fin
       *Salidas: ActionResult
       */
        //#Metodo: VentasPorProducto  
         [Authorize(Roles = "Area de Ventas, Administrador")]
        [HttpPost]
        public ActionResult VentasPorProducto(DateTime fecha_inicio, DateTime fecha_fin)
        {
           
               
                List<ReporteProducto> pedidos = new List<ReporteProducto>();
                ReporteProducto pedidoTemporal;
            try
             {
                 List<string> pedidosProducto = bl.consultarCantidadPedidoPorProducto(fecha_inicio, fecha_fin);
                for (int i = 0; i < pedidosProducto.Count() - 1; i += 3)
                {
                    pedidoTemporal = new ReporteProducto();
                    pedidoTemporal.FechaInicio = fecha_inicio;
                    ViewBag.desde = "desde";
                    ViewBag.hasta = "hasta";
                    ViewBag.fechainicio = fecha_inicio.ToString("yyyy-MM-dd");
                    pedidoTemporal.FechaFin = fecha_fin;
                    ViewBag.fechafin = fecha_fin.ToString("yyyy-MM-dd");
                    pedidoTemporal.Codigo = pedidosProducto.ElementAt(i);
                    pedidoTemporal.Producto = pedidosProducto.ElementAt(i + 1);
                    pedidoTemporal.Cantidad = pedidosProducto.ElementAt(i + 2);
                    pedidos.Add(pedidoTemporal);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Seleccione las fechas para generar el reporte");
            }

            return View(pedidos);
            
          
        }


        /*
      *Propósito: Genera el reporte de ventas por producto
      *Entradas: DateTime fecha_inicio, DateTime fecha_fin
      *Salidas: ActionResult
      */
        //#Metodo: ReporteProducto  
         [Authorize(Roles = "Area de Ventas, Administrador")]
        public ActionResult ReporteProducto(string id, DateTime fechai, DateTime fechaf)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reportes"), "VentasPorProducto.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }


            List<string> pedidosProducto = bl.consultarCantidadPedidoPorProducto(fechai, fechaf);
            List<ReporteProducto> pedidos = new List<ReporteProducto>();
            ReporteProducto pedidoTemporal;

            for (int i = 0; i < pedidosProducto.Count() - 1; i += 3)
            {
                pedidoTemporal = new ReporteProducto();
                pedidoTemporal.FechaInicio = fechai.Date;
                pedidoTemporal.FechaFin = fechaf.Date;    
                pedidoTemporal.Codigo = pedidosProducto.ElementAt(i);
                pedidoTemporal.Producto = pedidosProducto.ElementAt(i + 1);
                pedidoTemporal.Cantidad = pedidosProducto.ElementAt(i + 2);
                pedidos.Add(pedidoTemporal);
            }


            ReportDataSource rd = new ReportDataSource("VentasPorProductoDataSet", pedidos);
            lr.DataSources.Add(rd);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>" + id + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);



            return File(renderedBytes, mimeType);
        }


        /*
        *Propósito: Genera la lista de ventas por producto
        *Entradas: void
        *Salidas: ActionResult
        */
        //#Metodo:VentasPorCategoria
         [Authorize(Roles = "Area de Ventas, Administrador")]
        public ActionResult VentasPorCategoria()
        {
            List<ReporteCategoria> pedidos = new List<ReporteCategoria>();
            return View(pedidos);
        }


        /*
      *Propósito: Genera la lista de ventas por categoria
      *Entradas: DateTime fecha_inicio, DateTime fecha_fin
      *Salidas: ActionResult
      */
        //#Metodo: VentasPorCategoria 
         [Authorize(Roles = "Area de Ventas, Administrador")]
        [HttpPost]
        public ActionResult VentasPorCategoria(DateTime fecha_inicio, DateTime fecha_fin)
        {
            
            List<ReporteCategoria> pedidos = new List<ReporteCategoria>();
            ReporteCategoria pedidoTemporal;

            try
            {
                List<string> pedidosCategoria = bl.consultarCantidadPedidoPorCategoria(fecha_inicio, fecha_fin);
                for (int i = 0; i < pedidosCategoria.Count() - 1; i += 3)
                {
                    pedidoTemporal = new ReporteCategoria();
                    pedidoTemporal.FechaInicio = fecha_inicio;
                    ViewBag.desde = "desde";
                    ViewBag.hasta = "hasta";
                    ViewBag.fechainicio = fecha_inicio.ToString("yyyy-MM-dd");
                    pedidoTemporal.FechaFin = fecha_fin;
                    ViewBag.fechafin = fecha_fin.ToString("yyyy-MM-dd");
                    pedidoTemporal.Codigo = pedidosCategoria.ElementAt(i);
                    pedidoTemporal.Categoria = pedidosCategoria.ElementAt(i + 1);
                    pedidoTemporal.Cantidad = pedidosCategoria.ElementAt(i + 2);
                    pedidos.Add(pedidoTemporal);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Seleccione las fechas para generar el reporte");
            }

            
            return View(pedidos);


        }

        /*
        *Propósito: Genera el reporte de ventas por categoria
        *Entradas: DateTime fecha_inicio, DateTime fecha_fin
        *Salidas: ActionResult
        */
        //#Metodo: ReporteCategoria  
         [Authorize(Roles = "Area de Ventas, Administrador")]
        public ActionResult ReporteCategoria(string id, DateTime fechai, DateTime fechaf)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Reportes"), "VentasPorCategoria.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }


            List<string> pedidosCategoria = bl.consultarCantidadPedidoPorCategoria(fechai, fechaf);
            List<ReporteCategoria> pedidos = new List<ReporteCategoria>();
            ReporteCategoria pedidoTemporal;

            for (int i = 0; i < pedidosCategoria.Count() - 1; i += 3)
            {
                pedidoTemporal = new ReporteCategoria();
                pedidoTemporal.FechaInicio = fechai.Date;
                pedidoTemporal.FechaFin = fechaf.Date;    
                pedidoTemporal.Codigo = pedidosCategoria.ElementAt(i);
                pedidoTemporal.Categoria = pedidosCategoria.ElementAt(i + 1);
                pedidoTemporal.Cantidad = pedidosCategoria.ElementAt(i + 2);
                pedidos.Add(pedidoTemporal);
            }


            ReportDataSource rd = new ReportDataSource("VentasPorCategoriaDataSet", pedidos);
            lr.DataSources.Add(rd);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>" + id + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);



            return File(renderedBytes, mimeType);
        }



        /*
        *Propósito: Genera la lista de ventas por ciudad
        *Entradas: void
        *Salidas: ActionResult
        */
        //#Metodo:VentasPorCiudad
         [Authorize(Roles = "Area de Ventas, Administrador")]
        public ActionResult VentasPorCiudad()
        {
            List<ReporteCiudad> pedidos = new List<ReporteCiudad>();
            return View(pedidos);
        }



        /*
        *Propósito: Genera la lista de ventas por ciudad
        *Entradas: DateTime fecha_inicio, DateTime fecha_fin
        *Salidas: ActionResult
        */
        //#Metodo: VentasPorCiudad 
         [Authorize(Roles = "Area de Ventas, Administrador")]
        [HttpPost]
        public ActionResult VentasPorCiudad(DateTime fecha_inicio, DateTime fecha_fin)
        {

            
             
                List<ReporteCiudad> pedidos = new List<ReporteCiudad>();
                ReporteCiudad pedidoTemporal;
        try
                {
                    List<string> pedidosCiudad = bl.consultarCantidadPedidosPorCiudad(fecha_inicio, fecha_fin);
                for (int i = 0; i < pedidosCiudad.Count() - 1; i += 2)
                {
                    pedidoTemporal = new ReporteCiudad();
                    pedidoTemporal.FechaInicio = fecha_inicio;
                    ViewBag.desde = "desde";
                    ViewBag.hasta = "hasta";
                    ViewBag.fechainicio = fecha_inicio.ToString("yyyy-MM-dd");
                    pedidoTemporal.FechaFin = fecha_fin;
                    ViewBag.fechafin = fecha_fin.ToString("yyyy-MM-dd");
                    pedidoTemporal.Ciudad = pedidosCiudad.ElementAt(i);
                    pedidoTemporal.Cantidad = pedidosCiudad.ElementAt(i + 1);
                    pedidos.Add(pedidoTemporal);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Seleccione las fechas para generar el reporte");
            }

     
            return View(pedidos);
        }


        /*
        *Propósito: Genera el reporte de ventas por ciudad
        *Entradas: DateTime fecha_inicio, DateTime fecha_fin
        *Salidas: ActionResult
        */
        //#Metodo: VentasPorCiudad   
         [Authorize(Roles = "Area de Ventas, Administrador")]
        public ActionResult ReporteCiudad(string id, DateTime fechai, DateTime fechaf)
        {
            LocalReport lr = new LocalReport();
            string fileName = string.Concat("VentasPorCiudad.pdf"); 
            string path = Path.Combine(Server.MapPath("~/Reportes"), "VentasPorCiudad.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }

        
            List<string> pedidosCiudad = bl.consultarCantidadPedidosPorCiudad(fechai, fechaf);
            List<ReporteCiudad> pedidos = new List<ReporteCiudad>();
            ReporteCiudad pedidoTemporal;

            for (int i = 0; i < pedidosCiudad.Count() - 1; i += 2)
            {
                pedidoTemporal = new ReporteCiudad();
                pedidoTemporal.FechaInicio = fechai.Date;
                pedidoTemporal.FechaFin = fechaf.Date;
                pedidoTemporal.Ciudad = pedidosCiudad.ElementAt(i);
                pedidoTemporal.Cantidad = pedidosCiudad.ElementAt(i + 1);
                pedidos.Add(pedidoTemporal);
            }
         
   
            ReportDataSource rd = new ReportDataSource("VentasPorCiudadDataSet", pedidos);
            lr.DataSources.Add(rd);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>" + id + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);



            return File(renderedBytes, mimeType);
        }



        /*
       *Propósito: Genera la lista de ventas por dia de la semana
       *Entradas: void
       *Salidas: ActionResult
       */
        //#Metodo:VentasPorDiaSemana
         [Authorize(Roles = "Area de Ventas, Administrador")]
        public ActionResult VentasPorDiaSemana()
        {
            List<ReporteDiaSemana> pedidos = new List<ReporteDiaSemana>();
            return View(pedidos);
        }


        /*
       *Propósito: Genera la lista de ventas por dia de la semana
       *Entradas: DateTime fecha_inicio, DateTime fecha_fin
       *Salidas: ActionResult
       */
        //#Metodo: VentasPorDiaSemana  
         [Authorize(Roles = "Area de Ventas, Administrador")]
        [HttpPost]
        public ActionResult VentasPorDiaSemana(DateTime fecha_inicio, DateTime fecha_fin)
        {
            List<ReporteDiaSemana> pedidos = new List<ReporteDiaSemana>();
           ReporteDiaSemana pedidoTemporal;
            try
            {
                List<string> pedidosDiaSemana= bl.consultarCantidadPedidosPorDiasSemana(fecha_inicio, fecha_fin);
                for (int i = 0; i < pedidosDiaSemana.Count() - 1; i += 2)
                {
                    pedidoTemporal = new ReporteDiaSemana();
                    pedidoTemporal.FechaInicio = fecha_inicio;
                    ViewBag.desde = "desde";
                    ViewBag.hasta = "hasta";
                    ViewBag.fechainicio = fecha_inicio.ToString("yyyy-MM-dd");
                    pedidoTemporal.FechaFin = fecha_fin;
                    ViewBag.fechafin = fecha_fin.ToString("yyyy-MM-dd");
                    pedidoTemporal.Dia = pedidosDiaSemana.ElementAt(i);
                    pedidoTemporal.Cantidad = pedidosDiaSemana.ElementAt(i + 1);
                    pedidos.Add(pedidoTemporal);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Seleccione las fechas para generar el reporte");
            }


            return View(pedidos);
        }


        /*
        *Propósito: Genera el reporte de ventas por dia de la semana
        *Entradas: DateTime fecha_inicio, DateTime fecha_fin
        *Salidas: ActionResult
        */
        //#Metodo: VentasPorDiaSemana  
         [Authorize(Roles = "Area de Ventas, Administrador")]
        public ActionResult ReporteDiaSemana(string id, DateTime fechai, DateTime fechaf)
        {
            LocalReport lr = new LocalReport();
           
            string path = Path.Combine(Server.MapPath("~/Reportes"), "VentasPorDiaSemana.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }


            List<string> pedidosDiaSemana = bl.consultarCantidadPedidosPorDiasSemana(fechai, fechaf);
            List<ReporteDiaSemana> pedidos = new List<ReporteDiaSemana>();
            ReporteDiaSemana pedidoTemporal;

            for (int i = 0; i < pedidosDiaSemana.Count() - 1; i += 2)
            {
                pedidoTemporal = new ReporteDiaSemana();
                pedidoTemporal.FechaInicio = fechai.Date;
                pedidoTemporal.FechaFin = fechaf.Date;
                pedidoTemporal.Dia = pedidosDiaSemana.ElementAt(i);
                pedidoTemporal.Cantidad = pedidosDiaSemana.ElementAt(i + 1);
                pedidos.Add(pedidoTemporal);
            }


            ReportDataSource rd = new ReportDataSource("VentasPorSemanaDataSet", pedidos);
            lr.DataSources.Add(rd);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;



            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>" + id + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);



            return File(renderedBytes, mimeType);
        }

    }




}