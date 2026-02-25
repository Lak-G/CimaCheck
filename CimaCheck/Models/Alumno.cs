using System;
using System.Collections.Generic;
using System.Text;

namespace CimaCheck.Models
{
    public class Alumno
    {
        public int Id { get; set; }
        public int IdEscuela { get; set; }
        public int VisistanteId { get; set; }
        public bool Asistencia { get; set; }
        public string NombreAlumno { get; set; }

    }
}
