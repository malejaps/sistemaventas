using SistemaVentasBL;
using SistemaVentasEntidades;
/*
* NombreClase: ProductoController.cs
* Autores: Roger Andres Fernandez Garcia - 1310229
* Fecha: 16/Junio/2015
* Descripcion: Asocia todos los controles que permiten realizar las acciones de las diferentes vistas relacionadas con el producto.
*/

/*
* Métodos de la clase:
 *Index()
 *Index(string nombreProducto, string categoriaProducto)
 *Vista_AreaDeVentas(int? mensaje)
 *CrearProducto()
 *CrearProducto([Bind(Include = "NombreProducto,Cantidad,Precio,DescripcionProducto,CodCatProducto")] ProductoEntidad producto)
 *CrearProductosArchivo()
 *CrearProductosArchivo(HttpPostedFileBase rutaArchivo)
 *EditarProducto(int codigoProducto, int codCatProducto)
 *EditarProducto([Bind(Include = "CodProducto,NombreProducto,Cantidad,Precio,DescripcionProducto,RutaFoto,CodUsuario,CodCatProducto")] ProductoEntidad producto,string categoriaProducto)
 *EditarPrecioProductoArchivo()
 *EditarPrecioProductoArchivo(HttpPostedFileBase rutaArchivo)
 *EditarPrecioProducto(int codigoProducto)
 *EditarPrecioProducto([Bind(Include = "CodProducto,NombreProducto,Cantidad,Precio,DescripcionProducto,RutaFoto,CodUsuario,CodCatProducto")] ProductoEntidad producto)
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SistemaVentasWeb.Controllers
{
    public class ProductoController : Controller
    {
        BL bl = new BL();
        /*
         *Propósito: Metodo que permite consultar el inventario y mostrarlo en la vista
         *Entradas: Ninguna 
         *Salidas: View
         */
        //#Metodo: Index 
        public ActionResult Index()
        {
            var inventarioProductos = bl.consultarInventario();
            List<SelectListItem> listaDeCategorias=new List<SelectListItem>();
            listaDeCategorias.Add(new SelectListItem() { Value = "categoria",Text="Todas", Selected=true}); 

            foreach(var categoria in bl.listarCategorias())
            {
                listaDeCategorias.Add(new SelectListItem() { Value = categoria.NombreCatProducto, Text = categoria.NombreCatProducto});
            
            }

            //new SelectList(bl.listarCategorias(), "NombreCatProducto", "NombreCatProducto");
            ViewData["categoriaProducto"] = listaDeCategorias;

            return View(inventarioProductos);
        }

        /*
         *Propósito: Metodo que permite aplicar las consultas dependiendo del filtro que se aplique
         *Entradas: string,string 
         *Salidas: View
         */
        //#Metodo: Index
        [HttpPost]
        public ActionResult Index(string nombreProducto, string categoriaProducto)
        {
            var inventarioProductos = bl.consultarInventario();

            if (!nombreProducto.Equals("") && !categoriaProducto.Equals("categoria"))
            {
                inventarioProductos = bl.consultarProductoPorNombreYCategoria(nombreProducto, categoriaProducto);
                List<SelectListItem> listaDeCategorias = new List<SelectListItem>();
                listaDeCategorias.Add(new SelectListItem() { Value = "categoria", Text = "Todas", Selected = true });

                foreach (var categoria in bl.listarCategorias())
                {
                    listaDeCategorias.Add(new SelectListItem() { Value = categoria.NombreCatProducto, Text = categoria.NombreCatProducto });
                }
                //new SelectList(bl.listarCategorias(), "NombreCatProducto", "NombreCatProducto");
                ViewData["categoriaProducto"] = listaDeCategorias;
            }
            else
                if (!nombreProducto.Equals("") && categoriaProducto.Equals("categoria"))
                {
                    inventarioProductos = bl.consultarProductoPorNombre(nombreProducto);
                    List<SelectListItem> listaDeCategorias = new List<SelectListItem>();
                    listaDeCategorias.Add(new SelectListItem() { Value = "categoria", Text = "Todas", Selected = true });

                    foreach (var categoria in bl.listarCategorias())
                    {
                        listaDeCategorias.Add(new SelectListItem() { Value = categoria.NombreCatProducto, Text = categoria.NombreCatProducto });

                    }

                    //new SelectList(bl.listarCategorias(), "NombreCatProducto", "NombreCatProducto");
                    ViewData["categoriaProducto"] = listaDeCategorias;
                     
                }
                else
                    if (nombreProducto.Equals("") && !categoriaProducto.Equals("categoria"))
                    {

                        inventarioProductos = bl.consultarProductoPorCategoria(categoriaProducto);
                        List<SelectListItem> listaDeCategorias = new List<SelectListItem>();
                        listaDeCategorias.Add(new SelectListItem() { Value = "categoria", Text = "Todas", Selected = true });

                        foreach (var categoria in bl.listarCategorias())
                        {
                            listaDeCategorias.Add(new SelectListItem() { Value = categoria.NombreCatProducto, Text = categoria.NombreCatProducto });

                        }

                        //new SelectList(bl.listarCategorias(), "NombreCatProducto", "NombreCatProducto");
                        ViewData["categoriaProducto"] = listaDeCategorias;
                    }


                        else
                    {
                        //new SelectList(bl.listarCategorias(), "NombreCatProducto", "NombreCatProducto");
                        List<SelectListItem> listaDeCategorias = new List<SelectListItem>();
                        listaDeCategorias.Add(new SelectListItem() { Value = "categoria", Text = "Todas", Selected = true });

                        foreach (var categoria in bl.listarCategorias())
                        {
                            listaDeCategorias.Add(new SelectListItem() { Value = categoria.NombreCatProducto, Text = categoria.NombreCatProducto });

                        }
                        
                            ViewData["categoriaProducto"] = listaDeCategorias;
                        }
                    

            return View(inventarioProductos);
        }

        /*
         *Propósito: Metodo que consulta el inventario de productos y carga la vista para area de ventas
         *Entradas: Ninguna 
         *Salidas: View
         */
        //#Metodo: Vista_AreaDeVentas
        public ActionResult Vista_AreaDeVentas()
        {
            var inventarioProductos = bl.consultarInventario();        
            return View(inventarioProductos);
        }

        /*
         *Propósito: Metodo que permite cargar la vista para crear un producto
         *Entradas: Ninguna 
         *Salidas: View
         */
        //#Metodo: CrearProducto
        public ActionResult CrearProducto()
        {

            var listaDeCategorias = new SelectList(bl.listarCategorias(), "CodCatProducto", "NombreCatProducto");
            //foreach (var categoria in bl.listarCategorias())
            //{
             //   listaDeCategorias.Add(new SelectListItem() { Value = "" + categoria.CodCatProducto, Text = categoria.NombreCatProducto});
           // }
            //new SelectList(bl.listarCategorias(), "NombreCatProducto", "NombreCatProducto");
            ViewData["categoriaProducto"] = listaDeCategorias;
              
            return View();
        }

        /*
         *Propósito: Metodo que permite crear un producto.
         *Entradas: ProductoEntidad 
         *Salidas: View
         */
        //#Metodo: CrearProducto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearProducto([Bind(Include = "NombreProducto,Cantidad,Precio,DescripcionProducto,CodCatProducto")] ProductoEntidad producto)
        {
            var listaDeCategorias = new SelectList(bl.listarCategorias(), "CodCatProducto", "NombreCatProducto");
           
            ViewData["categoriaProducto"] = listaDeCategorias;

            if (ModelState.IsValid)
            {
                producto.RutaFoto = "";
                producto.CodUsuario = 1;
                bl.crearProducto(producto);
                return RedirectToAction("Index");
            }

            return View(producto);
                
        }

        /*
         *Propósito: Metodo que permite cargar la vista para crear productos desde un archivo.
         *Entradas: Ninguna 
         *Salidas: View
         */
        //#Metodo: CrearProductosArchivo
        public ActionResult CrearProductosArchivo()
        {

            return View();
        }

        /*
         *Propósito: Metodo que realiza las acciones para crear los productos atravez del archivo
         *Entradas: Ninguna 
         *Salidas: View
         */
        //#Metodo: CrearProductosArchivo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearProductosArchivo(HttpPostedFileBase rutaArchivo)
        {
            

            if (rutaArchivo != null && rutaArchivo.ContentLength > 0)
            {
                var fileName = Path.GetFileName(rutaArchivo.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/ArchivosDeCreacionProductos"), fileName);
                rutaArchivo.SaveAs(path);
                bl.guardarNuevosProductosDesdeArchivo(path);
            }
            return RedirectToAction("Index");
        }

        /*
         *Propósito: Metodo que permite cargar la vista para editar un producto
         *Entradas: int,int  
         *Salidas: View
         */
        //#Metodo: EditarProducto
        public ActionResult EditarProducto(int codigoProducto, int codCatProducto)
        {
            

            var producto = bl.consultarProductoPorCodigo(codigoProducto);
            if (producto == null)
            {
                return HttpNotFound();
            }

            List<SelectListItem> listaDeCategorias = new List<SelectListItem>();

            foreach (var categoria in bl.listarCategorias())
            {
                listaDeCategorias.Add(new SelectListItem() { Value = "" + categoria.CodCatProducto, Text = categoria.NombreCatProducto, Selected =codCatProducto== categoria.CodCatProducto });
            }

            //new SelectList(bl.listarCategorias(), "NombreCatProducto", "NombreCatProducto");
            ViewData["categoriaProducto"] = listaDeCategorias;
            return View(producto);
        }
        /*
         *Propósito: Metodo que permite editar un producto
         *Entradas: ProductoEntidad,string  
         *Salidas: View
         */
        //#Metodo: EditarProducto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarProducto([Bind(Include = "CodProducto,NombreProducto,Cantidad,Precio,DescripcionProducto,RutaFoto,CodUsuario,CodCatProducto")] ProductoEntidad producto,string categoriaProducto)
        {

            if (ModelState.IsValid)
            {
                producto.CodCatProducto = Convert.ToInt32(categoriaProducto);
                bl.editarProducto(producto);
                return RedirectToAction("Index");
            }
            //else
            //if (rutaFoto != null && rutaFoto.ContentLength > 0 && categoriaProducto == null)
            //    {
            //        var fileName = Path.GetFileName(rutaFoto.FileName);
            //        var path = Path.Combine(Server.MapPath("~/App_Data/CargaDefotos"), fileName);
            //        rutaFoto.SaveAs(path);
            //        producto.RutaFoto = "App_Data/CargaDefotos/"+fileName;
            //        bl.editarProducto(producto);
                    
            //    }
            //else
            //    if (rutaFoto != null && rutaFoto.ContentLength > 0 && categoriaProducto != null)
            //    {
            //        var fileName = Path.GetFileName(rutaFoto.FileName);
            //        var path = Path.Combine(Server.MapPath("~/App_Data/CargaDefotos"), fileName);
            //        rutaFoto.SaveAs(path);
            //        producto.RutaFoto = path;
            //        producto.CodCatProducto = Convert.ToInt32(categoriaProducto);
            //        bl.editarProducto(producto);
                
            //    }

            return View(producto);
        }

        /*
         *Propósito: Metodo que permite mostrar la vista asociada al area de ventas para editar los precios de los productos desde un archivo
         *Entradas: Ninguna
         *Salidas: View
         */
        //#Metodo: EditarPrecioProductoArchivo
        public ActionResult EditarPrecioProductoArchivo()
        {
            return View();
        }

        /*
         *Propósito: Metodo que permite editar los precios de los productos desde un archivo para el area de ventas
         *Entradas: Ninguna
         *Salidas: View
         */
        //#Metodo: EditarPrecioProductoArchivo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPrecioProductoArchivo(HttpPostedFileBase rutaArchivo)
        {
            try { 
            if (rutaArchivo != null && rutaArchivo.ContentLength > 0)
            {
                var fileName = Path.GetFileName(rutaArchivo.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/EditarPrecioProductoArchivo"), fileName);
                rutaArchivo.SaveAs(path); 
                bl.editarPreciosProductosDesdeArchivo(path);

            }


            
                return RedirectToAction("Vista_AreaDeVentas",1);
                }
            catch(Exception e) 
            {
                ModelState.AddModelError("", "Formato de archivo invalido");
            return View();
            }
        }

        /*
         *Propósito: Metodo que permite cargar la vista para editar el precio de un producto
         *Entradas: int
         *Salidas: View
         */
        //#Metodo: EditarPrecioProducto
        public ActionResult EditarPrecioProducto(int codigoProducto)
        {
            if (codigoProducto == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var producto = bl.consultarProductoPorCodigo(codigoProducto);
            if (producto == null)
            {
                return HttpNotFound();
            }
            return View(producto);
        }
        /*
         *Propósito: Metodo que permite editar el precio de un producto
         *Entradas: ProductoEntidad
         *Salidas: View
         */
        //#Metodo: EditarPrecioProducto
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditarPrecioProducto([Bind(Include = "CodProducto,NombreProducto,Cantidad,Precio,DescripcionProducto,RutaFoto,CodUsuario,CodCatProducto")] ProductoEntidad producto)
        {

            if (ModelState.IsValid)
            {
                //producto.CodCatProducto = Convert.ToInt32(categoriaProducto);
                bl.editarProducto(producto);
                return RedirectToAction("Vista_AreaDeVentas");
            }

            return View(producto);
        }




    }
}
