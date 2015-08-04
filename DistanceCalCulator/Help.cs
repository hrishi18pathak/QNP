using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace DistanceCalCulator
{
    public partial class Help : Form
    {
        public Help()
        {
            InitializeComponent();
        }

        private void aboutQuickNavPlannerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutQNP form = new AboutQNP();
            form.ShowDialog();
                                   
        }

        private string GetHelpFileUrl()
        {
            string parentDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string currDirectory = Path.Combine(parentDirectory, "QNP_Help");
            string chmFileFullPath = Path.Combine(currDirectory, "QNP_Help.chm");
            return new Uri(chmFileFullPath).AbsoluteUri;
            //return "/QNP_Help/QNP_Help.chm";
        }

        private void helpDocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Help.ShowHelp(this, GetHelpFileUrl());   
        }

        private static void Showhelp()
        {
            throw new NotImplementedException();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Help.ShowHelp(this, GetHelpFileUrl()); 
        }

        private void registerProductToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Register_product form = new Register_product();
            form.ShowDialog();

        }

        private void technicalSupportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Technical_Support form = new Technical_Support();
            form.ShowDialog();
        }

        private void disclaimerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Disclaimer form = new Disclaimer();
            form.ShowDialog();

        }

       
    }
}
