using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlServerCe;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace DistanceCalCulator
{
    public class ApplicationState
    {
        private static ApplicationState instance;
        private Int64 cruiseSpeed;
        private Int64 cruiseFuelFlow;
        private Int64 minFuel;
        private Int64 deckHoldFuel;
        private string speed;
        private string unit;
        private string utcOffset;    
        private string locationFormat;
        private string registeredClientName;

        public static ApplicationState Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ApplicationState();
                }
                return instance;
            }
        }


        private ApplicationState()
        {
            // read the application state from db
            SqlCeConnection _dataConn = null;
            try
            {
                _dataConn = new SqlCeConnection("Data Source=FlightPlannerDB.sdf;Persist Security Info=False;");
                _dataConn.Open();
                SqlCeCommand selectCmd = new SqlCeCommand();
                selectCmd.Connection = _dataConn;
                StringBuilder selectQuery = new StringBuilder();
                selectQuery.Append("SELECT cruiseSpeed,cruiseFuelFlow,minFuel,speed,unit,utcOffset,locationFormat,deckHoldFuel,registeredClientName FROM ApplicationState");
                selectCmd.CommandText = selectQuery.ToString();
                SqlCeResultSet results = selectCmd.ExecuteResultSet(ResultSetOptions.Scrollable);
                if (results.HasRows)
                {
                    results.ReadFirst();
                    cruiseSpeed = results.GetInt64(0);
                    cruiseFuelFlow = results.GetInt64(1);
                    minFuel = results.GetInt64(2);
                    speed = results.GetSqlString(3).ToString();
                    unit = results.GetSqlString(4).ToString();
                    utcOffset = results.GetSqlString(5).ToString();
                    locationFormat = results.GetSqlString(6).ToString();
                    deckHoldFuel = results.IsDBNull(7) ? 0 : results.GetInt64(7);
                    registeredClientName = results.IsDBNull(8) ? string.Empty : results.GetString(8);
                }
                
            }
           
            finally
            {
                _dataConn.Close();
            }
        }

        public void UpdateRegisteredClientName(string clientName)
        {
            registeredClientName = clientName;
            // update query
            SqlCeConnection _dataConn = null;
            _dataConn = new SqlCeConnection("Data Source=FlightPlannerDB.sdf;Persist Security Info=False;");
            _dataConn.Open();
            SqlCeCommand updCmd = new SqlCeCommand();
            updCmd.Connection = _dataConn;
            StringBuilder updQuery = new StringBuilder();
            updQuery.Append("UPDATE ApplicationState set ");
            updQuery.Append("registeredClientName = '" + cruiseSpeed + "'");
            updCmd.CommandText = updQuery.ToString();
            updCmd.ExecuteNonQuery();
        }

        public void saveToDB()
        {
            // read the application state from db
            SqlCeConnection _dataConn = null;
            try
            {
                _dataConn = new SqlCeConnection("Data Source=FlightPlannerDB.sdf;Persist Security Info=False;");
                _dataConn.Open();
                SqlCeCommand selectCmd = new SqlCeCommand();
                selectCmd.Connection = _dataConn;
                StringBuilder selectQuery = new StringBuilder();
                selectQuery.Append("SELECT cruiseSpeed,cruiseFuelFlow,minFuel,deckHoldFuel,speed,unit,utcOffset,locationFormat FROM ApplicationState");
                selectCmd.CommandText = selectQuery.ToString();
                SqlCeResultSet results = selectCmd.ExecuteResultSet(ResultSetOptions.Scrollable);
                if (results.HasRows)
                {
                    // update query
                    SqlCeCommand updCmd = new SqlCeCommand();
                    updCmd.Connection = _dataConn;
                    StringBuilder updQuery = new StringBuilder();
                    updQuery.Append("UPDATE ApplicationState set ");
                    updQuery.Append("cruiseSpeed = "+cruiseSpeed +",");
                    updQuery.Append("cruiseFuelFlow = " + cruiseFuelFlow + ",");
                    updQuery.Append("minFuel = " + minFuel + ",");
                    updQuery.Append("deckHoldFuel = " + deckHoldFuel + ",");
                    updQuery.Append("speed = '" + speed + "',");
                    updQuery.Append("unit = '" + unit + "',");
                    updQuery.Append("utcOffset = '" + utcOffset + "'");
                    //updQuery.Append("locationFormat = '" + locationFormat + "'");                   
                    updCmd.CommandText = updQuery.ToString();
                    updCmd.ExecuteNonQuery();
                
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while saving application state to the database. Please try again!");
            }
            finally
            {
                _dataConn.Close();
            }
        }

        public double getDoubleUtcOffset()
        {
            // default value
            double defaultVal = 5.5;
            if (!string.IsNullOrEmpty(utcOffset))
            {
                string[] u_parts = utcOffset.Split(new char[] { '+', '-' });
                if (u_parts.Length < 2) return defaultVal;
                int multiplier = (utcOffset.ElementAt(0) == '-') ? -1 : 1;
                string[] offsetVal = u_parts[u_parts.Length - 1].Split(':');
                double retVal = multiplier * ((double.Parse(offsetVal[0])) + (double.Parse(offsetVal[1]) / (float)60));
                return retVal;
            }

            return defaultVal;
        }

        

        public void setCruiseSpeed(Int64 cruisespeed)
        {
            cruiseSpeed = cruisespeed;
        }

        public Int64 getCruiseSpeed()
        {
            return cruiseSpeed;
        }

        public void setcruiseFuelFlow(Int64 cruisefuelflow)
        {
            cruiseFuelFlow = cruisefuelflow;
        }

        public Int64 getcruiseFuelFlow()
        {
            return cruiseFuelFlow;
        }
        
        public void setMinFuel(Int64 minfuel)
        {
            minFuel = minfuel;
        }

        public Int64 getMinFuelValue()
        {
            return minFuel;
        }

        public void setAppandonDeckHoldFuel(Int64 appandondeckholdfuel)
        {
            deckHoldFuel = appandondeckholdfuel;
        }

        public Int64 getAppandonDeckHoldFuelValue()
        {
            return deckHoldFuel;
        }

                
        public void setSpeed(string sp)
        {
            speed = sp;
        }

        public string getSpeed()
        {
            return speed;
        }

        public void setUnit(string input_unit)
        {
            unit = input_unit;
        }

        public string getUnit()
        {
            return unit;
        }

        public void setUtcOffset(string input_utcOffset)
        {
              utcOffset= input_utcOffset;
        }

        public string getUtcOffset()
        {
            return utcOffset;
        }

        public void setLocationFormat(string input_location_format)
        {
            locationFormat = input_location_format;
        }

        public string getLocationFormat()
        {
            return locationFormat;
        }

        internal string getRegisteredClientName()
        {
            return registeredClientName;
        }
    }
}
