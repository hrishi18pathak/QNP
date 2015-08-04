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
    public partial class UnitsSetup : Form
    {

        private void LoadUnitsettingfromApplicationState()
        {
            
            numericUpDn_cruiseFuelFlow.Value = ApplicationState.Instance.getcruiseFuelFlow();
            numericUpDn_cruiseSpeed.Value = ApplicationState.Instance.getCruiseSpeed();
            numericUpDn_minFuel.Value = ApplicationState.Instance.getMinFuelValue();
            numericUpDown_appAndDeckHoldFuel.Value = ApplicationState.Instance.getAppandonDeckHoldFuelValue();
            cmbLocationFormat.SelectedItem = ApplicationState.Instance.getLocationFormat();
            cmbSpeed.Text = ApplicationState.Instance.getSpeed();
            cmbUnit.Text = ApplicationState.Instance.getUnit();
            cmbUtcOffset.Text = ApplicationState.Instance.getUtcOffset();
            txtRegisteredTo.Text = ApplicationState.Instance.getRegisteredClientName();

        }

        public UnitsSetup()
        {
            InitializeComponent();
            LoadUnitsettingfromApplicationState();
        }

       
        private void SaveState()
        {
            ApplicationState.Instance.setcruiseFuelFlow((int)numericUpDn_cruiseFuelFlow.Value);
            ApplicationState.Instance.setMinFuel((int)numericUpDn_minFuel.Value);
            ApplicationState.Instance.setAppandonDeckHoldFuel((int)numericUpDown_appAndDeckHoldFuel.Value);
            ApplicationState.Instance.setCruiseSpeed((int)numericUpDn_cruiseSpeed.Value);
            ApplicationState.Instance.setLocationFormat((cmbLocationFormat.SelectedItem == null) ? "" : cmbLocationFormat.SelectedItem.ToString());
            ApplicationState.Instance.setSpeed((cmbSpeed.SelectedItem == null) ? "" : cmbSpeed.SelectedItem.ToString());
            ApplicationState.Instance.setUnit((cmbUnit.SelectedItem == null) ? "" : cmbUnit.SelectedItem.ToString());
            ApplicationState.Instance.setUtcOffset((cmbUtcOffset.SelectedItem == null) ? "" : cmbUtcOffset.SelectedItem.ToString());
            ApplicationState.Instance.saveToDB();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == MessageBox.Show("Confirm Settings", "Save Settings", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
            {
                SaveState();
                this.Close();
            }
        }

        private void numericUpDown_appAndDeckHoldFuel_ValueChanged(object sender, EventArgs e)
        {

        }

        
        

    }
}
