using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace ElkMate.Web.Captcha
{
    public class CaptchaImage
    {
        private Font imgFont;
        private string imgFontname = "Arial";
        private float imgFontsize = 24;
        private FontStyle imgFontstyle = FontStyle.Bold;
        private int imgHeight = 200;
        private int imgWidth = 450;
        private int nPoints = 3;
        private string sKeyword = "";

        private Color foreColor = Color.Black;
        private Color backColor = Color.White;

        #region Properties

        public Color ForeColor
        {
            set { foreColor = value; }
        }

        public Color BackColor
        {
            set { backColor = value; }
        }

        public int Width
        {
            set { imgWidth = value; }
        }

        public int Height
        {
            set { imgHeight = value; }
        }

        public string FontName
        {
            set { imgFontname = value; }
        }

        public float FontSize
        {
            set { imgFontsize = value; }
        }

        public FontStyle FontStyle
        {
            set { imgFontstyle = value; }
        }

        

        public int Points
        {
            set { nPoints = value; }
        }

        public string Keyword
        {
            set { sKeyword = value; }
        }

        #endregion

        public Bitmap MakeCaptcha()
        {
            var randomGenerator = new Random(DateTime.Now.Millisecond);

            var bmp = new Bitmap(imgWidth, imgHeight, PixelFormat.Format16bppRgb555);

            var sFormat = new StringFormat
                              {
                                  Alignment = StringAlignment.Center,
                                  LineAlignment = StringAlignment.Center
                              };

            var g = Graphics.FromImage(bmp);


            SizeF size;
            var fontSize = imgFontsize + 1;
            Font font;

            try
            {
                font = new Font(imgFontname, imgFontsize);
                font.Dispose();
            }
            catch (Exception)
            {
                imgFontname = FontFamily.GenericSerif.Name;
            }


            var tempKey = "";

            for (var ii = 0; ii < sKeyword.Length; ii++)
            {
                tempKey = String.Concat(tempKey, sKeyword[ii].ToString());
                tempKey = String.Concat(tempKey, " ");
            }
            tempKey = tempKey.Trim();

            do
            {
                fontSize--;
                font = new Font(imgFontname, fontSize, imgFontstyle);
                size = g.MeasureString(tempKey, font);
            } while (size.Height > (0.8*bmp.Height) || size.Width > (bmp.Width - 2));

            imgFont = new Font(font.FontFamily, font.Size + 1, font.Style); // font;


            g.Clear(backColor); 
            g.SmoothingMode = SmoothingMode.HighSpeed; 


            var coef = bmp.Width/20;
            var drawed = 0;
            while (drawed < 10)
            {
                for (var i = 0; i < 20; i++)
                {
                    if (randomGenerator.Next(0, 2) == 1)
                    {
                        drawed++;
                        g.DrawLine(new Pen(foreColor, 2f), i*coef, 0, i*coef, bmp.Height);
                    }
                }
            }

            double degree;
            do
            {
                degree = randomGenerator.NextDouble();
            } while (!(degree > 0.02 && degree < 0.04));

            /*if (randomGenerator.Next(0, 2) == 1)
                BitmapFilter.Swirl(bmp, degree, true);
            else
                BitmapFilter.Swirl(bmp, -degree, true);*/

            

            var ps = new Point[nPoints];

            for (var ii = 0; ii < nPoints; ii++)
            {
                var x = randomGenerator.Next(bmp.Width);
                var y = randomGenerator.Next(bmp.Height);
                ps[ii] = new Point(x, y);
            }


            var posx = Convert.ToInt32((imgWidth - size.Width)/2) + 2;
            if (posx < 0) posx = 0;


            for (var l = 0; l < tempKey.Length; l++)
            {
                var tempSize = g.MeasureString(tempKey[l].ToString(), imgFont);
                var posy = (randomGenerator.Next((int) (tempSize.Height/2),
                                                 imgHeight - (int) (tempSize.Height - (int) (tempSize.Height/2))));
                posx += Convert.ToInt32(tempSize.Width/(1.7));


                g.DrawString(tempKey[l].ToString(),
                             imgFont,
                             new SolidBrush(foreColor),
                             posx,
                             posy,
                             sFormat);
            }


            

            /*BitmapFilter.Swirl(bmp,
                               (randomGenerator.Next(-1, 1) < 0 ? -1 : 1)*0.0015*(randomGenerator.Next(-1, 1) < 0 ? 1 : 2),
                               true);*/

            font.Dispose();
            g.Dispose();

            return bmp;
        }
    }
}