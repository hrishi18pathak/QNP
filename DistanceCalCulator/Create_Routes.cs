using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlServerCe;

namespace DistanceCalCulator
{
    public partial class Create_Routes : Form
    {
        public Create_Routes()
        {
            InitializeComponent();
            LoadComboxFromAllControls();
        
        }

        private void LoadComboxFromAllControls()
        {
            foreach (Control control in this.Controls)
            {
                LoadComboxBoxFromControl(control);
            }
        }

        private void LoadComboxBoxFromControl(Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                if (childControl is ComboBox)
                {
                    ComboBox thisBox = childControl as ComboBox; 
                    LoadComboBox(thisBox);
                }
                else if (childControl.Controls.Count > 0)
                {
                    LoadComboxBoxFromControl(childControl);
                }
                else
                {
                    return;
                }
            }
        }

        private void LoadComboBox(System.Windows.Forms.ComboBox cmbBox)
        {
            Dictionary<string,List<Airport>> listofAirports = AirportDatabase.Instance.getAirportsDictionary();
            // now just loop through the dictionary returned by the airportsdatabase object
            // and populate the autocomplete collection
            foreach (string ident_entry in listofAirports.Keys)
            {
                if (!ident_entry.StartsWith("ROUTE_"))
                {
                    cmbBox.Items.Add(ident_entry);
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Route name cannot be empty", "Add Routes Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            IEnumerable<string> idents = GetIdentsForRoute().Reverse();
            
            if (idents.Count() == 0)
            {
                MessageBox.Show("Please select at least one ident", "Add Routes Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // save the route name
            string routeIdent = "ROUTE_" + textBox1.Text;
            
            // Add the association between route name and route idents
            if (AddIdentsToRoute(routeIdent, idents))
            {
                // add routeId as an ident to the idents database
                AirportDatabase.Instance.addRecordToDatabase(routeIdent, "ROUTE", textBox1.Text, 0.0, 0.0, "", "");
                MessageBox.Show("Route '" + routeIdent + "' added to database!");
            }
            
        }

        private bool AddIdentsToRoute(string routeIdent, IEnumerable<string> idents)
        {
            return AirportDatabase.Instance.addIdentsToRoute(routeIdent, idents);
        }

        private IEnumerable<string> GetIdentsForRoute()
        {
            List<string> idents = new List<string>();
            foreach (Control control in this.Controls)
            {
                idents.AddRange(GetIdentsFromControl(control));
            }

            return idents;
        }

        private List<string> GetIdentsFromControl(Control control)
        {
            List<string> identsFromControl = new List<string>();
            foreach (Control childControl in control.Controls)
            {
                if (childControl is ComboBox)
                {
                    ComboBox thisBox = childControl as ComboBox;
                    if (thisBox.SelectedIndex > -1)
                    {
                        identsFromControl.Add(thisBox.Items[thisBox.SelectedIndex].ToString());
                    }
                }
                else if (childControl.Controls.Count > 0)
                {
                   identsFromControl.AddRange(GetIdentsFromControl(childControl));
                }
            }

            return identsFromControl;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Create_Routes_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = Char.ToUpper(e.KeyChar);
        }
    }
}
