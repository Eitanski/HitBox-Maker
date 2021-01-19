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

namespace HitxBox_Maker
{
    class HitBoxData
    {
        public List<Rectangle> GreenHitBoxes { get; set; } = new List<Rectangle>();
        public List<Rectangle> RedHitBoxes { get; set; } = new List<Rectangle>();

        public HitBoxData()
        {

        }

        public static void Document()
        {

        }

        public string GreenToString()
        {
            string res = "";

            float w = Form1.frameWidth * Form1.inc;
            float h = Form1.frameHeight * Form1.inc;

            if (GreenHitBoxes.Any())
                foreach (Rectangle rect in GreenHitBoxes)
                    res += "X: " + (float)(rect.X - Form1.posX) / w + " Y: " + (float)(rect.Y - Form1.posY) / h + " Width: " + (float)rect.Width / w + " Height: " + (float)rect.Height / h + '\n';

            return res;
        }

        public string RedToString()
        {
            string res = "";

            float w = Form1.frameWidth * Form1.inc;
            float h = Form1.frameHeight * Form1.inc;

            if (RedHitBoxes.Any())
                foreach (Rectangle rect in RedHitBoxes)
                    res += "X: " + (float)(rect.X - Form1.posX) / w + " Y: " + (float)(rect.Y - Form1.posY) / h + " Width: " + (float)rect.Width / w + " Height: " + (float)rect.Height / h + '\n';

            return res;
        }
}
}
