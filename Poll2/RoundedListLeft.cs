﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows;

namespace Poll2
{
    internal class RoundedListLeft : FrameworkElement
    {
        private List<(Color, int)> colors = new List<(Color, int)>();
        private List<(double, double)> radius = new List<(double, double)>();
        private const int MAX_RADIUS = 10;
        private const double DELTA_TO_ADD = 0.1;
        private const int MAX_CIRCLES = 20 + (5 * 10);

        private bool animationRunning = false;

        public RoundedListLeft(List<(Color, int)> listOfColor)
        {
            colors = listOfColor;
            foreach (var color in colors)
            {
                radius.Add((MAX_RADIUS, MAX_RADIUS));
            }
            this.Height = 36;
            this.Width = 10;
        }

        public RoundedListLeft()
        {
            this.Height = 36;
            this.Width = 10;
        }

        private double getDeltaFromRadius(double radius)
        {
            return radius + (DELTA_TO_ADD + (radius / 10));
        }

        protected override void OnRender(DrawingContext dc)
        {
            int startX = 10;
            int index = 0;
            foreach (var (color, id) in colors)
            {
                var brush = new SolidColorBrush(color);
                var pen = new Pen();

                brush.Freeze();
                pen.Freeze();
                dc.DrawEllipse(brush, pen, new Point(Math.Min(startX, MAX_CIRCLES), 19), radius[index].Item1, radius[index].Item2);
                radius[index] = (
                    Math.Min(MAX_RADIUS, getDeltaFromRadius(radius[index].Item1)),
                    Math.Min(MAX_RADIUS, getDeltaFromRadius(radius[index].Item2)));
                startX += 5;
                index++;
            }

        }

        public void removeColor(int id)
        {
            foreach (var (color, _id) in colors)
            {
                if (_id == id)
                {
                    colors.Remove((color, _id));
                    InvalidateVisual();
                    break;
                }
            }
        }

        public void insertNewColor(Color color, int id)
        {
            this.Width = Math.Min((MAX_RADIUS * 2) + (5 * colors.Count), MAX_CIRCLES);

            colors.Add((color, id));
            radius.Add((0, 0));
            if (colors.Count > 14)
            {
                colors.RemoveAt(11);
                radius.RemoveAt(11);
            }
            if (!animationRunning)
                startAnimation();
        }

        private async void startAnimation()
        {
            await Task.Run(async () =>
            {
                animationRunning = true;
                while (radius.Last().Item1 != MAX_RADIUS)
                {
                    Task.Delay(16).Wait();

                    Dispatcher.Invoke(() =>
                    {
                        InvalidateVisual();
                    });
                }
                animationRunning = false;
            });
        }
    }
}