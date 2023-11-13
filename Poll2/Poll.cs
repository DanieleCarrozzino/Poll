using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace Poll2
{
    public class Poll : StackPanel
    {
        /// <summary>
        /// List of answers, textual question and the id
        /// </summary>
        private List<(string, int)> answers;

        /// <summary>
        /// Click action callback
        /// </summary>
        public Action<string, int, bool> ClickAction;

        /// <summary>
        /// Total count of selected answers
        /// </summary>
        private int total_selected = 0;

        /// <summary>
        /// Poll id, how to retrive your poll
        /// </summary>
        public int poll_id;

        public Poll(List<(string, int)> answers, string question, int poll_id)
        {
            this.poll_id                = poll_id;
            this.answers                = answers;
            this.Orientation            = Orientation.Vertical;
            this.HorizontalAlignment    = HorizontalAlignment.Left;
            this.Margin                 = new Thickness(10);

            // Add title / question
            var textBlock                   = new TextBlock();
            textBlock.Text                  = question;
            textBlock.FontWeight            = FontWeights.Bold;
            textBlock.VerticalAlignment     = VerticalAlignment.Center;
            textBlock.HorizontalAlignment   = HorizontalAlignment.Left;
            textBlock.TextWrapping          = TextWrapping.WrapWithOverflow;
            textBlock.MaxWidth              = 280;
            textBlock.FontSize              = 14;
            textBlock.Margin                = new Thickness(5, 0, 0, 0);

            // Icon
            Image iconPoll  = new Image();
            iconPoll.Source = StaticUtility.getPollIcon();
            iconPoll.Width  = 14;
            iconPoll.Height = 14;

            var stack = new StackPanel();
            stack.Orientation = Orientation.Horizontal;
            stack.MaxWidth = 310;
            stack.Margin = new Thickness(2, 0, 0, 0);
            stack.Children.Add(iconPoll);
            stack.Children.Add(textBlock);

            this.Children.Add(stack);

            // Add subtitle / rules
            var textRules = new TextBlock();
            textRules.Text = "choose 1 or more options";
            textRules.FontWeight = FontWeights.Normal;
            textRules.VerticalAlignment = VerticalAlignment.Center;
            textRules.HorizontalAlignment = HorizontalAlignment.Left;
            textRules.TextWrapping = TextWrapping.Wrap;
            textRules.FontSize = 13;
            textRules.Foreground = new SolidColorBrush(Colors.Gray);
            textRules.Margin = new Thickness(3, 0, 0, 0);

            // Icon
            Image iconMult = new Image();
            iconMult.Source = StaticUtility.getMultipleSelectionIcon();
            iconMult.Width = 17;
            iconMult.Height = 17;

            var stack2 = new StackPanel();
            stack2.Orientation = Orientation.Horizontal;
            stack2.MaxWidth = 310;
            stack2.Margin = new Thickness(1, 0, 0, 0);
            stack2.Children.Add(iconMult);
            stack2.Children.Add(textRules);

            this.Children.Add(stack2);

            // Add answers
            foreach (var answer in this.answers)
            {
                this.Children.Add((new SingleRowPoll(answer.Item1, answer.Item2)).setClickAction(ClickCallback));
            }

            
            // View result button
            Border border = new Border();
            border.Padding = new Thickness(17, 8, 17, 8);
            border.Margin = new Thickness(0, 10, 0, 0);
            border.CornerRadius = new CornerRadius(15);
            Color color = (Color)ColorConverter.ConvertFromString("#457da1");
            border.Background = new SolidColorBrush(color);
            border.HorizontalAlignment = HorizontalAlignment.Center;

            TextBlock textBlockResult   = new TextBlock();
            textBlockResult.Text        = "View result";
            textBlockResult.FontWeight  = FontWeights.DemiBold;
            textBlockResult.Foreground  = new SolidColorBrush(Colors.White);

            border.Child = textBlockResult;

            // Shadow effect
            border.Effect = new DropShadowEffect
            {
                ShadowDepth = 0,
                BlurRadius = 10,
                Color = (Color)ColorConverter.ConvertFromString("#55434343"),
                Opacity = 0.1
            };
            border.MouseEnter += StaticUtility.Border_MouseEnter;
            border.MouseLeave += StaticUtility.Border_MouseLeave;

            this.Children.Add(border);
        }

        /// <summary>
        /// external selection
        /// </summary>
        /// <param name="id">id question</param>
        /// <param name="id_user">id user</param>
        /// <param name="select"></param>
        public void selectAnswer(int id, int id_user, bool select = true)
        {
            total_selected += select ? 1 : -1;
            foreach (var row in this.Children)
            {
                if (row is SingleRowPoll)
                {
                    if ((row as SingleRowPoll).id == id)
                    {
                        (row as SingleRowPoll).select(id_user, select);
                        break;
                    }
                }
            }
            reDrawTheProgress();
        }

        private void reDrawTheProgress()
        {
            foreach (var row in this.Children)
            {
                if(row is SingleRowPoll)
                    (row as SingleRowPoll).setProgressValue(((row as SingleRowPoll).num_selection * 100) / (Math.Max(total_selected, 1)));
            }
        }

        /// <summary>
        /// Click manager callback
        /// </summary>
        /// <param name="text"></param>
        /// <param name="id"></param>
        private void ClickCallback(string text, int id, bool selected)
        {
            total_selected += selected ? 1 : -1;
            reDrawTheProgress();
            ClickAction?.Invoke(text, id, selected);
        }
    }
}
