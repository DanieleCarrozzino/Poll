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
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Threading;

namespace Poll2
{
    public class CallCalendar : Border
    {

        // Global objects
        private Border remindMeBorder = null;
        private RoundedListLeft roundedList = null;
        private TextBlock participantsInfoTextBlock = null;
        private StackPanel stackPanelPlaceholder;
        private StackPanel participantsInfoStackPanel;
        private Border sendParticipationBorder;
        private TextBlock startTimeTextBlock;
        private TextBlock durationTimeTextBlock;
        private TextBlock titleTextBlock;
        private TextBlock descriptionTextBlock;
        private bool selected = false;
        public int calendar_id = -1;
        public int personal_id = -1;
        public DateTime startTime;
        public int duration = 0; // duration in minutes

        /// <summary>
        /// Click action callback
        /// </summary>
        public Action<bool, int> ClickAction;


        public CallCalendar()
        {
            this.calendar_id = -1; // test
            createUI();
        }

        public CallCalendar(int id, Dispatcher dispatcher, int personal_id)
        {
            this.calendar_id = id;
            this.personal_id = personal_id;

            // Main thread
            dispatcher.Invoke(() =>
            {
                createUI();
            });
        }

        public void insertOrRemoveNewPartecipant(int id)
        {
            bool result = roundedList.insertOrRemoveNewColor(StaticUtility.getColorFromId(id), id);
            if(id == personal_id && result)
            {
                sendParticipationBorder.Visibility = Visibility.Collapsed;
            }
            changeLayoutAfterAddSomeone();
        }

        public void setDateAndDuration(string dateToFormat, int duration)
        {
            this.duration = duration;
            if (DateTime.TryParse(dateToFormat, out startTime))
            {
                startTimeTextBlock.Text = startTime.ToString("HH:mm dd/MM/yyyy");
            }            
            durationTimeTextBlock.Text  = duration.ToString() + " min";
        }

        public void setTitleAndDescription(string title, string description)
        {
            titleTextBlock.Text         = title;
            descriptionTextBlock.Text   = description;
        }

        // Layout
        private void createUI()
        {
            HorizontalAlignment = HorizontalAlignment.Center;
            VerticalAlignment = VerticalAlignment.Center;
            CornerRadius = new CornerRadius(10);
            Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#f0fff0"));
            Padding = new Thickness(10);
            Margin = new Thickness(10);
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
            titleTextBlock = new TextBlock
            {
                Margin = new Thickness(7, 0, 7, 0),
                Text = "",
                FontWeight = FontWeights.DemiBold,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 13
            };

            mainPanel.Children.Add(titleTextBlock);

            // Description TextBlock
            descriptionTextBlock = new TextBlock
            {
                Margin = new Thickness(7, 0, 7, 0),
                TextWrapping = TextWrapping.WrapWithOverflow,
                MaxWidth = 230,
                Text = ""
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
            participantsInfoStackPanel = new StackPanel
            {
                Margin = new Thickness(7, 0, 7, 0),
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Left
            };

            // RoundedList
            roundedList = new RoundedListLeft
            {
                HorizontalAlignment = HorizontalAlignment.Left
            };

            participantsInfoStackPanel.Children.Add(roundedList);

            // Participants Information TextBlock
            participantsInfoTextBlock = new TextBlock
            {
                Margin = new Thickness(5, 0, 0, 0),
                Text = "The scheduled conference has no booked participants at the moment.",
                TextWrapping = TextWrapping.WrapWithOverflow,
                MaxWidth = 150,
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                FontWeight = FontWeights.Normal,
                FontSize = 12
            };

            participantsInfoStackPanel.Children.Add(participantsInfoTextBlock);
            participantsInfoStackPanel.Visibility = Visibility.Collapsed;

            // Place holder
            Image image = new Image
            {
                Width = 25,
                Height = 25,
                Source = new BitmapImage(new Uri("pack://application:,,,/Poll2;component/resources/images/fired.png"))
            };

            // Create TextBlock element
            TextBlock textBlock = new TextBlock
            {
                Margin = new Thickness(5, 0, 0, 0),
                Text = "No participants yet",
                VerticalAlignment = VerticalAlignment.Center
            };

            // Create StackPanel element and add Image and TextBlock
            stackPanelPlaceholder = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(10, 0, 0, 0),
            };
            stackPanelPlaceholder.Children.Add(image);
            stackPanelPlaceholder.Children.Add(textBlock);
            stackPanelPlaceholder.Visibility = Visibility.Visible;


            mainPanel.Children.Add(participantsInfoStackPanel);
            mainPanel.Children.Add(stackPanelPlaceholder);
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



            StackPanel stackHoriMain = new StackPanel
            {
                Margin = new Thickness(7, 0, 7, 0),
                Orientation = Orientation.Horizontal
            };

            Grid stackVert1 = new Grid
            {
                VerticalAlignment = VerticalAlignment.Stretch,
            };

            RowDefinition col1 = new RowDefinition();
            RowDefinition col2 = new RowDefinition();
            col1.Height = new GridLength(1, GridUnitType.Star);
            col2.Height = new GridLength(1, GridUnitType.Star);
            stackVert1.RowDefinitions.Add(col1);
            stackVert1.RowDefinitions.Add(col2);

            StackPanel stackVert2 = new StackPanel
            {
                Margin = new Thickness(0, 0, 0, 0),
                Orientation = Orientation.Vertical,
                VerticalAlignment = VerticalAlignment.Stretch,
            };

            TextBlock startTextBlock = new TextBlock
            {
                Text = "start",
                FontWeight = FontWeights.Normal,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 12
            };

            TextBlock durationTextBlock = new TextBlock
            {
                Text = "duration",
                FontWeight = FontWeights.Normal,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 12
            };

            Grid.SetRow(startTextBlock, 0);
            stackVert1.Children.Add(startTextBlock);
            Grid.SetRow(durationTextBlock, 1);
            stackVert1.Children.Add(durationTextBlock);

            stackHoriMain.Children.Add(stackVert1);

            startTimeTextBlock = new TextBlock
            {
                Margin = new Thickness(5, 0, 0, 0),
                Text = "-",
                FontWeight = FontWeights.DemiBold,
                FontSize = 14
            };

            durationTimeTextBlock = new TextBlock
            {
                Margin = new Thickness(5, 0, 0, 0),
                Text = "-",
                FontWeight = FontWeights.DemiBold,
                FontSize = 14
            };

            stackVert2.Children.Add(startTimeTextBlock);
            stackVert2.Children.Add(durationTimeTextBlock);

            stackHoriMain.Children.Add(stackVert2);

            mainPanel.Children.Add(stackHoriMain);
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
            sendParticipationBorder = new Border
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
            sendParticipationBorder.MouseLeftButtonUp += SendParticipationBorder_MouseLeftButtonUp;

            // Send Participation Border Effect
            sendParticipationBorder.Effect = new DropShadowEffect
            {
                ShadowDepth = 0,
                BlurRadius = 10,
                Color = (Color)ColorConverter.ConvertFromString("#55434343"),
                Opacity = 0.1
            };
        }

        private void SendParticipationBorder_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (roundedList.insertOrRemoveNewColor(StaticUtility.getColorFromId(calendar_id), calendar_id))
            {
                sendParticipationBorder.Visibility = Visibility.Collapsed;
                ClickAction?.Invoke(true, calendar_id);
            }
            changeLayoutAfterAddSomeone();
        }

        private void changeLayoutAfterAddSomeone()
        {
            if (roundedList.getCountOfColors() > 0)
            {
                stackPanelPlaceholder.Visibility = Visibility.Collapsed;
                participantsInfoStackPanel.Visibility = Visibility.Visible;
                participantsInfoTextBlock.Text = "There are currently " + roundedList.getCountOfColors() + " participants attending the conference.";
            }
            else
            {
                stackPanelPlaceholder.Visibility = Visibility.Visible;
                participantsInfoStackPanel.Visibility = Visibility.Collapsed;
            }
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
