using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using CimaCheck.Models;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace CimaCheck.Controllers
{
    public static class AlumnosController
    {
        public static async Task<List<Alumno>> 
            ConseguirListasAlumnos(Supabase.Client supabase)
        {
            try
            {
                   if (supabase == null)
                    throw new ArgumentNullException("Supabase no inicializado");

                   var response = await supabase
                    .From<AlumnoEscuelaDb>()
                    //.Where(c => c.IdEscuela == idEscuela)
                    .Get();

                return response.Models.Select(c => new Alumno
                {
                    Id = c.Id,
                    IdEscuela = c.IdEscuela,
                    NombreAlumno = c.NombreCompleto,
                    NivelEcucativo = c.NivelEducativo,
                    Asistencia = c.asistencia,
                    VisistanteId = c.VisitanteId
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al intentar conseguir \n la lista de escuelas \n {ex}");
                return new List<Alumno>();
            }
        }
    }
}

#region Model_Alumno_Escuela
[Table("alumnos")]
public class AlumnoEscuelaDb : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }

    [Column("id_escuela")]
    public int IdEscuela { get; set; }

    [Column("nombre_Alumno")]
    public string NombreCompleto { get; set; }

    [Column ("asistencia")]
    public bool asistencia { get; set; }

    [Column("nivel_educativo")]
    public string NivelEducativo { get; set; }

    [Column ("visitante_id")]
    public int VisitanteId { get; set; }
}
#endregion
