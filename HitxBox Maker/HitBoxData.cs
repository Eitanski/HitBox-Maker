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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        public JArray AquireGreenJsonData()
        {   
            JArray jsonData = new JArray();
            JObject tmp;

            float w = Form1.frameWidth * Form1.inc;
            float h = Form1.frameHeight * Form1.inc;

            if (GreenHitBoxes.Any())
                foreach (Rectangle rect in GreenHitBoxes)
                {
                    tmp = new JObject();
                    tmp.Add(new JProperty("X", (float)(rect.X - Form1.posX) / w));
                    tmp.Add(new JProperty("Y", (float)(rect.Y - Form1.posY) / h));
                    tmp.Add(new JProperty("W", (float)rect.Width / w));
                    tmp.Add(new JProperty("H", (float)rect.Height / h));
                    jsonData.Add(tmp);
                }

            return jsonData;
        }

        public JArray AquireRedJsonData()
        {
            JArray jsonData = new JArray();
            JObject tmp = new JObject();

            float w = Form1.frameWidth * Form1.inc;
            float h = Form1.frameHeight * Form1.inc;

            if (RedHitBoxes.Any())
                foreach (Rectangle rect in RedHitBoxes)
                {
                    tmp = new JObject();
                    tmp.Add(new JProperty("X", (float)(rect.X - Form1.posX) / w));
                    tmp.Add(new JProperty("Y", (float)(rect.Y - Form1.posY) / h));
                    tmp.Add(new JProperty("W", (float)rect.Width / w));
                    tmp.Add(new JProperty("H", (float)rect.Height / h));
                    jsonData.Add(tmp);
                }

            return jsonData;
        }
        public JArray AquireMirroredGreenJsonData()
        {
            JArray jsonData = new JArray();
            JObject tmp;

            float w = Form1.frameWidth * Form1.inc;
            float h = Form1.frameHeight * Form1.inc;

            int realX, realY;

            if (GreenHitBoxes.Any())
                foreach (Rectangle rect in GreenHitBoxes)
                {
                    realX = rect.X - Form1.posX;
                    realY = rect.Y - Form1.posY;

                    realX = (int)w - realX - rect.Width;

                    tmp = new JObject();
                    tmp.Add(new JProperty("X", (float)realX / w));
                    tmp.Add(new JProperty("Y", (float)realY / h));
                    tmp.Add(new JProperty("W", (float)rect.Width / w));
                    tmp.Add(new JProperty("H", (float)rect.Height / h));
                    jsonData.Add(tmp);
                }

            return jsonData;
        }

        public JArray AquireMirroredRedJsonData()
        {
            JArray jsonData = new JArray();
            JObject tmp = new JObject();

            float w = Form1.frameWidth * Form1.inc;
            float h = Form1.frameHeight * Form1.inc;

            int realX, realY;

            if (RedHitBoxes.Any())
                foreach (Rectangle rect in RedHitBoxes)
                {
                    realX = rect.X - Form1.posX;
                    realY = rect.Y - Form1.posY;

                    realX = (int)w - realX - rect.Width;
                    
                    tmp = new JObject();
                    tmp.Add(new JProperty("X", (float)realX / w));
                    tmp.Add(new JProperty("Y", (float)realY / h));
                    tmp.Add(new JProperty("W", (float)rect.Width / w));
                    tmp.Add(new JProperty("H", (float)rect.Height / h));
                    jsonData.Add(tmp);
                }

            return jsonData;
        }
    }
}
