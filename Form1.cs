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
    
    public partial class Form1 : Form
    {
        VideoCapture CameraCapture;
        Image<Bgr, Byte> OriginalImage;
        Image<Hsv, byte> HSVImage;
        Image<Gray, byte> HSVMaskedImage;
        Image<Gray, Byte> FilteredImage;
        Image<Gray, byte> CannyImage;
        Image<Bgr, byte> DrawBound;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CameraCapture = new VideoCapture(2);
            Application.Idle += new EventHandler(Application_Idle);//Do things when UI is idle
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            OriginalImage = CameraCapture.QueryFrame().ToImage<Bgr, Byte>();
            imageBox1.Image = OriginalImage;


            HSVImage = OriginalImage.Convert<Hsv, byte>();          
            //imageBox2.Image = HSVImage;

            Hsv lowerLimit = new Hsv(trackBar_H_Low.Value, trackBar_S_Low.Value, trackBar_V_Low.Value); //50 80
            Hsv upperLimit = new Hsv(trackBar_H_Up.Value , trackBar_S_Up.Value, trackBar_V_Up.Value); //50 80            
            HSVMaskedImage = HSVImage.InRange(lowerLimit, upperLimit);
            imageBox2.Image = HSVMaskedImage;

            FilteredImage = HSVMaskedImage.SmoothMedian(25);
            imageBox3.Image = FilteredImage;

            CannyImage = FilteredImage.Clone();
            CvInvoke.Canny(FilteredImage, CannyImage, 255, 255, 5, true);
            //imageBox4.Image = CannyImage;
            DrawBound = OriginalImage.Clone();

            MyCV.MinAreaBoundingBox(CannyImage, DrawBound);
            imageBox4.Image = DrawBound;
            
        }

        private void TrackBar_H_Low_Scroll(object sender, EventArgs e)
        {
            if (trackBar_H_Low.Value > trackBar_H_Up.Value)
            {
                trackBar_H_Low.Value = trackBar_H_Up.Value;
            }
            label_H_Low.Text = trackBar_H_Low.Value.ToString();
        }

        private void TrackBar_S_Low_Scroll(object sender, EventArgs e)
        {
            if (trackBar_S_Low.Value > trackBar_S_Up.Value)
            {
                trackBar_S_Low.Value = trackBar_S_Up.Value;
            }
            label_S_Low.Text = trackBar_S_Low.Value.ToString();
        }

        private void TrackBar_V_Low_Scroll(object sender, EventArgs e)
        {
            if (trackBar_V_Low.Value > trackBar_V_Up.Value)
            {
                trackBar_V_Low.Value = trackBar_V_Up.Value;
            }
            label_V_Low.Text = trackBar_V_Low.Value.ToString();
        }

        private void TrackBar_H_Up_Scroll(object sender, EventArgs e)
        {
            if (trackBar_H_Up.Value < trackBar_H_Low.Value)
            {
                trackBar_H_Up.Value = trackBar_H_Low.Value;
            }
            label_H_Up.Text = trackBar_H_Up.Value.ToString();
        }

        private void TrackBar_S_Up_Scroll(object sender, EventArgs e)
        {
            if (trackBar_S_Up.Value < trackBar_S_Low.Value)
            {
                trackBar_S_Up.Value = trackBar_S_Low.Value;
            }
            label_S_Up.Text = trackBar_S_Up.Value.ToString();
        }

        private void TrackBar_V_Up_Scroll(object sender, EventArgs e)
        {
            if (trackBar_V_Up.Value < trackBar_V_Low.Value)
            {
                trackBar_V_Up.Value = trackBar_V_Low.Value;
            }
            label_V_Up.Text = trackBar_V_Up.Value.ToString();
        }
    }

    public class MyCV
    {
        public static void FindBoundingBox(Image<Gray, byte> src, Image<Bgr, byte> draw)
        {            
            // 使用 VectorOfVectorOfPoint 類別一次取得多個輪廓。
            using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
            {
                // 在這版本請使用FindContours，早期版本有cvFindContours等等，在這版都無法使用，
                // 由於這邊是要取得最外層的輪廓，所以第三個參數給 null，第四個參數則用 RetrType.External。
                CvInvoke.FindContours(src, contours, null, RetrType.External, ChainApproxMethod.ChainApproxSimple);
                Rectangle BoundingBox;
                int count = contours.Size;
                for (int i = 0; i < count; i++)
                {

                    using (VectorOfPoint contour = contours[i])
                    {
                        // 使用 BoundingRectangle 取得框選矩形
                        BoundingBox = CvInvoke.BoundingRectangle(contour);                        
                        CvInvoke.Rectangle(draw, BoundingBox, new MCvScalar(255, 0, 255, 255), 3);                      
                        Debug.WriteLine(BoundingBox.Size.ToString());                        
                    }
                }
                
            }
        }

        public static void MinAreaBoundingBox(Image<Gray, byte> src, Image<Bgr, byte> draw)
        {
            using (VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint())
            {
                CvInvoke.FindContours(src, contours, null, RetrType.External, ChainApproxMethod.ChainApproxSimple);

                int count = contours.Size;
                for (int i = 0; i < count; i++)
                {
                    using (VectorOfPoint contour = contours[i])
                    {
                        // MinAreaRect 是此版本找尋最小面積矩形的方法。
                        RotatedRect BoundingBox = CvInvoke.MinAreaRect(contour);
                        CvInvoke.Polylines(draw, Array.ConvertAll(BoundingBox.GetVertices(), Point.Round), true, new Bgr(Color.DeepPink).MCvScalar, 3);
                    }
                }
            }
        }
    }
}

