using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using PersonId = System.Tuple<string,string>;
namespace DistanceCalCulator
{
    class PilotsDatabase
    {

        private static PilotsDatabase instance;
        private SqlCeConnection _dataConn;
        Dictionary<PersonId, List<Pilot>> listofPilots;
        public List<PersonId> nameList;
        
        public static PilotsDatabase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PilotsDatabase();
                }
                return instance;
            }
        }

        public void InitializePilotData()
        {

            listofPilots = new Dictionary<PersonId,List<Pilot>>();
            nameList = new List<PersonId>();
            InitializePilotsDataFromDatabase();
        }

        

        public Dictionary<Tuple<string,string>, List<Pilot>> getPliotDictionary()
        {
            return listofPilots;
        }

        public Pilot getPilotObjectFromName(PersonId pid)
        {
            if (listofPilots.ContainsKey(pid))
            {
                return listofPilots[pid][0];
            }
            else
                return null;
        }

        #region update record 
        public void updateRecordInDatabase(string fName,
                                           string lName,
                                           double Weight
                                          )
        {  
            
        }
        #endregion

        #region insert record
        public void addRecordToDatabase(string first_name,
                                        string last_name,
                                        double weight
                                       )
        {
            try
            {
                _dataConn = new SqlCeConnection("Data Source=FlightPlannerDB.sdf;Persist Security Info=False;");
                _dataConn.Open();

                // first delete the existing data
                SqlCeCommand insertCmd = new SqlCeCommand();
                insertCmd.Connection = _dataConn;
                StringBuilder insertQuery = new StringBuilder();
                insertQuery.Append("INSERT INTO [Pilot Data] ([First Name], [Last Name],[Weight]");
                insertQuery.Append(") VALUES ('"+first_name+"','"+last_name+"',"+weight+")");
                insertCmd.CommandText = insertQuery.ToString();
                insertCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                _dataConn.Close();
                // reinit all the maps
                InitializePilotData();
            }

        }
        #endregion

        #region init from database
        public void InitializePilotsDataFromDatabase()
        {
            try
            {
                _dataConn = new SqlCeConnection("Data Source=FlightPlannerDB.sdf;Persist Security Info=False;");
                _dataConn.Open();
                SqlCeCommand selectCmd = new SqlCeCommand();
                selectCmd.Connection = _dataConn;
                StringBuilder selectQuery = new StringBuilder();
                selectQuery.Append("SELECT [First Name],[Last Name],[Weight]");
                selectQuery.Append(" FROM Airports");
                selectCmd.CommandText = selectQuery.ToString();
                SqlCeResultSet results = selectCmd.ExecuteResultSet(ResultSetOptions.Scrollable);
                if (results.HasRows)
                {
                    results.ReadFirst();
                    while (true)
                    {
                        Pilot currPilot = new Pilot();
                        currPilot.FName = results.GetSqlString(0).ToString();
                        currPilot.LName = results.GetSqlString(1).ToString();
                        currPilot.Weight = results.GetDouble(2);
                        PersonId pid = new PersonId(currPilot.FName,currPilot.LName);
                        if (!listofPilots.ContainsKey(pid))
                            listofPilots.Add(pid, new List<Pilot>());
                        listofPilots[pid].Add(currPilot);
                        nameList.Add(pid);
                        if (!results.Read())
                            break;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                _dataConn.Close();
            }
        }
        #endregion

    }
}
