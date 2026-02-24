using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Text;
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

namespace CimaCheck.Pages
{



    /// <summary>
    /// Lógica de interacción para GroupRegistration.xaml
    /// </summary>
    public partial class GroupRegistration : UserControl
    {
        private List<Escuela> lsEscuelas = new List<Escuela>();
        private List<Alumno> lsAlumnos = new List<Alumno>();
        private Escuela escuelaActual = new Escuela();

        public GroupRegistration()
        {
            InitializeComponent();

        }


        private async void CargarListas()
        {
            lsEscuelas = await JsonController.JsonDeserializer<Escuela>("Escuelas.json");
        }

        private void NivelEducativoComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            MessageBox.Show("Evento3");
            if (NivelEducativoComboBox.SelectedItem is ComboBoxItem cbi)
            {
                MessageBox.Show("Entroa4");

                var contenido = cbi.Content?.ToString() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(contenido) || contenido == "Selecciona una escuela")
                    return;

                try
                {
                    MessageBox.Show("entra2");

                    //aqui continua la logica de cargar escuelas 
                    CargarListas();

                    SchoolNameComboBox.Items.Clear();

                    foreach (var escuela in lsEscuelas)
                    {
                        if (escuela.NivelEducativo.Equals(contenido))
                        {
                            SchoolNameComboBox.Items.Add(escuela.NombreEscuela);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error en la carga de la lista: {ex.Message}");
                    return;
                }

            }
        }

        private async void CargarListaAlumnos()
        {
            lsAlumnos = await JsonController.JsonDeserializer<Alumno>("Alumnos.json");
        }

        private void SchoolNameComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            MessageBox.Show("Evento");
            if (SchoolNameComboBox.SelectedItem is ComboBoxItem cbi)
            {
                MessageBox.Show("entra1");
                //var contenido = cbi.Content.ToString() ?? string.Empty;

                //if (string.IsNullOrWhiteSpace(contenido) || contenido == "Selecciona una escuela")
                //    return;

                //try
                //{
                //    CargarListaAlumnos();

                //    SchoolNameComboBox.Items.Clear();

                //    foreach (var escuela in lsEscuelas)
                //    {
                //        if (escuela.NombreEscuela.Equals(contenido))
                //        {
                //            escuelaActual = escuela;
                //        }
                //    }

                //    ContenedorTarjetas.Children.Clear();

                //    foreach (var alumno in lsAlumnos)
                //    {
                //        if (alumno.IdEscuela == escuelaActual.Id)
                //        {
                //            AddCard(alumno);
                //        }
                //    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show($"Error en la carga de la lista: {ex.Message}");
                //    throw;
                //}
            }
        }

        private void temp()
        {
            if (NivelEducativoComboBox.SelectedItem is ComboBoxItem cbi)
            {
                var contenido = cbi.Content?.ToString() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(contenido) || contenido == "Selecciona una escuela")
                    return;
            }
        }



        private void AddCard(Alumno alumno)
        {
            Border Tarjeta = new Border()
            {
                Background = new SolidColorBrush(Colors.White),
                BorderBrush = new SolidColorBrush(Color.FromRgb(230, 230, 230)),
                BorderThickness = new Thickness(1),
                CornerRadius = new CornerRadius(12),
                Margin = new Thickness(0, 0, 0, 12),
                Padding = new Thickness(16),
                Height = 70
            };

            Grid gridInterno = new Grid();
            gridInterno.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            gridInterno.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });

            StackPanel InfoPanel = new StackPanel()
            {
                VerticalAlignment = VerticalAlignment.Center
            };

            TextBlock NombreAlumno = new TextBlock()
            {
                Text = alumno.NombreAlumno,
                FontSize = 14,
                FontWeight = FontWeights.SemiBold,
                Foreground = new SolidColorBrush(Colors.Black)
            };

            string escuela = "No tiene escuela xd";
            foreach (var scuela in lsEscuelas)
            {
                if(scuela.Id == alumno.IdEscuela)
                {   
                    escuela = scuela.NombreEscuela;
                }
            }

            TextBlock nombreEscuela = new TextBlock()
            {
                Text = escuela,
                FontSize = 12,
                Foreground = new SolidColorBrush(Colors.Gray)
            };

            InfoPanel.Children.Add(NombreAlumno);
            InfoPanel.Children.Add(nombreEscuela);

            CheckBox checkBoxAsistencia = new CheckBox()
            {
                IsChecked = alumno.Asistencia,
                VerticalAlignment   = VerticalAlignment.Center,
                Width = 20,
                Height = 20,
                Style = (Style)FindResource("CheckBoxUABC")
            };

            checkBoxAsistencia.Checked += (s, e) => alumno.Asistencia = true;
            checkBoxAsistencia.Unchecked += (s, e) => alumno.Asistencia = false;

            Grid.SetColumn(InfoPanel, 0);
            Grid.SetColumn(checkBoxAsistencia, 1);
            gridInterno.Children.Add(InfoPanel);
            gridInterno.Children.Add(checkBoxAsistencia);

            Tarjeta.Child = gridInterno;

            ContenedorTarjetas.Children.Add(Tarjeta);
        }
    }
}
