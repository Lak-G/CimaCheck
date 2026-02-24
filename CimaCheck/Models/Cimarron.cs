using System;
using System.Collections.Generic;
using System.Text;

namespace CimaCheck.Models
{
    class Cimarron
    {
        public int Id { get; set; }
        public int VisitanteId { get; set; }
        public string Nombre { get; set; }
        public int FacultadId { get; set; }
        public string Genero { get; set; }
        public int ProgramaId { get; set; }
    }
}
