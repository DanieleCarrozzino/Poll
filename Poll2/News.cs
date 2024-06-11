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
using static System.Net.Mime.MediaTypeNames;

namespace Poll2
{
    public class News : Border
    {
        private string title        = "";
        private string subtitle     = "";
        private string description  = "";
        private string imagePath    = "";
        private List<string> list_description_points;

        public Action<string> ButtonClicked = null;
        private LinearProgressBar linear;
        private TextBlock downloadText;
        private Border button;
        private ImageBrush image;
        private Border borderImage;
        private bool is_downloading = false;

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
            bitmap.UriSource = new Uri(imagePath == null ? @"pack://application:,,,/Poll2;component/resources/images/cats/" + StaticUtility.getRandomNameCat() : imagePath);
            bitmap.EndInit();

            image = new ImageBrush
            {
                Stretch     = Stretch.UniformToFill,
                ImageSource = bitmap,
            };

            borderImage = new Border
            {
                CornerRadius    = new CornerRadius(20, 0, 0, 20),
                Background      = image,
                Width           = 140,
                Margin          = new Thickness(0, 0, 0, 0),
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Left,
            };

            backPanel.Children.Add(borderImage);

            Border leftBorder = new Border
            {
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#fafaff")),
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
                    VerticalAlignment = VerticalAlignment.Center,
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

            Grid containerButton = new Grid
            {
                Margin = new Thickness(0, 20, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
            };

            button = new Border
            {
                Background = new SolidColorBrush(Utility.Blue),
                CornerRadius = new CornerRadius(17),
                VerticalAlignment= VerticalAlignment.Center,
                HorizontalAlignment= HorizontalAlignment.Center,
                Padding = new Thickness(0, 0, 0, 0),
                Width = 180,
                Height = 38,
                Effect = new DropShadowEffect
                {
                    ShadowDepth     = 5,
                    BlurRadius      = 5,
                    Opacity         = 0.5,
                    Color           = Colors.Gray,
                },
            };

            button.MouseLeftButtonUp += Button_MouseLeftButtonUp;

            downloadText = new TextBlock
            {
                Margin = new Thickness(0, 0, 0, 0),
                Text = "Download and install",
                FontWeight = FontWeights.DemiBold,
                Foreground = new SolidColorBrush(Colors.White),
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontSize = 13,
                TextWrapping = TextWrapping.Wrap,
            };

            downloadText.MouseLeftButtonUp += Button_MouseLeftButtonUp;

            linear = new LinearProgressBar(180, 38, Utility.Blue);

            containerButton.Children.Add(linear);
            containerButton.Children.Add(button);
            containerButton.Children.Add(downloadText);
            mainStackPanel.Children.Add(containerButton);

            leftBorder.Child = mainStackPanel;
            backPanel.Children.Add(leftBorder);
            this.Child = backPanel;
        }

        private void setImage(string path)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path);
            bitmap.EndInit();

            image = new ImageBrush
            {
                Stretch = Stretch.UniformToFill,
                ImageSource = bitmap,
            };
            borderImage.Background = image;
        }

        private void Button_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!is_downloading && ButtonClicked != null)
            {
                is_downloading = true;
                button.Opacity = 0;
                downloadText.Text = "Downloading...";
                ButtonClicked?.Invoke("");
            }            
        }

        public void setButtonText(string text)
        {
            downloadText.Text = text;
            setImage(@"pack://application:,,,/Poll2;component/resources/images/cats/happy_red_cat_dad.png");
        }

        public void update_progress(int value)
        {
            linear.setValue(value);
            if(value == 100)
            {
                is_downloading      = false;
                button.Opacity      = 1;
                downloadText.Text   = "Installing...";
            }
        }
    }
}
