/*
* NombreClase: AccountController.cs
* Autores: Edwin Gamboa - 1310233
* Fecha: 13/Junio/2015
* Descripcion: clae que e encarga de gestionar todo lo relacionado con el inicio de sesion
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using SistemaVentasWeb.Models;
using SistemaVentasBL;
using System.Web.Security;
using SistemaVentasEntidades;

namespace MvcMovie.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        public AccountController()
            : this(new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext())))
        {
        }

        public AccountController(UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;
            
            foreach (var perfil in bl.consultarTodosLosPerfiles())
            {
                var roles = Roles.GetAllRoles();
                if(!roles.Contains(perfil.Nombre)){
                    Roles.CreateRole(perfil.Nombre);
                }
            }
        }

        public UserManager<ApplicationUser> UserManager { get; private set; }
        public BL bl = new BL();

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var autenticar = Task.Factory.StartNew(() => bl.autenticarUsuario(model.UserName, model.Password));
                    var usuario = await autenticar;

                    FormsAuthentication.SetAuthCookie(usuario.NombreUsuario, model.RememberMe);
                    if (!Roles.IsUserInRole(usuario.NombreUsuario, usuario.Perfil.Nombre))
                    {
                        Roles.AddUserToRole(usuario.NombreUsuario, usuario.Perfil.Nombre);
                    }
                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", "Nombre de usuario o contraseña inválidos.");
            }
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        

        //
        // GET: /Account/Manage
        [Authorize(Roles = "Area de Ventas, Jefe de Produccion, Administrador")]
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Su contraseña ha sdo cambiada."
                : message == ManageMessageId.SetPasswordSuccess ? "Su contraseña fue asignada."
                : message == ManageMessageId.Error ? "Ocurrió un error inesperado."
                : "";
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Area de Ventas, Jefe de Produccion, Administrador")]
        public async Task<ActionResult> Manage(ManageUserViewModel model, string userName)
        {
            ViewBag.ReturnUrl = Url.Action("Manage");           
            if (ModelState.IsValid)
            {
                var cambiarContrasena = Task.Factory.StartNew(() => bl.cambiarContraseniaUsuario(userName, model.OldPassword, model.NewPassword));
                var exito = await cambiarContrasena;
                if (exito)
                {
                    return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                }
                else
                {
                   ModelState.AddModelError("", "La contrseña actual es incorrecta, por favor verifique");
                }
            }            
            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }        

        
      

        #region Helpers        
        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }
        #endregion
    }
}