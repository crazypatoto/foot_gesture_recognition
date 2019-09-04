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
using System.IO.Ports;

namespace FootDetection
{
    public partial class Direction : Form
    {
        Graphics DirectionGraph;
        int ports_count = 0;
        public Direction()
        {
            InitializeComponent();
        }

        private double DirectionAngle;
        public double Angle
        {
            get { return DirectionAngle; }
            set
            {
                if(value > 90)
                {
                    DirectionAngle = 90;
                }else if(value < -90)
                {
                    DirectionAngle = -90;
                }
                else
                {
                    DirectionAngle = value;
                }
            }
        }

        public void Moved()
        {
            label_Moved.Show();
            Button1_Click(null, null);
            timer_Countdown.Enabled = true;
            
        }

        private void Movement_Load(object sender, EventArgs e)
        {
            Application.Idle += new EventHandler(Idle);
            DirectionGraph = this.CreateGraphics();
            label_Moved.Hide();
            timer_Countdown.Enabled = false;
            timer_Countdown.Interval = 500;
            GetSerialPortsNames();
        }
        private void Idle(object sender, EventArgs e)
        {
            this.Refresh();
            Pen CirclePen = new Pen(Color.Red);
            CirclePen.Width = 5;
            
            DirectionGraph.DrawArc(CirclePen,10, 10, 300, 300,0,-180);
            Pen ArrowPen = new Pen(Color.Blue);
            ArrowPen.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            ArrowPen.EndCap = System.Drawing.Drawing2D.LineCap.RoundAnchor;
            ArrowPen.Width = 8;
            DirectionGraph.DrawLine(ArrowPen, AngleToPosition(DirectionAngle).X, AngleToPosition(DirectionAngle).Y, 160, 160);
        }

        private void Connect()
        {
            try
            {
                serialPort1.BaudRate = 9600;
                serialPort1.Parity = System.IO.Ports.Parity.None;
                serialPort1.StopBits = StopBits.One;
                serialPort1.PortName = comboBox_ports.Text;
                serialPort1.Open();
                button2.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
                     
        }

        private void GetSerialPortsNames()
        {
            string[] ports = SerialPort.GetPortNames();
            if (ports.Length != ports_count)
            {
                comboBox_ports.Items.Clear();
                ports_count = ports.Length;
                if (ports.Length != 0)
                {
                    comboBox_ports.Items.AddRange(ports);
                    comboBox_ports.SelectedIndex = 0;
                }
                else
                {
                    comboBox_ports.Text = "";
                    comboBox_ports.Items.Clear();
                }

            }
        }

        private Point AngleToPosition(double angle)
        {            
            double PI = 3.1415926;
            angle %= 360;
            double angle_rad = (angle / 180) * PI;

            return new Point((int)(160 + 145 * Math.Sin(angle_rad)), (int)(160 + 145 * -Math.Cos(angle_rad)));
        }

        private void Movement_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void Timer_Countdown_Tick(object sender, EventArgs e)
        {
            label_Moved.Hide();
            timer_Countdown.Enabled = false;
            Button3_Click(null, null);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            byte[] command = new byte[4];           

            command[0] = Convert.ToByte('H');
            command[1] = Convert.ToByte('H');
            command[2] = (byte)((DirectionAngle + 90) * 255 / 360);
            command[3] = 50;

            if (serialPort1.IsOpen)
            {
                System.Diagnostics.Debug.WriteLine(command[2].ToString());
                serialPort1.Write(command, 0, 4);
            }
           
            //serialPort1.WriteLine("HHZZ");
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            Connect();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            byte[] command = new byte[5]; 

            command[0] = Convert.ToByte('H');
            command[1] = Convert.ToByte('H');
            command[2] = 0;
            command[3] = 0;
            if (serialPort1.IsOpen)
            {
                serialPort1.Write(command, 0, 4);
            }
        }

      
    }
}
