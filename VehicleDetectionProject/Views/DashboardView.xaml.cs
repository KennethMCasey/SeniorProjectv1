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
using System.Windows.Threading;
using VehicleDetectionProject.Database;
using VehicleDetectionProject.ViewModel;
using MaterialDesignThemes.Wpf;
using System.Collections.ObjectModel;
using Carz;

namespace VehicleDetectionProject.Views
{
    /// <summary>
    /// Interaction logic for DashboardView.xaml
    /// </summary>
    public partial class DashboardView : UserControl
    {
        List<ParkingLot> pk = new List<ParkingLot>();
        DashboardViewModel dvm;
        Carz.VideoInterpreter vi;

        private static string videoFeed = "C:\\Users\\ps2ho\\Desktop\\ParkingLotBackendGUI-Kenny\\ParkingLotVideo-master\\FarmingdaleSmartParking2\\camera.mp4";
        private static string cvFile = "C:\\Users\\ps2ho\\Desktop\\ParkingLotBackendGUI-Kenny\\ParkingLotVideo-master\\FarmingdaleSmartParking2\\cars.xml";

        public void CarDidEnter(VideoInterpreter vi)
        {
            //cal car did enter stored procedire here with the currently selected parking lot id
            System.Diagnostics.Debug.WriteLine("CAR DID ENTER CALLED");
        }

        public void CarDidLeave(VideoInterpreter vi)
        {
            //Call car did leave stored procedure here with the currently selected prarking lot id
            System.Diagnostics.Debug.WriteLine("CAR DID LEAVE CALLED");
        }

        public DashboardView()
        {
            InitializeComponent();
        }

        private void DashboardView_Loaded(object sender, RoutedEventArgs e)
        {
            FillDataAsync();              
        }

        //User selects a parking lot and displays existing camera URL
        private void ParkingLot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
              if (vi != null) vi.stop();
              vi = new VideoInterpreter(videoFeed, cvFile, Dispatcher.CurrentDispatcher);
              vi.setCarDidEnterDelegate(CarDidEnter);
              vi.setCarDidLeaveDelegate(CarDidLeave);
              mediaElementPlayer.Source = new Uri(videoFeed);
              vi.start();
                                       
                int index = comboBoxParkingLot.SelectedIndex;
                string statusMsg = dvm.ParkingLotStatusLongDisplay(pk[index].Is_Lot_Open);

                //Status
                txtParkingLotStatus.Text = statusMsg;
                //Max Capacity
                txtParkingLotCurrentAvailable.Text = (pk[index].MaxCapacity - pk[index].Num_Of_Cars_Parked).ToString();
            }
            catch (Exception ex) {

            };
        }

        private void FillInfo()
        {
            try
            {
                ClearInfo();
                comboBoxParkingLot.ItemsSource = pk;
            }
            catch(Exception e)
            {

            }
        }

        private void ClearInfo()
        {
            //Status
            txtParkingLotStatus.Text = null;
            //Max Capacity
            txtParkingLotCurrentAvailable.Text = null;
        }

        private void connectionStatus(bool status)
        {
            //On
            if(status == true)
            {
                connectionStatusIcon.Foreground = Brushes.Green;
            }
            else //Off
            {
                connectionStatusIcon.Foreground = Brushes.Red;
            }
        }

        private void streamStatus(bool status)
        {
            //On
            if(status == true)
            {
                streamStatusIcon.Foreground = Brushes.Green;
            }
            else //Off
            {
                streamStatusIcon.Foreground = Brushes.Red;
            }
        }

        private void trackingStatus(bool status)
        {
            //On
            if(status == true)
            {
                trackingStatusIcon.Foreground = Brushes.Green;
            }
            else //Off
            {
                trackingStatusIcon.Foreground = Brushes.Red;
            }
        }

        private async Task RefreshDataAsync()
        {
            NoConnection.Visibility = Visibility.Hidden;
            RefreshDataIcon.Visibility = Visibility.Visible;
            dvm = new DashboardViewModel();

            bool status = await Task.Run(() => dvm.IsServerConnected());

            if (status == true) //Connection Found
            {
                pk = await Task.Run(() => dvm.GetParkingLots());
                FillInfo();
                connectionStatus(true);
                RefreshDataIcon.Visibility = Visibility.Hidden;
            }
            else //No Connection
            {
                RefreshDataIcon.Visibility = Visibility.Hidden;
                NoConnection.Visibility = Visibility.Visible;
                connectionStatus(false);
            }
        }

        private async Task FillDataAsync()
        {
            NoConnection.Visibility = Visibility.Hidden;
            LoadingData.Visibility = Visibility.Visible;
            dvm = new DashboardViewModel();

            bool status = await Task.Run(() => dvm.IsServerConnected());

            if (status == true) //Connection Found
            {
                pk = dvm.GetParkingLots();
                FillInfo();
                connectionStatus(true);
                LoadingData.Visibility = Visibility.Hidden;
            }
            else //No Connection
            {
                LoadingData.Visibility = Visibility.Hidden;
                NoConnection.Visibility = Visibility.Visible;
                connectionStatus(false);
            }
        }

        private void buttonRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshDataAsync();
        }
    }
}
