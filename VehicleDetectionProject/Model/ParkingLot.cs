using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleDetectionProject.Database
{
    public class ParkingLot
    {
        public int ParkingLotID { get; set; }

        public string LotName { get; set; }

        public string LotNumber { get; set; }

        public int Num_Of_Cars_Parked { get; set; }

        public char Is_lot_Full { get; set; }

        public int MaxCapacity { get; set; }

        public char Is_Lot_Open { get; set; }

        public string Lot_Message { get; set; }

        public string PermitType { get; set; }

        public string CameraURL { get; set; }

        public override string ToString()
        {

            return string.Format("{0} {1}", LotName, LotNumber);
        }
    }
}
