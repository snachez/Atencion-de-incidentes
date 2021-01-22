namespace Atencion_de_incidentes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TBMSMAEBaseConoc")]
    public partial class TBMSMAEBaseConoc
    {
        [Key]
        public int idBase { get; set; }

        public int? idTicket { get; set; }

        public int? idCategoria { get; set; }

        [StringLength(150)]
        public string BaseDescripcion { get; set; }

        public virtual TBMSCATCategorias TBMSCATCategorias { get; set; }

        public virtual TBMSMAETickets TBMSMAETickets { get; set; }
    }
}
