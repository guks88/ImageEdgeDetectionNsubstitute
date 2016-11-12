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
    public partial class MainForm : Form
    {
        private Bitmap originalBitmap = null;
        private Bitmap previewBitmap = null;
        private Bitmap resultBitmap = null;
        //Add an intermediate image to save the state after the filter and to pass it to the edge detection
        private Bitmap intermediateBitmap = null;

        public MainForm()
        {
            InitializeComponent();

            cmbEdgeDetection.SelectedIndex = 0;
            cmbFilter.SelectedIndex = 0;
            btnOpenOriginal_Click(null,null);
            //The image can't be saved at the beginning
            btnSaveNewImage.Enabled = false;
        }

        private void btnOpenOriginal_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select an image file.";
            ofd.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
            ofd.Filter += "|Bitmap Images(*.bmp)|*.bmp";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamReader streamReader = new StreamReader(ofd.FileName);
                originalBitmap = (Bitmap)Bitmap.FromStream(streamReader.BaseStream);
                streamReader.Close();

                previewBitmap = originalBitmap.CopyToSquareCanvas(picPreview.Width);
                picPreview.Image = previewBitmap;
                
                //Only the filters are available at the beginning
                cmbEdgeDetection.Enabled = false;
                cmbFilter.Enabled = true;

            }
        }

        private void btnSaveNewImage_Click(object sender, EventArgs e)
        {

            ApplyEdgeDetect(false);

            if (resultBitmap != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Specify a file name and file path";
                sfd.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
                sfd.Filter += "|Bitmap Images(*.bmp)|*.bmp";

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileExtension = Path.GetExtension(sfd.FileName).ToUpper();
                    ImageFormat imgFormat = ImageFormat.Png;

                    if (fileExtension == "BMP")
                    {
                        imgFormat = ImageFormat.Bmp;
                    }
                    else if (fileExtension == "JPG")
                    {
                        imgFormat = ImageFormat.Jpeg;
                    }

                    StreamWriter streamWriter = new StreamWriter(sfd.FileName, false);
                    //Line added to avoid an error : http://stackoverflow.com/questions/15571022/how-to-find-reason-for-generic-gdi-error-when-saving-an-image
                    Bitmap savedImage = new Bitmap(resultBitmap);
                    savedImage.Save(streamWriter.BaseStream, imgFormat);
                    streamWriter.Flush();
                    streamWriter.Close();

                    resultBitmap = null;
                }
            }
        }

        //Method simplified, because once a filter is applied the filters are disabled 
        private void ApplyFilter()
        {
            
            //cmbFilter instead of cmbEdgeDetection
            if (previewBitmap == null || cmbFilter.SelectedIndex == -1)
            {
                return;
            }

            Bitmap selectedSource = null;
            Bitmap bitmapResult = null;

            
                selectedSource = originalBitmap;
           

            if (selectedSource != null)
            {
                bitmapResult = ImageFilters.ApplyRegistredFilter(selectedSource,cmbFilter.SelectedItem.ToString());
            }

            if (bitmapResult != null)
            {
               
                    picPreview.Image = bitmapResult;
                    intermediateBitmap = bitmapResult;
               
            }

            //Disabling the filters and enabling the edge detection
            cmbEdgeDetection.Enabled = true;
            cmbFilter.Enabled = false;

            //From here, saving the image is possible, not loading a new one 
            btnSaveNewImage.Enabled = true;
            btnOpenOriginal.Enabled = false;



        }

        private void ApplyEdgeDetect(bool preview)
        {
            

            if (previewBitmap == null || cmbEdgeDetection.SelectedIndex == -1)
            {
                return;
            }

            Bitmap selectedSource = null;
            Bitmap bitmapResult = null;

            //We work with the intermediateBitmap coming from the filter
            if (preview == true)
            {
                selectedSource = intermediateBitmap;
            }
            else
            {
                selectedSource = intermediateBitmap;
            }

            if (selectedSource != null)
            {
                if (cmbEdgeDetection.SelectedItem.ToString() == "Edge Detection")
                {
                    bitmapResult = selectedSource;
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 3x3")
                {
                    bitmapResult = selectedSource.Laplacian3x3Filter(false);
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 3x3 Grayscale")
                {
                    bitmapResult = selectedSource.Laplacian3x3Filter(true);
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 5x5")
                {
                    bitmapResult = selectedSource.Laplacian5x5Filter(false);
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 5x5 Grayscale")
                {
                    bitmapResult = selectedSource.Laplacian5x5Filter(true);
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian of Gaussian")
                {
                    bitmapResult = selectedSource.LaplacianOfGaussianFilter();
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 3x3 of Gaussian 3x3")
                {
                    bitmapResult = selectedSource.Laplacian3x3OfGaussian3x3Filter();
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 3x3 of Gaussian 5x5 - 1")
                {
                    bitmapResult = selectedSource.Laplacian3x3OfGaussian5x5Filter1();
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 3x3 of Gaussian 5x5 - 2")
                {
                    bitmapResult = selectedSource.Laplacian3x3OfGaussian5x5Filter2();
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 5x5 of Gaussian 3x3")
                {
                    bitmapResult = selectedSource.Laplacian5x5OfGaussian3x3Filter();
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 5x5 of Gaussian 5x5 - 1")
                {
                    bitmapResult = selectedSource.Laplacian5x5OfGaussian5x5Filter1();
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Laplacian 5x5 of Gaussian 5x5 - 2")
                {
                    bitmapResult = selectedSource.Laplacian5x5OfGaussian5x5Filter2();
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Sobel 3x3")
                {
                    bitmapResult = selectedSource.Sobel3x3Filter(false);
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Sobel 3x3 Grayscale")
                {
                    bitmapResult = selectedSource.Sobel3x3Filter();
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Prewitt")
                {
                    bitmapResult = selectedSource.PrewittFilter(false);
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Prewitt Grayscale")
                {
                    bitmapResult = selectedSource.PrewittFilter();
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Kirsch")
                {
                    bitmapResult = selectedSource.KirschFilter(false);
                }
                else if (cmbEdgeDetection.SelectedItem.ToString() == "Kirsch Grayscale")
                {
                    bitmapResult = selectedSource.KirschFilter();
                }
            }

            if (bitmapResult != null)
            {
                if (preview == true)
                {
                    picPreview.Image = bitmapResult;
                }
                else
                {
                    resultBitmap = bitmapResult;
                }
            }
        }

        private void ChooseFilterEventHandler(object sender, EventArgs e)
        {
            ApplyFilter();
        }
        private void NeighbourCountValueChangedEventHandler(object sender, EventArgs e)
        {
            ApplyEdgeDetect(true);
        }

        private void picPreview_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
