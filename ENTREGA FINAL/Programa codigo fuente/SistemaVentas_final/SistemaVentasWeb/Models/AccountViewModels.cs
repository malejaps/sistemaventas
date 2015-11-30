/*
* NombreClase: AccountViewModel.cs
* Autores: Maria Alejandra Pabon - 1310263
* Fecha: 12/Junio/2015
* Descripcion: clase encargada de crear los modelos necesarios para las cuentas de usuaario y tambien
 * se encarga de crear los objetos necesarios para los reportes.
*/


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaVentasWeb.Models
{
    public class ManageUserViewModel
    {        
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña actual")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "La {0} debe tener al menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nueva contraseña")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar nueva contraseña")]
        [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Nombre de usuario")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Display(Name = "Recordarme")]
        public bool RememberMe { get; set; }
    } 

public class ReporteCiudad {

        [Display(Name = "Desde")]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Hasta")]
        public DateTime FechaFin { get; set; }

        [Display(Name = "Ciudad")]
        public string Ciudad { get; set; }

        [Display(Name = "Cantidad")]
        public string Cantidad { get; set; }    
    
    }

    public class ListaReporteCiudad
    {

        public List<ReporteCiudad> pedidos;

        public ListaReporteCiudad()
        {
            pedidos = new List<ReporteCiudad>();

        }

        public List<ReporteCiudad> getVentasCiudad()
        {

            return pedidos;
        }


    }

    public class ReporteCategoria
    {

        [Display(Name = "Desde")]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Hasta")]
        public DateTime FechaFin { get; set; }

        [Display(Name = "Codigo")]
        public string Codigo { get; set; }

        [Display(Name = "Categoria")]
        public string Categoria { get; set; }

        [Display(Name = "Cantidad")]
        public string Cantidad { get; set; }


    }

    public class ListaReporteCategoria
    {

        public List<ReporteCategoria> pedidos;

        public ListaReporteCategoria()
        {
            pedidos = new List<ReporteCategoria>();

        }

        public List<ReporteCategoria> getVentasCategoria()
        {

            return pedidos;
        }


    }

    public class ReporteProducto
    {

        [Display(Name = "Desde")]
        public DateTime FechaInicio { get; set; }

        [Display(Name = "Hasta")]
        public DateTime FechaFin { get; set; }

        [Display(Name = "Codigo")]
        public string Codigo { get; set; }

        [Display(Name = "Producto")]
        public string Producto { get; set; }

        [Display(Name = "Cantidad")]
        public string Cantidad { get; set; }


    }

    public class ListaReporteProducto
    {

        public List<ReporteProducto> pedidos;

        public ListaReporteProducto()
        {
            pedidos = new List<ReporteProducto>();

        }

        public List<ReporteProducto> getVentasProducto()
        {

            return pedidos;
        }

    }

    public class ReporteDiaSemana
        {

            [Display(Name = "Desde")]
            public DateTime FechaInicio { get; set; }

            [Display(Name = "Hasta")]
            public DateTime FechaFin { get; set; }

            [Display(Name = "Dia")]
            public string Dia { get; set; }

            [Display(Name = "Cantidad")]
            public string Cantidad { get; set; }


        }

        public class ListaReporteDiaSemana
        {

            public List<ReporteDiaSemana> pedidos;

            public ListaReporteDiaSemana()
            {
                pedidos = new List<ReporteDiaSemana>();

            }

             public List<ReporteDiaSemana> getVentasDiaSemana()
            {

                return pedidos;
            }


        }


    }		

