using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Resources;

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
    }
}
