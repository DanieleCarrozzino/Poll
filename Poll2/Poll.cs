using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
        private Action<string, int, bool> ClickAction;

        /// <summary>
        /// Total count of selected answers
        /// </summary>
        private int total_selected = 0;

        public Poll(List<(string, int)> answers, string question)
        {
            this.answers                = answers;
            this.Orientation            = Orientation.Vertical;
            this.HorizontalAlignment    = HorizontalAlignment.Left;
            this.Margin                 = new Thickness(6);

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
