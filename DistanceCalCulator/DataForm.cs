using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace DistanceCalCulator
{
    public partial class DataForm : Form
    {

        public void LoadDataInGrid()
        {
            string latlong;
            object[] row = new object[13];
            dataGridView2.RowCount = 1;    // Clear the previous
            dataGridView2.Rows.Clear();    // data .

            for (int index = 1; index < AirportDatabase.Instance.identList.Count; index++)
            {
                
                Airport currAirport = AirportDatabase.Instance.getAirportObjectFromIdent( AirportDatabase.Instance.identList[ index ] );
                row[0] = currAirport.ID;
                row[1] = currAirport.ident;
                row[2] = currAirport.type;
                row[3] = currAirport.name;

                CoordinateType enum1 = CoordinateType.latitude;   //Attach 'N'
                latlong = Utils.DDtoDMS(Utils.StrToDouble( currAirport.latitude_deg ), type: enum1); // Convert Deg decimal to 19 46' 18" N format.
                row[4] = latlong;

                CoordinateType enum2 = CoordinateType.longitude;  // Attach 'S' 
                latlong = Utils.DDtoDMS(Utils.StrToDouble( currAirport.longitude_deg ), type: enum2);//  Convert Deg decimal to 19 46' 18" N format.
                row[5] = latlong;
                row[6] = currAirport.elev_ft;
                row[7] = currAirport.municipality;
                row[8] = currAirport.frequency_khz;
                row[9] = currAirport.gps_code;
                row[10] = currAirport.iata_code;
                row[11] = currAirport.magnetic_variation_deg;
                row[12] = currAirport.associated_airport;
                dataGridView2.Rows.Add(row); // Add the row.
                dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView2.RowCount - 1; //keep scrolling index to bottom of dgv2.
                
               
            }
 
        }

        public DataForm()
        {           
            InitializeComponent();
            LoadDataInGrid(); 
  
        }
          
          
          public enum CoordinateType { latitude, longitude};
          private void addDataForm_Click(object sender, EventArgs e)
          {

              addDataForm f = new addDataForm();
              f.ShowDialog();
          }
          // Collect data from text file and show in grid.
          private void dataViewButton_Click(object sender, EventArgs e)
          {

              
          }

          //To display fields of selected row in edit box
          private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
          {
              addDataForm f = new addDataForm();
                // check if this is a header cell (e.rowIndex == -1). if yes then sort
              if (e.RowIndex == -1)
              {
                  // we get the column the user has clicked
                  DataGridViewColumn clickedColumn = this.dataGridView2.Columns[e.ColumnIndex];
                  this.dataGridView2.Sort(clickedColumn, ListSortDirection.Ascending);

              }
              else
              {
                  string currentValueHere;
                  currentValueHere = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                  f.identTextBox.Text = currentValueHere;
                  currentValueHere = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
                  f.nameTextBox.Text = currentValueHere;
                  currentValueHere = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                  f.typeTextBox.Text = currentValueHere;
                  currentValueHere = dataGridView2.Rows[e.RowIndex].Cells[4].Value.ToString();

                  //Seperation of Deg Min and Sec to display in addDataForm.(For Latitude)
                  Regex r1 = new Regex("(°)");
                  String[] s = r1.Split(currentValueHere);
                  string Deg = s[0];
                  string temp1 = s[2];
                  Regex r2 = new Regex("(')");
                  s = r2.Split(temp1);
                  string Min = s[0];
                  temp1 = s[2];
                  Regex r3 = new Regex("(\")");
                  s = r3.Split(temp1);
                  string Sec = s[0];
                  temp1 = s[2];

                  f.numericUpDownDegLat.Value = Convert.ToInt16(Deg);
                  f.numericUpDownMinLat.Value = Convert.ToInt16(Min);
                  f.numericUpDownSecLat.Value = Convert.ToInt16(Sec);
                  f.NSTextBox.Text = temp1;
                 
                  currentValueHere = dataGridView2.Rows[e.RowIndex].Cells[5].Value.ToString();
                  f.addDataButton.Text = "Save";
                  //Seperation of Deg Min and Sec to display in addDataForm.(For Longitude)
                  Regex r11 = new Regex("(°)");
                  String[] s1 = r11.Split(currentValueHere);
                  string Deg1 = s1[0];
                  string temp11 = s1[2];
                  Regex r21 = new Regex("(')");
                  s1 = r21.Split(temp11);
                  string Min1 = s1[0];
                  temp11 = s1[2];
                  Regex r31 = new Regex("(\")");
                  s1 = r31.Split(temp11);
                  string Sec1 = s1[0];
                  temp11 = s1[2];

                  f.numericUpDownDegLong.Value = Convert.ToInt16(Deg1);
                  f.numericUpDownMinLong.Value = Convert.ToInt16(Min1);
                  f.numericUpDownSecLong.Value = Convert.ToInt16(Sec1);
                  f.EWTextBox.Text = temp11;

                  currentValueHere = dataGridView2.Rows[e.RowIndex].Cells[6].Value.ToString();
                  f.elevTextBox.Text = currentValueHere;
                  currentValueHere = dataGridView2.Rows[e.RowIndex].Cells[8].Value.ToString();
                  f.freqTextBox.Text = currentValueHere;
                  
                  f.Show();

              }         
              
              
          }

        private void exitData_Click(object sender, EventArgs e)
        {
            LoadDataInGrid();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.dataGridView2.Sort(this.Column2, ListSortDirection.Ascending);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.dataGridView2.Sort(this.Column3, ListSortDirection.Ascending);
        }



        public CoordinateType latitude { get; set; }

        public CoordinateType longitude { get; set; }

        public TextBox currentValueHere { get; set; }

        private void EditDataForm_Click(object sender, EventArgs e)
        {
            LoadDataInGrid();
        }

        private void DataForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            LoadDataInGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            List<string> tobeDeletedIdents = new List<string>();
            DataGridViewSelectedRowCollection selectedRows =  dataGridView2.SelectedRows;
            foreach (DataGridViewRow selectedRow in selectedRows)
            {
                tobeDeletedIdents.Add( selectedRow.Cells[1].Value.ToString() );                
            }

            AirportDatabase.Instance.DeleteRecordInDatabase(tobeDeletedIdents);
            LoadDataInGrid();
        }
    }
    
    }

