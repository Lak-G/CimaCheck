using System;
using System.Collections.Generic;
using System.Text;

namespace CimaCheck.Models
{
    public class Carrera
    {
        public int Id { get; set; }
        public int IdPrograma { get; set; }
        public int FacultadId { get; set; }
        public string Nombre { get; set; }
    }
}
