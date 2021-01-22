namespace Atencion_de_incidentes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Atencion_de_incidentes.Models;

    public partial class AspNetUsers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AspNetUsers()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaims>();
            AspNetUserLogins = new HashSet<AspNetUserLogins>();
            CustomPermission = new HashSet<CustomPermission>();
            TBMSMAETickets = new HashSet<TBMSMAETickets>();
            TBMSMAETickets1 = new HashSet<TBMSMAETickets>();
            AspNetRoles = new HashSet<AspNetRoles>();
            Mensajes = new HashSet<Mensajes>();
        }

        public string Id { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        [StringLength(256)]
        [Required]
        [EmailAddress]
        [Index(IsUnique = true)]
        [RegularExpression(@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@uned\.ac\.cr$", ErrorMessage = "El correo es invalido debe tener @uned.ac.cr")]
        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[\u0021-\u002b\u003c-\u0040\u005f\u002D\u002E\u002C])(?=.*[A-Z])(?=.*[a-z])\S{8,16}$", ErrorMessage = "La contraseña debe tener digitos,minusculas, mayusculas y simbolos")]
        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        [Required]
        [Phone]
        [RegularExpression(@"^\+*506\d{8}$", ErrorMessage ="El telefono debe tener +506 y de 8 digitos")]
        public string PhoneNumber { get; set; }

        public bool PhoneNumberConfirmed { get; set; }

        public bool TwoFactorEnabled { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        [Required]
        [StringLength(256)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Solo letras se admite")]
        public string NombreCompleto { get; set; }

        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Solo letras se admite")]
        public string Departamento { get; set; }

        public int? idEspecialidad { get; set; }
        [RegularExpression(@"(.*png$)|(.*jpg$)|(.*jpeg$)|(.*gif$)", ErrorMessage = "Solo imagenes jpg,jpeg,gif,png")]
        public byte[] Imagen { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUserClaims> AspNetUserClaims { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUserLogins> AspNetUserLogins { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomPermission> CustomPermission { get; set; }

        public virtual TBMSMAEEspecialidades TBMSMAEEspecialidades { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBMSMAETickets> TBMSMAETickets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBMSMAETickets> TBMSMAETickets1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetRoles> AspNetRoles { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mensajes> Mensajes { get; set; }
    }
}
