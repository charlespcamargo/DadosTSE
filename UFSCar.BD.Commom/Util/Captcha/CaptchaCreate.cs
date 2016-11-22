using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UFSCar.BD.Commom.Util.Captcha
{
    /// <summary>
    /// Class can be used to validate whether a user is a user or a bot.
    /// </summary>
    /// <remarks></remarks>
    /// <dependencies>
    /// System.Drawing
    /// </dependencies>
    public class CaptchaCreate
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks></remarks>
        /// 

        public CaptchaCreate()
        {
        }

        /// <summary>
        /// Creates the skewed image with text and puts it into a memory stream.
        /// </summary>
        /// <param name="MemoryStream"></param>
        /// <remarks></remarks>
        public void CreateImage(ref System.IO.MemoryStream memoryStream)
        {
            _text = GetRandomText();
            Bitmap bitmap = new Bitmap(150, Width, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            Graphics g = default(Graphics);
            g = Graphics.FromImage(bitmap);
            Rectangle rect = new Rectangle(0, 0, 200, 30);
            int counter = 0;
            g.FillRectangle(FillColor, rect);
            int i = 0;
            for (i = 0; i <= _text.Length - 1; i++)
            {
                g.DrawString(_text.Substring(i, 1).ToString(), _font, GetRandomBrush(), new PointF(10 + counter, 5));
                counter += 20;
            }
            DrawRandomLines(g);
            bitmap.Save(memoryStream, ImageFormat.Jpeg);

            // Cleanup            
            g.Dispose();
            bitmap.Dispose();
        }

        /// <summary>
        /// Draws random lines via a graphics object.
        /// </summary>
        /// <param name="g"></param>
        /// <remarks></remarks>
        private void DrawRandomLines(Graphics g)
        {
            int i = 0;
            for (i = 0; i <= 10; i++)
            {
                g.DrawLine(new Pen(Color.Gray, 1), GetRandomPoint(), GetRandomPoint2());
            }
        }

        /// <summary>
        /// Gets a random point within the top half of the image boundaries
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private Point GetRandomPoint()
        {
            return new Point(_random.Next(0, 100), _random.Next(1, 25));
        }

        /// <summary>
        /// Gets a random point within the bottom half of the image boundaries
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private Point GetRandomPoint2()
        {
            return new Point(_random.Next(101, 200), _random.Next(26, 50));
        }

        /// <summary>
        /// Gets a random brush color
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private Brush GetRandomBrush()
        {
            switch (_random.Next(1, 5))
            {
                case 1:
                    return Brushes.Blue;
                case 2:
                    return Brushes.Black;
                case 3:
                    return Brushes.Red;
                case 4:
                    return Brushes.Green;
                case 5:
                    return Brushes.Maroon;
                default:
                    return Brushes.White;
            }
        }

        /// <summary>
        /// Gets random text.
        /// </summary>
        /// <returns></returns>
        /// <remarks></remarks>
        private string GetRandomText()
        {
            StringBuilder randomText = new StringBuilder();
            string characterSet = "abcdefghijkmnopqrstuvwxyz23456789";
            //Excluded l and 1 because they are similiar
            for (int counter = 0; counter <= Length; counter++)
            {
                randomText.Append(characterSet.Substring(_random.Next(characterSet.Length), 1));
            }
            return randomText.ToString();
        }


        //**********************************************************************************************
        //  Properties and class variables
        //**********************************************************************************************

        /// <summary>
        /// Random number generator.
        /// </summary>
        /// <remarks></remarks>

        private System.Random _random = new System.Random();
        private string _text = "";
        private int _length = 5;
        private int _width = 30;

        private Brush _FillColor = Brushes.White;
        /// <summary>
        /// Text to display on the image.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Currently, this is set to the value of the GetRandomText function when the CreateImage procedure is run.</remarks>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        private float _fontSize = 10;
        /// <summary>
        /// The font size to use on the image.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>The default font size is 10</remarks>
        public float FontSize
        {
            get { return _fontSize; }
            set { _fontSize = value; }
        }

        private Font _font = new Font("Courier New", 18, FontStyle.Bold);
        /// <summary>
        /// The font to use on the image.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>The default font is Courier New.</remarks>
        public Font Font
        {
            get { return _font; }
            set { _font = value; }
        }
        /// <summary>
        /// The number of characters in Captcha
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>The default length is 2</remarks>
        public int Length
        {
            get { return _length; }
            set { _length = value; }
        }
        /// <summary>
        /// Width of the Captcha image in px
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>The default width is 70</remarks>
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }
        /// <summary>
        /// Color of the background of the Captcha
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>The default is Dark Kahki</remarks>
        public Brush FillColor
        {
            get { return _FillColor; }
            set { _FillColor = value; }
        }
    }

    public class Captcha
    {
        public string CaptchaText;
        public string CaptchaImage { get; set; }
        public Captcha(string CaptchaText, string CaptchaImage)
        {
            this.CaptchaText = CaptchaText;
            this.CaptchaImage = CaptchaImage;
        }
    }

    public class CaptchaClass
    {
        public string CapText { get; set; }
        public string CapEncryptedKey { get; set; }
    }
}
