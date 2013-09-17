using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

namespace SmartElk.ElkMate.Web.Captcha
{
    public class CaptchaModel
    {        
        private CaptchaModel(int length, int width, int height, Color foreColor, Color backColor)
        {
            if (length < 1 || length > 10)
                throw new ArgumentOutOfRangeException("length", length, "Length must be from 1 to 10");

            Code = Guid.NewGuid().ToString().Replace("-", "").Substring(0, length);
            
            Width = width;
            Height = height;
            ForeColor = foreColor;
            BackColor = backColor;            
        }

        public static CaptchaModel Create(int length, int width, int height)
        {
            return Create(length, width, height, Color.Green, Color.White);
        }

        public static CaptchaModel Create(int length, int width, int height, Color foreColor, Color backColor)
        {
            return new CaptchaModel(length, width, height, foreColor, backColor);
        }

        public string Code { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Color ForeColor { get; private set; }
        public Color BackColor { get; private set; }

        private const string SessionName = "_Captcha";
        
        public byte[] Image
        {
            get
            {
                var i = new CaptchaImage
                            {
                                Width = Width,
                                Height = Height,
                                Keyword = Code.ToUpper(),
                                ForeColor = ForeColor,
                                BackColor = BackColor
                            };

                using (Image captchaImage = i.MakeCaptcha())
                {
                    using (var s = new MemoryStream())
                    {
                        captchaImage.Save(s, ImageFormat.Png);

                        return s.ToArray();
                    }
                }
            }
        }

        public CaptchaModel Save()
        {
            HttpContext.Current.Session[SessionName] = this;
            return this;
        }
        
        public static bool Verify(string code)
        {            
            var captcha = HttpContext.Current.Session[SessionName] as CaptchaModel;

            if (captcha == null)
                return false;

            var expectedCode = captcha.Code.ToLower().Replace("0", "o");
            var actualCode = code.ToLower().Replace("0", "o").Trim();

            HttpContext.Current.Session[SessionName] = null;

            return expectedCode == actualCode;
        }

        public static CaptchaModel GetCurrent()
        {
            return HttpContext.Current.Session[SessionName] as CaptchaModel;            
        }
    }
}