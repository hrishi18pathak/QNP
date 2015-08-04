

namespace DistanceCalCulator
{
    partial class File_Search_Window
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(File_Search_Window));
            this.delimeterTextBox = new System.Windows.Forms.TextBox();
            this.writeResultsButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.fileNameTextBox = new System.Windows.Forms.TextBox();
            this.resultsList = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openContainingFolderContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unicodeRadioButton = new System.Windows.Forms.RadioButton();
            this.asciiRadioButton = new System.Windows.Forms.RadioButton();
            this.startButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.includeSubDirsCheckBox = new System.Windows.Forms.CheckBox();
            this.selectSearchDirButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.searchDirTextBox = new System.Windows.Forms.TextBox();
            this.containingTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.containingCheckBox = new System.Windows.Forms.CheckBox();
            this.olderThanDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.newerThanCheckBox = new System.Windows.Forms.CheckBox();
            this.stopButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.olderThanCheckBox = new System.Windows.Forms.CheckBox();
            this.newerThanDateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.contextMenuStrip.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // delimeterTextBox
            // 
            this.delimeterTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.delimeterTextBox.Location = new System.Drawing.Point(254, 141);
            this.delimeterTextBox.MaxLength = 4;
            this.delimeterTextBox.Name = "delimeterTextBox";
            this.delimeterTextBox.Size = new System.Drawing.Size(38, 20);
            this.delimeterTextBox.TabIndex = 2;
            // 
            // writeResultsButton
            // 
            this.writeResultsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.writeResultsButton.Location = new System.Drawing.Point(295, 139);
            this.writeResultsButton.Name = "writeResultsButton";
            this.writeResultsButton.Size = new System.Drawing.Size(150, 23);
            this.writeResultsButton.TabIndex = 3;
            this.writeResultsButton.Text = "Write results to text file...";
            this.writeResultsButton.UseVisualStyleBackColor = true;
            this.writeResultsButton.Click += new System.EventHandler(this.writeResultsButton_Click_1);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(249, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Delimeter for text file (may include escapes \\r,\\n,\\t):";
            // 
            // fileNameTextBox
            // 
            this.fileNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.fileNameTextBox.Location = new System.Drawing.Point(260, 74);
            this.fileNameTextBox.Name = "fileNameTextBox";
            this.fileNameTextBox.Size = new System.Drawing.Size(201, 20);
            this.fileNameTextBox.TabIndex = 15;
            // 
            // resultsList
            // 
            this.resultsList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.resultsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.resultsList.ContextMenuStrip = this.contextMenuStrip;
            this.resultsList.FullRowSelect = true;
            this.resultsList.Location = new System.Drawing.Point(14, 19);
            this.resultsList.MultiSelect = false;
            this.resultsList.Name = "resultsList";
            this.resultsList.ShowItemToolTips = true;
            this.resultsList.Size = new System.Drawing.Size(430, 114);
            this.resultsList.TabIndex = 0;
            this.resultsList.UseCompatibleStateImageBehavior = false;
            this.resultsList.View = System.Windows.Forms.View.Details;
            this.resultsList.DoubleClick += new System.EventHandler(this.resultsList_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Path";
            this.columnHeader1.Width = 212;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Size";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.columnHeader2.Width = 90;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Last modified";
            this.columnHeader3.Width = 120;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openContainingFolderContextMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(198, 26);
            // 
            // openContainingFolderContextMenuItem
            // 
            this.openContainingFolderContextMenuItem.Name = "openContainingFolderContextMenuItem";
            this.openContainingFolderContextMenuItem.Size = new System.Drawing.Size(197, 22);
            this.openContainingFolderContextMenuItem.Text = "Open containing folder";
            // 
            // unicodeRadioButton
            // 
            this.unicodeRadioButton.AutoSize = true;
            this.unicodeRadioButton.Enabled = false;
            this.unicodeRadioButton.Location = new System.Drawing.Point(319, 97);
            this.unicodeRadioButton.Name = "unicodeRadioButton";
            this.unicodeRadioButton.Size = new System.Drawing.Size(65, 17);
            this.unicodeRadioButton.TabIndex = 7;
            this.unicodeRadioButton.TabStop = true;
            this.unicodeRadioButton.Text = "Unicode";
            this.unicodeRadioButton.UseVisualStyleBackColor = true;
            // 
            // asciiRadioButton
            // 
            this.asciiRadioButton.AutoSize = true;
            this.asciiRadioButton.Enabled = false;
            this.asciiRadioButton.Location = new System.Drawing.Point(261, 97);
            this.asciiRadioButton.Name = "asciiRadioButton";
            this.asciiRadioButton.Size = new System.Drawing.Size(52, 17);
            this.asciiRadioButton.TabIndex = 6;
            this.asciiRadioButton.TabStop = true;
            this.asciiRadioButton.Text = "ASCII";
            this.asciiRadioButton.UseVisualStyleBackColor = true;
            // 
            // startButton
            // 
            this.startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.startButton.Location = new System.Drawing.Point(385, 226);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 18;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.delimeterTextBox);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.writeResultsButton);
            this.groupBox2.Controls.Add(this.resultsList);
            this.groupBox2.Location = new System.Drawing.Point(6, 255);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(455, 168);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Results";
            // 
            // includeSubDirsCheckBox
            // 
            this.includeSubDirsCheckBox.AutoSize = true;
            this.includeSubDirsCheckBox.Location = new System.Drawing.Point(98, 51);
            this.includeSubDirsCheckBox.Name = "includeSubDirsCheckBox";
            this.includeSubDirsCheckBox.Size = new System.Drawing.Size(129, 17);
            this.includeSubDirsCheckBox.TabIndex = 13;
            this.includeSubDirsCheckBox.Text = "Include subdirectories";
            this.includeSubDirsCheckBox.UseVisualStyleBackColor = true;
            // 
            // selectSearchDirButton
            // 
            this.selectSearchDirButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.selectSearchDirButton.Location = new System.Drawing.Point(434, 25);
            this.selectSearchDirButton.Name = "selectSearchDirButton";
            this.selectSearchDirButton.Size = new System.Drawing.Size(24, 21);
            this.selectSearchDirButton.TabIndex = 12;
            this.selectSearchDirButton.Text = "...";
            this.selectSearchDirButton.UseVisualStyleBackColor = true;
            this.selectSearchDirButton.Click += new System.EventHandler(this.selectSearchDirButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Search directory:";
            // 
            // searchDirTextBox
            // 
            this.searchDirTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.searchDirTextBox.Location = new System.Drawing.Point(96, 25);
            this.searchDirTextBox.Name = "searchDirTextBox";
            this.searchDirTextBox.Size = new System.Drawing.Size(322, 20);
            this.searchDirTextBox.TabIndex = 11;
            // 
            // containingTextBox
            // 
            this.containingTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.containingTextBox.Enabled = false;
            this.containingTextBox.Location = new System.Drawing.Point(256, 71);
            this.containingTextBox.Name = "containingTextBox";
            this.containingTextBox.Size = new System.Drawing.Size(188, 20);
            this.containingTextBox.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(255, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Filename (may include wildcards, not case sensitive):";
            // 
            // containingCheckBox
            // 
            this.containingCheckBox.AutoSize = true;
            this.containingCheckBox.Location = new System.Drawing.Point(16, 73);
            this.containingCheckBox.Name = "containingCheckBox";
            this.containingCheckBox.Size = new System.Drawing.Size(224, 17);
            this.containingCheckBox.TabIndex = 4;
            this.containingCheckBox.Text = "Files containing the string (case sensitive):";
            this.containingCheckBox.UseVisualStyleBackColor = true;
            this.containingCheckBox.CheckedChanged += new System.EventHandler(this.containingCheckBox_CheckedChanged_1);
            // 
            // olderThanDateTimePicker
            // 
            this.olderThanDateTimePicker.CustomFormat = "dd.MM.yyyy HH:mm";
            this.olderThanDateTimePicker.Enabled = false;
            this.olderThanDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.olderThanDateTimePicker.Location = new System.Drawing.Point(256, 45);
            this.olderThanDateTimePicker.Name = "olderThanDateTimePicker";
            this.olderThanDateTimePicker.Size = new System.Drawing.Size(123, 20);
            this.olderThanDateTimePicker.TabIndex = 3;
            // 
            // newerThanCheckBox
            // 
            this.newerThanCheckBox.AutoSize = true;
            this.newerThanCheckBox.Location = new System.Drawing.Point(16, 22);
            this.newerThanCheckBox.Name = "newerThanCheckBox";
            this.newerThanCheckBox.Size = new System.Drawing.Size(106, 17);
            this.newerThanCheckBox.TabIndex = 0;
            this.newerThanCheckBox.Text = "Files newer than:";
            this.newerThanCheckBox.UseVisualStyleBackColor = true;
            this.newerThanCheckBox.CheckedChanged += new System.EventHandler(this.newerThanCheckBox_CheckedChanged_1);
            // 
            // stopButton
            // 
            this.stopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.stopButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(304, 226);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 17;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.unicodeRadioButton);
            this.groupBox1.Controls.Add(this.asciiRadioButton);
            this.groupBox1.Controls.Add(this.containingTextBox);
            this.groupBox1.Controls.Add(this.containingCheckBox);
            this.groupBox1.Controls.Add(this.olderThanDateTimePicker);
            this.groupBox1.Controls.Add(this.newerThanCheckBox);
            this.groupBox1.Controls.Add(this.olderThanCheckBox);
            this.groupBox1.Controls.Add(this.newerThanDateTimePicker);
            this.groupBox1.Location = new System.Drawing.Point(6, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(455, 120);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Restrictions";
            // 
            // olderThanCheckBox
            // 
            this.olderThanCheckBox.AutoSize = true;
            this.olderThanCheckBox.Location = new System.Drawing.Point(16, 48);
            this.olderThanCheckBox.Name = "olderThanCheckBox";
            this.olderThanCheckBox.Size = new System.Drawing.Size(100, 17);
            this.olderThanCheckBox.TabIndex = 2;
            this.olderThanCheckBox.Text = "Files older than:";
            this.olderThanCheckBox.UseVisualStyleBackColor = true;
            this.olderThanCheckBox.CheckedChanged += new System.EventHandler(this.olderThanCheckBox_CheckedChanged_1);
            // 
            // newerThanDateTimePicker
            // 
            this.newerThanDateTimePicker.CustomFormat = "dd.MM.yyyy HH:mm";
            this.newerThanDateTimePicker.Enabled = false;
            this.newerThanDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.newerThanDateTimePicker.Location = new System.Drawing.Point(256, 19);
            this.newerThanDateTimePicker.Name = "newerThanDateTimePicker";
            this.newerThanDateTimePicker.Size = new System.Drawing.Size(123, 20);
            this.newerThanDateTimePicker.TabIndex = 1;
            // 
            // File_Search_Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(473, 448);
            this.Controls.Add(this.fileNameTextBox);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.includeSubDirsCheckBox);
            this.Controls.Add(this.selectSearchDirButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.searchDirTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "File_Search_Window";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "File Search";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainWindow_FormClosing);
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.contextMenuStrip.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox delimeterTextBox;
        private System.Windows.Forms.Button writeResultsButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox fileNameTextBox;
        private System.Windows.Forms.ListView resultsList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem openContainingFolderContextMenuItem;
        private System.Windows.Forms.RadioButton unicodeRadioButton;
        private System.Windows.Forms.RadioButton asciiRadioButton;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox includeSubDirsCheckBox;
        private System.Windows.Forms.Button selectSearchDirButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox searchDirTextBox;
        private System.Windows.Forms.TextBox containingTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox containingCheckBox;
        private System.Windows.Forms.DateTimePicker olderThanDateTimePicker;
        private System.Windows.Forms.CheckBox newerThanCheckBox;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox olderThanCheckBox;
        private System.Windows.Forms.DateTimePicker newerThanDateTimePicker;

        public System.EventHandler Form1_Load { get; set; }
    }
}