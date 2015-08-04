namespace SoftwareLocker
{
    partial class frmDialog
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
            this.sebBaseString = new SerialBox.SerialBox();
            this.sebPassword = new SerialBox.SerialBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnTrial = new System.Windows.Forms.Button();
            this.grbRegister = new System.Windows.Forms.GroupBox();
            this.lblSerial = new System.Windows.Forms.Label();
            this.lblID = new System.Windows.Forms.Label();
            this.lblCallPhone = new System.Windows.Forms.Label();
            this.lblComment = new System.Windows.Forms.Label();
            this.lblDaysToEnd = new System.Windows.Forms.Label();
            this.lblDays = new System.Windows.Forms.Label();
            this.lblRunTimesLeft = new System.Windows.Forms.Label();
            this.lblTimes = new System.Windows.Forms.Label();
            this.grbTrialRunning = new System.Windows.Forms.GroupBox();
            this.txtRegisteredName = new System.Windows.Forms.TextBox();
            this.lblRegisteredTo = new System.Windows.Forms.Label();
            this.grbRegister.SuspendLayout();
            this.grbTrialRunning.SuspendLayout();
            this.SuspendLayout();
            // 
            // sebBaseString
            // 
            this.sebBaseString.CaptleLettersOnly = true;
            this.sebBaseString.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
            this.sebBaseString.Location = new System.Drawing.Point(24, 142);
            this.sebBaseString.Margin = new System.Windows.Forms.Padding(6);
            this.sebBaseString.Name = "sebBaseString";
            this.sebBaseString.ReadOnly = true;
            this.sebBaseString.Size = new System.Drawing.Size(586, 35);
            this.sebBaseString.TabIndex = 2;
            // 
            // sebPassword
            // 
            this.sebPassword.CaptleLettersOnly = true;
            this.sebPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.25F);
            this.sebPassword.Location = new System.Drawing.Point(24, 225);
            this.sebPassword.Margin = new System.Windows.Forms.Padding(6);
            this.sebPassword.Name = "sebPassword";
            this.sebPassword.Size = new System.Drawing.Size(586, 35);
            this.sebPassword.TabIndex = 4;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(242, 390);
            this.btnOK.Margin = new System.Windows.Forms.Padding(6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(150, 44);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "&OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnTrial
            // 
            this.btnTrial.Location = new System.Drawing.Point(412, 50);
            this.btnTrial.Margin = new System.Windows.Forms.Padding(6);
            this.btnTrial.Name = "btnTrial";
            this.btnTrial.Size = new System.Drawing.Size(198, 44);
            this.btnTrial.TabIndex = 2;
            this.btnTrial.Text = "Trial Run";
            this.btnTrial.UseVisualStyleBackColor = true;
            this.btnTrial.Click += new System.EventHandler(this.btnTrial_Click);
            // 
            // grbRegister
            // 
            this.grbRegister.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.grbRegister.Controls.Add(this.lblRegisteredTo);
            this.grbRegister.Controls.Add(this.txtRegisteredName);
            this.grbRegister.Controls.Add(this.lblSerial);
            this.grbRegister.Controls.Add(this.lblID);
            this.grbRegister.Controls.Add(this.lblCallPhone);
            this.grbRegister.Controls.Add(this.sebBaseString);
            this.grbRegister.Controls.Add(this.btnOK);
            this.grbRegister.Controls.Add(this.sebPassword);
            this.grbRegister.Location = new System.Drawing.Point(24, 110);
            this.grbRegister.Margin = new System.Windows.Forms.Padding(6);
            this.grbRegister.Name = "grbRegister";
            this.grbRegister.Padding = new System.Windows.Forms.Padding(6);
            this.grbRegister.Size = new System.Drawing.Size(634, 446);
            this.grbRegister.TabIndex = 1;
            this.grbRegister.TabStop = false;
            this.grbRegister.Text = "Registration Info";
            // 
            // lblSerial
            // 
            this.lblSerial.AutoSize = true;
            this.lblSerial.Location = new System.Drawing.Point(18, 194);
            this.lblSerial.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblSerial.Name = "lblSerial";
            this.lblSerial.Size = new System.Drawing.Size(73, 25);
            this.lblSerial.TabIndex = 3;
            this.lblSerial.Text = "Serial:";
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.Location = new System.Drawing.Point(18, 112);
            this.lblID.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(38, 25);
            this.lblID.TabIndex = 1;
            this.lblID.Text = "ID:";
            // 
            // lblCallPhone
            // 
            this.lblCallPhone.AutoSize = true;
            this.lblCallPhone.Location = new System.Drawing.Point(12, 40);
            this.lblCallPhone.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblCallPhone.Name = "lblCallPhone";
            this.lblCallPhone.Size = new System.Drawing.Size(630, 25);
            this.lblCallPhone.TabIndex = 0;
            this.lblCallPhone.Text = "Please get the serial number for your application by providing ID.";
            // 
            // lblComment
            // 
            this.lblComment.Location = new System.Drawing.Point(24, 17);
            this.lblComment.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblComment.Name = "lblComment";
            this.lblComment.Size = new System.Drawing.Size(634, 83);
            this.lblComment.TabIndex = 0;
            this.lblComment.Text = "Before buying, you can run this application in trial mode. In trial mode some fea" +
                "tures may be unavailable.";
            // 
            // lblDaysToEnd
            // 
            this.lblDaysToEnd.AutoSize = true;
            this.lblDaysToEnd.Location = new System.Drawing.Point(18, 42);
            this.lblDaysToEnd.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblDaysToEnd.Name = "lblDaysToEnd";
            this.lblDaysToEnd.Size = new System.Drawing.Size(240, 25);
            this.lblDaysToEnd.TabIndex = 3;
            this.lblDaysToEnd.Text = "Days to end trial period:";
            // 
            // lblDays
            // 
            this.lblDays.AutoSize = true;
            this.lblDays.ForeColor = System.Drawing.Color.Red;
            this.lblDays.Location = new System.Drawing.Point(264, 42);
            this.lblDays.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblDays.Name = "lblDays";
            this.lblDays.Size = new System.Drawing.Size(73, 25);
            this.lblDays.TabIndex = 4;
            this.lblDays.Text = "[Days]";
            // 
            // lblRunTimesLeft
            // 
            this.lblRunTimesLeft.AutoSize = true;
            this.lblRunTimesLeft.Location = new System.Drawing.Point(18, 92);
            this.lblRunTimesLeft.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblRunTimesLeft.Name = "lblRunTimesLeft";
            this.lblRunTimesLeft.Size = new System.Drawing.Size(149, 25);
            this.lblRunTimesLeft.TabIndex = 5;
            this.lblRunTimesLeft.Text = "Run times left:";
            // 
            // lblTimes
            // 
            this.lblTimes.AutoSize = true;
            this.lblTimes.ForeColor = System.Drawing.Color.Red;
            this.lblTimes.Location = new System.Drawing.Point(264, 92);
            this.lblTimes.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTimes.Name = "lblTimes";
            this.lblTimes.Size = new System.Drawing.Size(82, 25);
            this.lblTimes.TabIndex = 6;
            this.lblTimes.Text = "[Times]";
            // 
            // grbTrialRunning
            // 
            this.grbTrialRunning.Controls.Add(this.lblDaysToEnd);
            this.grbTrialRunning.Controls.Add(this.lblRunTimesLeft);
            this.grbTrialRunning.Controls.Add(this.lblDays);
            this.grbTrialRunning.Controls.Add(this.lblTimes);
            this.grbTrialRunning.Controls.Add(this.btnTrial);
            this.grbTrialRunning.Location = new System.Drawing.Point(24, 567);
            this.grbTrialRunning.Margin = new System.Windows.Forms.Padding(6);
            this.grbTrialRunning.Name = "grbTrialRunning";
            this.grbTrialRunning.Padding = new System.Windows.Forms.Padding(6);
            this.grbTrialRunning.Size = new System.Drawing.Size(634, 142);
            this.grbTrialRunning.TabIndex = 8;
            this.grbTrialRunning.TabStop = false;
            this.grbTrialRunning.Text = "Trial Running";
            // 
            // txtRegisteredName
            // 
            this.txtRegisteredName.Location = new System.Drawing.Point(24, 308);
            this.txtRegisteredName.Name = "txtRegisteredName";
            this.txtRegisteredName.Size = new System.Drawing.Size(298, 31);
            this.txtRegisteredName.TabIndex = 6;
            // 
            // lblRegisteredTo
            // 
            this.lblRegisteredTo.AutoSize = true;
            this.lblRegisteredTo.Location = new System.Drawing.Point(24, 277);
            this.lblRegisteredTo.Name = "lblRegisteredTo";
            this.lblRegisteredTo.Size = new System.Drawing.Size(153, 25);
            this.lblRegisteredTo.TabIndex = 7;
            this.lblRegisteredTo.Text = "Registered To:";
            // 
            // frmDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 729);
            this.Controls.Add(this.lblComment);
            this.Controls.Add(this.grbTrialRunning);
            this.Controls.Add(this.grbRegister);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDialog";
            this.Text = "Regitration Dialog";
            this.grbRegister.ResumeLayout(false);
            this.grbRegister.PerformLayout();
            this.grbTrialRunning.ResumeLayout(false);
            this.grbTrialRunning.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private SerialBox.SerialBox sebBaseString;
        private SerialBox.SerialBox sebPassword;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnTrial;
        private System.Windows.Forms.GroupBox grbRegister;
        private System.Windows.Forms.Label lblCallPhone;
        private System.Windows.Forms.Label lblComment;
        private System.Windows.Forms.Label lblSerial;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label lblDaysToEnd;
        private System.Windows.Forms.Label lblDays;
        private System.Windows.Forms.Label lblRunTimesLeft;
        private System.Windows.Forms.Label lblTimes;
        private System.Windows.Forms.GroupBox grbTrialRunning;
        private System.Windows.Forms.TextBox txtRegisteredName;
        private System.Windows.Forms.Label lblRegisteredTo;

    }
}