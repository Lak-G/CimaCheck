using CimaCheck.Models;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace CimaCheck.Controllers
{
    internal class CimarronController
    {
        public static async void RegistrarCimarron(Supabase.Client supabase, Cimarron cimarron)
        {
            try
            {
                if (supabase == null)
                {
                    throw new InvalidOperationException("Supabase no inicializada");
                }

                var cima= new CimarronDb
                {
                    VisitanteId = 3,
                    Nombre = cimarron.Nombre,
                    IdFacultad = cimarron.FacultadId,
                    IdPrograma = cimarron.ProgramaId,
                    Genero = cimarron.Genero
                };

                await supabase.From<CimarronDb>().Insert(cima);

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar cimarron: {ex.Message}");
                return;
            }
        }
    }
}

#region Modelo Cima

[Table("cimarron")]
public class CimarronDb : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }

    [Column("visitante_id")]
    public int VisitanteId { get; set; }

    [Column("nombre")]
    public string Nombre { get; set; }

    [Column("id_facultad")]
    public int IdFacultad { get; set; }

    [Column("genero")]
    public string Genero { get; set; }

    [Column("id_programa")]
    public int IdPrograma { get; set; }

}

#endregion
