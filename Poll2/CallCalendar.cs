using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Reflection.Metadata;
using System.Xml.Linq;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Reflection;
using System.Windows.Controls.Primitives;

namespace Poll2
{
    public class CallCalendar : Border
    {

        // Global objects
        private Border remindMeBorder = null;
        private bool selected = false;

        public CallCalendar()
        {
            createUI();
        }

        // Layout

        private void createUI()
        {
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
            CornerRadius = new CornerRadius(10);
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f0fff0"));
            Padding = new Thickness(10);
            Margin = new Thickness(4);
            Effect = new DropShadowEffect
            {
                ShadowDepth = 5,
                BlurRadius = 5,
                Opacity = 0.3,
                Color = Colors.Gray
            };

            StackPanel mainStackPanel = new StackPanel
            {
                Orientation = Orientation.Vertical
            };

            // Header Border
            createHeaderUI(mainStackPanel);

            // info conference
            createInfoUI(mainStackPanel);

            // Start and Duration TextBlocks
            createTimeUI(mainStackPanel);

            // Participants Border
            createPartecipatsUI(mainStackPanel);

            // Remind Me Border
            createBottomButtonsUI(mainStackPanel);
        }

        private void createHeaderUI(StackPanel mainPanel)
        {
            Border headerBorder = new Border
            {
                Padding = new Thickness(2)
            };

            StackPanel headerStackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal
            };

            Image headerImage = new Image
            {
                Width = 25,
                Height = 25,
                Margin = new Thickness(8, 5, 5, 5),
                Source = StaticUtility.getCalendarCallIcon()
            };

            TextBlock headerTextBlock = new TextBlock
            {
                Margin = new Thickness(4, 0, 0, 0),
                Text = "Conference",
                FontWeight = FontWeights.DemiBold,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 14
            };

            headerStackPanel.Children.Add(headerImage);
            headerStackPanel.Children.Add(headerTextBlock);
            headerBorder.Child = headerStackPanel;

            mainPanel.Children.Add(headerBorder);
            this.Child = mainPanel;
        }

        private void createInfoUI(StackPanel mainPanel)
        {
            // Title TextBlock
            TextBlock titleTextBlock = new TextBlock
            {
                Margin = new Thickness(7, 0, 7, 0),
                Text = "Title conference",
                FontWeight = FontWeights.DemiBold,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 13
            };

            mainPanel.Children.Add(titleTextBlock);

            // Description TextBlock
            TextBlock descriptionTextBlock = new TextBlock
            {
                Margin = new Thickness(7, 0, 7, 0),
                TextWrapping = TextWrapping.WrapWithOverflow,
                MaxWidth = 230,
                Text = "Description of the conference, with some interesting points"
            };

            mainPanel.Children.Add(descriptionTextBlock);
        }

        private void createPartecipatsUI(StackPanel mainPanel)
        {
            // Participants Border
            Border participantsBorder = new Border
            {
                Padding = new Thickness(2),
                Margin = new Thickness(0, 15, 0, 0)
            };

            // Participants StackPanel
            StackPanel participantsStackPanel = new StackPanel
            {
                Margin = new Thickness(5),
                Orientation = Orientation.Horizontal
            };

            participantsBorder.Child = participantsStackPanel;

            // Participants Image
            Image participantsImage = new Image
            {
                Width = 16,
                Height = 16,
                Margin = new Thickness(4, 0, 5, 0),
                Source = StaticUtility.getTeamIcon()
            };

            participantsStackPanel.Children.Add(participantsImage);

            // Participants TextBlock
            TextBlock participantsTextBlock = new TextBlock
            {
                Margin = new Thickness(4, 0, 0, 0),
                Text = "Partecipants",
                FontWeight = FontWeights.DemiBold,
                Foreground = Brushes.Black,
                FontSize = 12
            };

            participantsStackPanel.Children.Add(participantsTextBlock);

            mainPanel.Children.Add(participantsBorder);

            // Participants Information StackPanel
            StackPanel participantsInfoStackPanel = new StackPanel
            {
                Margin = new Thickness(7, 0, 7, 0),
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            // RoundedList
            RoundedListLeft roundedList = new RoundedListLeft
            {
                HorizontalAlignment = HorizontalAlignment.Left
            };

            participantsInfoStackPanel.Children.Add(roundedList);

            // Participants Information TextBlock
            TextBlock participantsInfoTextBlock = new TextBlock
            {
                Margin = new Thickness(5, 0, 0, 0),
                Text = "2 participants for this conference",
                TextWrapping = TextWrapping.WrapWithOverflow,
                MaxWidth = 150,
                VerticalAlignment = VerticalAlignment.Center,
                FontWeight = FontWeights.Normal,
                FontSize = 12
            };

            participantsInfoStackPanel.Children.Add(participantsInfoTextBlock);

            mainPanel.Children.Add(participantsInfoStackPanel);
        }

        private void createTimeUI(StackPanel mainPanel)
        {
            Border descriptionBorder = new Border
            {
                Padding = new Thickness(2),
                Margin = new Thickness(0, 15, 0, 0)
            };

            StackPanel descriptionStackPanel = new StackPanel
            {
                Margin = new Thickness(5),
                Orientation = Orientation.Horizontal
            };

            Image descriptionImage = new Image
            {
                Width = 16,
                Height = 16,
                Margin = new Thickness(4, 0, 5, 0),
                Source = StaticUtility.getCalendarClockIcon()
            };

            TextBlock descriptionTextBlock = new TextBlock
            {
                Margin = new Thickness(4, 0, 0, 0),
                Text = "Scheduled for",
                FontWeight = FontWeights.DemiBold,
                Foreground = Brushes.Black,
                FontSize = 12
            };

            descriptionStackPanel.Children.Add(descriptionImage);
            descriptionStackPanel.Children.Add(descriptionTextBlock);
            descriptionBorder.Child = descriptionStackPanel;

            mainPanel.Children.Add(descriptionBorder);

            StackPanel startDurationStackPanel = new StackPanel
            {
                Margin = new Thickness(7, 0, 7, 0),
                Orientation = Orientation.Horizontal
            };

            TextBlock startTextBlock = new TextBlock
            {
                Text = "start",
                FontWeight = FontWeights.Normal,
                FontSize = 12
            };

            TextBlock startTimeTextBlock = new TextBlock
            {
                Margin = new Thickness(5, 0, 0, 0),
                Text = "--:--",
                FontWeight = FontWeights.Normal,
                FontSize = 12
            };

            TextBlock durationTextBlock = new TextBlock
            {
                Margin = new Thickness(10, 0, 0, 0),
                Text = "duration",
                FontWeight = FontWeights.Normal,
                FontSize = 12
            };

            TextBlock durationTimeTextBlock = new TextBlock
            {
                Margin = new Thickness(5, 0, 0, 0),
                Text = "--:--",
                FontWeight = FontWeights.Normal,
                FontSize = 12
            };

            startDurationStackPanel.Children.Add(startTextBlock);
            startDurationStackPanel.Children.Add(startTimeTextBlock);
            startDurationStackPanel.Children.Add(durationTextBlock);
            startDurationStackPanel.Children.Add(durationTimeTextBlock);

            mainPanel.Children.Add(startDurationStackPanel);
        }

        private void createBottomButtonsUI(StackPanel mainPanel)
        {
            // Remind Me StackPanel
            StackPanel remindMeStackPanel = new StackPanel
            {
                Margin = new Thickness(7, 30, 7, 0),
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Center
            };

            mainPanel.Children.Add(remindMeStackPanel);

            // Remind Me Border
            remindMeBorder = new Border
            {
                Width = 18,
                Height = 18,
                CornerRadius = new CornerRadius(9),
                BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#45a15c")),
                BorderThickness = new Thickness(1),
                HorizontalAlignment = HorizontalAlignment.Left,
                Background = Brushes.White
            };

            remindMeStackPanel.Children.Add(remindMeBorder);

            // Remind Me TextBlock
            TextBlock remindMeTextBlock = new TextBlock
            {
                Margin = new Thickness(5, 0, 0, 0),
                Text = "Remind me this conference",
                FontWeight = FontWeights.Normal,
                FontSize = 12
            };
            remindMeTextBlock.MouseLeftButtonUp += RemindMeTextBlock_MouseLeftButtonUp;
            remindMeBorder.MouseLeftButtonUp    += RemindMeTextBlock_MouseLeftButtonUp;

            remindMeStackPanel.Children.Add(remindMeTextBlock);

            // Send Participation Border
            Border sendParticipationBorder = new Border
            {
                Padding = new Thickness(20, 8, 20, 8),
                Margin = new Thickness(0, 5, 0, 0),
                CornerRadius = new CornerRadius(15),
                Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#45a15c")),
                HorizontalAlignment = HorizontalAlignment.Center
            };

            mainPanel.Children.Add(sendParticipationBorder);

            // Send Participation TextBlock
            TextBlock sendParticipationTextBlock = new TextBlock
            {
                Text = "Send your participation",
                FontWeight = FontWeights.DemiBold,
                Foreground = Brushes.White
            };

            sendParticipationBorder.Child = sendParticipationTextBlock;
            sendParticipationBorder.MouseEnter += StaticUtility.Border_MouseEnter;
            sendParticipationBorder.MouseLeave += StaticUtility.Border_MouseLeave;

            // Send Participation Border Effect
            sendParticipationBorder.Effect = new DropShadowEffect
            {
                ShadowDepth = 0,
                BlurRadius = 10,
                Color = (Color)ColorConverter.ConvertFromString("#55434343"),
                Opacity = 0.1
            };
        }

        // Event

        private void RemindMeTextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            selected = !selected;
            if (selected)
            {
                remindMeBorder.Background = StaticUtility.getCheckedImage();
                remindMeBorder.BorderThickness = new Thickness(0);
            }
            else
            {
                remindMeBorder.Background = StaticUtility.getUncheckedBrush();
                remindMeBorder.BorderThickness = new Thickness(1);
            }
        }
    }
}
