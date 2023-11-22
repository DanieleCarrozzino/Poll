using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Poll2
{
    public class News : Border
    {
        private string title        = "";
        private string subtitle     = "";
        private string description  = "";
        private string imagePath    = "";
        private List<string> list_description_points;

        public News(string title, string subtitle, string description, List<string> list_description_points, string imagePath = null)
        {
            this.title                      = title;
            this.subtitle                   = subtitle;
            this.description                = description;
            this.list_description_points    = list_description_points;
            this.imagePath                  = imagePath;

            createUI();
        }

        private void createUI()
        {
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
            Margin = new Thickness(20);
            
            Effect = new DropShadowEffect
            {
                ShadowDepth = 5,
                BlurRadius = 5,
                Opacity = 0.3,
                Color = Colors.Gray
            };

            StackPanel backPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
            };

            // Postman image
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(imagePath == null ? @"pack://application:,,,/Poll2;component/resources/images/cats/red_cat_tv.png" : imagePath);
            bitmap.EndInit();

            ImageBrush image = new ImageBrush
            {
                Stretch     = Stretch.UniformToFill,
                ImageSource = bitmap,
            };

            Border border = new Border
            {
                CornerRadius    = new CornerRadius(20, 0, 0, 20),
                Background      = image,
                Width           = 140,
                Margin          = new Thickness(0, 0, 0, 0),
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Left,
            };

            backPanel.Children.Add(border);

            Border leftBorder = new Border
            {
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f0fff0")),
                CornerRadius = new CornerRadius(20),
                Padding = new Thickness(10),
                Margin = new Thickness(-20, 0, 0, 0),
                MaxWidth = 260,
                MinHeight = 150,
            };

            StackPanel mainStackPanel = new StackPanel
            {
                Orientation = Orientation.Vertical,
                HorizontalAlignment= HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(2),
            };

            // TITLE
            TextBlock titleBlock = new TextBlock
            {
                Text = this.title,
                FontWeight = FontWeights.DemiBold,
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontSize = 19
            };
            mainStackPanel.Children.Add(titleBlock);

            // SUB title
            TextBlock subBlock = new TextBlock
            {
                Margin = new Thickness(0, 8, 0, 0),
                Text = this.subtitle,
                FontWeight = FontWeights.DemiBold,
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment= HorizontalAlignment.Left,
                FontSize = 13
            };
            mainStackPanel.Children.Add(subBlock);

            //DESCRIPTION
            TextBlock desBlock = new TextBlock
            {
                Margin = new Thickness(0, 0, 0, 0),
                Text = this.description,
                FontWeight = FontWeights.Regular,
                TextWrapping = TextWrapping.Wrap,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Left,
                FontSize = 13
            };
            mainStackPanel.Children.Add(desBlock);


            // DIFFERENT POINTS
            foreach (var text in list_description_points)
            {
                Grid grid = new Grid
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left,
                };

                Border b = new Border
                {
                    Width = 5,
                    Height = 5,
                    Margin = new Thickness(0, 6, 0, 0), 
                    CornerRadius = new CornerRadius(3),
                    VerticalAlignment = VerticalAlignment.Top,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Background = new SolidColorBrush(Colors.Black),
                };

                //DESCRIPTION
                TextBlock point = new TextBlock
                {
                    Margin = new Thickness(14, 0, 0, 0),
                    Text = text,
                    FontWeight = FontWeights.Regular,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    FontSize = 13,
                    TextWrapping = TextWrapping.Wrap,
                };

                grid.Children.Add(b);
                grid.Children.Add(point);

                // Add to the main stack panel
                mainStackPanel.Children.Add(grid);
            }

            leftBorder.Child = mainStackPanel;
            backPanel.Children.Add(leftBorder);
            this.Child = backPanel;
        }

    }
}
