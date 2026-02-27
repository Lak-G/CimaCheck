using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CimaCheck.Pages
{
    /// <summary>
    /// Lógica de interacción para RegustroExitosoDialog.xaml
    /// </summary>
    public partial class RegustroExitosoDialog : Window
    {
        public RegustroExitosoDialog()
        {
            InitializeComponent();
            _ = ProgressBarInicio();
        }

        /// <summary>
        /// Metodo asincrono para que la barra se llene en automatico
        /// </summary>
        private async Task ProgressBarInicio()
        {
            for (int i = 0; i < 200; ++i)
            {
                ProgressBar1.Value += 1;
                await Task.Delay(5);
            }

            Close();
        }

        /// <summary>
        /// Evento de click para cerrar la ventana
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProgressBar1_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
