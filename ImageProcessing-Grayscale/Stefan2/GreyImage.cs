using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// For bitmap handling:
using System.Windows.Media;
using System.Windows.Media.Imaging;
namespace Stefan
{
    class GreyImage
    {
        private int width = 0;
        private int height = 0;
        private byte[] pixels = null;
        ///////////////////////////////////////////////
        // Constructor
        public GreyImage(int Width, int Height, byte[] PixelArray)
        {
            width = Width;
            height = Height;
            pixels = new byte[width * height];
            pixels = PixelArray;
        }
        ///////////////////////////////////////////////
        // Constructor
        public GreyImage(BitmapSource bitmap)
        {
            // Convert the image to greyscale format
            if (bitmap.Format != PixelFormats.Gray8)
            {
                bitmap = new FormatConvertedBitmap(bitmap, PixelFormats.Gray8, null, 0);
            }
            // Allocate array to hold pixels
            width = bitmap.PixelWidth;
            height = bitmap.PixelHeight;
            pixels = new byte[bitmap.PixelWidth * bitmap.PixelHeight];
            // Copy pixels
            bitmap.CopyPixels(pixels, bitmap.PixelWidth, 0);
        }
        ///////////////////////////////////////////////
        // Width property
        public int ImageWidth
        {
            get { return this.width; }
        }
        ///////////////////////////////////////////////
        // Height property
        public int ImageHeight
        {
            get { return this.height; }
        }
        ///////////////////////////////////////////////
        // PixelData property
        public byte[] PixelData
        {
            get { return this.pixels.Clone() as byte[]; }
        }
        ///////////////////////////////////////////////
        //
        public void Negative()
        {
            // Loop through all pixels in the image and invert them
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int i = width * y + x;
                    pixels[i] = (byte)(255 - pixels[i]);
                }
            }
        }
    }
}