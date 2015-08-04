using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace DistanceCalCulator
{
    public partial class addDataForm : Form
    {
        int latDirFlag = 1;
        int longDirFlag = 1;
        public addDataForm()
        {
            InitializeComponent();
        }
        
        private void addDataButton_Click_1(object sender, EventArgs e)
        {
            
                
                NSTextBox.Text = "N";
                double decimalDegreesLat;
                double decimalDegreesLong;
                int degreesLat = (int)numericUpDownDegLat.Value;
                int minutesLat = (int)numericUpDownMinLat.Value;
                int secondsLat = (int)numericUpDownSecLat.Value;
                int decimalSecondsLat = (int)numericUpDownDecimalSecLat.Value;

                int degreesLong = (int)numericUpDownDegLong.Value;
                int minutesLong = (int)numericUpDownMinLong.Value;
                int secondsLong = (int)numericUpDownSecLong.Value;
                int decimalSecondsLong = (int)numericUpDownDecimalSecLong.Value;
                decimalDegreesLat = Utils.ConvertDegreeAngleToDouble(degreesLat, minutesLat, secondsLat, decimalSecondsLat);
                decimalDegreesLong = Utils.ConvertDegreeAngleToDouble(degreesLong, minutesLong, secondsLong, decimalSecondsLong);

                string dataTextBox1 = identTextBox.Text + "\t" + typeTextBox.Text+ "\t" + nameTextBox.Text + "\t" + decimalDegreesLat * latDirFlag + "\t" + decimalDegreesLong * longDirFlag + "\t" +  elevTextBox.Text + "\t" + freqTextBox.Text + "\t"+ "-"+ "\t" + "-"+ "\t" + "-"+ "\t" + "-"+ "\t" + "-";
             
                if (this.addDataButton.Text == "Add Data")
                    AirportDatabase.Instance.addRecordToDatabase(identTextBox.Text,
                                                                 typeTextBox.Text,
                                                                 nameTextBox.Text,
                                                                 decimalDegreesLat * latDirFlag,
                                                                 decimalDegreesLong * longDirFlag,
                                                                 elevTextBox.Text,
                                                                 freqTextBox.Text
                                                                 );
                else
                {
                    double realLat = decimalDegreesLat * latDirFlag;
                    double realLong = decimalDegreesLong * longDirFlag;
                    AirportDatabase.Instance.updateRecordInDatabase(identTextBox.Text,
                                                                  typeTextBox.Text,
                                                                  nameTextBox.Text,
                                                                  realLat.ToString(),
                                                                  realLong.ToString(),
                                                                  elevTextBox.Text,
                                                                  freqTextBox.Text
                                                                  );
                    if (DialogResult.OK == MessageBox.Show("The record has been saved to the database", "Update Record Successfull", MessageBoxButtons.OK))
                    {
                        this.Close();
                    }
                }

        }
        // Validation of input data ..whether it is north or South.. sets flag to -1 if South.
        private void NSTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {

            string allowedCharacterSetLat = "sSnN\b\n\t";
            //Allowed character set

            if (allowedCharacterSetLat.Contains(e.KeyChar.ToString()))
            {


                if ((e.KeyChar.ToString() == "s") || e.KeyChar.ToString() == "S")
                {
                    latDirFlag = -1;
                   
                }

            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Accepted Values are n,N,s,S ");
            }
        }

        // Validation of input data ..whether it is East or West.. sets flag to -1 if West.
        private void EWTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {

            string allowedCharacterSetLong = "eEwW\b\n\t";
            //Allowed character set

            if (allowedCharacterSetLong.Contains(e.KeyChar.ToString()))
            {


                if ((e.KeyChar.ToString() == "w") || e.KeyChar.ToString() == "W")
                {
                    longDirFlag = -1;
                    
                }

            }
            else
            {
                e.Handled = true;
                MessageBox.Show("Accepted Values are e,E,w,W ");
            }
        }

        private void exitaddDataFormButton_Click(object sender, EventArgs e)
        {
            
            this.Hide();
        }

               
    }
}   
               

      
       
        


        
       
         









    

