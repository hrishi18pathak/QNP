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
    public partial class Main_Form_New_Design2 : Form
    {
        public Main_Form_New_Design2(bool IsTrial, string registeredClientName)
        {
            InitializeComponent();
            _Trial = IsTrial;
            // store the registered client name to the db immediately
            if (!IsTrial && !string.IsNullOrEmpty(registeredClientName))
            {
                ApplicationState.Instance.UpdateRegisteredClientName(registeredClientName);
            }
        }

        public bool _Trial { get; set; }
        public string _RegisteredClientName { get; set; }

        private void button8_Click(object sender, EventArgs e)
        {
            FlightPlanForm f = new FlightPlanForm();
            f.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataForm f = new DataForm();
            f.ShowDialog();
        }

       
        private void button11_Click(object sender, EventArgs e)
        {
            UnitsSetup f = new UnitsSetup();
            f.ShowDialog();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            File_Search_Window f = new File_Search_Window();
            f.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Help f = new Help() ;
            f.ShowDialog();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Create_Routes f = new Create_Routes();
            f.ShowDialog();
        }

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.BackColor = Color.Black;
          
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            button1.BackColor = Color.LightSeaGreen;
        }

        private void button10_MouseHover(object sender, EventArgs e)
        {
            button10.BackColor = Color.Black;
        }

        private void button10_MouseLeave(object sender, EventArgs e)
        {
            button10.BackColor = Color.DarkGray;
        }

        private void button11_MouseHover(object sender, EventArgs e)
        {
            button11.BackColor = Color.Black;
        }

        private void button11_MouseLeave(object sender, EventArgs e)
        {
            button11.BackColor = Color.DarkGray;
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            button4.BackColor = Color.Black;
        }

        private void button4_MouseLeave(object sender, EventArgs e)
        {
            button4.BackColor = Color.LightSeaGreen;
        }

        private void button8_MouseHover(object sender, EventArgs e)
        {
            button8.BackColor = Color.Black;
        }

        private void button8_MouseLeave(object sender, EventArgs e)
        {
            button8.BackColor = Color.IndianRed;
        }

        private void button6_MouseHover(object sender, EventArgs e)
        {
            button6.BackColor = Color.Black;

        }

        private void button6_MouseLeave(object sender, EventArgs e)
        {
            button6.BackColor = Color.CornflowerBlue;

        }




    }
}
