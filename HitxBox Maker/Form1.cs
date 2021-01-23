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
    public partial class Form1 : Form
    {
        private Image atlas; 
        private Graphics device;

        private int amount = 8;

        static public int frameHeight;
        
        static public int frameWidth;

        static public int posX, posY;

        private int fix = 280;
        
        public static float inc = 5.0f;

        private int count = -1;

        private int initialX, initialY;

        private bool isDown;

        private bool mode = true;

        private string animationName;

        private string ChampionName;

        List<HitBoxData> hitBoxDataArray = new List<HitBoxData>();

        List<bool> bools = new List<bool>();

        Rectangle tmp;

        public static int heighto;
        public static int widtho;

        public Form1()
        {
            InitializeComponent();
            MouseDown += new MouseEventHandler(Form1_MouseDown);
            MouseMove += new MouseEventHandler(Form1_MouseMove);
            MouseUp += new MouseEventHandler(Form1_MouseUp);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            device = CreateGraphics();

            InitHitBoxData();

            ofd.Filter = "PNG Image|*.png|JPEG Image|*.jpeg";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                atlas = Image.FromFile(ofd.FileName);
                animationName = Path.GetFileNameWithoutExtension(ofd.FileName);
                ChampionName = Path.GetFileName(Path.GetDirectoryName(ofd.FileName));
            }

            frameWidth = atlas.Width / amount;
            frameHeight = atlas.Height;

            posX = frameWidth - fix;
            posY = -200;

            WindowState = FormWindowState.Minimized;
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void Retrieve()
        {
            Refresh();
            
            if (count >= 0)
            {
                device.DrawImage(atlas, new RectangleF(new PointF(posX, posY), new SizeF(frameWidth * inc, frameHeight * inc)), new RectangleF(new PointF(frameWidth * count, 0), new SizeF(frameWidth, frameHeight)), GraphicsUnit.Pixel);

                Pen pen = new Pen(Color.LimeGreen, 1);

                foreach (Rectangle rect in hitBoxDataArray[count].GreenHitBoxes)
                    device.DrawRectangle(pen, rect);

                pen.Color = Color.Red;

                foreach (Rectangle rect in hitBoxDataArray[count].RedHitBoxes)
                    device.DrawRectangle(pen, rect);
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            isDown = true;
            initialX = e.X;
            initialY = e.Y;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDown == true)
            {
                Retrieve();
                Pen drawPen;
                if(mode) drawPen = new Pen(Color.LimeGreen, 1);
                else drawPen = new Pen(Color.Red, 1);

                tmp = new Rectangle(Math.Min(e.X, initialX),
                               Math.Min(e.Y, initialY),
                               Math.Abs(e.X - initialX),
                               Math.Abs(e.Y - initialY));

                device.DrawRectangle(drawPen, tmp);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            isDown = false;
        }

        void Form1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void btnMode_Click(object sender, EventArgs e)
        {
            mode = !mode;
        }

        private void btnUndo_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            count++;
            Attain();
            Retrieve();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            count--;
            if (count < 0) count = 0;
            Attain();
            Retrieve();
        }

        private void InitHitBoxData()
        {
            for (int i = 0; i < amount; i++) 
            {
                hitBoxDataArray.Add(new HitBoxData());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (count >= 0 && !hitBoxDataArray[count].RedHitBoxes.Contains(tmp) && !hitBoxDataArray[count].GreenHitBoxes.Contains(tmp))
            {
                if (mode) hitBoxDataArray[count].GreenHitBoxes.Add(tmp);
                else hitBoxDataArray[count].RedHitBoxes.Add(tmp);

                bools.Add(mode);
            }
        }

        private void btnUndo_Click(object sender, EventArgs e)
        {
            if (count >= 0)
            {
                if (bools.Any())
                {
                    if (hitBoxDataArray[count].GreenHitBoxes.Any())
                        if (bools.Last()) hitBoxDataArray[count].GreenHitBoxes.Remove(hitBoxDataArray[count].GreenHitBoxes.Last());

                    if (hitBoxDataArray[count].RedHitBoxes.Any())
                        if (!bools.Last()) hitBoxDataArray[count].RedHitBoxes.Remove(hitBoxDataArray[count].RedHitBoxes.Last());

                    bools.Remove(bools.Last());
                }
                Retrieve();
            }
            else
                Refresh();
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            string fileName = @"C:\Users\משתמש\Desktop\Game1\Game1\Hitboxes.json";
            
            if(File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            JObject jsonData = new JObject();
            JObject tmpObj;
            string[] dirPaths = Directory.GetDirectories(@"..\..\exports\");
            string[] tmp;
            foreach (string dirPath in dirPaths)
            {
                tmp = Directory.GetFiles(dirPath);
                tmpObj = new JObject();
                foreach (string filePath in tmp)
                {
                    tmpObj.Add(new JProperty(Path.GetFileNameWithoutExtension(filePath), JArray.Parse(File.ReadAllText(filePath))));
                }
                jsonData.Add(Path.GetFileName(dirPath), tmpObj);
            }
            
            File.WriteAllText(fileName, jsonData.ToString());
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            string fileName = @"..\..\exports\" + ChampionName + "\\" + animationName + ".json";

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

            int i = 0;
            JArray jsonData = new JArray();
            JObject tmpFrame;
            foreach (HitBoxData box in hitBoxDataArray)
            {
                tmpFrame = new JObject();
                tmpFrame.Add(new JProperty("Green", box.AquireGreenJsonData()));
                tmpFrame.Add(new JProperty("Red", box.AquireRedJsonData()));
                jsonData.Add(tmpFrame); 
            }

            File.WriteAllText(fileName, jsonData.ToString());
        }

        private void Attain()
        {
            count %= amount;
            lblFrame.Text = (count + 1).ToString();
        }
    }
}
