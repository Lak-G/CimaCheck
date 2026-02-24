using System;
using System.Collections.Generic;
using System.Text;
using CimaCheck.Models;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace CimaCheck.Controllers
{
    internal class SchoolsController
    {
        public static async Task<List<Escuela>> GetSchools(Supabase.Client? supabase)
        {
            try
            {
                if(supabase == null)
                    throw new ArgumentNullException(nameof(supabase));

                var response = await supabase
                    .From<EscuelaDb>()
                    .Get();

                return response.Models.Select(e => new Escuela
                {
                    Id = e.Id,
                    IdEscuela = e.IdEscuela,
                    NombreEscuela = e.NombreEscuela,
                    NivelEducativo = e.NivelEducativo
                }).ToList();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error al obtener las escuelas: {ex.Message}");
                return new List<Escuela>();
            }
        }
    }
}


[Table("escuela")]
public class EscuelaDb : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    [Column("id_escuela")]
    public int IdEscuela{ get; set; }
    [Column("nombre_escuela")]
    public string NombreEscuela { get; set; }
    [Column("nivel_educativo")]
    public string NivelEducativo { get; set; }
}