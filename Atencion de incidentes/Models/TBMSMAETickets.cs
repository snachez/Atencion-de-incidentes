namespace Atencion_de_incidentes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    public partial class TBMSMAETickets
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBMSMAETickets()
        {
            TBMSMAEBaseConoc = new HashSet<TBMSMAEBaseConoc>();
            Mensajes = new HashSet<Mensajes>();
        }

        [Key]
        public int idTicket { get; set; }

        [StringLength(128)]
        public string idUsuario { get; set; }

        [StringLength(128)]
        public string IdUsuarioTec { get; set; }

        public int idPrioridad { get; set; }

        public int? IdEstado { get; set; }
        [Required(ErrorMessage ="Elija una categoria")]
        public int? idCategoria { get; set; }
        [Required(ErrorMessage ="Elija una especialidad")]
        public int? idEspecialidad { get; set; }

        [Required]
        [StringLength(100)]
        public string titulo { get; set; }

        [Required]
        [StringLength(500)]
        public string descripcion { get; set; }

        [Column(TypeName = "date")]
        public DateTime fechaTicket { get; set; }

        public bool estadoTicket { get; set; }

        [Column(TypeName = "date")]
        public DateTime? fechaAtendido { get; set; }

        [StringLength(200)]
        [RegularExpression(@"(.*png$)|(.*jpg$)|(.*jpeg$)|(.*gif$)", ErrorMessage = "Solo imagenes jpg,jpeg,gif,png")]
        public string adjuntoTicket { get; set; }

        public int? idCalificacion { get; set; }

        [StringLength(100)]
        public string Solucion { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual AspNetUsers AspNetUsers1 { get; set; }

        public virtual TBMCALICalificacion TBMCALICalificacion { get; set; }

        public virtual TBMSCATCategorias TBMSCATCategorias { get; set; }

        public virtual TBMSCATEstadosTickets TBMSCATEstadosTickets { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TBMSMAEBaseConoc> TBMSMAEBaseConoc { get; set; }

        public virtual TBMSMAEEspecialidades TBMSMAEEspecialidades { get; set; }

        public virtual TBMSMAEPrioridades TBMSMAEPrioridades { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Mensajes> Mensajes { get; set; }
    }
}
