using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.IO;

namespace Stefan
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Image imgLeft;
        private Image imgRight;
        public MainWindow()
        {
            InitializeComponent();
            // Create images to hold the bitmaps
            imgLeft = new Image();
            imgRight = new Image();
            // Put the image controls in the scroll viewers to scroll larger images
            scrollLeft.Content = imgLeft;
            imgLeft.Stretch = Stretch.None;
            //scrollRight.Content = imgRight;
            //imgRight.Stretch = Stretch.None;
        }
        ///////////////////////////////////////////////
        //
        private string getOpenImageFilename()
        {
            string filename = null;
            // Configure open file dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            //dlg.FileName = "Document";
            dlg.DefaultExt = ".jpg";
            dlg.Filter =
            "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*";
            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();
            // Process open file dialog box results
            if (result == true)
            {
                filename = dlg.FileName;
            }
            return filename;
        }
        ///////////////////////////////////////////////
        // Function called by different processing buttons
        private void btnProcess_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BitmapSource bitmapIn;
                WriteableBitmap bitmapInGrey;
                WriteableBitmap bitmapOut;
                string imageFilename;
                // Get the name of the image file
                imageFilename = getOpenImageFilename();
                if (imageFilename == null)
                {
                    return;
                }
                // Load the image file
                bitmapIn = new BitmapImage(new Uri(imageFilename));
                // Create a greyscale image from the bitmap
                GreyImage testImage = new GreyImage(bitmapIn);
                // Create a writeable bitmap for a grey scale image of the original
                bitmapInGrey = new WriteableBitmap(testImage.ImageWidth,
                testImage.ImageHeight, 96, 96, PixelFormats.Gray8, null);
                // Apply the pixels to bitmap
                bitmapInGrey.WritePixels(new Int32Rect(0, 0, testImage.ImageWidth,
                testImage.ImageHeight), testImage.PixelData, testImage.ImageWidth, 0);
                // Process the image for the button that was clicked
                if (sender == btnNegative)
                {
                    // Create negative of the image
                    testImage.Negative();
                }
                // Create a writeable bitmap
                bitmapOut = new WriteableBitmap(testImage.ImageWidth,
                testImage.ImageHeight, 96, 96, PixelFormats.Gray8, null);
                // Apply the pixels to bitmap
                bitmapOut.WritePixels(new Int32Rect(0, 0, testImage.ImageWidth,
                testImage.ImageHeight), testImage.PixelData, testImage.ImageWidth, 0);
                // Add the original bitmap to the Image control to view it
                imgLeft.Source = bitmapInGrey;
                // Add the processed bitmap to the Image control to view it
                imgRight.Source = bitmapOut;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error occurred");
            }
        }

        private void btnOpen_Click_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                string imageFilename;
                // Get the name of the image file
                imageFilename = getOpenImageFilename();
                if (imageFilename == null)
                {
                    return;
                }
              
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error occurred");
            }
        }
    }
}
                    
