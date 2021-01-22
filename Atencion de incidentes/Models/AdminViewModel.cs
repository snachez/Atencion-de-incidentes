using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Atencion_de_incidentes.Models
{
    public class RoleViewModel
    {
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        [Display(Name = "RoleName")]
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class EditUserViewModel
    {
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        // Add the Address Info:
        public string Address { get; set; }
        public string City { get; set; }

        // Use a sensible display name for views:
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "Departamento")]
        public string Departamento { get; set; }

        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; set; }

        [Display(Name = "Imagen")]
        public byte[] Imagen { get; set; }

        [Display(Name = "id Especialidad")]
        public int idEspecialidad { get; set; }

        public IEnumerable<SelectListItem> RolesList { get; set; }
    }
}