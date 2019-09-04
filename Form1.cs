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
        Direction DirectionForm;
        VideoCapture CameraCapture;
        Image<Bgr, Byte> OriginalImage;

        RotatedRect MiddleFirstRect;
        RotatedRect MiddleSecondRect;
        RotatedRect SideRect_1;
        RotatedRect SideRect_2;


        public Form1()
        {
            InitializeComponent();          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Location = new Point(this.Location.X + 100, this.Location.Y);
           
            CameraCapture = new VideoCapture(0);                       
            Application.Idle += new EventHandler(Application_Idle);//Do things when UI is idle
            LiveImageForm = new Live();
            LiveImageForm.Show();
            DirectionForm = new Direction();
            DirectionForm.Show();
            LiveImageForm.Location = new Point(this.Location.X-LiveImageForm.Width,this.Location.Y);
            DirectionForm.Location = new Point(this.Location.X - LiveImageForm.Width, this.Location.Y+ LiveImageForm.Height);
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
        private void ProcessImage_Side1()
        {
            Image<Hsv, byte> HSVImage;
            Image<Gray, byte> HSVMaskedImage;
            Image<Gray, Byte> FilteredImage;
            Image<Gray, byte> CannyImage;
            Image<Bgr, byte> DrawBound;
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            RotatedRect BoundingBox;
            int Rectcount;
            float MaxArea = 0;
            RotatedRect MaxRect = new RotatedRect();

            HSVImage = OriginalImage.Convert<Hsv, byte>();         
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
            DrawBound = OriginalImage.Clone();
            
            CvInvoke.FindContours(CannyImage, contours, null, RetrType.External, ChainApproxMethod.ChainApproxSimple);
            Rectcount = contours.Size;

            for (int i = 0; i < Rectcount; i++)
            {

                using (VectorOfPoint contour = contours[i])
                {
                    // 使用 BoundingRectangle 取得框選矩形
                    BoundingBox = CvInvoke.MinAreaRect(contour);
                    if((BoundingBox.Size.Width * BoundingBox.Size.Height) > MaxArea)
                    {
                        MaxArea = BoundingBox.Size.Width * BoundingBox.Size.Height;                        
                        MaxRect = BoundingBox;
                    }                                        
                }                
            }
            CvInvoke.Polylines(DrawBound, Array.ConvertAll(MaxRect.GetVertices(), Point.Round), true, new Bgr(Color.DeepSkyBlue).MCvScalar, 3);
            CvInvoke.Line(DrawBound, new Point(0, HeightThreshold), new Point(OriginalImage.Width, HeightThreshold), new Bgr(Color.DeepPink).MCvScalar, 5);
            imageBox_Result_1.Image = DrawBound;
            SideRect_1 = MaxRect;
        }

        private void ProcessImage_Middle()
        {
            Image<Hsv, byte> HSVImage;
            Image<Gray, byte> HSVMaskedImage;
            Image<Gray, Byte> FilteredImage;
            Image<Gray, byte> CannyImage;
            Image<Bgr, byte> DrawBound;
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            RotatedRect BoundingBox;
            int Rectcount;
            float MaxArea = 0;
            float MaxArea_2 = 0;
            RotatedRect MaxRect_2 = new RotatedRect();
            RotatedRect MaxRect = new RotatedRect();            

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
            DrawBound = OriginalImage.Clone();

            CvInvoke.FindContours(CannyImage, contours, null, RetrType.External, ChainApproxMethod.ChainApproxSimple);
            Rectcount = contours.Size;

            for (int i = 0; i < Rectcount; i++)
            {                
                using (VectorOfPoint contour = contours[i])
                {
                    // 使用 BoundingRectangle 取得框選矩形
                    BoundingBox = CvInvoke.MinAreaRect(contour);                   
                    if ((BoundingBox.Size.Width * BoundingBox.Size.Height) > MaxArea)
                    {                        
                        MaxArea = BoundingBox.Size.Width * BoundingBox.Size.Height;
                        MaxRect_2 = MaxRect;
                        MaxRect = BoundingBox;                       
                    }else{
                        if ((BoundingBox.Size.Width * BoundingBox.Size.Height) > MaxArea_2)
                        {
                            MaxArea_2 = BoundingBox.Size.Width * BoundingBox.Size.Height;
                            MaxRect_2 = BoundingBox;
                        }
                    }
                     
                }                
            }
            CvInvoke.Polylines(DrawBound, Array.ConvertAll(MaxRect.GetVertices(), Point.Round), true, new Bgr(Color.DeepPink).MCvScalar, 3);
            CvInvoke.Polylines(DrawBound, Array.ConvertAll(MaxRect_2.GetVertices(), Point.Round), true, new Bgr(Color.DeepPink).MCvScalar, 3);
            CvInvoke.Line(DrawBound, new Point(0, HeightThreshold), new Point(OriginalImage.Width, HeightThreshold), new Bgr(Color.DeepPink).MCvScalar, 5);
            imageBox_Result_2.Image = DrawBound;

            MiddleFirstRect = MaxRect;
            MiddleSecondRect = MaxRect_2;
        }
        
        private void ProcessImage_Side2()
        {
            Image<Hsv, byte> HSVImage;
            Image<Gray, byte> HSVMaskedImage;
            Image<Gray, Byte> FilteredImage;
            Image<Gray, byte> CannyImage;
            Image<Bgr, byte> DrawBound;
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            RotatedRect BoundingBox;
            int Rectcount;
            float MaxArea = 0;
            RotatedRect MaxRect = new RotatedRect();

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
            DrawBound = OriginalImage.Clone();

            CvInvoke.FindContours(CannyImage, contours, null, RetrType.External, ChainApproxMethod.ChainApproxSimple);
            Rectcount = contours.Size;

            for (int i = 0; i < Rectcount; i++)
            {

                using (VectorOfPoint contour = contours[i])
                {
                    // 使用 BoundingRectangle 取得框選矩形
                    BoundingBox = CvInvoke.MinAreaRect(contour);
                    if ((BoundingBox.Size.Width * BoundingBox.Size.Height) > MaxArea)
                    {

                        MaxArea = BoundingBox.Size.Width * BoundingBox.Size.Height;
                        MaxRect = BoundingBox;
                    }
                }
            }
            CvInvoke.Polylines(DrawBound, Array.ConvertAll(MaxRect.GetVertices(), Point.Round), true, new Bgr(Color.Red).MCvScalar, 3);
            CvInvoke.Line(DrawBound, new Point(0, HeightThreshold), new Point(OriginalImage.Width, HeightThreshold), new Bgr(Color.DeepPink).MCvScalar, 5);
            imageBox_Result_3.Image = DrawBound;

            SideRect_2 = MaxRect;            
        }

        private void DetermineAngle()
        {
            if((SideRect_1.Center.Y < HeightThreshold) && (RectArea(SideRect_1)!=0))
            {
                return;
            }
            if ((SideRect_2.Center.Y < HeightThreshold) && (RectArea(SideRect_2) != 0))
            {
                return;
            }
            if ((MiddleFirstRect.Center.Y < HeightThreshold) && (RectArea(MiddleFirstRect) != 0))
            {
                return;
            }

            if (((RectArea(SideRect_1) + RectArea(SideRect_2)) == 0) && ((RectArea(MiddleFirstRect) + RectArea(MiddleSecondRect)) != 0))//僅偵測到中間
            {
                DirectionForm.Angle = 0;
            }
            else if ((RectArea(SideRect_1) > 750) && (RectArea(SideRect_2) == 0) && (RectArea(MiddleFirstRect) < 750))
            { //僅偵測到單邊
                DirectionForm.Angle = 90;
            }
            else if ((RectArea(SideRect_1) == 0) && (RectArea(SideRect_2) > 750) && (RectArea(MiddleFirstRect) < 750)) //僅偵測到單邊
            {
                DirectionForm.Angle = -90;
            }
            else if ((RectArea(SideRect_1) != 0) && (RectArea(SideRect_2) != 0) && (RectArea(MiddleFirstRect) > 750)) //偵測到中間及兩邊
            {
                DirectionForm.Angle = 0;
            }
            else if ((RectArea(SideRect_1) != 0) && (RectArea(SideRect_2) == 0) && (RectArea(MiddleFirstRect) != 0)) //偵測到中間及單邊
            {
                DirectionForm.Angle = 90 / RectArea(MiddleFirstRect) * RectArea(SideRect_1)/2;
            }
            else if ((RectArea(SideRect_1) == 0) && (RectArea(SideRect_2) != 0) && (RectArea(MiddleFirstRect) != 0)) //偵測到中間及單邊
            {
                DirectionForm.Angle = -90 / RectArea(MiddleFirstRect) * RectArea(SideRect_2) / 2;
            }
        }

        private void SetInitialThreshold()
        {
            trackBar_H_Low_1.Value = 0;
            trackBar_S_Low_1.Value = 160;
            trackBar_V_Low_1.Value = 40;

            trackBar_H_Low_2.Value = 78;
            trackBar_S_Low_2.Value = 28;
            trackBar_V_Low_2.Value = 35;

            trackBar_H_Low_3.Value = 14;
            trackBar_S_Low_3.Value = 131;
            trackBar_V_Low_3.Value = 112;

            trackBar_H_Up_1.Value = 17;
            trackBar_S_Up_1.Value = 255;
            trackBar_V_Up_1.Value = 255;

            trackBar_H_Up_2.Value = 101;
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

            label_Area_1.Text = RectArea(SideRect_1).ToString();
            label_Area_2.Text = RectArea(MiddleFirstRect).ToString();
            label_Area_3.Text = RectArea(SideRect_2).ToString();

            label_Center_1.Text = SideRect_1.Center.ToString();
            label_Center_2.Text = MiddleFirstRect.Center.ToString();
            label_Center_3.Text = SideRect_2.Center.ToString();
        }

        private void Timer_Process_Tick(object sender, EventArgs e)
        {
            GetLiveImage();
            ProcessImage_Side1();
            ProcessImage_Middle();
            ProcessImage_Side2();
            DetermineAngle();
            if (DetectMovement())
            {
                Debug.WriteLine($"Moved in {DirectionForm.Angle.ToString()}");
                DirectionForm.Moved();
            }
            UpdateLabelText();
        }

        private double RectArea(RotatedRect rect)
        {
            return rect.Size.Width * rect.Size.Height;
        }

        private bool Moved = false;
        private int HeightThreshold = 200;
        private bool DetectMovement()
        {
            
            if(Moved == false)
            {
                if ( (SideRect_1.Center.Y < HeightThreshold) && (RectArea(SideRect_1) != 0) )
                {
                    Moved = true;
                }
                if ((SideRect_2.Center.Y < HeightThreshold) && (RectArea(SideRect_2) != 0))
                {
                    Moved = true;
                }
                if ((MiddleFirstRect.Center.Y < HeightThreshold) && (RectArea(MiddleFirstRect) != 0))
                {
                    Moved = true;
                }
                if ((MiddleSecondRect.Center.Y < HeightThreshold) && (RectArea(MiddleSecondRect) != 0))
                {
                    Moved = true;
                }
                return Moved;
            }
            else
            {
                if ( (SideRect_1.Center.Y > HeightThreshold) && (RectArea(SideRect_1)!=0) )
                {
                    Moved = false;
                }
                if ( (SideRect_2.Center.Y > HeightThreshold) && (RectArea(SideRect_2) != 0) )
                {
                    Moved = false;
                }
                if ( (MiddleFirstRect.Center.Y > HeightThreshold) && (RectArea(MiddleFirstRect) != 0) )
                {
                    Moved = false;
                }
                return false;
            }            
        }

        private int FootAboveCount = 0;
        private bool DetectMovement_NEW()
        {
            bool FootAbove = false;
            

            if ((SideRect_1.Center.Y < HeightThreshold) && (RectArea(SideRect_1) != 0))
            {
                FootAbove = true;
            }
            else if ((SideRect_2.Center.Y < HeightThreshold) && (RectArea(SideRect_2) != 0))
            {
                FootAbove = true;
            }
            else if ((MiddleFirstRect.Center.Y < HeightThreshold) && (RectArea(MiddleFirstRect) != 0))
            {
                FootAbove = true;
            }

            if (FootAbove)
            {
                FootAboveCount++;
            }

            if(FootAboveCount == 10)
            {
                return true;
            }
            else
            {
                return false;
            }
            
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

