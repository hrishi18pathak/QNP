namespace DistanceCalCulator
{
    partial class Google_Earth_View
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Google_Earth_View));
            this.webBrowserCtrl = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowserCtrl
            // 
            this.webBrowserCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserCtrl.Location = new System.Drawing.Point(0, 0);
            this.webBrowserCtrl.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserCtrl.Name = "webBrowserCtrl";
            this.webBrowserCtrl.Size = new System.Drawing.Size(1284, 702);
            this.webBrowserCtrl.TabIndex = 1;
            // 
            // Google_Earth_View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1284, 702);
            this.Controls.Add(this.webBrowserCtrl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Google_Earth_View";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Google Earth View";
            this.Load += new System.EventHandler(this.Google_Earth_View_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowserCtrl;
    }
}