using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using CimaCheck.Models;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace CimaCheck.Controllers
{

    //Metodo a profundizar
    public static class AlumnosController
    {
        public static async Task<bool> ActualizarAsistencia(Supabase.Client supabase, int alumnoId, bool asistencia)
        {
            try
            {
                if (supabase == null)
                {
                    MessageBox.Show("Supabase no está inicializado");
                    return false;
                }

                System.Diagnostics.Debug.WriteLine($"DEBUG: Actualizando ID={alumnoId}, Asistencia={asistencia}");

                var updateData = new AlumnoEscuelaDb 
                { 
                    asistencia = asistencia,
                    Id = alumnoId
                };
                
                var response = await supabase
                    .From<AlumnoEscuelaDb>()
                    .Where(c => c.Id == alumnoId)
                    .Update(updateData);

                System.Diagnostics.Debug.WriteLine($"DEBUG: Response={response.ResponseMessage.StatusCode}");

                if (!response.ResponseMessage.IsSuccessStatusCode)
                {
                    var errorContent = await response.ResponseMessage.Content.ReadAsStringAsync();
                    MessageBox.Show($"Error de Supabase: {response.ResponseMessage.StatusCode} - {errorContent}");
                    return false;
                }

                MessageBox.Show($"Actualizado correctamente. ID: {alumnoId}, Valor: {asistencia}");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar asistencia: {ex.Message}");
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
