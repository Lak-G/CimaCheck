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
using CimaCheck.Controllers;
using CimaCheck.Models;

namespace CimaCheck.Pages
{
    /// <summary>
    /// Lógica de interacción para CimaRegistration.xaml
    /// </summary>
    public partial class CimaRegistration : UserControl
    {
        private List<Facultad> lsFacultades = new List<Facultad>();
        private List<Carrera> lsProgramas = new List<Carrera>();


        public CimaRegistration()
        {
            InitializeComponent();
            Loaded += async (s, e) =>
            {
                await CargarListasAsync();
                LlenarComboFacultades();
            };
        }

        private async Task CargarListasAsync()
        {
            lsFacultades = await JsonController.JsonDeserializer<Facultad>("Facultades.json");
            lsProgramas = await JsonController.JsonDeserializer<Carrera>("Carreras.json");
        }

        private void LlenarComboFacultades()
        {
            FacultyComboBox.Items.Clear();
            FacultyComboBox.Items.Add(new ComboBoxItem { Content = "Seleccione Facultad", IsEnabled = false, IsSelected = true });

            foreach (var facultad in lsFacultades)
            {
                FacultyComboBox.Items.Add(new ComboBoxItem { Content = facultad.Nombre, Tag = facultad.Id });
            }
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

        private void FacultyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FacultyComboBox.SelectedItem is ComboBoxItem cbi && cbi.Tag != null)
            {
                int facultadId = (int)cbi.Tag;

                ProEdComboBox.Items.Clear();
                ProEdComboBox.Items.Add(new ComboBoxItem { Content = "Seleccione una Carrera", IsEnabled = false, IsSelected = true });

                foreach (var carrera in lsProgramas)
                {
                    if (carrera.FacultadId == facultadId)
                    {
                        ProEdComboBox.Items.Add(new ComboBoxItem { Content = carrera.Nombre, Tag = carrera.Id });
                    }
                }
            }
        }

    }
}
