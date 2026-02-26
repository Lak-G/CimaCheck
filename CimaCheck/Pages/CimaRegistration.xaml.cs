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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CimaCheck.Models;

namespace CimaCheck.Pages
{
    /// <summary>
    /// Lógica de interacción para CimaRegistration.xaml
    /// </summary>
    public partial class CimaRegistration : UserControl
    {
        public CimaRegistration()
        {
            InitializeComponent();
        }

        private void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            Cimarron cima = new Cimarron()
            {
                Nombre = FullNameTextBox.Text,
                Genero = GenderComboBox.SelectedItem.ToString()
                //faltan las demas partes de la logica de registro, como facultad, programa, etc.
            };
        }
    }
}
