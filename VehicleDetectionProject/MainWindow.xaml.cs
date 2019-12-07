using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VehicleDetectionProject.Database;
using VehicleDetectionProject.ViewModel;

namespace VehicleDetectionProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<ParkingLot> parkingLots = new List<ParkingLot>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            btnOpenMenu.Visibility = Visibility.Collapsed;
            btnCloseMenu.Visibility = Visibility.Visible;
        }

        private void btnCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            btnOpenMenu.Visibility = Visibility.Visible;
            btnCloseMenu.Visibility = Visibility.Collapsed;
        }

        private void DashboardView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new DashboardViewModel();
        }
        
        private void SourceView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new SourceViewModel();
        }

        private void ConfigureView_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new ConfigureViewModel();

        }
        private void ParkingDataView_Clicked(object sender, RoutedEventArgs e)
        {
            //Initialize 
            DataContext = new ParkingDataViewModel();

        }


    }
}
