using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MechanicalAutoClicker
{
    class Taps
    {
        public double StartTime;
        public List<Tap> TapList = new List<Tap>();
        public Taps(string file, double dragSpeedMultiplier, double scale)
        {
            double sliderMultiplier = 1, baseSliderSpeed = 100;

            CultureInfo format = new CultureInfo("en-US");

            List<double[]> sliderSpeeds = new List<double[]>();

            IEnumerable<string> lines = File.ReadLines(file);

            string block = "";

            TapList.Add(new Tap(256 * scale, 60 - 192 * scale, 0));
            TapList.Add(new Tap(650 * scale, 60 - 400 * scale, 0));

            foreach (string line in lines)
            {
                if (line.StartsWith("["))
                {
                    block = line.Substring(1, line.Length - 2);
                    continue;
                }
                if (block == "Difficulty")
                {
                    string[] words = line.Split(':');
                    if (words[0] == "SliderMultiplier")
                    {
                        sliderMultiplier = double.Parse(words[1], format) * 100;
                    }
                }
                if (block == "TimingPoints")
                {
                    string[] words = line.Split(',');
                    if (words.Length != 8) continue;
                    double time = double.Parse(words[1], format) / 1000.0;
                    double sliderSpeed;
                    if (words[6] == "1")
                    {
                        double beatLength = double.Parse(words[1], format);
                        double beatPerSecond = 1000.0 / beatLength;
                        sliderSpeed = sliderMultiplier * beatPerSecond;
                        baseSliderSpeed = sliderSpeed;
                    }
                    else
                    {
                        double beatLengthMultiplier = -double.Parse(words[1], format) / 100.0;
                        sliderSpeed = baseSliderSpeed / beatLengthMultiplier;
                    }
                    sliderSpeeds.Add(new[] { time, sliderSpeed });
                }
                else if (block == "HitObjects")
                {
                    string[] words = line.Split(',');
                    if (words.Length < 4) continue;
                    int x = int.Parse(words[0]);
                    int y = int.Parse(words[1]);
                    double t = int.Parse(words[2]) / 1000.0;
                    int type = int.Parse(words[3]);

                    if ((type & (1 << 0)) != 0)
                    {
                        //hit circle
                        TapList.Add(new Tap(x * scale, 60 - y * scale, t));
                    }
                    else if ((type & (1 << 1)) != 0)
                    {
                        //slider

                        double sliderSpeed = 100;
                        foreach (double[] slider in sliderSpeeds)
                        {
                            if (slider[0] <= t)
                            {
                                sliderSpeed = slider[1];
                            }
                            else
                            {
                                break;
                            }
                        }

                        List<double[]> dragPoints = new List<double[]>();
                        double dragV = sliderSpeed * scale * dragSpeedMultiplier;
                        string[] pointsStr = words[5].Split('|');
                        double setS = double.Parse(words[7], format);
                        int repeats = int.Parse(words[6], format);

                        dragPoints.Add(new[] { x * scale, 60 - y * scale });
                        double s = 0;
                        for (int i = 1; i < pointsStr.Length; i++)
                        {
                            string[] pos = pointsStr[i].Split(':');
                            int prevX = x;
                            int prevY = y;
                            x = int.Parse(pos[0]);
                            y = int.Parse(pos[1]);
                            dragPoints.Add(new[] { x * scale, 60 - y * scale });
                            s += Math.Sqrt((prevX - x) * (prevX - x) + (prevY - y) * (prevY - y));
                        }

                        int slidePoints = dragPoints.Count;

                        while (true)
                        {
                            repeats--;
                            if (repeats <= 0) break;
                            for (int i = slidePoints - 2; i >= 0; i--)
                            {
                                dragPoints.Add(new[] { dragPoints[i][0], dragPoints[i][1] });
                            }
                            repeats--;
                            if (repeats <= 0) break;
                            for (int i = 1; i < slidePoints; i++)
                            {
                                dragPoints.Add(new[] { dragPoints[i][0], dragPoints[i][1] });
                            }
                        }

                        double distMul = s / setS;
                        TapList.Add(new Tap(dragPoints.ToArray(), dragV * distMul, t));
                    }
                    else if ((type & (1 << 3)) != 0)
                    {
                        //spinner
                        double endTime = int.Parse(words[5]) / 1000.0;
                        double length = endTime - t;

                        //center
                        double circleX = 256 * scale;
                        double circleY = 240 * scale;
                        int r = 5;

                        List<double[]> dragPoints = new List<double[]>();

                        double time = 0;

                        while (true)
                        {
                            dragPoints.Add(new [] { circleX - r, circleY - r });
                            time += Form1.DistToTime(r * 2);
                            if (time + Form1.DistToTime(r * 2) > length) break;
                            dragPoints.Add(new [] { circleX - r, circleY + r });
                            time += Form1.DistToTime(r * 2);
                            if (time + Form1.DistToTime(r * 2) > length) break;
                            dragPoints.Add(new [] { circleX + r, circleY + r });
                            time += Form1.DistToTime(r * 2);
                            if (time + Form1.DistToTime(r * 2) > length) break;
                            dragPoints.Add(new [] { circleX + r, circleY - r });
                            time += Form1.DistToTime(r * 2);
                            if (time + Form1.DistToTime(r * 2) > length) break;
                        }

                        TapList.Add(new Tap(dragPoints.ToArray(), 450, t));
                    }
                }
            }
            StartTime = TapList[2].T - 4;
            TapList[1].T = TapList[2].T - 2.22;
            TapList[0].T = TapList[2].T - 3.5;
        }
    }

    class Tap
    {
        public bool Drag;
        public double X, Y;
        public double EndX, EndY;
        public double T;
        public double DragV;
        public double[][] DragPoints;

        public Tap(double x, double y, double t)
        {
            this.X = x;
            this.Y = y;
            this.T = t;
            this.EndX = x;
            this.EndY = y;
            Drag = false;
        }
        public Tap(double[][] dragPoints, double dragV, double t)
        {
            this.DragPoints = dragPoints;
            X = dragPoints[0][0];
            Y = dragPoints[0][1];
            EndX = dragPoints[dragPoints.Length - 1][0];
            EndY = dragPoints[dragPoints.Length - 1][1];
            this.T = t;
            this.DragV = dragV;
            Drag = true;
        }
    }
}
