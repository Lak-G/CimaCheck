using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using CimaCheck.Models;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace CimaCheck.Controllers
{
    internal class RegistroIndividualController
    {

        public async static Task<bool> RegistroIndividual(Supabase.Client supabase, Externos registro)
        {
            try
            {
                if (supabase == null)
                {
                    MessageBox.Show("Supabase no iniciada");
                }

                var regTemp = new ExternosDb()
                {
                    VisitanteId = 2,
                    Nombre = registro.Nombre,
                    Genero = registro.Genero,
                    IdProcedencia = registro.IdProcedencia,
                    Edad = registro.Edad
                };

                supabase.From<ExternosDb>().Insert(regTemp);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en el registro del usuario{ex}");
                return false;
            }
        }
    }

    [Table("externos")]
    public class ExternosDb : BaseModel
    {
        [PrimaryKey("id")]
        public int Id { get; set; }

        [Column("visitante_id")]
        public int VisitanteId { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("genero")]
        public string Genero { get; set; }

        [Column("id_procedencia")]
        public int IdProcedencia { get; set; }

        [Column("edad")]
        public string Edad { get; set; }
    }
}
