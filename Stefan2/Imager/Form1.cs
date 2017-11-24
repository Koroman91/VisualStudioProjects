using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace Stefan
{
    public partial class Form1 : Form
    {
        Bitmap newBitmap;
        Image file; //slika objekt
        Boolean opened = false;
        int blurAmount = 1;
        float contrast = 0;
        float gamma = 1;
        float thresh = 128;
        private int x;





        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult dr = openFileDialog1.ShowDialog(); //ime varijable dr kao dialogresult

            if (dr == DialogResult.OK)
            {
                file = Image.FromFile(openFileDialog1.FileName);
                newBitmap = new Bitmap(openFileDialog1.FileName);
                pictureBox1.Image = file;
                opened = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult dr = saveFileDialog1.ShowDialog();

            if (dr == DialogResult.OK)
            {
                if (opened)
                {

                    if (saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.Length - 3).ToLower() == "bmp")
                    {
                        file.Save(saveFileDialog1.FileName, ImageFormat.Bmp);
                    }
                    if (saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.Length - 3).ToLower() == "jpg")
                    {

                        file.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
                    }
                    if (saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.Length - 3).ToLower() == "png")
                    {

                        file.Save(saveFileDialog1.FileName, ImageFormat.Png);
                    }
                    if (saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.Length - 3).ToLower() == "gif")
                    {

                        file.Save(saveFileDialog1.FileName, ImageFormat.Gif);
                    }
                }
                else
                {
                    MessageBox.Show("Morate otvoriti sliku!");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < newBitmap.Width; x++)
            {
                for (int y = 0; y < newBitmap.Height; y++)
                {
                    Color originalColor = newBitmap.GetPixel(x, y);

                    int grayScale = (int)((originalColor.R / 3) + (originalColor.G / 3) + (originalColor.B / 3));

                    Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);

                    newBitmap.SetPixel(x, y, newColor);
                }
            }

            pictureBox1.Image = newBitmap;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int x = blurAmount; x <= newBitmap.Width - blurAmount; x++)
            {
                for (int y = blurAmount; y <= newBitmap.Height - blurAmount; y++)
                {
                    try
                    {
                        Color prevX = newBitmap.GetPixel(x - blurAmount, y);
                        Color nextX = newBitmap.GetPixel(x + blurAmount, y);
                        Color prevY = newBitmap.GetPixel(x, y - blurAmount);
                        Color nextY = newBitmap.GetPixel(x, y + blurAmount);

                        int avgR = (int)((prevX.R + nextX.R + prevY.R + nextY.R) / 4);
                        int avgG = (int)((prevX.G + nextX.G + prevY.G + nextY.G) / 4);
                        int avgB = (int)((prevX.B + nextX.B + prevY.B + nextY.B) / 4);

                        newBitmap.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));

                    }
                    catch (Exception) { }
                }
            }

            pictureBox1.Image = newBitmap;
        }

        private void updateBlur(object sender, EventArgs e)
        {
            blurAmount = int.Parse(trackBar1.Value.ToString());
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            label2.Text = trackBar2.Value.ToString();

            pictureBox1.Image = AdjustBrightness(newBitmap, trackBar2.Value);
        }

        public static Bitmap AdjustBrightness(Bitmap Image, int Value)
        {
            Bitmap TempBitmap = Image;

            float FinalValue = (float)Value / 255.0f;

            Bitmap NewBitmap = new Bitmap(TempBitmap.Width, TempBitmap.Height);

            Graphics NewGraphics = Graphics.FromImage(NewBitmap);

            float[][] FloatColorMatrix ={
                new float[] {1, 0, 0, 0, 0},
                new float[] {0, 1, 0, 0, 0},
                new float[] {0, 0, 1, 0, 0},
                new float[] {0, 0, 0, 1, 0},
                new float[] {FinalValue, FinalValue, FinalValue, 1, 1}
        };

            ColorMatrix NewColorMatrix = new ColorMatrix(FloatColorMatrix);

            ImageAttributes Attributes = new ImageAttributes();

            Attributes.SetColorMatrix(NewColorMatrix);

            NewGraphics.DrawImage(TempBitmap, new Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height), 0, 0, TempBitmap.Width, TempBitmap.Height, GraphicsUnit.Pixel, Attributes);
            Attributes.Dispose();
            NewGraphics.Dispose();
            return NewBitmap;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int x = 0; x < newBitmap.Width; x++)
            {
                for (int y = 0; y < newBitmap.Height; y++)
                {
                    Color pixel = newBitmap.GetPixel(x, y);

                    int red = pixel.R;
                    int green = pixel.G;
                    int blue = pixel.B;

                    newBitmap.SetPixel(x, y, Color.FromArgb(255 - red, 255 - green, 255 - blue));
                }
            }
            pictureBox1.Image = newBitmap;


        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            label5.Text = trackBar3.Value.ToString();

            contrast = 0.04f * trackBar3.Value;

            Bitmap bm = new Bitmap(newBitmap.Width, newBitmap.Height);

            Graphics g = Graphics.FromImage(bm);
            ImageAttributes ia = new ImageAttributes();

            ColorMatrix cm = new ColorMatrix(new float[][] { 
                                             new float[]{contrast, 0f, 0f, 0f, 0f},
                                             new float[]{0f, contrast, 0f, 0f, 0f},
                                             new float[]{0f, 0f, contrast, 0f, 0f},
                                             new float[]{0f, 0f, 0f, 1f, 0f},
                                             new float[]{0.001f, 0.001f, 0.001f, 0f, 1f}});

            ia.SetColorMatrix(cm);
            g.DrawImage(newBitmap, new Rectangle(0, 0, newBitmap.Width, newBitmap.Height), 0, 0, newBitmap.Width, newBitmap.Height, GraphicsUnit.Pixel, ia);
            g.Dispose();
            ia.Dispose();
            pictureBox1.Image = bm;

        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {
            label7.Text = trackBar4.Value.ToString();

            gamma = 0.04f * trackBar4.Value;

            Bitmap bm = new Bitmap(newBitmap.Width, newBitmap.Height);

            Graphics g = Graphics.FromImage(bm);
            ImageAttributes ia = new ImageAttributes();

            ia.SetGamma(gamma);
            g.DrawImage(newBitmap, new Rectangle(0, 0, newBitmap.Width, newBitmap.Height), 0, 0, newBitmap.Width, newBitmap.Height, GraphicsUnit.Pixel, ia);
            g.Dispose();
            ia.Dispose();
            pictureBox1.Image = bm;

        }

        private void button6_Click(object sender, EventArgs e)
        {
            Bitmap bm = new Bitmap(newBitmap.Width, newBitmap.Height);
            {

                for (int x = 0; x < newBitmap.Height; ++x)
                {
                    for (int y = 0; y < newBitmap.Width; ++y)
                    {
                        Color pixel = newBitmap.GetPixel(y, x);

                        double magnitude = 1 / 3d * (pixel.B + pixel.G + pixel.R);

                        if (magnitude < thresh)
                        {
                            newBitmap.SetPixel(y, x, Color.FromArgb(0, 0, 0));
                        }
                        else
                        {
                            newBitmap.SetPixel(y, x, Color.FromArgb(255, 255, 255));
                        }
                    }
                    pictureBox1.Image = newBitmap;
                }


            }
        }
        private void Test()
        {
            Bitmap bm = new Bitmap(newBitmap.Width, newBitmap.Height);
            int[] histogram_r = new int[256];
            float max = 0;

            for (int x = 0; x < newBitmap.Width; x++)
            {
                for (int y = 0; y < newBitmap.Height; y++)
                {
                    int redValue = newBitmap.GetPixel(x, y).R;
                    histogram_r[redValue]++;
                    if (max < histogram_r[redValue])
                        max = histogram_r[redValue];
                }
            }

            int histHeight = 128;
            Bitmap img = new Bitmap(256, histHeight + 10);
            using (Graphics g = Graphics.FromImage(img))
            {
                for (int x = 0; x < histogram_r.Length; x++)
                {
                    float pct = histogram_r[x] / max;   // What percentage of the max is this value?
                    g.DrawLine(Pens.Black,
                        new Point(x, img.Height - 5),
                        new Point(x, img.Height - 5 - (int)(pct * histHeight))  // Use that percentage of the height
                        );
                }
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            {

                Color pixel;
                int[,] r1 = new int[newBitmap.Width, newBitmap.Height];
                int[,] r2 = new int[newBitmap.Width, newBitmap.Height];
                int[,] g1 = new int[newBitmap.Width, newBitmap.Height];
                int[,] g2 = new int[newBitmap.Width, newBitmap.Height];
                int[,] b1 = new int[newBitmap.Width, newBitmap.Height];
                int[,] b2 = new int[newBitmap.Width, newBitmap.Height];

                for (int y = 0; y < newBitmap.Height; y++)
                {


                    for (int x = 0; x < newBitmap.Width; x++)
                    {
                        pixel = newBitmap.GetPixel(x, y);
                        r1[x, y] = (pixel.R + pixel.G + pixel.B);
                        g1[x, y] = (pixel.R + pixel.G + pixel.B);
                        b1[x, y] = (pixel.R + pixel.G + pixel.B);
                    }
                }

                int err_r;
                int err_g;
                int err_b;

                for (int y = 0; y < newBitmap.Height; y++)
                {


                    for (int x = 0; x < newBitmap.Width; x++)
                    {

                        if (g1[x, y] < 128)
                        {
                            g2[x, y] = 0;

                        }
                        else
                        {
                            g2[x, y] = 255;

                        }
                        if (r1[x, y] < 128)
                        {
                            r2[x, y] = 0;
                        }
                        else
                        {
                            r2[x, y] = 255;
                        }
                        if (b1[x, y] < 128)
                        {
                            b2[x, y] = 0;

                        }
                        else
                        {
                            b2[x, y] = 255;

                        }

                        err_r = r1[x, y] - r2[x, y];
                        err_g = g1[x, y] - g2[x, y];
                        err_b = b1[x, y] - b2[x, y];



                        if (x < newBitmap.Width - 1)
                        {
                            r1[x + 1, y] += err_r * 7 / 16;
                            g1[x + 1, y] += err_g * 7 / 16;
                            b1[x + 1, y] += err_b * 7 / 16;
                        }
                        if (y < newBitmap.Height - 1)
                        {
                            r1[x, y + 1] += err_r * 5 / 16;
                            g1[x, y + 1] += err_g * 5 / 16;
                            b1[x, y + 1] += err_b * 5 / 16;
                        }





                    }
                }

                for (int y = 0; y < newBitmap.Height; y++)
                {


                    for (int x = 0; x < newBitmap.Width; x++)
                    {
                        newBitmap.SetPixel(x, y, Color.FromArgb(r2[x, y], g2[x, y], b2[x, y]));

                    }
                }
                pictureBox1.Image = newBitmap;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            SaveFileDialog dr = new SaveFileDialog();
            dr.Filter = "jpg (*.jpg)|*.jpg|bmp (*.bmp)|*.bmp|png (*.png)|*.png";
            if (dr.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string fileExtension = Path.GetExtension(dr.FileName).ToUpper();
                ImageFormat imgFormat = ImageFormat.Png;

                if (fileExtension == "BMP")
                {
                    imgFormat = ImageFormat.Bmp;
                }
                else if (fileExtension == "JPG")
                {
                    imgFormat = ImageFormat.Jpeg;
                }

                StreamWriter streamWriter = new StreamWriter(dr.FileName, false);
                pictureBox1.Image.Save(streamWriter.BaseStream, imgFormat);
                streamWriter.Flush();
                streamWriter.Close();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int width = newBitmap.Width;
            int height = newBitmap.Height;
            Color p;
            Bitmap bm = new Bitmap(newBitmap.Width, newBitmap.Height);

            for (int y = 0; y < newBitmap.Height; y++)
            {
                for (int x = 0; x < newBitmap.Width; x++)
                {
                    p = newBitmap.GetPixel(x, y);

                    int r = p.R;
                    int g = p.G;
                    int b = p.B;

                    int tr = (int)(0.393 * r + 0.769 * g + 0.189 * b);
                    int tg = (int)(0.349 * r + 0.686 * g + 0.168 * b);
                    int tb = (int)(0.272 * r + 0.534 * g + 0.131 * b);

                    if (tr > 255)
                    {
                        r = 255;
                    }
                    else
                    {
                        r = tr;
                    }
                    if (tg > 255)
                    {
                        g = 255;
                    }
                    else
                    {
                        g = tg;
                    }
                    if (tb > 255)
                    {
                        b = 255;
                    }
                    else
                    {
                        b = tb;
                    }

                    newBitmap.SetPixel(x, y, Color.FromArgb(r, g, b));

                }
            }
            pictureBox1.Image = newBitmap;
        }

        
    }
}
        
    



        

       

       

     

        
        
 


