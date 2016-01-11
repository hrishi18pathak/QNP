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
    public partial class IdentInputDialogForm : Form
    {
        public FlightPlanForm _parent;
        public IdentInputDialogForm(FlightPlanForm parent)
        {
            InitializeComponent();
            InitIdentAutoCompleteCollection();
            _parent = parent;            
        }

        private void InitIdentAutoCompleteCollection()
        {
            this.txtIdentRepl.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.txtIdentRepl.AutoCompleteSource = AutoCompleteSource.CustomSource;
            this.txtIdentRepl.AutoCompleteCustomSource = FlightPlanForm.GetAirportIdentAutoCompleteCollection();
        }
    }
}
