namespace Atencion_de_incidentes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class TBMSMAEPrioridades
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBMSMAEPrioridades()
        {
            TBMSMAETickets = new HashSet<TBMSMAETickets>();
        }

        [Key]
        public int idPrioridad { get; set; }

        public bool? estadoPrioridad { get; set; }

        [StringLength(20)]
        public string nombrePrioridad { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBMSMAETickets> TBMSMAETickets { get; set; }
    }
}
