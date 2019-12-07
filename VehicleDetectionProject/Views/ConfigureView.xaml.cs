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
using Carz;
using System.Threading;
using System.Windows.Threading;

namespace VehicleDetectionProject.Views
{
    /// <summary>
    /// Interaction logic for Controls.xaml
    /// </summary>
    public partial class ConfigureView : UserControl
    {
        List<ParkingLot> pk = new List<ParkingLot>();
        List<ParkingLot> msg = new List<ParkingLot>();
        ConfigureViewModel cvm;

        //Vehicle Detection
        VideoInterpreter videoInterpreter;

        //Track of if new message is selected
        bool newMsgSelected = false;

        public ConfigureView()
        {
            InitializeComponent();
        }

        public void CarDidEnter(VideoInterpreter vi)
        {
          cvm.CarEntered(1);
            System.Diagnostics.Debug.WriteLine("HERE");
        }


        private void ConfigureView_Loaded(object sender, RoutedEventArgs e)
        {
           FillDataAsync();
     

        }


        //User selects a parking lot and displays existing camera URL
        private void ParkingLot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
             
                int index = comboBoxParkingLot.SelectedIndex;
                string statusMsg = cvm.ParkingLotStatusLongDisplay(pk[index].Is_Lot_Open);
                //Status
                comboBoxStatus.Text = statusMsg;
                NoParkingLotSelected.Visibility = Visibility.Hidden;
                comboBoxMessage.Visibility = Visibility.Visible;
                textBoxMessage.Visibility = Visibility.Hidden;
            }
            catch (ArgumentOutOfRangeException ex) { };
        }

        private void FillInfoAsync()
        {
            try
            {
                ParkingLot emptyMsg = new ParkingLot();
                emptyMsg.Lot_Message = ""; //Add empty message if user wants no lot message
                msg.Insert(0, emptyMsg);
                
                //Set itemsource to list
                comboBoxParkingLot.ItemsSource = pk;
                listViewParkingLot.ItemsSource = pk;
                comboBoxMessage.ItemsSource = msg;

            }
            catch (Exception e)
            {

            }
        }

        private void AddNewMessage_Selected(object sender, RoutedEventArgs e)
        {
            if (newMsgSelected == false) //New Message Box Display
            {
                comboBoxMessage.Visibility = Visibility.Hidden;
                textBoxMessage.Visibility = Visibility.Visible;
                newMsgSelected = true;
            }
            else //New Message Box Hide
            {
                comboBoxMessage.Visibility = Visibility.Visible;
                textBoxMessage.Visibility = Visibility.Hidden;
                newMsgSelected = false;
            }
        }

        //Empty
        private void Modify_Click(object sender, RoutedEventArgs e)
        {
            if (!NoConnection.IsVisible)
            { //Connection is there
              //A parking lot is selected
                if (comboBoxParkingLot.SelectedItem != null)
                {
                    int index = comboBoxParkingLot.SelectedIndex + 1;

                    //If Camera Source is not Empty
                    if (textBoxCameraURL.Text.Trim() != "")
                    {
                        string cameraURL = textBoxCameraURL.Text.Trim();
                        cvm.UpdateCameraURL(index, cameraURL);
                    }

                    //Message
                    string status = comboBoxStatus.Text.ToString();

                    if (newMsgSelected == false) //Check for Combobox
                    {
                        if (comboBoxStatus.SelectedItem != null) //If Status is not empty
                        {
                            //Message is not empty, get message index*
                            if (comboBoxMessage.Text != "")
                            {
                                cvm.ParkingLotStatus(index, status, comboBoxMessage.Text);
                            }
                            else //Message is empty, status is set	
                            {
                                cvm.ParkingLotStatus(index, status, null);
                            }
                        }
                    }
                    else //Check for New Message
                    {
                        string newMessage = textBoxMessage.Text.Trim();

                        if (newMessage != "") //User wants to add a new message, we use the insert statement
                        {
                            cvm.ParkingLotStatus(index, status, textBoxMessage.Text.Trim());
                        }
                    }

                    //If cars parked is not null
                    if (textBoxCarsParked.Text.Trim() != null)
                    {
                        int num;
                        int.TryParse(textBoxCarsParked.Text.Trim(), out num);
                        cvm.LotInfoParkedCars(index, num);
                    }

                    //If max capacity and Permit type is not null
                    if (textBoxMaxCapacity.Text.Trim() != null && comboBoxPermitType.SelectedItem != null)
                    {
                        int capacity;
                        int.TryParse(textBoxMaxCapacity.Text.Trim(), out capacity);
                        cvm.ParkingLotInfo(index, capacity, comboBoxPermitType.Text);
                    }
                    //Else if max capacity is not null
                    else if (textBoxMaxCapacity.Text.Trim() != null)
                    {
                        int capacity;
                        int.TryParse(textBoxMaxCapacity.Text.Trim(), out capacity);
                        cvm.ParkingLotInfo(index, capacity, null);
                    }
                    //Else permit type is not null and max capacity is null
                    else if (comboBoxPermitType.SelectedItem != null)
                    {
                        cvm.ParkingLotInfo(index, null, comboBoxPermitType.Text);
                    }
                    RefreshDataAsync();
                }
                else //Display that no parking lot is selected message
                {
                    NoParkingLotSelected.Visibility = Visibility.Visible;
                }
            }
        }
        private void buttonRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshDataAsync();
        }

        private async Task RefreshDataAsync()
        {
            NoConnection.Visibility = Visibility.Hidden;
            RefreshData.Visibility = Visibility.Visible;

            cvm = new ConfigureViewModel();
            bool status = await Task.Run(() => cvm.IsServerConnected()); //Wait to get response from server

            if (status == true) //Connected
            {
                pk = cvm.GetParkingLots();
                //msg = await Task.Run(() => cvm.GetStatusMessage());
                msg = cvm.GetStatusMessage();
                FillInfoAsync();
                RefreshData.Visibility = Visibility.Hidden;
            }
            else //Not Connected
            {
                RefreshData.Visibility = Visibility.Hidden;
                NoConnection.Visibility = Visibility.Visible;
                Console.WriteLine("Not Connected");
            }
        }

        private async Task FillDataAsync()
        {
            LoadingData.Visibility = Visibility.Visible; //Loading Data Picture - Visible

            cvm = new ConfigureViewModel();
            bool status = await Task.Run(() => cvm.IsServerConnected()); //Wait to get response from server

            if (status == true) //Connected
            {
                pk = cvm.GetParkingLots();
                msg = cvm.GetStatusMessage();
                FillInfoAsync(); //Wait until info is filled
                LoadingData.Visibility = Visibility.Hidden; //Loading Data Picture - Hidden
            }
            else //Not Connected
            {
                NoConnection.Visibility = Visibility.Visible; //No Connection Picture - Visible
                LoadingData.Visibility = Visibility.Hidden; //Loading Data Picture - Hidden
                Console.WriteLine("Not Connected");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            comboBoxMessage.Visibility = Visibility.Visible;
            textBoxMessage.Visibility = Visibility.Hidden;
            newMsgSelected = false;
            textBoxMessage.Text = null;
        }
    }
}
