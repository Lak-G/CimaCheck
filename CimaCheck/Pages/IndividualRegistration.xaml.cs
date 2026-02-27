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
    /// Lógica de interacción para IndividualRegistration.xaml
    /// </summary>
    public partial class IndividualRegistration : UserControl
    {
        private List<Company> lsCompanies = new List<Company>();
        public IndividualRegistration()
        {
            InitializeComponent();
            CargarOrg();
        }
        private void EdadTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }
        private bool IsTextNumeric(string text)
        {
            return text.All(char.IsDigit);
        }

        private async void CargarOrg()
        {
            try
            {
                lsCompanies = await JsonController.JsonDeserializer<Company>("Organizaciones.json");

                OrganizationComboBox.Items.Clear();

                foreach (var company in lsCompanies)
                {
                    OrganizationComboBox.Items.Add(company.NombreProc);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar organizaciones{ex}");
                throw;
            }
        }

        private void SubmitButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (NombreCompletoTextBox.Text.Trim() == "" || CorreoElectronicoTextBox.Text.Trim() == "" ||
                    GenderComboBox.SelectedIndex == 0)
                {
                    NombreCompletoLabel.Foreground = new SolidColorBrush(Colors.DarkRed);
                    CorreoElectronicoLabel.Foreground = new SolidColorBrush(Colors.DarkRed);
                    GenderLabel.Foreground = new SolidColorBrush(Colors.DarkRed);
                    return;
                }
                else
                {
                    NombreCompletoLabel.Foreground =
                        new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF555555"));
                    CorreoElectronicoLabel.Foreground =
                        new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF555555"));
                    GenderLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF555555"));
                }

                string nombre = NombreCompletoTextBox.Text;

                string genero = "";
                if (GenderComboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    genero = selectedItem.Content.ToString() switch
                    {
                        "Masculino" => "Masculino",
                        "Femenino" => "Femenino",
                        "Otro" => "Otro",
                        _ => ""
                    };
                }

                int idProcedencia = OrganizationComboBox.SelectedIndex + 1;
                string edad = EdadTextBox.Text;

                Externos registro = new Externos
                {
                    Nombre = nombre,
                    Genero = genero,
                    IdProcedencia = idProcedencia,
                    Edad = edad
                };

                DataManager.RegistroIndividual(registro);

                LimpiarFormulario();

                var dialog = new RegustroExitosoDialog();
                dialog.Owner = Window.GetWindow(this);
                dialog.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error en el registro: {ex}");
                throw;
            }
        }

        private void LimpiarFormulario()
        {
            NombreCompletoTextBox.Text = "";
            CorreoElectronicoTextBox.Text = "";
            GenderComboBox.SelectedIndex = 0;
        }

    }

}
