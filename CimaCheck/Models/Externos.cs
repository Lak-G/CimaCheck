using System;
using System.Collections.Generic;
using System.Text;

namespace CimaCheck.Models
{
    public class Externos
    {
        public int Id { get; set; }

        public int VisitanteId { get; set; }
        public string Nombre { get; set; }
        public string Genero { get; set; }
        public int IdProcedencia { get; set; }
        public string Edad { get; set; }
    }
}
