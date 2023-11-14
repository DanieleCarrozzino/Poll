using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Poll2
{
    internal class RoundedList : FrameworkElement
    {
        private List<(Color, int)> colors = new List<(Color, int)>();
        private List<(double, double)> radius = new List<(double, double)>();
        private const int       MAX_RADIUS      = 10;
        private const double    DELTA_TO_ADD    = 0.1;
        private const int       MAX_CIRCLES     = 20 + (5 * 10);

        private bool animationRunning = false;

        public RoundedList(List<(Color, int)> listOfColor)
        {
            colors = listOfColor;
            foreach (var color in colors)
            {
                radius.Add((MAX_RADIUS, MAX_RADIUS));
            }
            this.Height = 36;
            this.Width  = MAX_CIRCLES + 10;
        }

        private double getDeltaFromRadius(double radius)
        {
            return radius + (DELTA_TO_ADD + (radius / 10));
        }

        protected override void OnRender(DrawingContext dc)
        {
            int startX = MAX_CIRCLES;
            int index  = 0;
            foreach (var (color, id) in colors)
            {
                var brush   = new SolidColorBrush(color);
                var pen     = new Pen();

                brush.Freeze();
                pen.Freeze();

                //Circle
                dc.DrawEllipse(brush, pen, new Point(Math.Max(startX, 20), 20), radius[index].Item1, radius[index].Item2);

                // Initials
                dc.DrawText(getTextFormat("DC", Math.Max(radius[index].Item1, 1)),
                    new Point(Math.Min(startX, MAX_CIRCLES) - (7 + ((MAX_RADIUS - radius[index].Item1) / 2.2)) + (MAX_RADIUS - radius[index].Item1),
                    14 + ((MAX_RADIUS - radius[index].Item1) / 2)));

                radius[index] = (
                    Math.Min(MAX_RADIUS, getDeltaFromRadius(radius[index].Item1)), 
                    Math.Min(MAX_RADIUS, getDeltaFromRadius(radius[index].Item2)));
                startX -= 5;
                index  ++;
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
            int index = 0;
            foreach(var (color, _id) in colors)
            {
                if(_id == id)
                {
                    colors.Remove((color, _id));
                    radius.RemoveAt(index);
                    InvalidateVisual();
                    break;
                }
                index++;
            }
        }

        public void insertNewColor(Color color, int id)
        {
            colors.Add((color, id));
            radius.Add((0, 0));
            if (colors.Count > 14)
            {
                colors.RemoveAt(11);
                radius.RemoveAt(11);
            }
            if(!animationRunning)
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
