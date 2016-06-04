using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data.Sql;
using System.Data.SqlServerCe;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace DistanceCalCulator
{
    public class AirportDatabase
    {
        private static AirportDatabase instance;
        private SqlCeConnection _dataConn;
        Dictionary<string, List<Airport>> listofAirports;
        public List<string> identList;
        
        public static AirportDatabase Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AirportDatabase();
                }
                return instance;
            }
        }

        public void InitializeAirportData()
        {

            listofAirports = new Dictionary<string, List<Airport>>();
            identList = new List<string>();
            InitializeAirportdataFromDatabase();


        }

        private AirportDatabase() 
        {
            InitializeAirportData();
        }

        public Dictionary<string, List<Airport>> getAirportsDictionary()
        {
            return listofAirports;
        }

        public Airport getAirportObjectFromIdent(string ident)
        {
            if (listofAirports.ContainsKey(ident))
            {
                return listofAirports[ident][0];
            }
            else
                return null;
        }

        #region update record 
        public void updateRecordInDatabase(string ident,
                                         string type,
                                         string name,
                                         string lat,
                                         string longit,
                                         string elev,
                                         string frequency
                                        )
        {

            try
            {
                if (listofAirports.ContainsKey(ident))
                {
                    listofAirports[ident][0].ident = ident;
                    listofAirports[ident][0].type = type;
                    listofAirports[ident][0].name = name;
                    listofAirports[ident][0].latitude_deg = lat;
                    listofAirports[ident][0].longitude_deg = longit;
                    listofAirports[ident][0].elev_ft = elev;
                    listofAirports[ident][0].frequency_khz = frequency;
                }
                _dataConn = new SqlCeConnection("Data Source=FlightPlannerDB.sdf;Persist Security Info=False;");
                _dataConn.Open();

                // first delete the existing data
                SqlCeCommand updateCmd = new SqlCeCommand();
                updateCmd.Connection = _dataConn;
                StringBuilder updateQuery = new StringBuilder();
                updateQuery.Append("UPDATE Airports set ");
                updateQuery.Append("airport_type = '"+ type + "',");
                updateQuery.Append("airport_name = '"+ name + "',"); 
                updateQuery.Append("airport_latdeg = '"+ lat + "',airport_longdeg = '"+ longit+ "',");
                updateQuery.Append("airport_elev_ft = '" + elev + "',");
                updateQuery.Append("airport_freqkhz = '"+frequency + "' ");
                updateQuery.Append("where airport_ident = '" + ident + "'");
                updateCmd.CommandText = updateQuery.ToString();
                updateCmd.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (_dataConn != null)
                {
                    _dataConn.Close();
                }
                InitializeAirportData();
            }
            
        }
        #endregion

        #region Delete Records
        public void DeleteRecordInDatabase(List<string> identCodes
                                        )
        {

            try
            {
                foreach (string ident in identCodes)
                {
                    if (listofAirports.ContainsKey(ident))
                    {
                        listofAirports.Remove(ident);
                    }
                    _dataConn = new SqlCeConnection("Data Source=FlightPlannerDB.sdf;Persist Security Info=False;");
                    _dataConn.Open();

                    // first delete the existing data
                    SqlCeCommand deleteCmd = new SqlCeCommand();
                    deleteCmd.Connection = _dataConn;
                    StringBuilder deleteQuery = new StringBuilder();
                    deleteQuery.Append("DELETE FROM Airports ");
                    deleteQuery.Append("where airport_ident = '" + ident + "'");
                    deleteCmd.CommandText = deleteQuery.ToString();
                    deleteCmd.ExecuteNonQuery();
                    // delete the records also from the route_idents table
                    if(RouteAlreadyExists(ident, _dataConn)){
                        DeleteRoute(_dataConn, ident);
                    }
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                _dataConn.Close();
                InitializeAirportData();
            }

        }

        private void DeleteRoute(SqlCeConnection _dataConn, string ident)
        {
            SqlCeCommand deleteCmd = new SqlCeCommand();
            deleteCmd.Connection = _dataConn;
            StringBuilder deleteQuery = new StringBuilder();
            deleteQuery.Append("DELETE FROM RouteIdents ");
            deleteQuery.Append("where RouteId = '" + ident + "'");
            deleteCmd.CommandText = deleteQuery.ToString();
            deleteCmd.ExecuteNonQuery();
        }

        #endregion

        #region insert record
        public void addRecordToDatabase(string ident,
                                        string airport_type,
                                        string airport_name,
                                        double airport_lat,
                                        double airport_long,
                                        string elevation,
                                        string frequency)
        {
            try
            {
                _dataConn = new SqlCeConnection("Data Source=FlightPlannerDB.sdf;Persist Security Info=False;");
                _dataConn.Open();
                if (!CheckIfIdentExists(_dataConn, ident))
                {
                    // first delete the existing data
                    SqlCeCommand insertCmd = new SqlCeCommand();
                    insertCmd.Connection = _dataConn;
                    StringBuilder insertQuery = new StringBuilder();
                    insertQuery.Append("INSERT INTO Airports (airport_ident, airport_type, airport_name, airport_latdeg, airport_longdeg, airport_elev_ft,airport_freqkhz,airport_muncipality,airport_gps_code,");
                    insertQuery.Append("airport_iata_code, airport_magnetic_deg, associated_airport) VALUES (");
                    insertQuery.Append("'" + ident + "',");
                    insertQuery.Append("'" + airport_type + "',");
                    insertQuery.Append("'" + airport_name + "',");
                    insertQuery.Append("'" + airport_lat.ToString() + "',");
                    insertQuery.Append("'" + airport_long.ToString() + "',");
                    insertQuery.Append("'" + elevation + "',");
                    insertQuery.Append("'" + frequency + "',");
                    insertQuery.Append("'-','-','-','-','-')");
                    insertCmd.CommandText = insertQuery.ToString();
                    insertCmd.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("Airport " + ident + " already exists!");
                }

            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (_dataConn != null)
                {
                    _dataConn.Close();
                }
                // reinit all the maps
                InitializeAirportData();
            }
        }

        private bool CheckIfIdentExists(SqlCeConnection _dataConn, string ident)
        {
            SqlCeCommand checkIdentCmd = new SqlCeCommand();
            checkIdentCmd.Connection = _dataConn;
            StringBuilder checkIdentExistsQuery = new StringBuilder();
            checkIdentExistsQuery.Append("Select count(*) FROM Airports where airport_ident = '" + ident + "'");
            checkIdentCmd.CommandText = checkIdentExistsQuery.ToString();
            int identCount = (int)checkIdentCmd.ExecuteScalar();
            return identCount > 0;
        }

        public bool addIdentsToRoute(string routeIdent, IEnumerable<string> idents)
        {
            try
            {
                _dataConn = new SqlCeConnection("Data Source=FlightPlannerDB.sdf;Persist Security Info=False;");
                _dataConn.Open();
                // check if route exists in database
                if (RouteAlreadyExists(routeIdent,_dataConn))
                {
                    MessageBox.Show("Route '"+ routeIdent + "' already exists! Cannot add duplicate");
                    return false;
                }

                SqlCeCommand insertCmd = new SqlCeCommand();
                insertCmd.Connection = _dataConn;
                int seqNum = 0;
                foreach (string ident in idents)
                {
                    StringBuilder insertQuery = new StringBuilder();
                    insertQuery.Append("INSERT INTO RouteIdents (RouteId, AirportIdent, RouteSequenceNumber) VALUES ('" + routeIdent + "'," + "'" + ident + "'," + seqNum + ")");
                    insertCmd.CommandText = insertQuery.ToString();
                    insertCmd.ExecuteNonQuery();
                    ++seqNum;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error encountered!Cannot add route to database");
                return false;
            }
            finally
            {
                if (_dataConn != null)
                {
                    _dataConn.Close();
                }
                // reinit all the maps
         
            }
        }

        private bool RouteAlreadyExists(string routeIdent,SqlCeConnection conn)
        {
            SqlCeCommand existsCmd = new SqlCeCommand();
            existsCmd.Connection = _dataConn;
            existsCmd.CommandText = "Select count(*) from RouteIdents where RouteId = '" + routeIdent + "'";
            int count = (int)existsCmd.ExecuteScalar();
            return count > 0;
        }

        #endregion

        #region init from database
        public void InitializeAirportdataFromDatabase()
        {
            try
            {
                _dataConn = new SqlCeConnection("Data Source=FlightPlannerDB.sdf;Persist Security Info=False;");
                _dataConn.Open();

                // first delete the existing data
                SqlCeCommand selectCmd = new SqlCeCommand();
                selectCmd.Connection = _dataConn;
                StringBuilder selectQuery = new StringBuilder();
                selectQuery.Append("SELECT airport_id, airport_ident, airport_type, airport_name, airport_latdeg, airport_longdeg, airport_elev_ft, airport_muncipality, airport_freqkhz, airport_gps_code,");
                selectQuery.Append("airport_iata_code, airport_magnetic_deg, associated_airport FROM Airports");
                selectCmd.CommandText = selectQuery.ToString();
                SqlCeResultSet results = selectCmd.ExecuteResultSet(ResultSetOptions.Scrollable);
                if (results.HasRows)
                {
                    results.ReadFirst();
                    while (true)
                    {
                        Airport currAirport = new Airport();

                        currAirport.ID = results.GetSqlString(0).ToString();
                        currAirport.ident = results.GetSqlString(1).ToString();
                        currAirport.type = results.GetSqlString(2).ToString();
                        currAirport.name = results.GetSqlString(3).ToString();
                        currAirport.latitude_deg = results.GetSqlString(4).ToString();
                        currAirport.longitude_deg = results.GetSqlString(5).ToString();
                        currAirport.elev_ft = results.GetSqlString(6).ToString();


                        currAirport.municipality = results.GetSqlString(7).ToString();
                        currAirport.frequency_khz = results.GetSqlString(8).ToString();


                        currAirport.gps_code = results.GetSqlString(9).ToString();
                        currAirport.iata_code = results.GetSqlString(10).ToString();
                        currAirport.magnetic_variation_deg = results.GetSqlString(11).ToString();
                        currAirport.associated_airport = results.GetSqlString(12).ToString();


                        if (!listofAirports.ContainsKey(currAirport.ident))
                            listofAirports.Add(currAirport.ident, new List<Airport>());
                        listofAirports[currAirport.ident].Add(currAirport);

                        identList.Add(currAirport.ident);

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
                if (_dataConn != null)
                {
                    _dataConn.Close();
                }
            }
        }

        public List<string> getIdentsFromRoute(string routeIdent)
        {
            List<string> idents = new List<string>();
            try
            {
                _dataConn = new SqlCeConnection("Data Source=FlightPlannerDB.sdf;Persist Security Info=False;");
                _dataConn.Open();
                 // first delete the existing data
                SqlCeCommand selectCmd = new SqlCeCommand();
                selectCmd.Connection = _dataConn;
                StringBuilder selectQuery = new StringBuilder();
                selectQuery.Append(string.Format("SELECT AirportIdent FROM RouteIdents where RouteId = '{0}' order by RouteSequenceNumber asc",routeIdent));
                selectCmd.CommandText = selectQuery.ToString();
                SqlCeResultSet results = selectCmd.ExecuteResultSet(ResultSetOptions.Scrollable);
                if (results.HasRows)
                {
                    results.ReadFirst();
                    while (true)
                    {
                        idents.Add(results.GetSqlString(0).ToString());
                        if (!results.Read())
                            break;
                    }
                }

                return idents;
            }
            catch (Exception ex)
            {
                return idents;
            }
            finally
            {
                if (_dataConn != null)
                {
                    _dataConn.Close();
                }
                
            }
        }
        
        #endregion

         
    }
}
