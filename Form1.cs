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
        Live LiveImageForm;
        VideoCapture CameraCapture;
        Image<Bgr, Byte> OriginalImage;       

        public Form1()
        {
            InitializeComponent();          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Location = new Point(this.Location.X + 100, this.Location.Y);
           
            CameraCapture = new VideoCapture(2);                       
            Application.Idle += new EventHandler(Application_Idle);//Do things when UI is idle
            LiveImageForm = new Live();
            LiveImageForm.Show();
            LiveImageForm.Location = new Point(this.Location.X-LiveImageForm.Width,this.Location.Y);
            timer_Process.Interval = 50;
            timer_Process.Start();

            SetInitialThreshold();
        }

        private void Application_Idle(object sender, EventArgs e)
        {
            //GetAndProcess_1();
            //UpdateLabelText();
        }

        private void TrackBar_H_Low_Scroll(object sender, EventArgs e)
        {
            if (trackBar_H_Low_1.Value > trackBar_H_Up_1.Value)
            {
                trackBar_H_Low_1.Value = trackBar_H_Up_1.Value;
            }
        }

        private void TrackBar_S_Low_Scroll(object sender, EventArgs e)
        {
            if (trackBar_S_Low_1.Value > trackBar_S_Up_1.Value)
            {
                trackBar_S_Low_1.Value = trackBar_S_Up_1.Value;
            }

        }

        private void TrackBar_V_Low_Scroll(object sender, EventArgs e)
        {
            if (trackBar_V_Low_1.Value > trackBar_V_Up_1.Value)
            {
                trackBar_V_Low_1.Value = trackBar_V_Up_1.Value;
            }

        }

        private void TrackBar_H_Up_Scroll(object sender, EventArgs e)
        {
            if (trackBar_H_Up_1.Value < trackBar_H_Low_1.Value)
            {
                trackBar_H_Up_1.Value = trackBar_H_Low_1.Value;
            }
            
        }

        private void TrackBar_S_Up_Scroll(object sender, EventArgs e)
        {
            if (trackBar_S_Up_1.Value < trackBar_S_Low_1.Value)
            {
                trackBar_S_Up_1.Value = trackBar_S_Low_1.Value;
            }
       
        }

        private void TrackBar_V_Up_Scroll(object sender, EventArgs e)
        {
            if (trackBar_V_Up_1.Value < trackBar_V_Low_1.Value)
            {
                trackBar_V_Up_1.Value = trackBar_V_Low_1.Value;
            }        
        }

        private void GetLiveImage()
        {
            try
            {
                OriginalImage = CameraCapture.QueryFrame().ToImage<Bgr, Byte>();
                LiveImageForm.LiveImage = OriginalImage;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                timer_Process.Enabled = false;
                this.Close();
            }
          
        }
        private void ProcessImage_1()
        {
            Image<Hsv, byte> HSVImage;
            Image<Gray, byte> HSVMaskedImage;
            Image<Gray, Byte> FilteredImage;
            Image<Gray, byte> CannyImage;
            Image<Bgr, byte> DrawBound;     

            HSVImage = OriginalImage.Convert<Hsv, byte>();
            //imageBox2.Image = HSVImage;

            Hsv lowerLimit = new Hsv(trackBar_H_Low_1.Value, trackBar_S_Low_1.Value, trackBar_V_Low_1.Value); //50 80
            Hsv upperLimit = new Hsv(trackBar_H_Up_1.Value, trackBar_S_Up_1.Value, trackBar_V_Up_1.Value); //50 80            
            HSVMaskedImage = HSVImage.InRange(lowerLimit, upperLimit);
            imageBox_HSV_1.Image = HSVMaskedImage;

            FilteredImage = HSVMaskedImage.SmoothMedian(25);
            if (checkBox_Filter_1.Checked)
            {                
                imageBox_HSV_1.Image = FilteredImage;
            }
            else
            {
                imageBox_HSV_1.Image = HSVMaskedImage;
            }

            CannyImage = FilteredImage.Clone();
            CvInvoke.Canny(FilteredImage, CannyImage, 255, 255, 5, true);
            //imageBox4.Image = CannyImage;
            DrawBound = OriginalImage.Clone();

            label_Count_1.Text = MyCV.FindBoundingBox(CannyImage, DrawBound).ToString();
            imageBox_Result_1.Image = DrawBound;
        }

        private void ProcessImage_2()
        {
            Image<Hsv, byte> HSVImage;
            Image<Gray, byte> HSVMaskedImage;
            Image<Gray, Byte> FilteredImage;
            Image<Gray, byte> CannyImage;
            Image<Bgr, byte> DrawBound;          

            HSVImage = OriginalImage.Convert<Hsv, byte>();


            Hsv lowerLimit = new Hsv(trackBar_H_Low_2.Value, trackBar_S_Low_2.Value, trackBar_V_Low_2.Value); //50 80
            Hsv upperLimit = new Hsv(trackBar_H_Up_2.Value, trackBar_S_Up_2.Value, trackBar_V_Up_2.Value); //50 80            
            HSVMaskedImage = HSVImage.InRange(lowerLimit, upperLimit);
            imageBox_HSV_2.Image = HSVMaskedImage;

            FilteredImage = HSVMaskedImage.SmoothMedian(25);
            if (checkBox_Filter_2.Checked)
            {
                imageBox_HSV_2.Image = FilteredImage;
            }
            else
            {
                imageBox_HSV_2.Image = HSVMaskedImage;
            }

            CannyImage = FilteredImage.Clone();
            CvInvoke.Canny(FilteredImage, CannyImage, 255, 255, 5, true);
            //imageBox4.Image = CannyImage;
            DrawBound = OriginalImage.Clone();

            label_Count_2.Text = MyCV.FindBoundingBox(CannyImage, DrawBound).ToString();
            imageBox_Result_2.Image = DrawBound;
        }

        private void ProcessImage_3()
        {
            Image<Hsv, byte> HSVImage;
            Image<Gray, byte> HSVMaskedImage;
            Image<Gray, Byte> FilteredImage;
            Image<Gray, byte> CannyImage;
            Image<Bgr, byte> DrawBound;       

            HSVImage = OriginalImage.Convert<Hsv, byte>();


            Hsv lowerLimit = new Hsv(trackBar_H_Low_3.Value, trackBar_S_Low_3.Value, trackBar_V_Low_3.Value); //50 80
            Hsv upperLimit = new Hsv(trackBar_H_Up_3.Value, trackBar_S_Up_3.Value, trackBar_V_Up_3.Value); //50 80            
            HSVMaskedImage = HSVImage.InRange(lowerLimit, upperLimit);
            imageBox_HSV_3.Image = HSVMaskedImage;

            FilteredImage = HSVMaskedImage.SmoothMedian(25);
            if (checkBox_Filter_3.Checked)
            {
                imageBox_HSV_3.Image = FilteredImage;
            }
            else
            {
                imageBox_HSV_3.Image = HSVMaskedImage;
            }

            CannyImage = FilteredImage.Clone();
            CvInvoke.Canny(FilteredImage, CannyImage, 255, 255, 5, true);
            //imageBox4.Image = CannyImage;
            DrawBound = OriginalImage.Clone();

            label_Count_3.Text = MyCV.FindBoundingBox(CannyImage, DrawBound).ToString();
            imageBox_Result_3.Image = DrawBound;
        }

        private void SetInitialThreshold()
        {
            trackBar_H_Low_1.Value = 0;
            trackBar_S_Low_1.Value = 160;
            trackBar_V_Low_1.Value = 40;

            trackBar_H_Low_2.Value = 77;
            trackBar_S_Low_2.Value = 86;
            trackBar_V_Low_2.Value = 28;

            trackBar_H_Low_3.Value = 14;
            trackBar_S_Low_3.Value = 131;
            trackBar_V_Low_3.Value = 112;

            trackBar_H_Up_1.Value = 17;
            trackBar_S_Up_1.Value = 255;
            trackBar_V_Up_1.Value = 255;

            trackBar_H_Up_2.Value = 94;
            trackBar_S_Up_2.Value = 255;
            trackBar_V_Up_2.Value = 255;

            trackBar_H_Up_3.Value = 31;
            trackBar_S_Up_3.Value = 255;
            trackBar_V_Up_3.Value = 255;
        }



        private void UpdateLabelText()
        {
            label_H_Low_1.Text = trackBar_H_Low_1.Value.ToString();
            label_S_Low_1.Text = trackBar_S_Low_1.Value.ToString();
            label_V_Low_1.Text = trackBar_V_Low_1.Value.ToString();
            label_H_Up_1.Text = trackBar_H_Up_1.Value.ToString();
            label_S_Up_1.Text = trackBar_S_Up_1.Value.ToString();
            label_V_Up_1.Text = trackBar_V_Up_1.Value.ToString();

            label_H_Low_2.Text = trackBar_H_Low_2.Value.ToString();
            label_S_Low_2.Text = trackBar_S_Low_2.Value.ToString();
            label_V_Low_2.Text = trackBar_V_Low_2.Value.ToString();
            label_H_Up_2.Text = trackBar_H_Up_2.Value.ToString();
            label_S_Up_2.Text = trackBar_S_Up_2.Value.ToString();
            label_V_Up_2.Text = trackBar_V_Up_2.Value.ToString();

            label_H_Low_3.Text = trackBar_H_Low_3.Value.ToString();
            label_S_Low_3.Text = trackBar_S_Low_3.Value.ToString();
            label_V_Low_3.Text = trackBar_V_Low_3.Value.ToString();
            label_H_Up_3.Text = trackBar_H_Up_3.Value.ToString();
            label_S_Up_3.Text = trackBar_S_Up_3.Value.ToString();
            label_V_Up_3.Text = trackBar_V_Up_3.Value.ToString();
        }

        private void Timer_Process_Tick(object sender, EventArgs e)
        {
            GetLiveImage();
            ProcessImage_1();
            ProcessImage_2();
            ProcessImage_3();
            UpdateLabelText();
        }
    }


    public class MyCV
    {
        public static int FindBoundingBox(Image<Gray, byte> src, Image<Bgr, byte> draw)
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
                return count;
            }
        }

        public static int MinAreaBoundingBox(Image<Gray, byte> src, Image<Bgr, byte> draw)
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
                return count;
            }
        }
    }
}

