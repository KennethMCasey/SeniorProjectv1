using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleDetectionProject.Database;
using MaterialDesignThemes.Wpf;

namespace VehicleDetectionProject.ViewModel
{
    public class ConfigureViewModel
    {
        DataAccess db = new DataAccess();

        public bool IsServerConnected()
        {
            return db.IsServerConnected();
        }

        public List<ParkingLot> GetParkingLots()
        {
            return db.GetAllParkingLot();
        }

        public List<ParkingLot> GetStatusMessage()
        {
            return db.GetStatusMessage();
        }

        public void UpdateCameraURL(int parkingLotID, string cameraURL)
        {
            db.UpdateCameraURL(parkingLotID, cameraURL);
        }

        public void LotInfoParkedCars(int parkingLotID, int carsParked)
        {
            db.LotInfoParkedCars(parkingLotID, carsParked);
        }

        public void ParkingLotInfo(int parkingLotID, int? maxCapacity, string permitType)
        {       
         db.ParkingLotInfo(parkingLotID, maxCapacity, permitType);
        }

        public void ParkingLotStatus(int parkingLotID, string status, string message)
        {
            char pStatus = ParkingLotStatusShortDisplay(status);
            db.ParkingLotStatus(parkingLotID, pStatus, message);
        }

        public void CarLeft(int parkingLotID)
        {
            db.CarLeft(parkingLotID);
        }

        public void CarEntered(int parkingLotID)
        {
            db.CarParked(parkingLotID);
        }

        public string ParkingLotStatusLongDisplay(char status)
        {
            return status == 'Y' ? "Open" : "Closed";
        }

        public char ParkingLotStatusShortDisplay(string status)
        {
            return status == "Open" ? 'Y' : 'N';
        }
    }
}
