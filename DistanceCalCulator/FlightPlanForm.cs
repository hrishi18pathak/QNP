 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using EARTHLib;
using GEPlugin;


namespace DistanceCalCulator
{
    public struct PlaceMark
    {
        public string Location;
        public double latitude;
        public double longitude;

    }

    [ComVisibleAttribute(true)]
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]

    public partial class FlightPlanForm : Form
    {

        static Dictionary<string, List<Airport>> listofAirports;
        static double ALT = 0;
        static double IAS = 0;
        static double TAS = 0;
        static int wINDDIR = 0;
        static int wINDSPEED = 0;
        static double DIST = 0.0;
        static int BEARING = 0;
        static long FUELFLOW = 0;
        static double time1 = 0;
        static string time2;
        double groundspeed;
        double heading = 0.0;
        double DistTotal = 0.0;
        double TimeTotal = 0.0;
        double StartFuel = 0.0;
        double FuelTotal = 0.0;
        Int32 MaxRows { get; set; }
        private AutoCompleteStringCollection sc = null; // Column 0

        private const string PLUGIN_URL =
           @"http://earth-api-samples.googlecode.com/svn/trunk/demos/desktop-embedded/pluginhost.html";

        private IGEPlugin _ge = null;

        private void RefreshAutoCompleteCollection()
        {
            listofAirports = AirportDatabase.Instance.getAirportsDictionary();
            sc = new AutoCompleteStringCollection();
            // now just loop through the dictionary returned by the airportsdatabase object
            // and populate the autocomplete collection
            foreach (string ident_entry in listofAirports.Keys)
            {
                sc.Add(ident_entry);
            }
        }

        public void InitializeAirportData()
        {
            RefreshAutoCompleteCollection();
        }

        private void ShowError(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ShowWarning(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void InitGridStyles()
        {
            dataGridView1.Columns["clmnFrom"].DefaultCellStyle.Tag = "TOUPPER";
            dataGridView1.Columns["clmnTo"].DefaultCellStyle.Tag = "TOUPPER";
            dataGridView2.Columns["clmn_dgrv2_From"].DefaultCellStyle.Tag = "TOUPPER";
            dataGridView2.Columns[0].ReadOnly = true;
            dataGridView2.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(192, 192, 255);
            dataGridView2.Columns[2].ReadOnly = true;
            dataGridView2.Columns[2].DefaultCellStyle.BackColor = Color.FromArgb(192, 192, 255);
            dataGridView2.Columns[3].ReadOnly = true;
            dataGridView2.Columns[3].DefaultCellStyle.BackColor = Color.FromArgb(192, 192, 255);
            dataGridView2.Columns[4].ReadOnly = true;
            dataGridView2.Columns[4].DefaultCellStyle.BackColor = Color.FromArgb(192, 192, 255);
            dataGridView2.Columns[5].ReadOnly = true;
            dataGridView2.Columns[5].DefaultCellStyle.BackColor = Color.FromArgb(192, 192, 255);
        }

        // constructor
        public FlightPlanForm()
        {
            MaxRows = 14;
            InitializeComponent();
            InitializeAirportData();
            InitGridStyles();
            dataGridView1.Rows.Add(1);
        }

        private bool GetLatLongFromIdentString(string dest, out double lat, out double longit)
        {
            lat = 0.0;
            longit = 0.0;
            // loop through the collection of airports for a given ident string
            if (listofAirports.ContainsKey(dest))
            {
                List<Airport> airports = listofAirports[dest];
                if (airports.Count == 1)
                {
                    lat = Utils.StrToDouble(listofAirports[dest][0].latitude_deg);
                    longit = Utils.StrToDouble(listofAirports[dest][0].longitude_deg);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                // this means that the airport data for the given dest did not exist
                return false;
            }
            return true;
        }


        void refreshAllTotals()
        {
            UpdateTotal();
            txtTotalDist.Text = Math.Round(DistTotal).ToString();
            txtTotalTime.Text = Utils.displayTIME(TimeTotal);
            txtTotalFuel.Text = Math.Round(FuelTotal).ToString();
            total_Flt_Fuel_Calculation();
        }

        private void total_Flt_Fuel_Calculation()
        {
            // Fuel flow considered for Reserve Fuel calculations is taken from 'Unit Setting'...
            double fuelReserveValue = Convert.ToDouble(numericUpDn_Reserve.Value);
            double cruiseFuelFlowPerMinute = fuelReserveValue * ApplicationState.Instance.getcruiseFuelFlow() / 60.0;
            txtBox_fltFuel.Text = Math.Round(FuelTotal).ToString();
            txtBox_Reserve.Text = Math.Round(cruiseFuelFlowPerMinute).ToString();
            txtBox_Contingency.Text = Math.Round(FuelTotal * 0.1).ToString(); //10% contingency fuel
            txtBox_Tot_FltFuel.Text = Convert.ToString(numericUpDn_StartTaxiHold.Value + Convert.ToInt16(txtBox_fltFuel.Text) + Convert.ToInt16(txtBox_Reserve.Text) + Convert.ToInt16(txtBox_Contingency.Text) + numericUpDn_AddReserve.Value);
        }

        private bool CalculateDistance(string fromDest, string toDest, out double dist, out int bearing, out string errorMessage)
        {
            dist = 0.0;
            bearing = 0;
            double lat1, long1;
            double lat2, long2;
            if (GetLatLongFromIdentString(fromDest, out lat1, out long1)
                && GetLatLongFromIdentString(toDest, out lat2, out long2))
            {
                errorMessage = "";
                dist = Utils.distance(lat1, long1, lat2, long2, 'N');
                // in degrees
                bearing = Utils.Bearing(lat1, long1, lat2, long2);

                return true;
            }
            else
            {
                errorMessage = "Input airfield is not in database!!!";
                return false;
            }
        }

        private void addAndUpdateRowDataGridView2(string from_dest, string to_dest, int rowIndex)
        {
            int dgv2Row1Index = rowIndex;
            int dgv2Row2Index = rowIndex + 1;
            // if true we want to update the exisiting rows in the grid2
            if (dgv2Row1Index < dataGridView2.Rows.Count && dgv2Row2Index < dataGridView2.Rows.Count)
            {
                dataGridView2.Rows[dgv2Row1Index].Cells[0].Value = from_dest;
                dataGridView2.Rows[dgv2Row2Index].Cells[0].Value = to_dest;
            }
            else
            {
                if (rowIndex == 0)
                {
                    DataGridViewRow newRow1 = new DataGridViewRow();
                    DataGridViewCell cell1 = new DataGridViewTextBoxCell();
                    cell1.Value = from_dest;
                    //readonly cell
                    cell1.Style.BackColor = Color.FromArgb(192, 192, 255);
                    newRow1.Cells.Add(cell1);
                    dataGridView2.Rows.Add(newRow1);
                    SetColumnsOfGridView2Row(rowIndex);
                }
                DataGridViewRow newRow2 = new DataGridViewRow();
                DataGridViewCell cell2 = new DataGridViewTextBoxCell();
                cell2.Value = to_dest;
                // readonly cell
                cell2.Style.BackColor = Color.FromArgb(192, 192, 255);
                newRow2.Cells.Add(cell2);
                dataGridView2.Rows.Add(newRow2);
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if ((e.RowIndex == -1))
            {
                return;
            }
            else
            {
                string toDest = string.Empty;
                string fromDest = string.Empty;
                if (e.RowIndex != -1)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells[1].Value != null)
                    {
                        toDest = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString().ToUpper();
                    }
                    if (dataGridView1.Rows[e.RowIndex].Cells[0].Value != null)
                    {
                        fromDest = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString().ToUpper();
                    }
                }
                switch (e.ColumnIndex)
                {
                    case 0:
                        if (fromDest.StartsWith("route_", StringComparison.CurrentCultureIgnoreCase))
                        {
                            AddRouteToGrid(fromDest, e.RowIndex, e.ColumnIndex);
                            return;
                        }
                        addAndUpdateRowDataGridView2(fromDest, toDest, e.RowIndex);
                        break;
                    case 1:
                        if (toDest.StartsWith("route_", StringComparison.CurrentCultureIgnoreCase))
                        {
                            AddRouteToGrid(toDest, e.RowIndex, e.ColumnIndex);
                            return;
                        }

                        UpdateEntireRow(e.RowIndex);
                        if (e.RowIndex == dataGridView1.Rows.Count - 1) // new row
                        {
                            if (dataGridView1.Rows.Count < MaxRows)
                            {
                                dataGridView1.Rows.Add(1);
                                // SET the cell to be readonly
                                dataGridView1.Rows[e.RowIndex + 1].Cells["clmnFrom"].ReadOnly = true;
                                dataGridView1.Rows[e.RowIndex + 1].Cells["clmnLanded"].Value = false;
                                // set the background color as well
                                dataGridView1.Rows[e.RowIndex + 1].Cells["clmnFrom"].Style.BackColor = Color.FromArgb(192, 192, 255);
                                dataGridView1.Rows[e.RowIndex + 1].Cells["clmnFrom"].Value = dataGridView1.Rows[e.RowIndex].Cells[1].Value;
                            }
                        }
                        else
                        {
                            dataGridView1.Rows[e.RowIndex + 1].Cells["clmnFrom"].Value = dataGridView1.Rows[e.RowIndex].Cells[1].Value;
                            if (e.RowIndex != 0)
                            {
                                UpdateEntireRow(e.RowIndex + 1);
                            }
                        }
                        addAndUpdateRowDataGridView2(fromDest, toDest, e.RowIndex);
                        break;
                    case 2:
                    case 18:
                        Fuel();
                        break;
                    case 4:
                    case 5:
                    case 7:
                    case 8:
                    case 13:
                    case 17:
                        UpdateEntireRow(e.RowIndex);
                        break;
                }
            }

        }

        private void AddRouteToGrid(string dest, int rowIndex, int columnIndex)
        {
            //get Route idents from database
            List<string> routeIdents = AirportDatabase.Instance.getIdentsFromRoute(dest);
            if (routeIdents.Count == 0)
            {
                MessageBox.Show("No waypoints present in route " + dest);
                return;
            }

            if (rowIndex == 0 && columnIndex == 0)
            {
                dataGridView1.Rows[rowIndex].Cells[columnIndex].Value = routeIdents[0];
                routeIdents.RemoveAt(0);
            }

            foreach (string ident in routeIdents)
            {
                dataGridView1.Rows[rowIndex].Cells[1].Value = ident;
                ++rowIndex;
            }

        }

        private void UpdateEntireRow(int currRowIndex)
        {
            if (dataGridView1.Rows[currRowIndex].Cells["clmnTo"].Value != null)
            {
                if (dataGridView1.Rows[currRowIndex].Cells["clmnIAS"].Value != null
                    && !string.IsNullOrEmpty(dataGridView1.Rows[currRowIndex].Cells["clmnIAS"].Value.ToString()))
                {
                    IAS = Convert.ToInt16(dataGridView1.Rows[currRowIndex].Cells["clmnIAS"].Value);
                }
                else
                {
                    //Initialise "IAS" default values from Unit Settings
                    dataGridView1.Rows[currRowIndex].Cells["clmnIAS"].Value = ApplicationState.Instance.getCruiseSpeed();
                    IAS = Convert.ToInt16(dataGridView1.Rows[currRowIndex].Cells["clmnIAS"].Value);
                }

                if (dataGridView1.Rows[currRowIndex].Cells["clmnAlt"].Value != null
                   && !string.IsNullOrEmpty(dataGridView1.Rows[currRowIndex].Cells["clmnAlt"].Value.ToString()))
                {
                    ALT = Convert.ToInt32(dataGridView1.Rows[currRowIndex].Cells["clmnAlt"].Value);
                    TAS = Utils.IAStoTAS(IAS, ALT);
                    dataGridView1.Rows[currRowIndex].Cells["clmnTAS"].Value = TAS;
                }
                else
                {
                    ALT = 0;
                    dataGridView1.Rows[currRowIndex].Cells["clmnTAS"].Value = IAS;
                    TAS = IAS;
                }

                if (dataGridView1.Rows[currRowIndex].Cells["wnddir"].Value != null
                    && !string.IsNullOrEmpty(dataGridView1.Rows[currRowIndex].Cells["wnddir"].Value.ToString()))
                {
                    wINDDIR = Convert.ToInt16(dataGridView1.Rows[currRowIndex].Cells["wnddir"].Value);
                }
                else
                {
                    wINDDIR = 0;
                }

                if (dataGridView1.Rows[currRowIndex].Cells["wndsp"].Value != null
                    && !string.IsNullOrEmpty(dataGridView1.Rows[currRowIndex].Cells["wndsp"].Value.ToString()))
                {
                    wINDSPEED = Convert.ToInt16(dataGridView1.Rows[currRowIndex].Cells["wndsp"].Value);
                }
                else
                {
                    wINDSPEED = 0;
                }
                // Now refresh the bearing
                string toDest = dataGridView1.Rows[currRowIndex].Cells["clmnTo"].Value.ToString().ToUpper();
                string fromDest = dataGridView1.Rows[currRowIndex].Cells["clmnFrom"].Value.ToString().ToUpper();
                string errorMessage;
                if (!CalculateDistance(fromDest, toDest, out DIST, out BEARING, out errorMessage))
                {
                    MessageBox.Show(errorMessage);
                    DIST = 0.0;
                    BEARING = 0;
                }

                dataGridView1.Rows[currRowIndex].Cells["clmnDist"].Value = DIST;
                dataGridView1.Rows[currRowIndex].Cells["clmnTrack"].Value = BEARING;
                heading = Utils.TRACKtoHEADING(wINDDIR, wINDSPEED, TAS, BEARING);
                if (!double.IsNaN(heading))
                {
                    dataGridView1.Rows[currRowIndex].Cells["clmnHeading"].Value = heading;
                }
                groundspeed = Utils.TAStoGroundspeed(TAS, BEARING, heading, wINDSPEED, wINDDIR);
                if (!double.IsNaN(heading))
                {
                    dataGridView1.Rows[currRowIndex].Cells["GndSpeed"].Value = groundspeed;
                }

                if (dataGridView1.Rows[currRowIndex].Cells["clmnFuelFlow"].Value != null &&
                    !string.IsNullOrEmpty(dataGridView1.Rows[currRowIndex].Cells["clmnFuelFlow"].Value.ToString()))
                {
                    FUELFLOW = Convert.ToInt32(dataGridView1.Rows[currRowIndex].Cells["clmnFuelFlow"].Value);
                }
                else
                {
                    dataGridView1.Rows[currRowIndex].Cells["clmnFuelFlow"].Value = ApplicationState.Instance.getcruiseFuelFlow();
                    FUELFLOW = Convert.ToInt32(dataGridView1.Rows[currRowIndex].Cells["clmnFuelFlow"].Value);
                }
                time1 = Utils.calculateTIME(DIST, groundspeed);
                time2 = Utils.displayTIME(time1);
                if (!double.IsNaN(time1))
                {
                    dataGridView1.Rows[currRowIndex].Cells["Time"].Value = time2;
                    dataGridView1.Rows[currRowIndex].Cells["clmnActualTime"].Value = time1;
                    dataGridView1.Rows[currRowIndex].Cells["FuelReq"].Value = (FUELFLOW / (double)60) * time1;

                }
                // also update the fuel required if fuel flow is entered
                Fuel();
                refreshAllTotals();
            }
            else
            {
                ShowError("Please set the \"TO\" destination first!", "Error");
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                var tec = (e.Control as DataGridViewTextBoxEditingControl);
                if (tec != null)
                {
                    RefreshAutoCompleteCollection();
                    tec.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    tec.AutoCompleteSource = AutoCompleteSource.CustomSource;

                    switch (dataGridView1.CurrentCell.ColumnIndex)
                    {
                        case 0:
                            tec.AutoCompleteCustomSource = sc;
                            break;
                        case 1:
                            tec.AutoCompleteCustomSource = sc;
                            break;
                    }
                }

            }
            // This will convert all keyinput to UPPER for "dgrv1 FROM and TO" clmn
            if ((e.Control is TextBox) && (e.CellStyle.Tag != null) && (e.CellStyle.Tag.ToString() == "TOUPPER"))
                ((TextBox)e.Control).CharacterCasing = CharacterCasing.Upper;
            else
                ((TextBox)e.Control).CharacterCasing = CharacterCasing.Normal;

        }

        private void SetColumnsOfGridView2Row(int rowIndex)
        {
            string locIdent = dataGridView2.Rows[rowIndex].Cells[0].Value.ToString().ToUpper();
            // now get the lat and long
            double latLoc = 0.0, longLoc = 0.0;
            if (GetLatLongFromIdentString(locIdent, out latLoc, out longLoc))
            {
                dataGridView2.Rows[rowIndex].Cells["clmn_Lat_DD_MM_SS"].Value = Utils.DDtoDMS(latLoc, DataForm.CoordinateType.latitude);
                dataGridView2.Rows[rowIndex].Cells["clmn_Long_DD_MM_SS"].Value = Utils.DDtoDMS(longLoc, DataForm.CoordinateType.longitude);

                //store the values of lat long on degdecimal format for further use(Google Earth Plotting) in hidden columns
                dataGridView2.Rows[rowIndex].Cells["clmn_Lat_DecDeg"].Value = latLoc;
                dataGridView2.Rows[rowIndex].Cells["clmn_Long_DecDeg"].Value = longLoc;

                // store the values of lat long on degdecimal format for further use(date change event) in hidden columns
                dataGridView2.Rows[rowIndex].Cells["clmnSunrise_DDMM"].Value = Convert.ToInt16(latLoc);
                dataGridView2.Rows[rowIndex].Cells["clmnSunset_DDMM"].Value = Convert.ToInt16(longLoc);


                // also set the sunrise and sunset
                string sunrisestr, sunsetstr;
                Utils.getSunRiseSunset(dateTimePicker1.Value, latLoc, longLoc, out sunrisestr, out sunsetstr);
                dataGridView2.Rows[rowIndex].Cells["clmnSunrise"].Value = sunrisestr.Substring(0, 5);
                dataGridView2.Rows[rowIndex].Cells["clmnSunset"].Value = sunsetstr.Substring(0, 5);
            }
        }

        private void dataGridView2_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
                return;

            switch (e.ColumnIndex)
            {

                case 0:
                    SetColumnsOfGridView2Row(e.RowIndex);
                    break;
                case 1:

                    if (dataGridView2.Rows[e.RowIndex].Cells["clmn_dgrv2_From"].Value == null
                        || dataGridView2.Rows[e.RowIndex].Cells["clmn_dgrv2_Location"].Value == null)
                        return;
                    string errorMessage;
                    int track = 0;
                    double distance = 0.0;
                    string fromIdent = dataGridView2.Rows[e.RowIndex].Cells["clmn_dgrv2_Location"].Value.ToString().ToUpper();
                    string locIdent = dataGridView2.Rows[e.RowIndex].Cells["clmn_dgrv2_From"].Value.ToString().ToUpper();

                    if (!CalculateDistance(locIdent, fromIdent, out distance, out track, out errorMessage))
                        MessageBox.Show(errorMessage);
                    else
                    // Displays the calculated value in grid form.
                    {
                        dataGridView2.Rows[e.RowIndex].Cells["clmn_dgrv2_Track"].Value = track;
                        dataGridView2.Rows[e.RowIndex].Cells["clmn_dgrv2_Dist"].Value = distance;
                    }
                    break;
            }
        }

        private void dataGridView2_EditingControlShowing_1(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                var tec = (e.Control as DataGridViewTextBoxEditingControl);
                tec.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                tec.AutoCompleteSource = AutoCompleteSource.CustomSource;
                RefreshAutoCompleteCollection();
                switch (dataGridView2.CurrentCell.ColumnIndex)
                {
                    case 1:
                        tec.AutoCompleteCustomSource = sc;
                        break;
                }
            }

            // This will convert all keyinput to UPPER for " clmn_dgrv2_From" clmn
            if ((e.Control is TextBox) && (e.CellStyle.Tag != null) && (e.CellStyle.Tag.ToString() == "TOUPPER"))
                ((TextBox)e.Control).CharacterCasing = CharacterCasing.Upper;
            else
                ((TextBox)e.Control).CharacterCasing = CharacterCasing.Normal;
        }


        private void UpdateTotal()
        {
            //tHIS FUNCTION WILL ADD ALL VALUES IN DIST , TIME AND FUEL COLUMN
            int counter = 0;
            DistTotal = 0.0;
            TimeTotal = 0.0;
            FuelTotal = 0.0;
            for (counter = 0; counter < (dataGridView1.Rows.Count);
                counter++)
            {
                if (dataGridView1.Rows[counter].Cells["clmnTo"].Value != null
                    && !string.IsNullOrEmpty(dataGridView1.Rows[counter].Cells["clmnTo"].Value.ToString()))
                {
                    if (dataGridView1.Rows[counter].Cells["clmnDIST"].Value != null &&
                        !string.IsNullOrEmpty(dataGridView1.Rows[counter].Cells["clmnDIST"].Value.ToString()))
                        DistTotal += double.Parse(dataGridView1.Rows[counter].Cells["clmnDIST"].Value.ToString());

                    if (dataGridView1.Rows[counter].Cells["clmnActualTime"].Value != null &&
                        !string.IsNullOrEmpty(dataGridView1.Rows[counter].Cells["clmnActualTime"].Value.ToString()))
                        TimeTotal += double.Parse(dataGridView1.Rows[counter].Cells["clmnActualTime"].Value.ToString());

                    if (dataGridView1.Rows[counter].Cells["fuelReq"].Value != null &&
                        !string.IsNullOrEmpty(dataGridView1.Rows[counter].Cells["fuelReq"].Value.ToString()))
                        FuelTotal += double.Parse(dataGridView1.Rows[counter].Cells["fuelReq"].Value.ToString());
                }
            }

        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

            switch (e.ColumnIndex)
            {
                case 0:
                case 1:
                    e.Cancel = !string.IsNullOrEmpty(e.FormattedValue.ToString()) &&
                               !listofAirports.ContainsKey(e.FormattedValue.ToString());
                    if (e.Cancel)
                        ShowWarning("Input airfield is not in database", "Error");
                    break;
                case 4:
                case 5:
                case 7:
                case 8:
                case 13:
                case 17:
                    {
                        DataGridViewTextBoxCell cell = dataGridView1[e.ColumnIndex, e.RowIndex] as DataGridViewTextBoxCell;

                        if (cell != null)
                        {
                            char[] chars = e.FormattedValue.ToString().ToCharArray();
                            foreach (char c in chars)
                            {
                                if (char.IsDigit(c) == false)
                                {
                                    MessageBox.Show("You have to enter digits only");

                                    e.Cancel = true;
                                    break;
                                }
                            }
                        }


                    }
                    break;

            }
        }

        private void startFuelBox_ValueChanged(object sender, EventArgs e)
        {
            //THIS FUNCTION WILL only UPDATE ALL "FUEL LEFT" VALUES WHEN USER CHANGES "START FUEL"
            Fuel();
        }

        private void Fuel()
        {
            //THIS FUNCTION WILL UPDATE ALL "FUEL LEFT" VALUES FOR ANY CHANGES MADE IN "FUEL FLOW,REFUEL" COLUMNS.
            // THIS WILL BE CALLED BY EVERY ROW UPDATE EVENT AND START FUEL CHANGE EVENT.
            StartFuel = Convert.ToInt16(numericUpDn_startFuel.Value);
            int Refuel = 0;
            int counter = 0;

            for (counter = 0; counter < (dataGridView1.Rows.Count);
                counter++)
            {
                if (dataGridView1.Rows[counter].Cells["clmnTo"].Value != null
                   && !string.IsNullOrEmpty(dataGridView1.Rows[counter].Cells["clmnTo"].Value.ToString()))
                {

                    if (counter == 0)
                    {
                        dataGridView1.Rows[counter].Cells["FuelLeft"].Value = StartFuel - Convert.ToInt16(dataGridView1.Rows[counter].Cells["FuelReq"].Value);
                        cellColourChangeToRed(counter);
                    }
                    else
                    {
                        int refuelVal = 0;
                        if (dataGridView1.Rows[counter - 1].Cells["Refuel"].Value != null)
                        {
                            Int32.TryParse(dataGridView1.Rows[counter - 1].Cells["Refuel"].Value.ToString(), out refuelVal);
                        }

                        Refuel = refuelVal;
                        if (Refuel == 0)
                        {
                            dataGridView1.Rows[counter].Cells["FuelLeft"].Value = Convert.ToInt16(dataGridView1.Rows[counter - 1].Cells["FuelLeft"].Value) - Convert.ToInt16(dataGridView1.Rows[counter].Cells["FuelReq"].Value);
                            cellColourChangeToRed(counter);
                        }
                        else
                        {
                            dataGridView1.Rows[counter].Cells["FuelLeft"].Value = Refuel - Convert.ToInt16(dataGridView1.Rows[counter].Cells["FuelReq"].Value);
                            cellColourChangeToRed(counter);
                        }

                    }

                    // adjust fuel left based on whether we are landing at this location or not
                    double fuelLeft = double.Parse(dataGridView1.Rows[counter].Cells["FuelLeft"].Value.ToString());
                    if (dataGridView1.Rows[counter].Cells[2].Value != null && dataGridView1.Rows[counter].Cells[2].Value.ToString() == "true")
                    {
                        dataGridView1.Rows[counter].Cells["FuelLeft"].Value = fuelLeft - ApplicationState.Instance.getAppandonDeckHoldFuelValue();
                    }
                }
            }
        }


        private void total_Flt_Fuel()
        {
            txtBox_Reserve.Text = Convert.ToString(numericUpDn_Reserve.Value * (ApplicationState.Instance.getcruiseFuelFlow() / 60));
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            int counter = 0;
            double latLoc = 0.0, longLoc = 0.0;
            for (counter = 0; counter < (dataGridView2.Rows.Count);
                counter++)
            {
                if (dataGridView2.Rows[counter].Cells["clmn_dgrv2_Location"].Value != null
                   && !string.IsNullOrEmpty(dataGridView2.Rows[counter].Cells["clmn_dgrv2_Location"].Value.ToString()))
                {
                    string sunrisestr, sunsetstr;
                    latLoc = Convert.ToInt16(dataGridView2.Rows[counter].Cells["clmnSunrise_DDMM"].Value);
                    longLoc = Convert.ToInt16(dataGridView2.Rows[counter].Cells["clmnSunset_DDMM"].Value);
                    Utils.getSunRiseSunset(dateTimePicker1.Value, latLoc, longLoc, out sunrisestr, out sunsetstr);
                    dataGridView2.Rows[counter].Cells["clmnSunrise"].Value = sunrisestr.Substring(0, 5);
                    dataGridView2.Rows[counter].Cells["clmnSunset"].Value = sunsetstr.Substring(0, 5);
                }
            }
        }

        private void cellColourChangeToRed(int counter)
        {
            // THIS FUNCTION CHANGES "FUEL LEFT" CELL COLOUR TO RED BASED ON CURRENT VALUE OF "FUEL REQ" CELL AND "MIN FUEL" SETTINGS.
            if (Convert.ToInt32(dataGridView1.Rows[counter].Cells["FuelLeft"].Value.ToString()) < ApplicationState.Instance.getMinFuelValue())
                dataGridView1.Rows[counter].Cells["FuelLeft"].Style.BackColor = Color.FromArgb(255, 0, 0);
            else
                dataGridView1.Rows[counter].Cells["FuelLeft"].Style.BackColor = Color.FromArgb(255, 255, 255);

        }

        private string getCellFormatedValue(DataGridViewCell currCell)
        {
            if (currCell.Value == null)
                return string.Empty;
            else
                return currCell.EditedFormattedValue.ToString();


        }

        private void drawGridOnPdfdocument(DataGridView displayGrid, XGraphics gfx, int displayXOffset, int displayYOffset)
        {

            XColor back = XColors.Transparent;
            back.A = 0.1;
            XSolidBrush BorderBrush = new XSolidBrush(back);
            XPen pen = new XPen(XColors.Gray, 1);
            //Side Panels
            double height = displayGrid.Height + 5;
            double width = displayGrid.Width + 7;
            gfx.DrawRoundedRectangle(pen, BorderBrush, displayGrid.Location.X - 8, displayGrid.Location.Y - 20 + displayYOffset, width, height, 10, 10);
            gfx.DrawRoundedRectangle(pen, BorderBrush, displayGrid.Location.X - 5, displayGrid.Location.Y + 365, 1058, 28, 5, 5);
            XFont headerfont = new XFont("Aerial", 13, XFontStyle.Bold);
            XTextFormatter tf = new XTextFormatter(gfx);
            XPen newPen = new XPen(Color.Black);
            XColor fillColor = XColors.DarkOrange;
            XBrush brush = new XSolidBrush(fillColor);
            fillColor.A = 0.8;

            int headerOffset = 0;
            foreach (DataGridViewColumn colmn in displayGrid.Columns)
            {
                if (colmn.Visible)
                {
                    Rectangle header_rect = displayGrid.GetCellDisplayRectangle(colmn.Index, -1, false);
                    XRect pdf_rect = new XRect(header_rect.X + displayXOffset, header_rect.Y + displayYOffset, header_rect.Width, header_rect.Height);
                    headerOffset = header_rect.Height;
                    gfx.DrawRectangle(newPen, pdf_rect);
                    tf.Alignment = XParagraphAlignment.Center;
                    tf.DrawString(colmn.HeaderText, headerfont, XBrushes.Black, pdf_rect, XStringFormats.TopLeft);
                }

            }

            XFont font = new XFont("Aerial", 13, XFontStyle.Bold);
            foreach (DataGridViewRow dgRow in displayGrid.Rows)
                foreach (DataGridViewColumn dgCol in displayGrid.Columns)
                {
                    if (dgCol.Visible)
                    {
                        Rectangle rect = displayGrid.GetCellDisplayRectangle(dgCol.Index, dgRow.Index, true);
                        XRect pdf_rect = new XRect(rect.X + displayXOffset, rect.Y + displayYOffset, rect.Width, rect.Height);
                        gfx.DrawRectangle(newPen, pdf_rect);
                        tf.Alignment = XParagraphAlignment.Center;
                        string displayText = getCellFormatedValue(displayGrid.Rows[dgRow.Index].Cells[dgCol.Index]);
                        tf.DrawString(displayText, headerfont, XBrushes.Black, pdf_rect, XStringFormats.TopLeft);
                    }

                }
        }


        private void DrawThisControlOnPdfDocument(Control control, XGraphics gfx, int displayXOffset, int displayYOffset)
        {
            XTextFormatter tf = new XTextFormatter(gfx);
            XPen newPen = new XPen(Color.Black);
            XRect pdf_rect = new XRect(control.Location.X + displayXOffset, control.Location.Y + displayYOffset, control.Width, control.Height);
            gfx.DrawRectangle(newPen, pdf_rect);
            tf.Alignment = XParagraphAlignment.Center;
            string displayText = control.Text;
            XFont font = new XFont("Aerial", 11, XFontStyle.Bold);
            tf.DrawString(displayText, font, XBrushes.Black, pdf_rect, XStringFormats.TopLeft);
        }

        private void DrawThisControlOnPdfDocument2(Control control, XGraphics gfx, int displayXOffset, int displayYOffset)
        {
            XTextFormatter tf = new XTextFormatter(gfx);
            XPen newPen = new XPen(Color.Black);
            XRect pdf_rect = new XRect(control.Location.X + displayXOffset, control.Location.Y + displayYOffset, control.Width, control.Height);
            gfx.DrawRectangle(newPen, pdf_rect);
            tf.Alignment = XParagraphAlignment.Left;
            string displayText = control.Text;
            XFont font = new XFont("Aerial", 11, XFontStyle.Bold);
            tf.DrawString(displayText, font, XBrushes.Black, pdf_rect, XStringFormats.TopLeft);
        }

        private void DrawRemainingControls(XGraphics gfx)
        {
            int displayYOffset = 50;
            int displayXOffset = 50;
            foreach (Control control in this.Controls)
            {
                if (control.Name != "dataGridView1" && control.Name != "dataGridView2")
                {
                    DrawThisControlOnPdfDocument(control, gfx, displayXOffset, displayYOffset);
                    displayYOffset += control.Location.Y + control.Height + 20;
                }
            }
        }

        private void createPdfDocument()
        {


            PdfDocument document = new PdfDocument();
            int displayYOffset = 50;
            int displayXOffset = 5;
            // int height = 5;
            // int width = 5;

            PdfPage page = document.AddPage();
            page.Width = this.Width;
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Aerial", 13, XFontStyle.Bold);
            XColor back = XColors.Transparent;
            back.A = 0.3;
            XSolidBrush BorderBrush = new XSolidBrush(back);
            XPen pen = new XPen(XColors.Gray, 1);

            drawGridOnPdfdocument(dataGridView1, gfx, displayXOffset + 5, displayYOffset + 2);

            displayYOffset = this.dataGridView1.Location.Y + this.dataGridView1.Height + 57;

            gfx.DrawString("Total Dist:", font, XBrushes.Black, lblTotalDist.Location.X + displayXOffset, displayYOffset);
            gfx.DrawString(txtTotalDist.Text, font, XBrushes.Black, txtTotalDist.Location.X + displayXOffset, displayYOffset);


            gfx.DrawString("Total Time (HR:MIN)", font, XBrushes.Black, displayXOffset + lblSpeed.Location.X, displayYOffset);
            gfx.DrawString(this.txtTotalTime.Text, font, XBrushes.Black, displayXOffset + txtTotalTime.Location.X, displayYOffset);

            gfx.DrawString("Total Fuel:", font, XBrushes.Black, displayXOffset + label14.Location.X + displayXOffset, displayYOffset);
            gfx.DrawString(this.txtTotalFuel.Text, font, XBrushes.Black, displayXOffset + txtTotalFuel.Location.X, displayYOffset);


            displayYOffset = this.dataGridView1.Location.Y + this.dataGridView1.Height + 80;


            drawGridOnPdfdocument(dataGridView2, gfx, displayXOffset + 5, displayYOffset + 35);


            gfx.DrawString("Relative Bearings", font, XBrushes.DarkBlue, displayXOffset + 7, displayYOffset + 30);
            gfx.DrawString("Additional Information", font, XBrushes.DarkBlue, displayXOffset + 568, displayYOffset + 30);

            gfx.DrawRoundedRectangle(pen, displayXOffset + 565, displayYOffset + 33, 500, 285, 10, 10);
            DrawThisControlOnPdfDocument2(this.additional_Info_richTextBox, gfx, displayXOffset - 4, displayYOffset + 17);

            displayXOffset = this.dataGridView1.Location.X + this.dataGridView1.Width + 10;
            displayYOffset = 40;

            gfx.DrawString("Date", font, XBrushes.Black, displayXOffset + 7, displayYOffset + 30);
            DrawThisControlOnPdfDocument(this.dateTimePicker1, gfx, displayXOffset, displayYOffset);

            displayYOffset = 75;
            gfx.DrawString("Call Sign", font, XBrushes.Black, displayXOffset + 7, displayYOffset + 30);
            DrawThisControlOnPdfDocument(this.callsignBox, gfx, displayXOffset, displayYOffset);
            displayYOffset += 35;
            gfx.DrawString("Pilot", font, XBrushes.Black, displayXOffset + 7, displayYOffset + 30);
            DrawThisControlOnPdfDocument(this.pilotNameBox, gfx, displayXOffset, displayYOffset);
            displayYOffset += 07;
            gfx.DrawString("Co-Pilot", font, XBrushes.Black, displayXOffset + 7, displayYOffset + 60);
            DrawThisControlOnPdfDocument(this.copilotNameBox, gfx, displayXOffset, displayYOffset);
            displayYOffset += 50;
            gfx.DrawString("Start Fuel", font, XBrushes.Black, displayXOffset + 7, displayYOffset + 45);
            DrawThisControlOnPdfDocument(this.numericUpDn_startFuel, gfx, displayXOffset, displayYOffset);

            displayYOffset += 60;
            gfx.DrawString("Start Up", font, XBrushes.Black, displayXOffset + 7, displayYOffset + 35);
            DrawThisControlOnPdfDocument(this.timeStartBox, gfx, displayXOffset, displayYOffset);
            displayYOffset += 10;
            gfx.DrawString("Take Off", font, XBrushes.Black, displayXOffset + 7, displayYOffset + 60);
            DrawThisControlOnPdfDocument(this.timeTakeoffBox, gfx, displayXOffset, displayYOffset);
            displayYOffset += 10;
            gfx.DrawString("Landing", font, XBrushes.Black, displayXOffset + 7, displayYOffset + 80);
            DrawThisControlOnPdfDocument(this.timeLandBox, gfx, displayXOffset, displayYOffset);
            displayYOffset += 10;
            gfx.DrawString("Switch Off", font, XBrushes.Black, displayXOffset + 7, displayYOffset + 105);
            DrawThisControlOnPdfDocument(this.timeSWoffBox, gfx, displayXOffset, displayYOffset);

            displayYOffset += 123;
            gfx.DrawString("Start + Taxi + Hold", font, XBrushes.Black, displayXOffset + 7, displayYOffset + 35);
            DrawThisControlOnPdfDocument(this.numericUpDn_StartTaxiHold, gfx, displayXOffset + 5, displayYOffset);
            displayYOffset += 07;
            gfx.DrawString("Flt Fuel", font, XBrushes.Black, displayXOffset + 7, displayYOffset + 55);
            DrawThisControlOnPdfDocument(this.txtBox_fltFuel, gfx, displayXOffset + 5, displayYOffset);
            displayYOffset += 07;
            gfx.DrawString("Reserve", font, XBrushes.Black, displayXOffset + 7, displayYOffset + 80);
            DrawThisControlOnPdfDocument(this.numericUpDn_Reserve, gfx, displayXOffset - 4, displayYOffset);
            gfx.DrawString("Min", font, XBrushes.Black, displayXOffset + 95, displayYOffset + 80);
            DrawThisControlOnPdfDocument(this.txtBox_Reserve, gfx, displayXOffset + 5, displayYOffset);
            displayYOffset += 07;
            gfx.DrawString("10% Contingency", font, XBrushes.Black, displayXOffset + 7, displayYOffset + 105);
            DrawThisControlOnPdfDocument(this.txtBox_Contingency, gfx, displayXOffset + 5, displayYOffset);
            displayYOffset += 07;
            gfx.DrawString("Additional Reserve", font, XBrushes.Black, displayXOffset + 7, displayYOffset + 130);
            DrawThisControlOnPdfDocument(this.numericUpDn_AddReserve, gfx, displayXOffset + 5, displayYOffset);
            displayYOffset += 130;
            gfx.DrawString("Total Flt Fuel", font, XBrushes.DarkBlue, displayXOffset + 7, displayYOffset + 30);
            DrawThisControlOnPdfDocument(this.txtBox_Tot_FltFuel, gfx, displayXOffset + 12, displayYOffset + 10);


            //
            DrawThisControlOnPdfDocument(this.additional_Info_richTextBox, gfx, displayXOffset - 25, displayYOffset + 10);




            back.A = 0.3;
            XSolidBrush brush = new XSolidBrush(back);

            //Side Panels
            gfx.DrawRoundedRectangle(pen, 1075, 48, 190, 175, 10, 10);
            gfx.DrawRoundedRectangle(brush, 1075, 48, 190, 175, 10, 10);
            gfx.DrawRoundedRectangle(pen, 1075, 238, 190, 140, 10, 10);
            gfx.DrawRoundedRectangle(brush, 1075, 238, 190, 140, 10, 10);
            gfx.DrawRoundedRectangle(pen, 1075, 392, 195, 195, 10, 10);
            gfx.DrawRoundedRectangle(brush, 1075, 392, 195, 195, 10, 10);
            /*
            //for dataGridview1
            gfx.DrawRoundedRectangle(pen, 5, 48, 1058, 328, 10, 10);
             */


            /*
            XImage xImage = XImage.FromFile(imagePath);
            gfx.DrawImage(xImage, xPosition, yPosition, xImage.PixelWidth, xImage.PixelWidth);

            */


            // Create a font
            XFont font1 = new XFont("Aerial", 30, XFontStyle.Regular);
            XFont font2 = new XFont("Aerial", 14, XFontStyle.Italic);
            // Draw the Main Heading text and Caution Note
            //gfx.DrawRoundedRectangle(XBrushes.Navy, 570, 2, 1024, 35, 10, 10);
            gfx.DrawString("QNP Nav Plan [Raymond Ltd]", font1, XBrushes.Black,
              new XRect(0, 0, page.Width, page.Height),
              XStringFormats.TopCenter);
            gfx.DrawString("Caution ", font2, XBrushes.Red, 5, 820);
            gfx.DrawString(": Check the correctness of calculated data with other resources.", font2, XBrushes.Black, 50, 820);


            // TODO DISPLAY A FILE SAVE DIALOG ALLOWING THE USER TO SET THE NAME
            // OF THE CURRENTLY GENERATED REPORT


            //save file dialog proc
            SaveFileDialog SaveFileDialog1 = new SaveFileDialog();

            //saveFileDialog1.ShowDialog();

            //Nullable<bool> result = SaveFileDialog1.ShowDialog();
            saveFileDialog1.Tag = DateTime.Now.ToString("yyyy_MM_dd_HH_mm");
            // Process save file dialog box results
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Save document
                String rawFileName = saveFileDialog1.FileName;
                String taggedFileName = AppendDate(rawFileName);
                document.Save(taggedFileName);
                Process.Start(taggedFileName);
            }

        }

        // Attach date time tag to File Name
        public string AppendDate(String rawFileName)
        {
            FileInfo info = new FileInfo(rawFileName);
            String folder = Path.GetDirectoryName(rawFileName);
            String fileName = Path.GetFileNameWithoutExtension(rawFileName);
            String extension = Path.GetExtension(rawFileName);
            String newNameWithDate = folder + "\\" + fileName + "_" + ISO_Date() + extension;
            return (newNameWithDate);
        }

        // Date time tag.
        static String ISO_Date()
        {
            return DateTime.Now.ToString("yyyy_MM_dd_HH_mm");
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            createPdfDocument();

        }

        private void numericUpDn_StartTaxiHold_ValueChanged(object sender, EventArgs e)
        {
            total_Flt_Fuel_Calculation();
        }

        private void numericUpDn_AddReserve_ValueChanged(object sender, EventArgs e)
        {
            total_Flt_Fuel_Calculation();
        }

        private void numericUpDn_Reserve_ValueChanged(object sender, EventArgs e)
        {
            total_Flt_Fuel_Calculation();
        }

        private void callsignBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void pilotNameBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void copilotNameBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        // Google Earth Interface 


        public void GEView()
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<PlaceMark> placeMarks = new List<PlaceMark>();
            for (int index = 0; index < dataGridView2.Rows.Count - 1; ++index)
            {
                DataGridViewRow row = dataGridView2.Rows[index];
                PlaceMark pMark = new PlaceMark();
                pMark.Location = row.Cells[0].Value.ToString();
                pMark.latitude = double.Parse(row.Cells[10].Value.ToString());
                pMark.longitude = double.Parse(row.Cells[11].Value.ToString());
                placeMarks.Add(pMark);
            }
            Google_Earth_View f = new Google_Earth_View();
            f.SetPlaceMarks(placeMarks);
            f.Show();
        }

        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                e.Row.Cells[2].Value = false;
            }
            catch (Exception ex)
            {
                //mainForm.staticvar.logger.Write(ex);
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridView1_CurrentCellDirtyStateChanged_1(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 2 && dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
    }
}
