namespace DistanceCalCulator
{
    partial class DataForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataForm));
            this.EditDataForm = new System.Windows.Forms.Button();
            this.addDataForm = new System.Windows.Forms.Button();
            this.exitData = new System.Windows.Forms.Button();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LatitudeDataForm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LongitudeDataForm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ElevationDataForm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnMunicipality = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FrequencyDataForm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnGpsCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnIataCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmnMagneticVariation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AssociatedAFDataForm = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnDelete = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // EditDataForm
            // 
            this.EditDataForm.Location = new System.Drawing.Point(33, 7);
            this.EditDataForm.Name = "EditDataForm";
            this.EditDataForm.Size = new System.Drawing.Size(103, 30);
            this.EditDataForm.TabIndex = 1;
            this.EditDataForm.Text = "&Refresh Form";
            this.EditDataForm.UseVisualStyleBackColor = true;
            this.EditDataForm.Click += new System.EventHandler(this.EditDataForm_Click);
            // 
            // addDataForm
            // 
            this.addDataForm.Location = new System.Drawing.Point(291, 7);
            this.addDataForm.Name = "addDataForm";
            this.addDataForm.Size = new System.Drawing.Size(103, 30);
            this.addDataForm.TabIndex = 3;
            this.addDataForm.Text = "&Add Data";
            this.addDataForm.UseVisualStyleBackColor = true;
            this.addDataForm.Click += new System.EventHandler(this.addDataForm_Click);
            // 
            // exitData
            // 
            this.exitData.Location = new System.Drawing.Point(807, 7);
            this.exitData.Name = "exitData";
            this.exitData.Size = new System.Drawing.Size(103, 30);
            this.exitData.TabIndex = 4;
            this.exitData.Text = "&Exit";
            this.exitData.UseVisualStyleBackColor = true;
            this.exitData.Click += new System.EventHandler(this.exitData_Click);
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4,
            this.LatitudeDataForm,
            this.LongitudeDataForm,
            this.ElevationDataForm,
            this.clmnMunicipality,
            this.FrequencyDataForm,
            this.clmnGpsCode,
            this.clmnIataCode,
            this.clmnMagneticVariation,
            this.AssociatedAFDataForm});
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.Size = new System.Drawing.Size(942, 567);
            this.dataGridView2.TabIndex = 2;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "NUMBER";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "APT CODE/IDENT";
            this.Column2.Name = "Column2";
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "TYPE";
            this.Column3.Name = "Column3";
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Programmatic;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "NAME";
            this.Column4.Name = "Column4";
            // 
            // LatitudeDataForm
            // 
            this.LatitudeDataForm.HeaderText = "Latitude";
            this.LatitudeDataForm.Name = "LatitudeDataForm";
            this.LatitudeDataForm.ReadOnly = true;
            // 
            // LongitudeDataForm
            // 
            this.LongitudeDataForm.HeaderText = "Longitude";
            this.LongitudeDataForm.Name = "LongitudeDataForm";
            this.LongitudeDataForm.ReadOnly = true;
            // 
            // ElevationDataForm
            // 
            this.ElevationDataForm.HeaderText = "Elevation";
            this.ElevationDataForm.Name = "ElevationDataForm";
            this.ElevationDataForm.ReadOnly = true;
            // 
            // clmnMunicipality
            // 
            this.clmnMunicipality.HeaderText = "Municipality";
            this.clmnMunicipality.Name = "clmnMunicipality";
            // 
            // FrequencyDataForm
            // 
            this.FrequencyDataForm.HeaderText = "Frequency";
            this.FrequencyDataForm.Name = "FrequencyDataForm";
            this.FrequencyDataForm.ReadOnly = true;
            // 
            // clmnGpsCode
            // 
            this.clmnGpsCode.HeaderText = "GPS Code";
            this.clmnGpsCode.Name = "clmnGpsCode";
            // 
            // clmnIataCode
            // 
            this.clmnIataCode.HeaderText = "IATA Code";
            this.clmnIataCode.Name = "clmnIataCode";
            // 
            // clmnMagneticVariation
            // 
            this.clmnMagneticVariation.HeaderText = "Magnetic Variation";
            this.clmnMagneticVariation.Name = "clmnMagneticVariation";
            // 
            // AssociatedAFDataForm
            // 
            this.AssociatedAFDataForm.HeaderText = "Associated Air Field";
            this.AssociatedAFDataForm.Name = "AssociatedAFDataForm";
            this.AssociatedAFDataForm.ReadOnly = true;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(549, 7);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(103, 30);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "&Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.EditDataForm);
            this.splitContainer1.Panel2.Controls.Add(this.exitData);
            this.splitContainer1.Panel2.Controls.Add(this.btnDelete);
            this.splitContainer1.Panel2.Controls.Add(this.addDataForm);
            this.splitContainer1.Size = new System.Drawing.Size(942, 616);
            this.splitContainer1.SplitterDistance = 567;
            this.splitContainer1.TabIndex = 6;
            // 
            // DataForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 616);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DataForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Data Base";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DataForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button EditDataForm;
        private System.Windows.Forms.Button addDataForm;
        private System.Windows.Forms.Button exitData;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn LatitudeDataForm;
        private System.Windows.Forms.DataGridViewTextBoxColumn LongitudeDataForm;
        private System.Windows.Forms.DataGridViewTextBoxColumn ElevationDataForm;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnMunicipality;
        private System.Windows.Forms.DataGridViewTextBoxColumn FrequencyDataForm;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnGpsCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnIataCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmnMagneticVariation;
        private System.Windows.Forms.DataGridViewTextBoxColumn AssociatedAFDataForm;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.SplitContainer splitContainer1;

        public System.EventHandler textBox1_TextChanged { get; set; }
    }
}