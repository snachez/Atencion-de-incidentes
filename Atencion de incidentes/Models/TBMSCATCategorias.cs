namespace Atencion_de_incidentes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TBMSCATCategorias
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBMSCATCategorias()
        {
            TBMSMAEBaseConoc = new HashSet<TBMSMAEBaseConoc>();
            TBMSESPECIALIDADXCATEGORIAs = new HashSet<TBMSESPECIALIDADXCATEGORIA>();
            TBMSMAETickets = new HashSet<TBMSMAETickets>();
        }

        [Key]
        public int idCategoria { get; set; }

        [Required(ErrorMessage ="Categoria es requerido")]
        [StringLength(20)]
        [Index(IsUnique = true)]
        [RegularExpression(@"^[a-zA-Z_ ]*$", ErrorMessage = "Solo letras se admite")]
        public string tipoCategoria { get; set; }

        public bool? estadoCategoria { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBMSMAEBaseConoc> TBMSMAEBaseConoc { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBMSESPECIALIDADXCATEGORIA> TBMSESPECIALIDADXCATEGORIAs { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBMSMAETickets> TBMSMAETickets { get; set; }
    }
}
