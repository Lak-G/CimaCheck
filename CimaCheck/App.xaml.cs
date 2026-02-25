using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using CimaCheck.Services;
using Microsoft.Extensions.Configuration;


namespace CimaCheck
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IConfiguration? Configuration { get; private set; }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Cargar configuración
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            Configuration = builder.Build();

            // Inicializar Supabase
            try
            {
                await DataManager.InicializarAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar Supabase: {ex.Message}");
            }
        }
    }

}
