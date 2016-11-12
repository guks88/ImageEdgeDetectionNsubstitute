using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ImageEdgeDetection
{
    public static class ImageFilters
    {
        public const string NIGHT = "Night Filter";
        public const string HELL = "Hell Filter";
        public const string MIAMI = "Miami Filter";
        public const string ZEN = "Zen Filter";
        public const string BLACK = "Black and White";
        public const string SWAP = "Swap Filter";
        public const string CRAZY = "Crazy Filter";
        public const string MEGA_GREEN = "MegaFilter Green";
        public const string MEGA_ORANGE = "MegaFilter Orange";
        public const string MEGA_PINK = "MegaFilter Pink";
        
        public static Bitmap ApplyRegistredFilter(Bitmap img, string filter)
        {
            switch (filter)
            {
                case NIGHT:
                    return ApplyFilter(img, 1, 1, 1, 25);
                case HELL:
                    return ApplyFilter(img, 1, 1, 10, 15);
                case MIAMI:
                    return ApplyFilter(img, 1, 1, 10, 1);
                case ZEN:
                    return ApplyFilter(img, 1, 10, 1, 1);
                case BLACK:
                    return BlackWhite(img);
                case SWAP:
                    return ApplyFilterSwap(img);
                case CRAZY:
                    System.Drawing.Image te = ApplyFilterSwapDivide(img, 1, 1, 2, 1);
                    return ApplyFilterSwap(new Bitmap(te));
                case MEGA_GREEN:
                    Color g = Color.Green;
                    return ApplyFilterMega(img, 230, 110, g);
                case MEGA_ORANGE:
                    Color o = Color.Orange;
                    return ApplyFilterMega(img, 230, 110, o);
                case MEGA_PINK:
                    Color p = Color.Pink;
                    return ApplyFilterMega(img, 230, 110, p);
            }
            return img;
        }
        //apply color filter at your own taste
        public static Bitmap ApplyFilter(Bitmap bmp, int alpha, int red, int blue, int green)
        {
            if(alpha<1||red<1||blue<1|| green<1)
                throw new ArgumentException();
            Bitmap temp = new Bitmap(bmp.Width, bmp.Height);


            for (int i = 0; i < bmp.Width; i++)
                for (int x = 0; x < bmp.Height; x++)
                    {
                        Color c = bmp.GetPixel(i, x);
                        Color cLayer = Color.FromArgb(c.A / alpha, c.R / red, c.G / green, c.B / blue);
                        temp.SetPixel(i, x, cLayer);
                    }
            return temp;
        }

        //black and white filter
        /*Modified by Zappellaz Nancy & Mabillard Julien: We had to change this method because it was returning bmp(so the original image was permanently changed) instead of temp , 
        moreover the variable was written with a capital letter...*/
        public static Bitmap BlackWhite(Bitmap bmp)
        {
            Bitmap temp = new Bitmap(bmp.Width, bmp.Height);
            int rgb;
            Color c;

            for (int y = 0; y < bmp.Height; y++)
                for (int x = 0; x < bmp.Width; x++)
                {
                    c = bmp.GetPixel(x, y);
                    rgb = (int)((c.R + c.G + c.B) / 3);
                    temp.SetPixel(x, y, Color.FromArgb(rgb, rgb, rgb));
                }
            return temp;

        }

        //apply color filter to swap pixel colors
        public static Bitmap ApplyFilterSwap(Bitmap bmp)
        {

            Bitmap temp = new Bitmap(bmp.Width, bmp.Height);


            for (int i = 0; i < bmp.Width; i++)
            {
                for (int x = 0; x < bmp.Height; x++)
                {
                    Color c = bmp.GetPixel(i, x);
                    Color cLayer = Color.FromArgb(c.A, c.G, c.B, c.R);
                    temp.SetPixel(i, x, cLayer);
                }

            }
            return temp;
        }

        //apply color filter to swap pixel colors
        public static Bitmap ApplyFilterSwapDivide(Bitmap bmp, int a, int r, int g, int b)
        {

            Bitmap temp = new Bitmap(bmp.Width, bmp.Height);


            for (int i = 0; i < bmp.Width; i++)
            {
                for (int x = 0; x < bmp.Height; x++)
                {
                    Color c = bmp.GetPixel(i, x);
                    Color cLayer = Color.FromArgb(c.A / a, c.G / g, c.B / b, c.R / r);
                    temp.SetPixel(i, x, cLayer);
                }

            }
            return temp;
        }

        //apply color filter to swap pixel colors
        public static Bitmap ApplyFilterMega(Bitmap bmp, int max, int min, Color co)
        {

            Bitmap temp = new Bitmap(bmp.Width, bmp.Height);

            for (int i = 0; i < bmp.Width; i++)
            {
                for (int x = 0; x < bmp.Height; x++)
                {

                    Color c = bmp.GetPixel(i, x);
                    if (c.G > min && c.G < max)
                    {
                        Color cLayer = Color.White;
                        temp.SetPixel(i, x, cLayer);
                    }
                    else
                    {
                        temp.SetPixel(i, x, co);
                    }

                }

            }
            return temp;
        }

        //apply magic mosaic
        public static Bitmap DivideCrop(Bitmap bmp)
        {
            int razX = Convert.ToInt32(bmp.Width / 3);
            int razY = Convert.ToInt32(bmp.Height / 3);

            Bitmap temp = new Bitmap(bmp.Width, bmp.Height);


            for (int i = 0; i < bmp.Width - 1; i++)
            {
                for (int x = 0; x < bmp.Height - 1; x++)
                {
                    if (i < razX && x < razY)
                    {
                        temp.SetPixel(i, x, bmp.GetPixel(i, x));
                    }
                    else if (i < (razX * 2) && x < (razY))
                    {
                        temp.SetPixel(i, x, bmp.GetPixel(x, i));
                    }
                    else if (i < (razX * 3) && x < (razY))
                    {
                        temp.SetPixel(i, x, bmp.GetPixel(i, x));
                    }
                    else if (i < (razX) && x < (razY * 2))
                    {
                        temp.SetPixel(i, x, bmp.GetPixel(x, i));
                    }
                    else if (i < (razX) && x < (razY * 3))
                    {
                        temp.SetPixel(i, x, bmp.GetPixel(i, x));
                    }
                    else if (i < (razX * 2) && x < (razY * 2))
                    {
                        temp.SetPixel(i, x, bmp.GetPixel(i, x));
                    }
                    else if (i < (razX * 4) && x < (razY * 1))
                    {
                        temp.SetPixel(i, x, bmp.GetPixel(i, x));
                    }
                    else if (i < (razX * 4) && x < (razY * 2))
                    {
                        temp.SetPixel(i, x, bmp.GetPixel(x / 2, i / 2));
                    }
                    else if (i < (razX * 4) && x < (razY * 3))
                    {
                        temp.SetPixel(i, x, bmp.GetPixel(x / 3, i / 3));
                    }

                }

            }
            return temp;
        }

    }
}
