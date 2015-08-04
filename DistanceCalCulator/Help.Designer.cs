namespace DistanceCalCulator
{
    partial class Help
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Help));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.registerProductToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.technicalSupportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutQuickNavPlannerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disclaimerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.registerProductToolStripMenuItem,
            this.technicalSupportToolStripMenuItem,
            this.aboutQuickNavPlannerToolStripMenuItem,
            this.disclaimerToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(506, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(113, 20);
            this.toolStripMenuItem1.Text = "QNP User Manual";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // registerProductToolStripMenuItem
            // 
            this.registerProductToolStripMenuItem.Name = "registerProductToolStripMenuItem";
            this.registerProductToolStripMenuItem.Size = new System.Drawing.Size(106, 20);
            this.registerProductToolStripMenuItem.Text = "Register Product";
            this.registerProductToolStripMenuItem.Click += new System.EventHandler(this.registerProductToolStripMenuItem_Click);
            // 
            // technicalSupportToolStripMenuItem
            // 
            this.technicalSupportToolStripMenuItem.Name = "technicalSupportToolStripMenuItem";
            this.technicalSupportToolStripMenuItem.Size = new System.Drawing.Size(115, 20);
            this.technicalSupportToolStripMenuItem.Text = "Technical Support";
            this.technicalSupportToolStripMenuItem.Click += new System.EventHandler(this.technicalSupportToolStripMenuItem_Click);
            // 
            // aboutQuickNavPlannerToolStripMenuItem
            // 
            this.aboutQuickNavPlannerToolStripMenuItem.Name = "aboutQuickNavPlannerToolStripMenuItem";
            this.aboutQuickNavPlannerToolStripMenuItem.Size = new System.Drawing.Size(80, 20);
            this.aboutQuickNavPlannerToolStripMenuItem.Text = "About QNP";
            this.aboutQuickNavPlannerToolStripMenuItem.Click += new System.EventHandler(this.aboutQuickNavPlannerToolStripMenuItem_Click);
            // 
            // disclaimerToolStripMenuItem
            // 
            this.disclaimerToolStripMenuItem.Name = "disclaimerToolStripMenuItem";
            this.disclaimerToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.disclaimerToolStripMenuItem.Text = "Disclaimer";
            this.disclaimerToolStripMenuItem.Click += new System.EventHandler(this.disclaimerToolStripMenuItem_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Enabled = false;
            this.richTextBox1.Font = new System.Drawing.Font("Constantia", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.richTextBox1.Location = new System.Drawing.Point(0, 24);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.richTextBox1.Size = new System.Drawing.Size(506, 131);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = "        QNP  1.0";
            // 
            // Help
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(506, 155);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Help";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem registerProductToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem technicalSupportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutQuickNavPlannerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disclaimerToolStripMenuItem;
        private System.Windows.Forms.RichTextBox richTextBox1;

    }
}