using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CimaCheck.Controllers;
using CimaCheck.Models;
using CimaCheck.Services;

namespace CimaCheck
{
    /// <summary>
    /// Lógica de interacción para PagePrincipal.xaml
    /// </summary>
    public partial class PagePrincipal : UserControl
    {

        public PagePrincipal()
        {
            InitializeComponent();
            CheckConnection();
            
        }

        /// <summary>
        /// Este metodo hacer la verificacion de la conexion a la base de datos
        /// en supabase, si la conexion es exitosa se muestra un mensaje de exito,
        /// de lo contrario se muestra un mensaje de error
        /// </summary>
        private async void CheckConnection()
        {
            bool result = await DataManager.ProbarConexionAsync();

            if (result)
            {
                CheckImageOk.Visibility = Visibility.Visible;
            }
            else
            {
                CheckImageError.Visibility = Visibility.Visible;
            }

        }



        /// <summary>
        /// Handles the Click event of the support button by opening the default web browser to compose an email to the
        /// support address using Gmail.
        /// </summary>
        /// <remarks>If the default web browser cannot be opened, an error message is displayed to the
        /// user.</remarks>
        /// <param name="sender">The source of the event, typically the button that was clicked.</param>
        /// <param name="e">The event data associated with the click event.</param>
        private void btnSoporte_Click(object sender, RoutedEventArgs e)
        {
            string destinatario = "amado.garcia.ramirez@uabc.edu.mx";
            string asunto = "Soporte CimaCheck: *Escriba su problema aqui";


            string gmailUrl = "https://mail.google.com/mail/?view=cm" +
                              $"&to={Uri.EscapeDataString(destinatario)}";

            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = gmailUrl,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al abrir el navegador: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
