namespace Atencion_de_incidentes.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Web;

    [Table("TBMSESPECIALIDADXCATEGORIA")]
    public partial class TBMSESPECIALIDADXCATEGORIA
    {
        [Key]
        public int Id { get; set; }
        public int IdEspecialidad { get; set; }
        public int IdCategoria { get; set; }

        public bool Estado { get; set; }

        public virtual TBMSMAEEspecialidades TBMSMAEEspecialidades { get; set; }
        public virtual TBMSCATCategorias TBMSCATCategorias { get; set; }
    }
}