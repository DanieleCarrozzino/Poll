using System;
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
            var result = radius + (DELTA_TO_ADD + (radius / 10));
            result = Math.Round(result, 1);
            return result;
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

                // Circle
                dc.DrawEllipse(brush, pen, new Point(Math.Min(startX, MAX_CIRCLES), 19), radius[index].Item1, radius[index].Item2);

                // Initials
                dc.DrawText(getTextFormat("DC", Math.Max(radius[index].Item1, 1)), 
                    new Point(Math.Min(startX, MAX_CIRCLES) - (7 + ((MAX_RADIUS - radius[index].Item1) / 2.2)) + (MAX_RADIUS - radius[index].Item1), 
                    14 + ((MAX_RADIUS - radius[index].Item1) / 2)));

                radius[index] = (
                    Math.Min(MAX_RADIUS, getDeltaFromRadius(radius[index].Item1)),
                    Math.Min(MAX_RADIUS, getDeltaFromRadius(radius[index].Item2)));
                startX += 5;
                index++;
            }

        }

        private FormattedText getTextFormat(string initials, double fontSize)
        {
            // Create a text format
            Typeface typeface = new Typeface("Arial");
            string textToDraw = "DC";

            // Create the formatted text object
            FormattedText formattedText = new FormattedText(
                textToDraw,
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                typeface,
                fontSize,
                Brushes.White,
                new NumberSubstitution(),
                1);
            formattedText.SetFontWeight(FontWeights.DemiBold);

            return formattedText;
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

        public bool insertOrRemoveNewColor(Color color, int id)
        {
            if(colors.Contains((color, id)))
            {
                for(int i = 0; i < colors.Count; i++)
                {
                    if (colors[i].Item2 == id)
                    {
                        colors.RemoveAt(i);
                        radius.RemoveAt(i);
                        break;
                    }
                }

                this.Width = Math.Min((MAX_RADIUS) + (10 * colors.Count), MAX_CIRCLES);
                InvalidateVisual();
                return false;
            }

            insertNewColor(color, id);
            return true;
        }

        public int getCountOfColors()
        {
            return colors.Count;
        }

        private async void startAnimation()
        {
            await Task.Run(async () =>
            {
                animationRunning = true;
                while (radius.Count > 0 && radius.Last().Item1 != MAX_RADIUS)
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
