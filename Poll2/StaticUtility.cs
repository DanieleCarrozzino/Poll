using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Resources;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows;

namespace Poll2
{
    internal static class StaticUtility
    {
        /// <summary>
        /// Checked image cache
        /// </summary>
        private static ImageBrush checkedImage = null;

        /// <summary>
        /// Get the checked image 
        /// for the selection mode of a row
        /// </summary>
        /// <returns></returns>
        public static ImageBrush getCheckedImage()
        {
            if (checkedImage == null)
            {
                // create a new BitmapImage
                BitmapImage bitmapImage = new BitmapImage();

                // set the image source to the path of the image file
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(
                    @"pack://application:,,,/Poll2;component/resources/images/checked.png",
                    UriKind.RelativeOrAbsolute);
                bitmapImage.EndInit();

                // create a new ImageBrush from the BitmapImage
                ImageBrush imageBrush = new ImageBrush(bitmapImage);
                imageBrush.Stretch = Stretch.Fill;

                if (imageBrush.CanFreeze)
                {
                    imageBrush.Freeze();
                }

                checkedImage = imageBrush;
            }
            return checkedImage;
        }

        /// <summary>
        /// brush cache
        /// </summary>
        private static SolidColorBrush uncheckedBrush = null;

        /// <summary>
        /// get the solidcolorbrush for
        /// teh unselected mode of a single row
        /// </summary>
        /// <returns></returns>
        public static SolidColorBrush getUncheckedBrush()
        {
            if (uncheckedBrush == null)
            {
                uncheckedBrush = new SolidColorBrush(Colors.White);
                uncheckedBrush.Freeze();
            }
            return uncheckedBrush;                        
        }


        /// <summary>
        /// Poll icon
        /// </summary>
        private static BitmapImage pollIcon = null;

        /// <summary>
        /// Get the checked image 
        /// for the selection mode of a row
        /// </summary>
        /// <returns></returns>
        public static BitmapImage getPollIcon()
        {
            if (pollIcon == null)
            {
                // create a new BitmapImage
                pollIcon = new BitmapImage();

                // set the image source to the path of the image file
                pollIcon.BeginInit();
                pollIcon.UriSource = new Uri(
                    @"pack://application:,,,/Poll2;component/resources/images/poll.png",
                    UriKind.RelativeOrAbsolute);
                pollIcon.EndInit();
                pollIcon.Freeze();
            }
            return pollIcon;
        }


        /// <summary>
        /// multiple selection icon cache
        /// </summary>
        private static BitmapImage multipleSelectionIcon = null;

        /// <summary>
        /// Get the checked image 
        /// for the selection mode of a row
        /// </summary>
        /// <returns></returns>
        public static BitmapImage getMultipleSelectionIcon()
        {
            if (multipleSelectionIcon == null)
            {
                // create a new BitmapImage
                multipleSelectionIcon = new BitmapImage();

                // set the image source to the path of the image file
                multipleSelectionIcon.BeginInit();
                multipleSelectionIcon.UriSource = new Uri(
                    @"pack://application:,,,/Poll2;component/resources/images/multiple_selection.png",
                    UriKind.RelativeOrAbsolute);
                multipleSelectionIcon.EndInit();
                multipleSelectionIcon.Freeze();
            }
            return multipleSelectionIcon;
        }

        /// <summary>
        /// multiple selection icon cache
        /// </summary>
        private static BitmapImage teamIcon = null;

        /// <summary>
        /// Get the checked image 
        /// for the selection mode of a row
        /// </summary>
        /// <returns></returns>
        public static BitmapImage getTeamIcon()
        {
            if (teamIcon == null)
            {
                // create a new BitmapImage
                teamIcon = new BitmapImage();

                // set the image source to the path of the image file
                teamIcon.BeginInit();
                teamIcon.UriSource = new Uri(
                    @"pack://application:,,,/Poll2;component/resources/images/team.png",
                    UriKind.RelativeOrAbsolute);
                teamIcon.EndInit();
                teamIcon.Freeze();
            }
            return teamIcon;
        }

        /// <summary>
        /// multiple selection icon cache
        /// </summary>
        private static BitmapImage calendarClock = null;

        /// <summary>
        /// Get the checked image 
        /// for the selection mode of a row
        /// </summary>
        /// <returns></returns>
        public static BitmapImage getCalendarClockIcon()
        {
            if (calendarClock == null)
            {
                // create a new BitmapImage
                calendarClock = new BitmapImage();

                // set the image source to the path of the image file
                calendarClock.BeginInit();
                calendarClock.UriSource = new Uri(
                    @"pack://application:,,,/Poll2;component/resources/images/calendar_clock.png",
                    UriKind.RelativeOrAbsolute);
                calendarClock.EndInit();
                calendarClock.Freeze();
            }
            return calendarClock;
        }

        /// <summary>
        /// multiple selection icon cache
        /// </summary>
        private static BitmapImage calendarCall = null;

        /// <summary>
        /// Get the checked image 
        /// for the selection mode of a row
        /// </summary>
        /// <returns></returns>
        public static BitmapImage getCalendarCallIcon()
        {
            if (calendarCall == null)
            {
                // create a new BitmapImage
                calendarCall = new BitmapImage();

                // set the image source to the path of the image file
                calendarCall.BeginInit();
                calendarCall.UriSource = new Uri(
                    @"pack://application:,,,/Poll2;component/resources/images/calendar_call.png",
                    UriKind.RelativeOrAbsolute);
                calendarCall.EndInit();
                calendarCall.Freeze();
            }
            return calendarCall;
        }



        public static void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            ElevationAnimation((Border)sender, true, true);
        }

        public static void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            ElevationAnimation((Border)sender, false, false);
        }

        private static void ElevationAnimation(Border border, bool up, bool selected)
        {
            DoubleAnimation anim = new DoubleAnimation();
            anim.From = up ? 0 : (selected ? 5 : 5);
            anim.To = up ? (selected ? 5 : 5) : 0;
            anim.Duration = TimeSpan.FromSeconds(0.2);

            DoubleAnimation animOpacity = new DoubleAnimation();
            animOpacity.From = up ? 0.1 : (selected ? 0.5 : 0.5);
            animOpacity.To = up ? (selected ? 0.5 : 0.5) : 0.1;
            animOpacity.Duration = TimeSpan.FromSeconds(0.2);

            Storyboard sb = new Storyboard();
            sb.Children.Add(anim);
            sb.Children.Add(animOpacity);

            Storyboard.SetTarget(anim, border);
            Storyboard.SetTargetProperty(anim, new PropertyPath("(Border.Effect).(DropShadowEffect.ShadowDepth)"));

            Storyboard.SetTarget(animOpacity, border);
            Storyboard.SetTargetProperty(animOpacity, new PropertyPath("(Border.Effect).(DropShadowEffect.Opacity)"));
            sb.Begin();
        }


        public static Color getColorFromId(int id)
        {
            int color = Math.Abs(id % 9);
            ColorDictionary.TryGetValue(color, out string outValue);
            return (Color)ColorConverter.ConvertFromString(outValue); ;
        }

        public static Dictionary<int, string> ColorDictionary = new Dictionary<int, string>() {

            {8,  "#FF9900"},
            {7,  "#D329D3"},
            {6,  "#1EB1D8"},
            {5,  "#2A9B2A"},
            {4,  "#E01E1E"},
            {3,  "#7A4610"},
            {2,  "#3A36CB"},
            {1,  "#666666"},
            {0,  "#000000"},

        };
    }
}
