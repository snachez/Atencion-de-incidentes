using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atencion_de_incidentes.Models
{
    public class ResponseModel
    {
        public bool respuesta { get; set; }
        public string redirect { get; set; }
        public string error { get; set; }
        public string mensaje { get; set; }
    }
}