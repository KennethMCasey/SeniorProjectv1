using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*
 * Class: DataAccess
 * Created By: Kevin Wang
 * Purpose: Using the SQL connection string, update SQL by running commands to the SQL database
 */
namespace VehicleDetectionProject.Database
{
    public class DataAccess
    {
        /*
         * Method: IsServerConnected()
         * Input: None
         * Output: None
         * Purpose: Used to test to see if server connection is good
         */
        public bool IsServerConnected()
        {
            using (var l_oConnection = new SqlConnection(SQLConnection.ConnString("ParkingLotDB")))
            {
                try
                {
                    l_oConnection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }

        /*
         * Method: GetParkingLot
         * Input: [string] lotName, [string] lotNum
         * Output: [List] of Parking Lot
         * Purpose: Used to get specific parking lot information defined by parameter
         */
        public List<ParkingLot> GetParkingLot(string lotName, string lotNum)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(SQLConnection.ConnString("ParkingLotDB")))
            {
                return connection.Query<ParkingLot>($"SELECT * FROM viewParkingLotInfo WHERE LotName = '" + lotName + "' AND (LotNumber = '" + lotNum + "' OR LotNumber IS NULL)").ToList();
            }
        }

        /*
         * Method: GetParkingLot
         * Input: None
         * Output: [List] of Parking Lot
         * Purpose: Used to get all parking lot information 
         */
        public List<ParkingLot> GetAllParkingLot()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(SQLConnection.ConnString("ParkingLotDB")))
            {
                try
                {
                    return connection.Query<ParkingLot>($"SELECT * FROM viewParkingLotInfo").ToList();
                }
                catch(Exception ex)
                {

                }
                return null;
            }
        }
        /*
         * Method: GetStatusMessage
         * Input: None
         * Output: [List] of Parking Lot
         * Purpose: Used to get all parking lot status messages information 
         */
        public List<ParkingLot> GetStatusMessage()
        {
            using (IDbConnection connection = new SqlConnection(SQLConnection.ConnString("ParkingLotDB")))
            {
                try
                {
                    return connection.Query<ParkingLot>($"SELECT * FROM viewParkingLotMessages").ToList();
                }
                catch (Exception ex)
                {

                }
                return null;
            }
        }

        /*
         * Method: UpdateCameraURL
         * Input: [int] parkingLotID, [string] cameraURL
         * Output: [List] of Parking Lot
         * Purpose: Used to get all parking lot status messages information 
         */
        //Used to update camera url for parking lot
        public void UpdateCameraURL(int parkingLotID, string cameraURL)
        {
            using (SqlConnection connection = new SqlConnection(SQLConnection.ConnString("ParkingLotDB")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("spUpdateCameraURL", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@ParkingLotID", parkingLotID));
                        command.Parameters.Add(new SqlParameter("@CameraURL", cameraURL));
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        /*
         * Method: LotInfoParkedCars
         * Input: [int] parkingLotID, [int] carsParked
         * Output: None
         * Purpose: Used to update number of cars parked for a parking lot
         */
        public void LotInfoParkedCars(int parkingLotID, int carsParked)
        {
            using (SqlConnection connection = new SqlConnection(SQLConnection.ConnString("ParkingLotDB")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("spLotInfoParkedCars", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@ParkingLotID", parkingLotID));
                        command.Parameters.Add(new SqlParameter("@CarsParked", carsParked));
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        /*
         * Method: ParkingLotInfo
         * Input: [int] parkingLotID, [int?] maxCapacity, [string] permitType
         * Output: None
         * Purpose: Used to update max capacity and permit type for parking lot
         */
        public void ParkingLotInfo(int parkingLotID, int? maxCapacity, string permitType)
        {
            using (SqlConnection connection = new SqlConnection(SQLConnection.ConnString("ParkingLotDB")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("spParkingLotInfo", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@ParkingLotID", parkingLotID));
                        command.Parameters.Add(new SqlParameter("@MaxCapacity", maxCapacity));
                        command.Parameters.Add(new SqlParameter("@PermitType", permitType));
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        /*
         * Method: ParkingLotInfo
         * Input: [int] parkingLotID, [char] status, [string] message
         * Output: None
         * Purpose: Used to update status and message for parking lot
         */
        public void ParkingLotStatus(int parkingLotID, char status, string message)
        {
            using (SqlConnection connection = new SqlConnection(SQLConnection.ConnString("ParkingLotDB")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("spParkingLotStatus", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@ParkingLotID", parkingLotID));
                        command.Parameters.Add(new SqlParameter("@Status", status));
                        command.Parameters.Add(new SqlParameter("@Message", message));
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        /*
         * Method: CarParked
         * Input: [int] parkingLotID
         * Output: None
         * Purpose: Used to record a vehicle leaving which triggers the database to check if lots are full
         */
        public void CarParked(int parkingLotID)
        {
            using (SqlConnection connection = new SqlConnection(SQLConnection.ConnString("ParkingLotDB")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("spCarParked", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@ParkingLotID", parkingLotID));
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
        /*
         * Method: CarLeft
         * Input: [int] parkingLotID
         * Output: None
         * Purpose: Used to record a vehicle leaving which triggers the database to check if lots are full
         */
        public void CarLeft(int parkingLotID)
        {
            using (SqlConnection connection = new SqlConnection(SQLConnection.ConnString("ParkingLotDB")))
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("spCarLeft", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.Add(new SqlParameter("@ParkingLotID", parkingLotID));
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}
