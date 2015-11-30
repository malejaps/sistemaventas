/*
* NombreClase: UsuarioEntidad
* Autores: Edwin Gamboa - 1310233
* Fecha: 08/May/2015
* Descripcion: Clase que representa un Usuario.
*/

/*
* Métodos de la clase:
* UsuarioEntidad generarUsuario(int cod_usuario, string nombre_usuario, 
			string contrasena_usuario, string nombre, string correo, 
			string telefono, string ruta_foto, int cod_perfil)
*/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SistemaVentasEntidades
{
    public class UsuarioEntidad
    {

		/*
         *Propósito: Método que permite crear un objeto de tipo UsuarioEntidad
         *Entradas: cod_usuario, nombre_usuario, contrasena_usuario, nombre,
            correo, telefono, ruta_foto, cod_perfil
         *Salidas: UsuarioEntidad
         */
        //#Metodo: generarUsuario 
        public static UsuarioEntidad generarUsuario(int cod_usuario, string nombre_usuario, string contrasena_usuario, string nombre,
            string correo, string telefono, string ruta_foto, int cod_perfil)
        { 
        return new UsuarioEntidad{ CodUsuario=cod_usuario, NombreUsuario=nombre_usuario, ContrasenaUsuario=contrasena_usuario, 
            Nombre=nombre, Correo=correo, Telefono=telefono, RutaFoto= ruta_foto, CodPerfil=cod_perfil };
        }

        [Display(Name = "Código de Usuario")]
        public int CodUsuario { get; set; }

        [Display(Name = "Nombre de Usuario")]
        [StringLength(100)]
        [Required]
        public string NombreUsuario { get; set; }

        [Display(Name = "Contraseña")]
        public string ContrasenaUsuario { get; set; }

        [Display(Name = "Nombre Completo")]
        [Required]
        [StringLength(50)]
        public string Nombre { get; set; }

        [Display(Name = "E-Mail")]
        [Required]
        [RegularExpression(@"^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$")]
        [StringLength(50)]
        public string Correo { get; set; }

        [Display(Name = "Telefono")]
        [Required]
        [StringLength(50)]
        public string Telefono { get; set; }

        [Display(Name = "Foto")]
        [Required]
        public string RutaFoto { get; set; }

        [Display(Name = "Perfil")]
        [Range(0, Int32.MaxValue)]
        [Required]
        public int CodPerfil { get; set; }



        public ObservableCollection<AuditoriaEntidad> Auditoria { get; set; }
        public ObservableCollection<PedidoEntidad> Pedido { get; set; }
        public PerfilEntidad Perfil { get; set; }
        public ObservableCollection<ProductoEntidad> Producto { get; set; }

        }

    }

