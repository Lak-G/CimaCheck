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
using CimaCheck.Services;

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
            try
            {
                if (FullNameTextBox.Text.Trim() == "" || GenderComboBox.SelectedIndex == 0 ||
                    FacultyComboBox.SelectedIndex == 0 || ProEdComboBox.SelectedIndex == 0)
                {
                    FullNameLabel.Foreground = new SolidColorBrush(Colors.DarkRed);
                    GenderLabel.Foreground = new SolidColorBrush(Colors.DarkRed);
                    FacultyLabel.Foreground = new SolidColorBrush(Colors.DarkRed);
                    ProEdLabel.Foreground = new SolidColorBrush(Colors.DarkRed);
                    return;
                }
                else
                {
                    FullNameLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF555555"));
                    GenderLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF555555"));
                    FacultyLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF555555"));
                    ProEdLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF555555"));
                }

                string nombre = FullNameTextBox.Text;

                string genero = "";
                if (GenderComboBox.SelectedItem is ComboBoxItem generoItem)
                {
                    genero = generoItem.Content.ToString() switch
                    {
                        "Masculino" => "Masculino",
                        "Femenino" => "Femenino",
                        "Otro" => "Otro",
                        _ => ""
                    };
                }

                int facultadId = 0;
                if (FacultyComboBox.SelectedItem is ComboBoxItem facultadItem && facultadItem.Tag != null)
                    facultadId = (int)facultadItem.Tag;

                int programaId = 0;
                if (ProEdComboBox.SelectedItem is ComboBoxItem programaItem && programaItem.Tag != null)
                    programaId = (int)programaItem.Tag;

                Cimarron cima = new Cimarron
                {
                    Nombre = nombre,
                    Genero = genero,
                    FacultadId = facultadId,
                    ProgramaId = programaId
                };

                DataManager.RegistrarCimarron(cima);

                LimpiarFormulario();

                var dialog = new RegustroExitosoDialog();
                dialog.Owner = Window.GetWindow(this);
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en el registro: {ex}");
            }
        }

        private void LimpiarFormulario()
        {
            FullNameTextBox.Text = "";
            MatriculaTextBox.Text = "";
            GenderComboBox.SelectedIndex = 0;
            FacultyComboBox.SelectedIndex = 0;
            ProEdComboBox.Items.Clear();
            ProEdComboBox.Items.Add(new ComboBoxItem { Content = "Seleccione una Carrera", IsEnabled = false, IsSelected = true });
        }

        private void MatriculaTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
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
