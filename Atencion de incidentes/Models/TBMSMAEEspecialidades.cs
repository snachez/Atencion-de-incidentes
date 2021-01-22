namespace Atencion_de_incidentes.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class TBMSMAEEspecialidades
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBMSMAEEspecialidades()
        {
            AspNetUsers = new HashSet<AspNetUsers>();
            TBMSMAETickets = new HashSet<TBMSMAETickets>();
            TBMSESPECIALIDADXCATEGORIAs = new HashSet<TBMSESPECIALIDADXCATEGORIA>();
        }

        [Key]
        public int idEspecialidad { get; set; }
        [Required]
        public bool? estadoEspecialidad { get; set; }


        [Required(ErrorMessage = "Especialidad es requerido")]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Solo letras se admite")]
        public string tipoEspecialidad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AspNetUsers> AspNetUsers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBMSESPECIALIDADXCATEGORIA> TBMSESPECIALIDADXCATEGORIAs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBMSMAETickets> TBMSMAETickets { get; set; }
}
}

