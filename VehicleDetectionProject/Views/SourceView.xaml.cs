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

namespace VehicleDetectionProject.Views
{
    /// <summary>
    /// Interaction logic for Configure.xaml
    /// </summary>
    public partial class SourceView : UserControl
    {
        List<ParkingLot> pk = new List<ParkingLot>();
        SourceViewModel svm;
        public SourceView()
        {
            InitializeComponent();
        }

        private void SourceView_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataAsync();
        }

        private void buttonAddCamera_Click(object sender, RoutedEventArgs e)
        {
            if (!NoConnection.IsVisible) //There is a connection
            {
                try
                {
                    int index = comboBoxParkingLot.SelectedIndex + 1;
                    string url = textBoxCameraURL.Text.Trim();
                    //Update parking lots camera url
                    svm.UpdateCameraURL(index, url);
                    RefreshDataASync();
                }
                catch (ArgumentOutOfRangeException ex) { };
            }
        }

        //Add Refresh when inserting/updating camera url to database is complete
        //Clears the information previously and adds up-to-date data
        private void FillInfoASync()
        {
            try
            {
                //Add to comboBoxParkingLot combobox
                comboBoxParkingLot.ItemsSource = pk;
                listViewParkingLot.ItemsSource = pk;
            }
            catch(Exception e)
            {

            }
        }

        private void buttonRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshDataASync();
        }

        private async Task FillDataAsync()
        {
            NoConnection.Visibility = Visibility.Hidden;
            LoadingData.Visibility = Visibility.Visible;
            svm = new SourceViewModel();

            bool status = await Task.Run(() => svm.IsServerConnected());

            if (status == true) //Connection Established
            {
                pk = svm.GetParkingLots();
                FillInfoASync();
                LoadingData.Visibility = Visibility.Hidden;
            }
            else //No Connection
            {
                NoConnection.Visibility = Visibility.Visible;
                LoadingData.Visibility = Visibility.Hidden;
                Console.WriteLine("Not Connected");
            }
        }

        private async Task RefreshDataASync()
        {
            NoConnection.Visibility = Visibility.Hidden;
            RefreshDataIcon.Visibility = Visibility.Visible;
            svm = new SourceViewModel();

            bool status = await Task.Run(() => svm.IsServerConnected());

            if (status == true) //Connection Established
            {
                pk = await Task.Run(() => svm.GetParkingLots());
                FillInfoASync();
                RefreshDataIcon.Visibility = Visibility.Hidden;
            }
            else //No Connection
            {
                NoConnection.Visibility = Visibility.Visible;
                RefreshDataIcon.Visibility = Visibility.Hidden;
                Console.WriteLine("Not Connected");
            }
        }
    }
}
