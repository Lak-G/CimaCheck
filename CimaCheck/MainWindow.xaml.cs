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
using CimaCheck.Pages;
using CimaCheck.Services;

namespace CimaCheck
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            NavigateToPagePrincipal();
            DataReader();
        }

        private void NavigateToPagePrincipal()
        {
            var pagePrincipal = new PagePrincipal();
            PageContainer.Content = pagePrincipal;

            pagePrincipal.GroupRegistrationButton.Click += (s, e) =>
            {
                NavigateToGroupRegistration();   
            };

            pagePrincipal.CimaRegistrationButton.Click += (s, e) =>
            {
                NavigateToCimaRegistration();
            };

            pagePrincipal.IndividualRegistrationButton.Click += (s, e) =>
            {
                NavigateToSingleRegistration();
            };
        }

        private void NavigateToGroupRegistration()
        {
            var groupRegistrationPage = new GroupRegistration();
            PageContainer.Content = groupRegistrationPage;

            groupRegistrationPage.BackButton.Click += (s, e) =>
            {
                NavigateToPagePrincipal();
            };
        }

        private void NavigateToCimaRegistration()
        {
            var cimaRegistration = new CimaRegistration();
            PageContainer.Content = cimaRegistration;

            cimaRegistration.BackButton.Click += (s, e) =>
            {
                NavigateToPagePrincipal();
            };
        }

        private void NavigateToSingleRegistration()
        {
            var individualRegistration = new IndividualRegistration();
            PageContainer.Content = individualRegistration;

            individualRegistration.BackButton.Click += (s, e) =>
            {
                NavigateToPagePrincipal();
            };
        }

        private void DataReader()
        {
            JsonController.UpdateAllJsons();
        }

    }
}