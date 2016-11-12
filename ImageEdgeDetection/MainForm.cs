/*
 * The Following Code was developed by Dewald Esterhuizen
 * View Documentation at: http://softwarebydefault.com
 * Licensed under Ms-PL 
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace ImageEdgeDetection
{
    public partial class MainForm : Form , IUserInterface
    {
        private IOrchestrator orchestrator;
        private Bitmap showedImage = null;
        
        public MainForm()
        {
            InitializeComponent();
            this.orchestrator = new Orchestrator(this);
            cmbEdgeDetection.SelectedIndex = 0;
        }

        private void btnOpenOriginal_Click(object sender, EventArgs e)
        {
            orchestrator.loadOriginaleImage();
        }

        private void btnSaveNewImage_Click(object sender, EventArgs e)
        {
            orchestrator.saveResultImage();
        }

        private void NeighbourCountValueChangedEventHandler(object sender, EventArgs e)
        {
            orchestrator.performEdgeDetection();
        }


        public void displayImage(Image image)
        {
            picPreview.Image = image;
        }

        public void setEdgeSelection(string[] liste)
        {
            cmbEdgeDetection.Items.Clear();

            foreach (string current in liste)
            {
                cmbEdgeDetection.Items.Add(current);
            }
        }

        public string selectEdgeDetection()
        {
            return (string)cmbEdgeDetection.SelectedItem;
        }
    }
}
