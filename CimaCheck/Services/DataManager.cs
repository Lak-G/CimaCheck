using CimaCheck.Controllers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using CimaCheck.Models;

namespace CimaCheck.Services
{
    public static class DataManager
    {
        private static Supabase.Client? _supabase;

        #region Constructor

        public static async Task InicializarAsync()
        {
            var settings = App.Configuration.GetSection("Supabase").Get<SupabaseSettings>();

            if (settings == null || string.IsNullOrEmpty(settings.Url) || string.IsNullOrEmpty(settings.Key))
            {
                throw new InvalidOperationException(
                    "La configuración de Supabase no está completa. Verifica el archivo appsettings.json");
            }

            var options = new Supabase.SupabaseOptions
            {
                AutoRefreshToken = true,
                AutoConnectRealtime = false
            };

            _supabase = new Supabase.Client(settings.Url, settings.Key, options);
            await _supabase.InitializeAsync();
        }

        #endregion

        /// <summary>
        /// Attempts to verify whether the Supabase connection has been initialized and is available.
        /// </summary>
        /// <remarks>If the Supabase connection is not initialized or an error occurs, an error message is
        /// displayed to the user and the method returns <see langword="false"/>.</remarks>
        /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the Supabase
        /// connection is initialized; otherwise, <see langword="false"/>.</returns>
        public static async Task<bool> ProbarConexionAsync()
        {
            try
            {
                if (_supabase == null)
                    throw new InvalidOperationException("Supabase no ha sido inicializado");
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error de conexión: {ex.Message}");
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static async Task<bool> ActualizarAsistenciaAlumno(int Id, bool asistencia)
        {
            return await AlumnosController.ActualizarAsistencia(_supabase, Id, asistencia);
        }

        

        public static Supabase.Client? ObtenerSupabase() => _supabase;

        /// <summary>
        /// Asynchronously retrieves a list of all students.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Alumno"/>
        /// objects representing the students. The list will be empty if no students are found.</returns>
        public static async Task<List<Alumno>> ConseguirListaAlumnos()
        {
            List<Alumno> lsAlumnos = await AlumnosController.ConseguirListasAlumnos(_supabase);
            return lsAlumnos;
        }

        public static async Task<List<Escuela>> ConseguirEscuelas()
        {
            List<Escuela> lsAlumnos = await SchoolsController.GetSchools(_supabase);
            return lsAlumnos;
        }

        public static async Task<List<Carrera>> ConseguirCarreras()
        {
            List<Carrera> lsCarreras = await UniversitiesController.GetCareer(_supabase);
            return lsCarreras;
        }

        public static async Task<List<Facultad>> ConseguirFacultades()
        {
            List<Facultad> lsFaculty = await UniversitiesController.GetFacultades(_supabase);
            return lsFaculty;
        }

        #region Registro Cimarrones

        public static async void RegistrarCimarron(Cimarron cima)
        {
            CimarronController.RegistrarCimarron(_supabase, cima);
        }

        #endregion

        #region Organizaciones

        public static async Task<List<Company>> ConseguirOrg()
        {
            List<Company> lsOrg = await OrganizacionesController.GetOrg(_supabase);
            return lsOrg;
        }

        #endregion
    }
}
