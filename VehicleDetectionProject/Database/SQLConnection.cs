using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Class: SQLConnection
 * Created By: Kevin Wang
 * Purpose: Gets the SQL connection string for Parking Lot Database
 */
namespace VehicleDetectionProject.Database
{
    public static class SQLConnection
    {
        public static string ConnString(string name) //ParkingLotDB
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

    }
}
