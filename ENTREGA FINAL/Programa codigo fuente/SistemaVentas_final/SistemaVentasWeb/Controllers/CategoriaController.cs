/*
* NombreClase: CategoriaController.cs
* Autores: Roger Andres Fernandez Garcia - 1310229
* Fecha: 16/Junio/2015
* Descripcion: Asocia todos los controles que permiten realizar las acciones de las diferentes vistas relacionadas con las categorias.
*/

/*
* Métodos de la clase:
 *Index()
 *CrearCategoria()
 *CrearCategoria(Categoria_ProductoEntidad categoria)
 *EditarCategoria(int codCatProducto)
 *EditarCategoria(Categoria_ProductoEntidad categoria)
*/

using SistemaVentasBL;
using SistemaVentasEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaVentasWeb.Controllers
{
    public class CategoriaController : Controller
    {
        BL bl = new BL();

        /*
         *Propósito: Metodo que permite consultar todas las categorias y mostrarlas en la vista 
         *Entradas: Ninguna 
         *Salidas: View
         */
        //#Metodo: Index
        public ActionResult Index()
        {
            var categoria = bl.listarCategorias();
            return View(categoria);
        }

        /*
         *Propósito: Metodo que permite mostrar la vista para crear una categoria
         *Entradas: Ninguna 
         *Salidas: View
         */
        //#Metodo: CrearCategoria
        public ActionResult CrearCategoria()
        {
            return View();
        }
        /*
         *Propósito: Metodo que permite crear una categoria
         *Entradas: Ninguna 
         *Salidas: View
         */
        //#Metodo: CrearCategoria
        [HttpPost]
        public ActionResult CrearCategoria(Categoria_ProductoEntidad categoria)
        {
            bl.crearCategoriaProducto(categoria);
            return RedirectToAction("Index");
        }
        /*
         *Propósito: Metodo que permite mostrar la vista para editar una categoria
         *Entradas: int 
         *Salidas: View
         */
        //#Metodo: EditarCategoria
        public ActionResult EditarCategoria(int codCatProducto)
        {
            var categoria = bl.consultarCategoriaPorCodigo(codCatProducto);
            return View(categoria);
        }
        /*
         *Propósito: Metodo que permite editar la categoria 
         *Entradas: Categoria_ProductoEntidad 
         *Salidas: View
         */
        //#Metodo: EditarCategoria
        [HttpPost]
        public ActionResult EditarCategoria(Categoria_ProductoEntidad categoria)
        {
            bl.editarCategoria_ProductoEntidad(categoria);
            return RedirectToAction("Index");
        }
    }
}