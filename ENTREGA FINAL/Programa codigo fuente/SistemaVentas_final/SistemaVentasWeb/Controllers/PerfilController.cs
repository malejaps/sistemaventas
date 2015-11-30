/*
* NombreClase: PerfilController
* Autores: Edwin Gamboa -1310233
* Fecha: 15/Jun/2015
* Descripcion: Clase que permite controlar las vistas asociadas a la gestión de perfiles.
*/

/*
* ActionResult Index(string searchString)
* ActionResult Create()
* ActionResult Create([Bind(Include = "CodPerfil,Nombre")] PerfilEntidad perfil)
* ActionResult Edit(int id)
* ActionResult Edit([Bind(Include = "CodPerfil,Nombre")] PerfilEntidad perfil)
*/
using SistemaVentasBL;
using SistemaVentasEntidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaVentasWeb.Controllers
{
    public class PerfilController : Controller
    {
        BL bl = new BL();

        /*
         *Propósito: Permite listar los perfiles
         *Entradas: string
         *Salidas: ActionResult
         */
        //#Metodo: Index
        // GET: Perfil
        [Authorize(Roles = "Administrador")]
        public ActionResult Index(string searchString)
        {
            ObservableCollection<PerfilEntidad> perfiles;
            if (!String.IsNullOrEmpty(searchString))
            {
                perfiles = bl.consultarPerfilPorNombre(searchString);
            }
            else
            {
                perfiles = bl.consultarTodosLosPerfiles();
            }
            return View(perfiles);
        }

        /*
         *Propósito: Permite crear perfiles
         *Entradas:  
         *Salidas: ActionResult
         */
        //#Metodo: Create
        // GET: Perfil/Create
         [Authorize(Roles = "Administrador")]
        public ActionResult Create()
        {
            return View();
        }

        /*
         *Propósito: Permite crear perfiles
         *Entradas: PerfilEntidad
         *Salidas: ActionResult
         */
        //#Metodo: Create
        // POST: Perfil/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Create([Bind(Include = "CodPerfil,Nombre")]
            PerfilEntidad perfil)
        {
            if (ModelState.IsValid)
            {
                bl.crearPerfil(perfil);                
                return RedirectToAction("Index");
            }

            return View(perfil);
        }


        /*
         *Propósito: Permite editar perfiles
         *Entradas:  int
         *Salidas: ActionResult
         */
        //#Metodo: Edit
        // GET: Perfil/Edit/5
         [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int id)
        {
            PerfilEntidad perfil = bl.consultarPerfilPorCodigo(id);
            return View(perfil);
        }

        /*
         *Propósito: Permite editar perfiles
         *Entradas:  PerfilEntidad
         *Salidas: ActionResult
         */
        //#Metodo: Edit
        // POST: Perfil/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit([Bind(Include = "CodPerfil,Nombre")]
            PerfilEntidad perfil)
        {
            if (ModelState.IsValid)
            {
                bl.editarPerfil(perfil);
                return RedirectToAction("Index");
            }
            return View(perfil);
        }
        
    }
}
