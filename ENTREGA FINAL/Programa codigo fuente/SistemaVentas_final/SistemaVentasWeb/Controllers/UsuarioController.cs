/*
* NombreClase: PerfilController
* Autores: Edwin Gamboa -1310233 - Maria Alejandra Pabon 1310263
* Fecha: 15/Jun/2015
* Descripcion: Clase que permite controlar las vistas asociadas a la gestión de usuarios.
*/

/*
* ActionResult Index(string searchString)
* ActionResult VendedoresSinSincronizacion() 
* ActionResult Create()
* ActionResult Create(Bind(Include = "NombreUsuario,ContrasenaUsuario,Nombre,Correo,Telefono,RutaFoto,CodPerfil")] UsuarioEntidad usuario)
* ActionResult Edit(int id)
* ActionResult Edit([Bind(Include = "CodUsuario,NombreUsuario,ContrasenaUsuario,Nombre,Correo,Telefono,RutaFoto,CodPerfil"")] UsuarioEntidad usuario)
*/
using SistemaVentasBL;
using SistemaVentasEntidades;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace SistemaVentasWeb.Controllers
{
    public class UsuarioController : Controller
    {
        BL bl = new BL();

        /*
         *Propósito: Permite listar los usuarios
         *Entradas: string
         *Salidas: ActionResult
         */
        //#Metodo: Index
        // GET: Usuario 
         [Authorize(Roles = "Administrador")]
        public ActionResult Index(string searchString)
        {            
            ObservableCollection<UsuarioEntidad> usuarios;
            if (!String.IsNullOrEmpty(searchString))
            {
                usuarios = bl.consultarUsuarioPorNombre(searchString);
            }
            else
            {
                usuarios = bl.consultarTodosLosUsuarios();
            }
            return View(usuarios);
        }

        /*
         *Propósito: Permite listar los vendededores que no han sincronizado entre las 5pm y 5.30pm
         *Entradas: void
         *Salidas: ActionResult
         */
        //#Metodo: VendedoresSinSincronizacion
        // GET: Usuario       
        [Authorize(Roles = "Area de Ventas, Administrador")]
        public ActionResult VendedoresSinSincronizacion()
        {
            ObservableCollection<UsuarioEntidad> usuariosNoSincronizados;
            DateTime now = DateTime.Now;
            usuariosNoSincronizados = bl.consultarVendedoresNoSincronizados(now);
           
            //Enviar correo notificando que deben sincronizar
            for (int i = 0; i < usuariosNoSincronizados.Count(); i++) {
                bl.MandarMensajeCorreo(usuariosNoSincronizados.ElementAt(i).Correo);
            }
            ModelState.AddModelError("", "Se ha enviado un correo a los vendedores que no han sincronizado");
               
            return View(usuariosNoSincronizados);
        }

        /*
         *Propósito: Permite crear perfiles
         *Entradas: 
         *Salidas: ActionResult
         */
        //#Metodo: Create
        // GET: Usuario/Create
        [Authorize(Roles="Administrador")]
        public ActionResult Create()
        {
            var perfilesDropList = new SelectList(bl.consultarTodosLosPerfiles(), "CodPerfil", "Nombre");
            
            ViewBag.perfilUsuario = perfilesDropList;
            return View();
        }

        /*
         *Propósito: Permite crear perfiles
         *Entradas: UsuarioEntidad
         *Salidas: ActionResult
         */
        //#Metodo: Create
        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Create([Bind(Include = "NombreUsuario,ContrasenaUsuario,Nombre,Correo,Telefono,RutaFoto,CodPerfil")]
            UsuarioEntidad usuario)
        {
            var perfilesDropList = new SelectList(bl.consultarTodosLosPerfiles(), "CodPerfil", "Nombre");

            ViewBag.perfilUsuario = perfilesDropList;
            if (ModelState.IsValid)
            {
                bl.crearUsuario(usuario);                
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        /*
         *Propósito: Permite editar usuarios
         *Entradas:  int
         *Salidas: ActionResult
         */
        //#Metodo: Edit
        // GET: Vuelo/Edit/5
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit(int id)
        {
            UsuarioEntidad usuario = bl.consultarUsuarioPorCodigo(id);
            var Perfiles = bl.consultarTodosLosPerfiles();

            var perfilesDropList = new List<SelectListItem>();

            foreach (var perfil in Perfiles)
            {
                if(usuario.CodPerfil == perfil.CodPerfil){
                    perfilesDropList.Add(new SelectListItem
                    {
                        Text = perfil.Nombre,
                        Value = perfil.CodPerfil.ToString(),
                        Selected = true
                    });
                }else{
                    perfilesDropList.Add(new SelectListItem
                    {
                        Text = perfil.Nombre,
                        Value = perfil.CodPerfil.ToString()
                    });
                }
            }

            ViewBag.perfilUsuario = perfilesDropList;
            return View(usuario);
        }

        /*
         *Propósito: Permite editar usuarios
         *Entradas:  UsuarioEntidad
         *Salidas: ActionResult
         */
        //#Metodo: Edit
        // POST: Vuelo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
        public ActionResult Edit([Bind(Include = "CodUsuario,NombreUsuario,ContrasenaUsuario,Nombre,Correo,Telefono,RutaFoto,CodPerfil")]
            UsuarioEntidad usuario)
        {            
            if (ModelState.IsValid)
            {
                bl.editarUsuario(usuario);
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

    }
}