namespace Atencion_de_incidentes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;


    public partial class Mensajes
    {
       [Key]
        public int idMensaje { get; set; }

        public int idTicket { get; set; }

        [Required]
        [StringLength(128)]
        public string idUsuario { get; set; }

        [Required]
        [StringLength(200)]
        public string texto { get; set; }

        public DateTime fechaCreacion { get; set; }

        public bool Estado { get; set; }

        public virtual AspNetUsers AspNetUsers { get; set; }

        public virtual TBMSMAETickets TBMSMAETickets { get; set; }
    }
}