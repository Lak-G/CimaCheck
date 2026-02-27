using CimaCheck.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace CimaCheck.Controllers
{
    internal class OrganizacionesController
    {
        public static async Task<List<Company>> GetOrg(Supabase.Client? supabase)
        {
            try
            {
                if (supabase == null)
                    throw new ArgumentNullException(nameof(supabase));

                var response = await supabase
                    .From<OrganizacionDb>()
                    .Get();

                return response.Models.Select(o => new Company
                {
                    Id = o.Id,
                    IdProcedencia = o.IdProcedencia,
                    NombreProc = o.NombreProcedencia
                }).ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener las organizaciones: {ex.Message}");
                return new List<Company>();

            }
        }
    }
}


    [Table("procedencia")]
    public class OrganizacionDb : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("id_procedencia")]
        public int IdProcedencia { get; set; }

        [Column("nombre_proc")]
        public string NombreProcedencia { get; set; }
    }
