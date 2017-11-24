using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ImageProcessing
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            mag = new bmpfile_magic();
            mag.magic = new char[2];
            pic = new pix[512, 512];
            picOriginal = new pix[512, 512];
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            DeleteFilesNotInUse();
        }
        bmpfile_magic mag;         
        bmpfile_header fh;
        bmpifo_header fih;
        pix[,] pic, picOriginal;
        int[,] dct;
        double[,] COS;
        double[] histo;
        
        int n, m;
        string name;


        static bool FileInUse(string path)
        {
            string __message;
            try
            {
                //Just opening the file as open/create
                using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
                {
                    //If required we can check for read/write by using fs.CanRead or fs.CanWrite
                }
                return false;
            }
            catch (IOException ex)
            {
                //check if message is for a File IO
                __message = ex.Message.ToString();
                if (__message.Contains("The process cannot access the file"))
                    return true;
                else
                    throw;
            }
        }

        public void DeleteFilesNotInUse()
        {
            string [] fileEntries = Directory.GetFiles("Slike");
            string __message;
            foreach(string fileName in fileEntries){
                if(fileName.Contains("Processed")){
                    try
                    {
                        using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate))
                        {

                        }
                        File.Delete(fileName);
                    }
                     catch (IOException ex)
                     {
                        //check if message is for a File IO
                       __message = ex.Message.ToString();
                        if (!__message.Contains("The process cannot access the file"))
                            throw;
                    }    
                }
            }
        }

        #region READ/WRITE BMP FILE

        public struct bmpfile_magic {
            public char[] magic;           
        };

        public struct bmpifo_header {
            public Int32 header_sz;
            public Int32 width;
            public Int32 height;
            public Int16 nplanes;
            public Int16 bitspp;
            public Int32 compress_type;
            public Int32 bmp_bytesz;
            public Int32 hres;
            public Int32 vres;
            public Int32 ncolors;
            public Int32 nimpcolors;
        };

        public struct pix {
	        public int B, G, R;
        };

        public struct bmpfile_header {
                public Int32 filesz;
                public Int16 creator1;
                public Int16 creator2;
                public Int32 bmp_offset;
            };

        public void readBMPFile(string xx){
            pictureBox1.Image = null;
            FileStream slika=new FileStream("Slike\\"+xx, FileMode.Open);
            BinaryReader input = new BinaryReader((slika));
            
            mag.magic[0] = input.ReadChar();
            mag.magic[1] = input.ReadChar();

            fh.filesz = input.ReadInt32();
            fh.creator1 = input.ReadInt16();
            fh.creator2 = input.ReadInt16();
            fh.bmp_offset = input.ReadInt32();

            fih.header_sz=input.ReadInt32();
            fih.width=input.ReadInt32();
            fih.height=input.ReadInt32();
            fih.nplanes=input.ReadInt16();
            fih.bitspp=input.ReadInt16();
            fih.compress_type=input.ReadInt32();
            fih.bmp_bytesz=input.ReadInt32();
            fih.hres=input.ReadInt32();
            fih.vres=input.ReadInt32();
            fih.ncolors=input.ReadInt32();
            fih.nimpcolors=input.ReadInt32();

            if (fih.bitspp != 24)
            {
                MessageBox.Show("Picture is not 24 bpp!");
            }

            int pad = fih.width % 4;
            char[] pp = new char[3];
            n = fih.height;
            m = fih.width;
            for (int i = 0; i < fih.height; i++)
            {
                for (int j = 0; j < fih.width; j++)
                {
                    pic[i, j].B = (int)input.ReadByte();                    
                    pic[i, j].G = (int)input.ReadByte();
                    pic[i, j].R = (int)input.ReadByte();

                    picOriginal[i, j].B = pic[i, j].B;
                    picOriginal[i, j].G = pic[i, j].G;
                    picOriginal[i, j].R = pic[i, j].R;
                }
                for (int k = 0; k < pad; k++)
                {
                    pp[k] = input.ReadChar();
                }
            }
            input.Close();
            slika.Dispose();
            
        }

        public void writeBMPFile(string yy, char original='n', char sa='n'){
            yy = "Slike\\" + yy;
            pictureBox1.Image.Dispose();
            while (FileInUse(yy))
                yy = yy.Substring(0,yy.Length-4) + "Processed1" + ".bmp";
            FileStream fs = new FileStream(yy, FileMode.OpenOrCreate);//, FileAccess.ReadWrite, FileShare.ReadWrite, 512, FileOptions.DeleteOnClose);           
            //FileStream fs = new FileStream(yy, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite, 500000,);

            using (BinaryWriter output = new BinaryWriter(fs))
            {

                output.Write(mag.magic[0]);
                output.Write(mag.magic[1]);

                output.Write(fh.filesz);
                output.Write(fh.creator1);
                output.Write(fh.creator2);
                output.Write(fh.bmp_offset);

                output.Write(fih.header_sz);
                output.Write(fih.width);
                output.Write(fih.height);
                output.Write(fih.nplanes);
                output.Write(fih.bitspp);
                output.Write(fih.compress_type);
                output.Write(fih.bmp_bytesz);
                output.Write(fih.hres);
                output.Write(fih.vres);
                output.Write(fih.ncolors);
                output.Write(fih.nimpcolors);

                int pad = fih.width % 4;

                for (int i = 0; i < fih.height; i++)
                {
                    for (int j = 0; j < fih.width; j++)
                    {
                        switch (original)
                        {
                            case 'y':
                                output.Write((byte)picOriginal[i, j].B);
                                output.Write((byte)picOriginal[i, j].G);
                                output.Write((byte)picOriginal[i, j].R);
                                break;
                            default:
                                output.Write((byte)pic[i, j].B);
                                output.Write((byte)pic[i, j].G);
                                output.Write((byte)pic[i, j].R);
                                break;
                        }
                    }
                    for (int k = 0; k < pad; k++)
                    {
                        output.Write((byte)'0');

                    }
                }
                
            }
            if(sa=='n')
                pictureBox1.Image = Image.FromFile(yy);
            

            fs.Dispose();
            DeleteFilesNotInUse();
        }
        #endregion

        public void colorNegative()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    pic[i, j].R = 255 - picOriginal[i, j].R;
                    pic[i, j].G = 255 - picOriginal[i, j].G;
                    pic[i, j].B = 255 - picOriginal[i, j].B;
                }
            }
        }

        public void grayNegative()
        {
            averaging();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    pic[i, j].R = 255 - pic[i, j].R;
                    pic[i, j].G = 255 - pic[i, j].G;
                    pic[i, j].B = 255 - pic[i, j].B;
                }
            }
        }

        public void component(char color)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    switch (color)
                    {
                        case 'r':
                            pic[i, j].R = pic[i, j].R;
                            pic[i, j].G = 0;
                            pic[i, j].B = 0;
                            break;
                        case 'g':
                            pic[i, j].G = pic[i, j].G;
                            pic[i, j].R = 0;
                            pic[i, j].B = 0;
                            break;
                        case 'b':
                            pic[i, j].B = pic[i, j].B;
                            pic[i, j].G = 0;
                            pic[i, j].R = 0;
                            break;
                        case 'c':
                            pic[i, j].R = 0;
                            pic[i, j].G = pic[i, j].G;
                            pic[i, j].B = pic[i, j].B;
                            break;
                        case 'm':
                            pic[i, j].G = 0;
                            pic[i, j].R = pic[i, j].R;
                            pic[i, j].B = pic[i, j].B;
                            break;
                        case 'y':
                            pic[i, j].B = 0;
                            pic[i, j].G = pic[i, j].G;
                            pic[i, j].R = pic[i, j].R;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void contrast(int c)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (picOriginal[i, j].R + c > 255)
                        pic[i, j].R = 255;
                    else if (picOriginal[i, j].R + c < 0)
                        pic[i, j].R = 0;
                    else pic[i, j].R = picOriginal[i, j].R + c;
                    if (picOriginal[i, j].G + c > 255)
                        pic[i, j].G = 255;
                    else if (picOriginal[i, j].R + c < 0)
                        pic[i, j].G = 0;
                    else pic[i, j].G = picOriginal[i, j].G + c;
                    if (picOriginal[i, j].B + c > 255)
                        pic[i, j].B = 255;
                    else if (picOriginal[i, j].R + c < 0)
                        pic[i, j].B = 0;
                    else pic[i, j].B = picOriginal[i, j].B + c;
                }
            }
        }

        public void threshlod(int c)
        {
            int ostatak;
            for (int i = 0; i < n-1; i++)
            {
                for (int j = 0; j < m-1; j++)
                {
                    if (pic[i, j].R > c)
                    {
                        ostatak = pic[i, j].R - 255;
                        pic[i, j].R = 255;                        
                    }
                    else
                    {
                        ostatak = pic[i, j].R;
                        pic[i, j].R = 0;
                    }
                    pic[i + 1, j].R += ostatak * 3 / 8;
                    pic[i, j + 1].R += ostatak * 3 / 8;
                    pic[i + 1, j + 1].R += ostatak * 2 / 8;

                    if (pic[i, j].G > c)
                    {
                        ostatak = pic[i, j].G - 255;
                        pic[i, j].G = 255;
                    }
                    else
                    {
                        ostatak = pic[i, j].G;
                        pic[i, j].G = 0;
                    }
                    pic[i + 1, j].G += ostatak * 3 / 8;
                    pic[i, j + 1].G += ostatak * 3 / 8;
                    pic[i + 1, j + 1].G += ostatak * 2 / 8;
                    
                    if (pic[i, j].B > c)
                    {
                        ostatak = pic[i, j].B - 255;
                        pic[i, j].B = 255;
                    }
                    else
                    {
                        ostatak = pic[i, j].B;
                        pic[i, j].B = 0;
                    }
                    pic[i + 1, j].B += ostatak * 3 / 8;
                    pic[i, j + 1].B += ostatak * 3 / 8;
                    pic[i + 1, j + 1].B += ostatak * 2 / 8;
                }                
            }
            for (int i = 0; i < n; i++)
            {
                if (pic[i, m - 1].R > c)
                {
                    pic[i, m - 1].R = 255;
                }
                else
                {
                    pic[i, m - 1].R = 0;
                }
                if (pic[n - 1, i].R > c)
                {
                    pic[n - 1, i].R = 255;
                }
                else
                {
                    pic[n - 1, i].R = 0;
                }

                if (pic[i, m-1].G > c)
                    {                        
                        pic[i, m-1].G = 255;
                    }
                    else
                    {
                        pic[i, m-1].G = 0;
                    }
                if (pic[n-1, i].G > c)
                {
                    pic[n -1, i].G = 255;
                }
                else
                {
                    pic[n-1, i].G = 0;
                }

                if (pic[i, m - 1].B > c)
                {
                    pic[i, m - 1].B = 255;
                }
                else
                {
                    pic[i, m - 1].B = 0;
                }
                if (pic[n - 1, i].B > c)
                {
                    pic[n - 1, i].B = 255;
                }
                else
                {
                    pic[n - 1, i].B = 0;
                }
            }
        }

        public void floydStainerg(int c)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (pic[i, j].R > c)
                        pic[i, j].R = 255;
                    else pic[i, j].R = 0;
                    if (pic[i, j].G > c)
                        pic[i, j].G = 255;
                    else pic[i, j].G = 0;
                    if (pic[i, j].B > c)
                        pic[i, j].B = 255;
                    else pic[i, j].B = 0;
                }
            }
        }

        public void gamma(double g)
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    pic[i, j].R = Convert.ToInt32(Math.Pow(pic[i, j].R, g));
                    pic[i, j].G = Convert.ToInt32(Math.Pow(pic[i, j].G, g));
                    pic[i, j].B = Convert.ToInt32(Math.Pow(pic[i, j].B, g));
                }
            }
        }

        public void averaging()
        {
            int average;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    average = (pic[i, j].R + pic[i, j].G + pic[i, j].B + 1) / 3;
                    pic[i, j].R = average;
                    pic[i, j].G = average;
                    pic[i, j].B = average;
                }
            }
        }

        public void smoothing(char c)
        {
            int[,] mask = new int[,] {{1,1,1},{1,1,1},{1,1,1}};
            int delioc=9;
            int i, j;
            switch(c){
                case 'a': 
                    mask = new int[,] {{1,1,1},{1,1,1},{1,1,1}};
                    delioc = 9;
                    break;
                case 'k':
                    mask = new int[,] { { 1, 2, 1 }, { 2, 4, 2 }, { 1, 2, 1 } };
                    delioc = 16;
                    break;
                default:
                    break;
            }
            int sumaR, sumaG, sumaB;
            for (i = 1; i < n-2; i++)
            {
                for (j = 1; j < m-2; j++)
                {
                    sumaB = 0;
                    sumaG = 0;
                    sumaR = 0;
                    for (int k = i; k < i+3; k++)
                    {                        
                        for (int l = j; l < j+3; l++)
                        {
                            sumaR += pic[k, l].R * mask[k-i,l-j];
                            sumaG += pic[k, l].G * mask[k - i, l - j];
                            sumaB += pic[k, l].B * mask[k - i, l - j];
                        }
                    }

                    sumaR = sumaR / delioc;
                    sumaG = sumaG / delioc;
                    sumaB = sumaB / delioc;
                    for (int k = i; k < 3+i; k++)
                    {
                        for (int l = j; l < 3+j; l++)
                        {
                            pic[k, l].R = sumaR;
                            pic[k, l].G = sumaG;
                            pic[k, l].B = sumaB;
                        }
                    }

                }
            }
            //ivice smooth
            for (i = 1; i < n-1; i++)
            {
                pic[i, 0].R = (pic[i - 1, 0].R + pic[i, 0].R + pic[i + 1, 0].R + pic[i - 1, 1].R + pic[i, 1].R + pic[i + 1, 1].R) / 6;
                pic[i, m-1].R = (pic[i - 1, m-1].R + pic[i, m-1].R + pic[i + 1, m-1].R + pic[i - 1, m-2].R + pic[i, m-2].R + pic[i + 1, m-2].R) / 6;
                pic[i, 0].G = (pic[i - 1, 0].G + pic[i, 0].G + pic[i + 1, 0].G + pic[i - 1, 1].G + pic[i, 1].G + pic[i + 1, 1].G) / 6;
                pic[i, m - 1].G = (pic[i - 1, m - 1].G + pic[i, m - 1].G + pic[i + 1, m - 1].G + pic[i - 1, m - 2].G + pic[i, m - 2].G + pic[i + 1, m - 2].G) / 6;
                pic[i, 0].B = (pic[i - 1, 0].B + pic[i, 0].B + pic[i + 1, 0].B + pic[i - 1, 1].B + pic[i, 1].B + pic[i + 1, 1].B) / 6;
                pic[i, m - 1].B = (pic[i - 1, m - 1].B + pic[i, m - 1].B + pic[i + 1, m - 1].B + pic[i - 1, m - 2].B + pic[i, m - 2].B + pic[i + 1, m - 2].B) / 6;
            }

            for (i = 1; i < m-1; i++)
            {
                pic[0, i].R = (pic[0, i-1].R + pic[0, i].R + pic[0, i+1].R + pic[1, i-1].R + pic[1, i].R + pic[1, 1+i].R) / 6;
                pic[n - 1, i].R = (pic[n - 1, i - 1].R + pic[n - 1, i].R + pic[n - 1, i + 1].R + pic[n - 2, i - 1].R + pic[n - 2, i].R + pic[n - 2, i + 1].R) / 6;
                pic[i, 0].G = (pic[0, i - 1].G + pic[0, i].G + pic[0, i + 1].G + pic[1, i - 1].G + pic[1, i].G + pic[1, i + 1].G) / 6;
                pic[i, m - 1].G = (pic[n - 1, i - 1].G + pic[n - 1, i].G + pic[n - 1, i + 1].G + pic[n - 2, i - 1].G + pic[i, m - 2].G + pic[i + 1, m - 2].G) / 6;
                pic[i, 0].B = (pic[0, i - 1].B + pic[0, i].B + pic[0, i + 1].B + pic[1, i - 1].B + pic[1, i].B + pic[1, i + 1].B) / 6;
                pic[i, m - 1].B = (pic[n - 1, i - 1].B + pic[n - 1, i].B + pic[n - 1, i + 1].B + pic[n - 2, i - 1].B + pic[n - 2, i].B + pic[n - 2, i + 1].B) / 6;
            }
        }

        public void media()
        {
            
            int[] nizR=new int[9];
            int[] nizG = new int[9];
            int[] nizB = new int[9];
            for (int i = 0; i < n - 2; i++)
            {
                for (int j = 0; j < m - 2; j++)
                {
                    int s = 0;
                    for (int k = i; k < i + 3; k++)
                    {
                        for (int l = j; l < j + 3; l++)
                        {                            
                            nizR[s] = pic[k, l].R;
                            nizG[s] = pic[k, l].G;
                            nizB[s] = pic[k, l].B;
                            s++;
                        }
                    }                    
                    Array.Sort(nizR);
                    Array.Sort(nizG);
                    Array.Sort(nizB);

                    pic[i+1,j+1].R = nizR[4];
                    pic[i+1, j+1].G = nizG[4];
                    pic[i+1, j+1].B = nizB[4];

                }
            }            
        }

        public void unsharpHighBust(int koef)
        {
            smoothing('a');
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    pic[i, j].R = picOriginal[i, j].R - pic[i, j].R;
                    pic[i, j].G = picOriginal[i, j].G - pic[i, j].G;
                    pic[i, j].B = picOriginal[i, j].B - pic[i, j].B;
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (picOriginal[i, j].R + koef*pic[i, j].R > 255)
                        pic[i, j].R = 255;
                    else if (picOriginal[i, j].R + koef*pic[i, j].R < 0)
                        pic[i, j].R = 0;
                    else pic[i, j].R = picOriginal[i, j].R + koef*pic[i, j].R;
                    if (picOriginal[i, j].G + koef*pic[i, j].G > 255)
                        pic[i, j].G = 255;
                    else if (picOriginal[i, j].G + koef*pic[i, j].G < 0)
                        pic[i, j].G = 0;
                    else pic[i, j].G = picOriginal[i, j].G + koef*pic[i, j].G;
                    if (picOriginal[i, j].B + koef*pic[i, j].B > 255)
                        pic[i, j].B = 255;
                    else if (picOriginal[i, j].B + koef*pic[i, j].B < 0)
                        pic[i, j].B = 0;
                    else pic[i, j].B = picOriginal[i, j].B + koef*pic[i, j].B;
                }
            }
        }

        public void hystogram()
        {
            pix[] hystogram = new pix[256];

            for (int i = 0; i < 256; i++)
            {
                hystogram[i].R = 0;
                hystogram[i].G = 0;
                hystogram[i].B = 0;
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    hystogram[pic[i, j].R].R++;
                    hystogram[pic[i, j].G].G++;
                    hystogram[pic[i, j].B].B++;
                }
            }

        }

        public void firstDerivates()
        {
            for (int i = 0; i < n-1; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    pic[i, j].R = pic[i + 1, j].R - pic[i, j].R;
                    pic[i, j].G = pic[i + 1, j].G - pic[i, j].G;
                    pic[i, j].B = pic[i + 1, j].B - pic[i, j].B;
                }
            }
        }

        public void secondDerivates()
        {
            for (int i = 1; i < n - 1; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    pic[i, j].R = pic[i + 1, j].R + pic[i-1,j].R - 2 * pic[i, j].R;
                    pic[i, j].G = pic[i + 1, j].G + pic[i-1,j].G - 2 * pic[i, j].G;
                    pic[i, j].B = pic[i + 1, j].B + pic[i-1,j].B - 2 * pic[i, j].B;
                }
            }
        }

        public void Laplacian()
        {
            for (int i = 1; i < n - 1; i++)
            {
                for (int j = 1; j < m-1; j++)
                {
                    pic[i, j].R = pic[i,j].R-(pic[i + 1, j].R + pic[i - 1, j].R + pic[i, j+1].R+pic[i,j-1].R)+4*pic[i,j].R;
                    if (pic[i, j].R > 255) pic[i, j].R = 255;
                    else if (pic[i, j].R < 0) pic[i, j].R = 0;
                    pic[i, j].G = pic[i, j].G - (pic[i + 1, j].G + pic[i - 1, j].G + pic[i, j + 1].G + pic[i, j - 1].G) + 4 * pic[i, j].G;
                    if (pic[i, j].G > 255) pic[i, j].G = 255;
                    else if (pic[i, j].G < 0) pic[i, j].G = 0;
                    pic[i, j].B = pic[i, j].B - (pic[i + 1, j].B + pic[i - 1, j].B + pic[i, j + 1].B + pic[i, j - 1].B )+ 4 * pic[i, j].B;
                    if (pic[i, j].B > 255) pic[i, j].B = 255;
                    else if (pic[i, j].B < 0) pic[i, j].B = 0;
                }
            }
        }

        public double c(int i)
        {
            if (i == 0) return 1 / Math.Sqrt(2);
            else return 1;
        }

        public void racCos()
        {
            COS = new double[8, 8];
            int i, j;
            for (i = 0; i < 8; i++)
            {
                for (j = 0; j < 8; j++)
                    COS[i,j] = Math.Cos((2 * i + 1) * j * Math.Acos(-1) / 16.0);
            }
        }

        public void DCT()
        {
            averaging();
            dct = new int[512, 512];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    pic[i, j].R -= 128;
                    pic[i, j].G -= 128;
                    pic[i, j].B -= 128;
                }
            }

            /*for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    dct[i, j] = sumMatrix(i, j);
                }
            }*/
            racCos();
            int r, x, y;
            for (r = 0; r < 64; r++)
                for (int l = 0; l < 64; l++)
                    for (int i = 0; i < 8; i++)
                        for (int j = 0; j < 8; j++)
                        {
                            double sum = 0;
                            for (x = 0; x < 8; x++)
                                for (y = 0; y < 8; y++)
                                    sum += pic[r * 8 + x, l * 8 + y].R * COS[x, i] * COS[y, j];
                            sum *= c(i) * c(j) * 0.25;
                            dct[r * 8 + i, l * 8 + j] = (int)sum;
                        }
						

           
        }

        public void inverzDCT()
        {
            /*for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    pic[i,j].R = inverzSum(i, j);
                    pic[i, j].B = inverzSum(i, j);
                    pic[i, j].G = inverzSum(i, j);
                }
            }*/
            int r, l, i, j, x, y;
            for (r = 0; r < 64; r++)
                for (l = 0; l < 64; l++)
                    for (i = 0; i < 8; i++)
                        for (j = 0; j < 8; j++)
                        {
                            double sum = 0;
                            for (x = 0; x < 8; x++)
                                for (y = 0; y < 8; y++)
                                    sum += c(x) * c(y) * dct[r * 8 + x, l * 8 + y] * COS[i, x] * COS[j, y];
                            sum *= 0.25;
                            sum += 128;

                            pic[r * 8 + i, l * 8 + j].R = (int)sum;
                            pic[r * 8 + i, l * 8 + j].G = (int)sum;
                            pic[r * 8 + i, l * 8 + j].B = (int)sum;
                        }
            
            
        }

        public void lowpass(int c)
        {
            //averaging();
            racCos();
            DCT();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if ((i%8 + j%8) > c) dct[i, j] = 0;
                }
            }
            inverzDCT();
            MessageBox.Show("Done");
        }
        public void highpass(int c)
        {
            //averaging();
            racCos();
            DCT();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if ((i % 8 + j % 8) < c) dct[i, j] = 0;
                }
            }
            inverzDCT();
            MessageBox.Show("Done");
        }
        public void bandpass(int c1, int c2)
        {
            //averaging();
            racCos();
            DCT();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if ((i % 8 + j % 8) < c1 && (i % 8 + j % 8) > c2) dct[i, j] = 0;
                }
            }
            inverzDCT();
            MessageBox.Show("Done");
        }


        private void DrawOriginal()
        {
            averaging();
            System.Drawing.Pen p;
            p = new System.Drawing.Pen(Color.Black);
            histo = new double[256];
            double d = 1.0 / (double)m / (double)n;            
            System.Drawing.Graphics g = pictureBoxOriginal.CreateGraphics();
            
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    histo[pic[i, j].R]+=d;
                }
            }
            
            int y = pictureBoxOriginal.Height - 2;
            double min = histo.Min();
            double max = histo.Max();
            p.Width = 1;
            for (int i = 0; i < 256; i++)
            {
                //histo[i] = ((histo[i]-min)/(max-min));
                //MessageBox.Show(histo[i].ToString());
                g.DrawLine(p, i *2+3, y, i *2+3, y-(int)(histo[i]*7000));
            }
            //pictureBoxOriginal.Refresh();
            

            p.Dispose();
            g.Dispose();
        }

        public void histogramEqualization()
        {
            double[] FH = new double[256];
            FH[0] = histo[0]*255;
            double s = histo[0];
            for (int i = 1; i < 256; i++)
            {
                s += histo[i];
                FH[i] = s*255;
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    pic[i, j].R = (int)(FH[pic[i, j].R]);
                    pic[i, j].G = (int)(FH[pic[i, j].R]);
                    pic[i, j].B = (int)(FH[pic[i, j].R]);
                }
            }

        }

        int[,] q50 ={{16, 11, 10, 16, 24, 40, 51, 61},
                    {12, 12, 14, 19, 26, 58, 60, 55},
                    {14, 13, 16, 24, 40, 57, 69, 56},
                    {14, 17, 22, 29, 51, 87, 80, 62},
                    {18, 22, 37, 56, 68, 109, 103, 77},
                    {24, 35, 55, 64, 81, 104, 113, 92},
                    {49, 64, 78, 87, 103, 121, 120, 101},
                    {72, 92, 95, 98, 112, 100, 103, 99}};
        int[,] q;
        public void JPEG(int K)
        {
            //racCos();
            DCT();
            q = new int[8, 8];
           for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    q[i, j] = q50[i,j];
           if(K>50)
           {
            int i, j;
            for (i = 0; i < 8; i++)
                for (j = 0; j < 8; j++)
                    q[i, j] = (int)Math.Round(q50[i, j] * ((100 - K) / 50.0));
            
           }

           else if(K<50)
           {
            int i, j;
            for (i = 0; i < 8; i++)
                for (j = 0; j < 8; j++)
                    q[i, j] = (int)Math.Round(q50[i, j] * (float)(50 / K));            
          }
           


            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (q[i % 8, j % 8] != 0)
                        dct[i, j] = dct[i, j] / q[i % 8, j % 8];
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (q[i % 8, j % 8]!=0)
                        dct[i, j] = dct[i, j] * q[i % 8, j % 8];
                }
            }
            inverzDCT();
            MessageBox.Show("Compressed");
        }
     

        pix[,] newPic;

        public void erozija()
        {
            bool imaCrno=false;
            newPic = new pix[n, m];
            for (int i = 0; i < n-3; i++)
            {
                for (int j = 0; j < m-3; j++)
                {
                    imaCrno=false;
                    newPic[i, j].R = 255;
                    newPic[i, j].G = 255;
                    newPic[i, j].B = 255;
                    for (int k = i; k < i+3; k++)
                    {
                        for (int l = j; l < j+3; l++)
                        {
                            if (pic[k, l].R == 0) imaCrno = true;
                            
                        }
                    }
                    if (imaCrno)
                    {
                        for (int k = i; k < i + 4; k++)
                        {
                            for (int l = j; l < j + 4; l++)
                            {
                                newPic[i, j].R = 0;
                                newPic[i, j].G = 0;
                                newPic[i, j].B = 0;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < n - 3; i++)
                for (int j = 0; j < m - 3; j++)
                {
                    pic[i, j].R = newPic[i, j].R;
                    pic[i, j].G = newPic[i, j].G;
                    pic[i, j].B = newPic[i, j].B;
                }
        }

        public void diletacija()
        {
            bool imaBelo = false;
            newPic = new pix[n, m];
            for (int i = 0; i < n - 3; i++)
            {
                for (int j = 0; j < m - 3; j++)
                {
                    imaBelo = false;
                    newPic[i, j].R = 0;
                    newPic[i, j].G = 0;
                    newPic[i, j].B = 0;
                    for (int k = i; k < i + 3; k++)
                    {
                        for (int l = j; l < j + 3; l++)
                        {
                            if (pic[k, l].R == 255) imaBelo = true;
                            
                        }
                    }
                    if (imaBelo)
                    {
                        for (int k = i; k < i + 4; k++)
                        {
                            for (int l = j; l < j + 4; l++)
                            {
                                newPic[i, j].R = 255;
                                newPic[i, j].G = 255;
                                newPic[i, j].B = 255;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < n - 3; i++)
                for (int j = 0; j < m - 3; j++)
                {
                    pic[i, j].R = newPic[i, j].R;
                    pic[i, j].G = newPic[i, j].G;
                    pic[i, j].B = newPic[i, j].B;
                }
        }

        public void opening()
        {
            erozija();
            diletacija();
        }

        public void closing()
        {
            diletacija();
            erozija();
        }

        //process
        private void button1_Click(object sender, EventArgs e)
        {
            
            
        }


        //reset
        private void button2_Click_1(object sender, EventArgs e)
        {            
            writeBMPFile(name,'y');
            for (int i = 0; i < 512; i++)
            {
                for (int j = 0; j < 512; j++)
                {
                    pic[i, j].R = picOriginal[i, j].R;
                    pic[i, j].G = picOriginal[i, j].G;
                    pic[i, j].B = picOriginal[i, j].B;
                }
            }
        }

        //open picture
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog selectPicture = new OpenFileDialog();
            
            //selectPicture.InitialDirectory = @"Slike";

            selectPicture.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Slike");
            selectPicture.Filter = "bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
            selectPicture.FilterIndex = 0;
            //selectPicture.RestoreDirectory = true;

            if (selectPicture.ShowDialog() == DialogResult.OK)
            {
                name = selectPicture.FileName;
                readBMPFile(name.Substring(name.LastIndexOf('\\') + 1));
                pictureBox1.Image = Image.FromFile(name);
                name = name.Substring(name.LastIndexOf('\\') + 1);
                DrawOriginal();
            }
        }
                
               
       

        private void sharpeningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //unsharp();
            //writeBMPFile(name.Substring(0,name.Length-4) + "Processed" + ".bmp");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteFilesNotInUse();
            this.Close();
            DeleteFilesNotInUse();
            
        }

        
        

        #region hide
        private void redToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void greenToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void blueToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void grayToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void colorToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void grayToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void smoothToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {


        }
        #endregion

        

        

        

        

        private void buttonGrey_Click(object sender, EventArgs e)
        {
            grayNegative();
            writeBMPFile(name.Substring(0, name.Length - 4) + "Processed" + ".bmp");
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            colorNegative();
            writeBMPFile(name.Substring(0, name.Length - 4) + "Processed" + ".bmp");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            media();
            writeBMPFile(name.Substring(0, name.Length - 4) + "Processed" + ".bmp");
        }

        private void buttonCyan_Click(object sender, EventArgs e)
        {
            component('c');
            writeBMPFile(name.Substring(0, name.Length - 4) + "Processed" + ".bmp");
        }

        private void buttonMagenta_Click(object sender, EventArgs e)
        {
            component('m');
            writeBMPFile(name.Substring(0, name.Length - 4) + "Processed" + ".bmp");
        }

        private void buttonYellow_Click(object sender, EventArgs e)
        {
            component('y');
            writeBMPFile(name.Substring(0, name.Length - 4) + "Processed" + ".bmp");
        }

        

        private void button3_Click(object sender, EventArgs e)
        {
            gamma(Convert.ToDouble(numericUpDown3.Value));
            writeBMPFile(name.Substring(0, name.Length - 4) + "Processed" + ".bmp");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DeleteFilesNotInUse();
        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonUnsharp_Click(object sender, EventArgs e)
        {
            unsharpHighBust(1);
            writeBMPFile(name.Substring(0, name.Length - 4) + "Processed" + ".bmp");
        }

     

        private void buttonFirst_Click(object sender, EventArgs e)
        {
            firstDerivates();
            writeBMPFile(name.Substring(0, name.Length - 4) + "Processed" + ".bmp");
        }

        private void buttonSecond_Click(object sender, EventArgs e)
        {
            secondDerivates();
            writeBMPFile(name.Substring(0, name.Length - 4) + "Processed" + ".bmp");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Laplacian();
            writeBMPFile(name.Substring(0, name.Length - 4) + "Processed" + ".bmp");
        }

        private void buttonHighPass_Click(object sender, EventArgs e)
        {
            highpass(8);
            writeBMPFile(name.Substring(0, name.Length - 4) + "Processed" + ".bmp");
        }

        private void buttonLowpass_Click(object sender, EventArgs e)
        {
            lowpass(5);
            writeBMPFile(name.Substring(0, name.Length - 4) + "Processed" + ".bmp");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            averaging();
            writeBMPFile(name.Substring(0, name.Length - 4) + "Processed" + ".bmp");
        }

        private void buttonBandpass_Click(object sender, EventArgs e)
        {
            bandpass(7,9);
            writeBMPFile(name.Substring(0, name.Length - 4) + "Processed" + ".bmp");
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {
            pictureBoxOriginal.Refresh();
            DrawOriginal();
        }

        private void buttonEqualization_Click(object sender, EventArgs e)
        {
           
            histogramEqualization();
            writeBMPFile(name.Substring(0, name.Length - 4) + "Processed" + ".bmp");

        }

        private void tabPage4_Layout(object sender, LayoutEventArgs e)
        {
        }

        private void tabPage4_Paint(object sender, PaintEventArgs e)
        {
            pictureBoxOriginal.Refresh();
            DrawOriginal();
        }

        private void buttonJPEG_Click(object sender, EventArgs e)
        {
            JPEG((int)numericUpDown5.Value);
            writeBMPFile(name.Substring(0, name.Length - 4) + "Processed" + ".bmp");
        }

        private void buttonErozija_Click(object sender, EventArgs e)
        {
            erozija();
            writeBMPFile(name.Substring(0, name.Length - 4) + "Processed" + ".bmp");
        }

        private void buttonDiletacija_Click(object sender, EventArgs e)
        {
            diletacija();
            writeBMPFile(name.Substring(0, name.Length - 4) + "Processed" + ".bmp");
        }

        private void buttonOpening_Click(object sender, EventArgs e)
        {

        }

    

      

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog selectPicture = new SaveFileDialog();

            //selectPicture.InitialDirectory = @"Slike";

            selectPicture.InitialDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "Slike");
            selectPicture.Filter = "bmp files (*.bmp)|*.bmp|All files (*.*)|*.*";
            selectPicture.FilterIndex = 0;
           
            string nameSave;
            if (selectPicture.ShowDialog() == DialogResult.OK)
            {
                nameSave = selectPicture.FileName;
                pictureBox1.Image.Save(nameSave);            
                
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
                      
            
        }


        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void buttonMatching_Click(object sender, EventArgs e)
        {

        }

        
        

        
    }
}
