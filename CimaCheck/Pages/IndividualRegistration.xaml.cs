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

namespace CimaCheck.Pages
{
    /// <summary>
    /// Lógica de interacción para IndividualRegistration.xaml
    /// </summary>
    public partial class IndividualRegistration : UserControl
    {
        public IndividualRegistration()
        {
            InitializeComponent();
        }
        private void EdadTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextNumeric(e.Text);
        }
        private bool IsTextNumeric(string text)
        {
            return text.All(char.IsDigit);
        }
    }

}
