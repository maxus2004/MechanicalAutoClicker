using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MechanicalAutoClicker
{
    class Taps
    {
        public double startTime = 0;
        public List<Tap> taps = new List<Tap>();
        public Taps(string file, double dragSpeedMult, double scale)
        {
            double SliderMultiplier = 1, baseSliderSpeed = 100;

            CultureInfo format = new CultureInfo("en-US");

            List<double[]> sliderSpeeds = new List<double[]>();

            IEnumerable<string> lines = File.ReadLines(file);

            string block = "";

            taps.Add(new Tap(256 * scale, 60 - 192 * scale, 0));
            taps.Add(new Tap(650 * scale, 60 - 400 * scale, 0));

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
                        SliderMultiplier = double.Parse(words[1], format) * 100;
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
                        sliderSpeed = SliderMultiplier * beatPerSecond;
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
                        taps.Add(new Tap(x * scale, 60 - y * scale, t));
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
                        double dragV = sliderSpeed * scale * dragSpeedMult;
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

                        double distMult = s / setS;
                        taps.Add(new Tap(dragPoints.ToArray(), dragV * distMult, t));
                    }
                    else if ((type & (1 << 3)) != 0)
                    {
                        //spinner
                        double endTime = int.Parse(words[5]) / 1000.0;
                        double length = endTime - t;

                        //center
                        double sircleX = 256 * scale;
                        double sircleY = 240 * scale;
                        int r = 5;

                        List<double[]> dragPoints = new List<double[]>();

                        double time = 0;

                        while (true)
                        {
                            dragPoints.Add(new double[] { sircleX - r, sircleY - r });
                            time += Form1.DistToTime(r * 2);
                            if (time + Form1.DistToTime(r * 2) > length) break;
                            dragPoints.Add(new double[] { sircleX - r, sircleY + r });
                            time += Form1.DistToTime(r * 2);
                            if (time + Form1.DistToTime(r * 2) > length) break;
                            dragPoints.Add(new double[] { sircleX + r, sircleY + r });
                            time += Form1.DistToTime(r * 2);
                            if (time + Form1.DistToTime(r * 2) > length) break;
                            dragPoints.Add(new double[] { sircleX + r, sircleY - r });
                            time += Form1.DistToTime(r * 2);
                            if (time + Form1.DistToTime(r * 2) > length) break;
                        }

                        taps.Add(new Tap(dragPoints.ToArray(), 450, t));
                    }
                }
            }
            startTime = taps[2].t - 4;
            taps[1].t = taps[2].t - 2.22;
            taps[0].t = taps[2].t - 3.5;
        }
    }

    class Tap
    {
        public bool drag;
        public double x, y;
        public double endX, endY;
        public double t;
        public double dragV;
        public double[][] dragPoints;

        public Tap(double x, double y, double t)
        {
            this.x = x;
            this.y = y;
            this.t = t;
            this.endX = x;
            this.endY = y;
            drag = false;
        }
        public Tap(double[][] dragPoints, double dragV, double t)
        {
            this.dragPoints = dragPoints;
            x = dragPoints[0][0];
            y = dragPoints[0][1];
            endX = dragPoints[dragPoints.Length - 1][0];
            endY = dragPoints[dragPoints.Length - 1][1];
            this.t = t;
            this.dragV = dragV;
            drag = true;
        }
    }
}
