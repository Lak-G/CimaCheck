using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using CimaCheck.Models;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace CimaCheck.Controllers
{
    internal class UniversitiesController
    {
        public static async  Task<List<Carrera>> GetCareer(Supabase.Client supabase)
        {
            try
            {
                if (supabase == null)
                    throw new ArgumentNullException("supabase no inicializado");

                var response = await supabase
                    .From<ProgramaDB>()
                    .Get();

                return response.Models.Select(c => new Carrera
                {
                    Id = c.Id,
                    IdPrograma = c.IdPrograma,
                    FacultadId = c.IdFacultad,
                    Nombre = c.Nombre
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al intentar Conseguir\nLa Lista de Carreras");
                return new List<Carrera>();
            }
        }

        public static async Task<List<Facultad>> GetFacultades(Supabase.Client supabase)
        {
            try
            {
                if (supabase == null)
                    throw new ArgumentNullException("supabase no inicializado");

                var response = await supabase
                    .From<FacultadDB>()
                    .Get();

                return response.Models.Select(c => new Facultad
                {
                    Id = c.Id,
                    Id_facultad = c.IdFacultad,
                    Nombre = c.Nombre
                }).ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al intentar Conseguir\nLa Lista de Facultades {ex}");
                return new List<Facultad>();
            }
        }
    }
}

#region Modelos

[Table("programas")]
public class ProgramaDB: BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }

    [Column("id_programa")]
    public int IdPrograma { get; set; }

    [Column("id_facultad")]
    public int IdFacultad { get; set; }

    [Column("nombre")]
    public string Nombre { get; set; }
}

[Table("facultad")]
public class FacultadDB : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }

    [Column("id_facultad")]
    public int IdFacultad { get; set; }

    [Column("nombre")]
    public string Nombre { get; set; }
}
#endregion
