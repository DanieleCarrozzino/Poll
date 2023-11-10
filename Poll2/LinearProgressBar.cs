using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Poll2
{
    internal class LinearProgressBar : FrameworkElement
    {
        private const int   MAX_WIDTH = 260;
        private const float UNITY     = (float)MAX_WIDTH / (float)100;

        private double value = 0;
        private double value_to_reach = 0;
        private bool animationRunning = false;

        public LinearProgressBar()
        {
            this.Margin = new Thickness(24, 0, 0, 0);
            this.Height = 12;
            this.Width  = MAX_WIDTH;
        }

        protected override void OnRender(DrawingContext dc)
        {
            // Gray rectangle on the background
            Rect rect = new Rect(0, 0, MAX_WIDTH, 10);
            double cornerRadius = 5;
            dc.DrawRoundedRectangle(Brushes.LightGray, new Pen(), rect, cornerRadius, cornerRadius);

            // Main rectangle
            rect = new Rect(0, -1, Math.Max(value * UNITY, 0), 12);
            dc.DrawRoundedRectangle(Brushes.Green, new Pen(), rect, cornerRadius, cornerRadius);
        }

        public void setValue(int value)
        {
            value_to_reach = value;
            if(!animationRunning)
                startAnimation();
        }

        private async void startAnimation()
        {
            await Task.Run(() =>
            {
                animationRunning = true;
                while (value != value_to_reach)
                {
                    Task.Delay(10).Wait();

                    // get the difference
                    var add = Math.Abs(value - value_to_reach);

                    // divide 10 and get the minimun between
                    // 0.1 and teh result of the division
                    add /= 10;
                    add = Math.Max(0.1, add);
                    
                    // Add the result
                    value += value_to_reach > value ? add : -add;
                    try
                    {
                        Dispatcher.Invoke(() =>
                        {
                            InvalidateVisual();
                        });
                    }
                    catch(Exception ex) { 
                        // the object is closed
                    }
                }
                animationRunning = false;
            });
        }

    }
}
