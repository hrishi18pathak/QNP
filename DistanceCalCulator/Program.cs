using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlServerCe;
using System.Data.SqlClient;
using System.Text;
using SoftwareLocker;

namespace DistanceCalCulator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!System.Diagnostics.Debugger.IsAttached)
            {
                TrialMaker t = new TrialMaker("NavPlanner", Application.StartupPath + "\\RegFile.reg",
                    Environment.GetFolderPath(Environment.SpecialFolder.System) + "\\TMSetp.dbf",
                    "Email: hrishi18pathak@gmail.com\n  captsapathak@gmail.com",
                   60,Int32.MaxValue, "786");

                byte[] MyOwnKey = { 97, 250, 1, 5, 84, 21, 7, 63,
            4, 54, 87, 56, 123, 10, 3, 62,
            7, 9, 20, 36, 37, 21, 101, 57};
                t.TripleDESKey = MyOwnKey;

                var RT = t.ShowDialog();
                bool is_trial;
                if (RT.Item1 != TrialMaker.RunTypes.Expired)
                {
                    if (RT.Item1 == TrialMaker.RunTypes.Full)
                        is_trial = false;
                    else
                        is_trial = true;

                    Application.Run(new Main_Form_New_Design2(is_trial, RT.Item2));

                }
            }
            else
            {
                Application.Run(new Main_Form_New_Design2(true, string.Empty));
            }
        }

        static void BulkCopy()
        {
            SqlCeConnection _conn = new SqlCeConnection("Data Source=AirportsDB.sdf;Persist Security Info=False;");
            _conn.Open();

            // first delete the existing data
            SqlCeCommand delcmd = new SqlCeCommand();
            delcmd.Connection = _conn;
            delcmd.CommandText = "delete from airports";
            int delrc = delcmd.ExecuteNonQuery();

            string line = string.Empty;
            Dictionary<string, bool> dictIdent = new Dictionary<string,bool>();
            System.IO.StreamReader file =
               new System.IO.StreamReader("data.txt");
            int index = 0;
            int affected_rc = 0;
            while ((line = file.ReadLine()) != null)
            {
                Airport currAirport = new Airport();
                string[] fields = line.Split('\t');
                if (fields.Count() < 13)
                    continue;

                currAirport.ID = fields[0];
                currAirport.ident = fields[1];
                currAirport.type = fields[2];
                currAirport.name = fields[3];
                currAirport.latitude_deg = fields[4];
                currAirport.longitude_deg = fields[5];
                currAirport.elev_ft = fields[6];


                currAirport.municipality = fields[7];
                currAirport.frequency_khz = fields[8];


                currAirport.gps_code = fields[9];
                currAirport.iata_code = fields[10];
                currAirport.magnetic_variation_deg = fields[11];

                

                currAirport.associated_airport = fields[12];
                if (index != 0 && !dictIdent.ContainsKey(currAirport.ident))
                {
                    SqlCeCommand cmd = new SqlCeCommand();
                    cmd.Connection = _conn;
                    StringBuilder queryBuilder = new StringBuilder("insert into airports values ('");
                    queryBuilder.Append(currAirport.ID + "','");
                    queryBuilder.Append(currAirport.ident + "','");
                    queryBuilder.Append(currAirport.type + "','");
                    queryBuilder.Append(currAirport.name + "','");
                    queryBuilder.Append(currAirport.latitude_deg + "','");
                    queryBuilder.Append(currAirport.longitude_deg + "','");
                    queryBuilder.Append(currAirport.elev_ft + "','");
                    queryBuilder.Append(currAirport.municipality + "','");
                    queryBuilder.Append(currAirport.frequency_khz + "','");
                    queryBuilder.Append(currAirport.gps_code + "','");
                    queryBuilder.Append(currAirport.iata_code + "','");
                    queryBuilder.Append(currAirport.magnetic_variation_deg + "','");
                    queryBuilder.Append(currAirport.associated_airport + "')");

                    cmd.CommandText = queryBuilder.ToString();
                    affected_rc += cmd.ExecuteNonQuery();
                    dictIdent.Add(currAirport.ident, true);
                }
                ++index;
            }
            MessageBox.Show("Bulk copy is done. No of rows inserted ="+affected_rc.ToString());
        }
    }
}
