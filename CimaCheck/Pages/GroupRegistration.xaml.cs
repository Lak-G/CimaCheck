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
using CimaCheck.Services;

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

        private bool check = true;

        public GroupRegistration()
        {
            InitializeComponent();
            Loaded += async (s, e) =>
            {
                await CargarListasAsync();
                await CargarListaAlumnosAsync();
            };
        }

        private async Task CargarListasAsync()
        {
            lsEscuelas = await JsonController.JsonDeserializer<Escuela>("Escuelas.json");
        }

        private async Task CargarListaAlumnosAsync()
        {
            lsAlumnos = await JsonController.JsonDeserializer<Alumno>("Alumnos.json");
        }


        /// <summary>
        /// Handles the SelectionChanged event for the educational level ComboBox, updating the list of available
        /// schools based on the selected educational level.
        /// </summary>
        /// <remarks>If the selected educational level is not valid or not specified, the list of schools
        /// is not updated. Any errors encountered during the update process are displayed to the user in a message
        /// box.</remarks>
        /// <param name="sender">The source of the event, typically the educational level ComboBox.</param>
        /// <param name="e">The event data that contains information about the selection change.</param>
        private void NivelEducativoComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (NivelEducativoComboBox.SelectedItem is ComboBoxItem cbi)
            {

                var contenido = cbi.Content?.ToString() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(contenido) || contenido == "Selecciona una escuela")
                    return;

                try
                {
                    SchoolNameComboBox.Items.Clear();

                    foreach (var escuela in lsEscuelas)
                    {
                        if (escuela.NivelEducativo.Equals(contenido))
                        {
                            SchoolNameComboBox.Items.Add(new ComboBoxItem { Content = escuela.NombreEscuela });
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

        /// <summary>
        /// Handles the SelectionChanged event for the school name ComboBox, updating the displayed list of students
        /// based on the selected school.
        /// </summary>
        /// <remarks>If the selected item is not a valid school, the method exits without updating the
        /// student list. If an error occurs while loading or displaying students, an error message is shown and the
        /// exception is rethrown.</remarks>
        /// <param name="sender">The source of the event, typically the ComboBox whose selection has changed.</param>
        /// <param name="e">The event data that contains information about the selection change.</param>
        private void SchoolNameComboBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (SchoolNameComboBox.SelectedItem is ComboBoxItem cbi)
            {
                var contenido = cbi.Content.ToString() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(contenido) || contenido == "Selecciona una escuela")
                    return;

                try
                {
                    foreach (var escuela in lsEscuelas)
                    {
                        if (escuela.NombreEscuela.Equals(contenido))
                        {
                            escuelaActual = escuela;
                        }
                    }

                    ContenedorTarjetas.Children.Clear();

                    var alumnosDeEscuela = lsAlumnos.Where(a => a.IdEscuela == escuelaActual.Id).ToList();
                    
                    foreach (var alumno in alumnosDeEscuela)
                    {
                        AddCard(alumno);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error en la carga de la lista: {ex.Message}");
                    throw;
                }
            }
        }

        /// <summary>
        /// Adds a visual card representing the specified student to the card container.
        /// </summary>
        /// <remarks>The card displays the student's name, associated school, and attendance status.
        /// Changes to the attendance checkbox are reflected in the student's attendance property.</remarks>
        /// <param name="alumno">The student whose information will be displayed on the card. Cannot be null.</param>
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

        private void MarcarTodosCheckBox(bool marcar)
        {
            foreach (Border tarjeta in ContenedorTarjetas.Children.OfType<Border>())
            {
                // Border → Grid → CheckBox
                if (tarjeta.Child is Grid grid)
                {
                    var checkBox = grid.Children.OfType<CheckBox>().FirstOrDefault();
                    if (checkBox != null)
                        checkBox.IsChecked = marcar;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the Submit button to register attendance for all students in the current list.
        /// </summary>
        /// <remarks>Displays a message to the user indicating the result of the attendance registration.
        /// If there are no students to register, a warning message is shown. Any errors encountered during the process
        ///  are reported to the user via a message box.</remarks>
        /// <param name="sender">The source of the event, typically the Submit button.</param>
        /// <param name="e">The event data associated with the Click event.</param>
        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            if (lsAlumnos == null || lsAlumnos.Count == 0)
            {
                MessageBox.Show("No hay alumnos para registrar.");
                return;
            }

            try
            {
                int exitosos = 0;
                int fallos = 0;

                foreach (var alumno in lsAlumnos)
                {
                    if (alumno.IdEscuela == escuelaActual.Id)
                    {
                        bool resultado = await DataManager.ActualizarAsistenciaAlumno(alumno.Id, alumno.Asistencia);
                        if (resultado)
                            exitosos++;
                        else
                            fallos++;

                    }
                }

                if (fallos == 0)
                {
                    var dialog = new RegustroExitosoDialog();
                    dialog.Owner = Window.GetWindow(this);
                    dialog.ShowDialog();
                }
                else
                    MessageBox.Show($"Se registró la asistencia de {exitosos} alumno(s). {fallos} fallos.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al registrar asistencia: {ex.Message}");
            }
        }

        private void SelectAllBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (check)
            {
                MarcarTodosCheckBox(true);
                check = false;
            }else
            {
                MarcarTodosCheckBox(false);
                check = true;
            }
        }

        private void NombreCompletoTextBox_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            ContenedorTarjetas.Children.Clear();

            string filtro = NombreCompletoTextBox.Text.ToUpper().Trim();

            foreach (var temp in lsAlumnos)
            {
                if (temp.NombreAlumno.ToUpper().Contains(filtro))
                {
                    AddCard(temp);
                }
            }
        }
    }
}
