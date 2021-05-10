using System;
using System.Globalization;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;

namespace MechanicalAutoClicker
{
    public partial class Form1 : Form
    {
        static CultureInfo format = new CultureInfo("en-US");

        public static Form1 OpenedForm;

        private Taps taps;

        private Random random = new Random();
        private enum Modes
        {
            Normal,
            TouchOnly
        };

        bool randomTapping = false;
        bool autoTapping = false;
        private bool moveImmediately = true;

        private Modes mode = Modes.TouchOnly;
        public double PrevX = 0, PrevY = 0;
        public double X = 0, Y = 0;
        private int xOff = 0, yOff = 0;

        private static double phoneXSize = 135, phoneYSize = 60;
        private static int moveSpeed = 450, moveAcc = 8000;
        private static double tapDelay = 0, tapTime = 0.075, printerDelay = 0.05;

        SerialPort PrinterPort;
        SerialPort StylusPort;

        public Form1()
        {
            OpenedForm = this;
            InitializeComponent();
            DrawPanel.MouseDown += Press;
            DrawPanel.MouseUp += Release;
            DrawPanel.MouseMove += SetPos;
            Closed += Stop;

            PrinterPort = new SerialPort("COM3", 115200);
            StylusPort = new SerialPort("COM4", 115200);
            PrinterPort.Open();
            StylusPort.Open();

            PrinterPort.WriteLine("M204 T" + moveAcc);
            PrinterPort.WriteLine("M205 B0 X0 Y0");
            PrinterPort.WriteLine("M17 X Y Z");
            PrinterPort.WriteLine("G0 F" + (moveSpeed * 60));
            new Thread(() =>
            {
                while (PrinterPort.IsOpen)
                {
                    if (mode == Modes.Normal && !autoTapping)
                        MovePrinter();
                    else Thread.Sleep(50);
                }
            }).Start();
        }

        public static double GetTime()
        {
            return Environment.TickCount / 1000.0;
        }

        public static void SleepUntil(double time)
        {
            int delay = (int)((time - GetTime()) * 1000);
            if (delay < 0)
            {
                delay = 0;
                Console.WriteLine("can't keep up!");
            }
            Thread.Sleep(delay);
        }
        public static void Sleep(double time)
        {
            int delay = (int)(time * 1000);
            if (delay < 0)
            {
                delay = 0;
                Console.WriteLine("can't keep up!");

            }
            Thread.Sleep(delay);
        }
        public static void AccurateSleep(double time)
        {
            double t = GetTime() + time;
            while (GetTime() < t)
            {
            }
        }

        public static double DistToTime(double s, double v)
        {
            double a = moveAcc;
            double t;

            double maxTa = Math.Sqrt(s / a);
            double maxV = maxTa * a;

            if (maxV > v)
            {
                double ta = v / a;
                double sa = (v * v) / (2 * a);
                double sv = s - sa * 2;
                double tv = sv / v;
                t = ta + tv + ta;
            }
            else
            {
                t = 2 * maxTa;
            }

            return t;
        }
        public static double DistToTime(double s)
        {
            return DistToTime(s, moveSpeed);
        }

        public void Stop(object sender, EventArgs e)
        {
            StylusPort.Write("d");
            StylusPort.Close();
            PrinterPort.Close();
        }

        public void MoveAndTap(double x, double y, double prevX, double prevY)
        {
            double dist = Math.Sqrt((x - prevX) * (x - prevX) + (y - prevY) * (y - prevY));
            double time = DistToTime(dist);
            MoveTo(x, y);
            Sleep(time + printerDelay - tapDelay);
            StylusPort.Write("t");
            if (randomTapping)
            {
                AccurateSleep(tapTime);
                MoveAndTap(random.NextDouble() * phoneXSize, random.NextDouble() * phoneYSize, x, y);
            }
        }

        public void MoveAndTap(double x, double y, double prevX, double prevY, double t)
        {
            double dist = Math.Sqrt((x - prevX) * (x - prevX) + (y - prevY) * (y - prevY));
            double time = DistToTime(dist);
            MoveTo(x, y);
            Sleep(time + printerDelay - tapDelay);
            SleepUntil(t - tapDelay);
            StylusPort.Write("t");
        }

        public void MoveAndDrag(double[][] dragPoints, double dragV, double prevX, double prevY, double t)
        {
            double time;
            {
                double x = dragPoints[0][0];
                double y = dragPoints[0][1];
                time = t - tapDelay;
                MoveTo(x, y);
                SleepUntil(time);
            }
            StylusPort.Write("p");
            AccurateSleep(tapDelay);
            for (int i = 1; i < dragPoints.Length; i++)
            {
                double x = dragPoints[i - 1][0];
                double y = dragPoints[i - 1][1];
                double endX = dragPoints[i][0];
                double endY = dragPoints[i][1];
                double dist = Math.Sqrt((x - endX) * (x - endX) + (y - endY) * (y - endY));
                time += DistToTime(dist, dragV);
                MoveTo(endX, endY, dragV);
                SleepUntil(time);
            }
            AccurateSleep(printerDelay);
            StylusPort.Write("r");
        }

        public void MoveTo(double x, double y)
        {
            MoveTo(x, y, moveSpeed);
        }
        public void MoveTo(double x, double y, double v)
        {
            x += xOff;
            y += yOff;
            PrinterPort.WriteLine($"G0 X{x.ToString("#.##", format)} Y{y.ToString("#.##", format)} F{(int)(v * 60)}");
            X = x;
            Y = y;
        }
        public void Press(object sender, MouseEventArgs e)
        {
            PrevX = X;
            PrevY = Y;
            Y = phoneYSize - e.X * (phoneYSize / DrawPanel.Width);
            X = phoneXSize - e.Y * (phoneXSize / DrawPanel.Height);
            if (mode == Modes.Normal)
            {
                StylusPort.Write("p");
            }
            else
            {
                MoveAndTap(X, Y, PrevX, PrevY);
            }
        }

        private void AutoTouchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            autoTapping = AutoTouchCheckBox.Checked;

            if (autoTapping)
            {
                if (taps is null)
                {
                    autoTapping = false;
                    AutoTouchCheckBox.Checked = false;
                    return;
                }
                StartAutoTapping();
            }
        }

        public void Release(object sender, MouseEventArgs e)
        {
            Y = phoneYSize - e.X * (phoneYSize / DrawPanel.Width);
            X = phoneXSize - e.Y * (phoneXSize / DrawPanel.Height);
            StylusPort.Write("r");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            moveAcc = int.Parse(textBox2.Text, format);
            PrinterPort.WriteLine("M204 T" + moveAcc);
        }

        private void MoveImmediatelyCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            moveImmediately = MoveImmediatelyCheckBox.Checked;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            moveSpeed = int.Parse(textBox1.Text, format);
            PrinterPort.WriteLine("G0 F" + (moveSpeed * 60));
        }

        private void LoadTapsBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog { RestoreDirectory = true };

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string file = dialog.FileName;
                FileLabel.Text = file.Substring(file.LastIndexOfAny(new[] { '\\', '/' }));
                taps = new Taps(file,
                    double.Parse(SliderSpeedTextBox.Text, format),
                    double.Parse(ScaleTextBox.Text, format));
            }
        }

        public void SetPos(object sender, MouseEventArgs e)
        {
            if (mode == Modes.TouchOnly) return;

            Y = phoneYSize - e.X * (phoneYSize / DrawPanel.Width);
            X = phoneXSize - e.Y * (phoneXSize / DrawPanel.Height);
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            randomTapping = RandomTapCheckBox.Checked;
        }

        void MovePrinter()
        {
            try
            {
                if (PrinterPort.ReadLine() != "ok") return;
            }
            catch (IOException e)
            {
                if (PrinterPort.IsOpen)
                {
                    Console.Error.WriteLine(e);
                }
                return;
            }
            PrinterPort.WriteLine($"G0 X{X.ToString("#.##", format)} Y{Y.ToString("#.##", format)}\r\n");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            mode = ModeCheckBox.Checked ? Modes.TouchOnly : Modes.Normal;
        }

        void StartAutoTapping()
        {
            new Thread(() =>
            {
                double startTime = GetTime() - taps.StartTime;
                for (int i = 0; i < taps.TapList.Count; i++)
                {
                    if (!autoTapping) break;

                    double x = taps.TapList[i].X;
                    double y = taps.TapList[i].Y;
                    double t = taps.TapList[i].T;


                    double prevX, prevY;
                    if (i > 0)
                    {
                        prevX = taps.TapList[i - 1].EndX;
                        prevY = taps.TapList[i - 1].EndY;
                    }
                    else
                    {
                        prevX = X;
                        prevY = Y;
                    }

                    if (!moveImmediately)
                    {
                        double dist = Math.Sqrt((x - prevX) * (x - prevX) + (y - prevY) * (y - prevY));
                        double moveTime = DistToTime(dist) + tapDelay + printerDelay;
                        double time = GetTime() - startTime;
                        int delayTime = (int)((t - time - moveTime) * 1000);
                        if (delayTime < 0)
                        {
                            delayTime = 0;
                            Console.WriteLine("can't keep up!");
                        }

                        Thread.Sleep(delayTime);
                    }

                    if (taps.TapList[i].Drag)
                    {
                        Console.WriteLine("drag " + i + "/" + taps.TapList.Count);
                        MoveAndDrag(taps.TapList[i].DragPoints, taps.TapList[i].DragV, prevX, prevY, startTime + t);
                    }
                    else
                    {
                        Console.WriteLine("tap " + i+"/"+taps.TapList.Count);
                        MoveAndTap(x, y, prevX, prevY, startTime + t);
                    }

                }
                Console.WriteLine("DONE!");
            }).Start();
        }
    }
}
