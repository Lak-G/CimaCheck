using System;
using System.Collections.Generic;
using System.Text;

namespace CimaCheck.Models
{
    public class Escuela
    {

        public Escuela()
        {
        }
        public int Id { get; set; }
        public int IdEscuela { get; set; }
        public string NombreEscuela { get; set; }
        public string NivelEducativo { get; set; }
    }
}
