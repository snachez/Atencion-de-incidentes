using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Atencion_de_incidentes.Models
{
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity>
            GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager
                .CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }

        public string Address { get; set; }
        public string City { get; set; }

        [Display(Name = "Departamento")]
        public string Departamento { get; set; }

        [Display(Name = "Nombre Completo")]
        public string NombreCompleto { get; set; }

        [Display(Name = "Imagen")]
        public byte[] Imagen { get; set; }

        // Use a sensible display name for views:
        [Display(Name = "Postal Code")]
        public string PostalCode { get; set; }

        [Display(Name = "idEspecialidad")]
        public int? idEspecialidad { get; set; }

        // Concatenate the address info for display in tables and such:
        public string DisplayAddress
        {
            get
            {
                string dspAddress = string.IsNullOrWhiteSpace(this.Address) ? "" : this.Address;
                string dspCity = string.IsNullOrWhiteSpace(this.City) ? "" : this.City;
                string dspPostalCode = string.IsNullOrWhiteSpace(this.PostalCode) ? "" : this.PostalCode;
                return string.Format("{0} {1} {2}", dspAddress, dspCity, dspPostalCode);
            }
        }
    }


    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string name) : base(name) { }
        public string Description { get; set; }

    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ModeloUnedMS", throwIfV1Schema: false)
        {
        }

        static ApplicationDbContext()
        {
            Database.SetInitializer<ApplicationDbContext>(new ApplicationDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}