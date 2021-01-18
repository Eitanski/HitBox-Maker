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

    }
}
