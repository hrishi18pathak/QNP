using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DistanceCalCulator
{
    public partial class pilot_DataForm : Form
    {
        private void LoadDataInGrid()
        {

            object[] row = new object[13];
            dataGridView1.Rows.Clear();    // data .

            for (int index = 1; index < PilotsDatabase.Instance.nameList.Count; index++)
            {

                Pilot currPilot = PilotsDatabase.Instance.getPilotObjectFromName(PilotsDatabase.Instance.nameList[index]);
                row[0] = currPilot.FName;
                row[1] = currPilot.LName;
                row[2] = currPilot.Weight;
                dataGridView1.Rows.Add(row); // Add the row.
            }

        }

        public pilot_DataForm()
        {
            InitializeComponent();
            LoadDataInGrid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void pilot_DataForm_Load(object sender, EventArgs e)
        {

        }

        private void btnAddData_Click(object sender, EventArgs e)
        {

        }
        private void dataGridView1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {

        }

    }
}
