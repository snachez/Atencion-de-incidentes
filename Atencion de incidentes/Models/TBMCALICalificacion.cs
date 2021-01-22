namespace Atencion_de_incidentes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TBMCALICalificacion")]
    public partial class TBMCALICalificacion
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBMCALICalificacion()
        {
            TBMSMAETickets = new HashSet<TBMSMAETickets>();
        }

        [Key]
        public int idCalificacion { get; set; }
        [Required]
        public int valoracion { get; set; }

        [Required]
        [StringLength(200)]
        public string comentario { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBMSMAETickets> TBMSMAETickets { get; set; }
    }
}
