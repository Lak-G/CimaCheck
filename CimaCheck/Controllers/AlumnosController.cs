using System;
using System.Collections.Generic;
using System.Security.RightsManagement;
using System.Text;
using System.Windows;
using CimaCheck.Models;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace CimaCheck.Controllers
{
    public static class AlumnosController
    {
        public static async Task<bool> ActualizarAsistencia(Supabase.Client supabase, int Id, bool Asistencia)
        {
            try
            {
                if (supabase == null)
                {
                    MessageBox.Show("Supabase no inicializada");
                    return false;
                }

                // 1. Verifica que el registro existe ANTES de actualizar
                var existe = await supabase
                    .From<AlumnoEscuelaDb>()
                    .Where(c => c.Id == Id)
                    .Get();


                if (existe.Models.Count == 0)
                {
                    MessageBox.Show($"No existe alumno con Id: {Id}");
                    return false;
                }

                // 2. Intenta el update
                var update = await supabase
                    .From<AlumnoEscuelaDb>()
                    .Where(c => c.Id == Id)
                    .Set(c => c.asistencia, Asistencia)
                    .Update();

                return update.Models.Count > 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}\n\nStackTrace: {ex.StackTrace}");
                return false;
            }
        }


        public static async Task<List<Alumno>> ConseguirListasAlumnos(Supabase.Client supabase)
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

    [Column("nombre_alumno")]
    public string NombreCompleto { get; set; }

    [Column ("asistencia")]
    public bool asistencia { get; set; }

    //[Column("nivel_educativo")]
    //public string NivelEducativo { get; set; }

    [Column ("visitante_id")]
    public int VisitanteId { get; set; }
}
#endregion
