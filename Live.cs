using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System.Diagnostics;

namespace FootDetection
{
    public partial class Live : Form
    {
        public Live()
        {
            InitializeComponent();
        }
        public Image<Bgr, Byte> LiveImage
        {
            get; set;
        }
        public Image<Bgr, Byte> ProcessedImage
        {
            get; set;
        }
        private void Live_Load(object sender, EventArgs e)
        {
            Application.Idle += new EventHandler(Idle);
        }
        
        private void Idle(object sender, EventArgs e)
        {
            imageBox_Before.Image = LiveImage;
            imageBox_After.Image = ProcessedImage;
        }

        private void Live_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
