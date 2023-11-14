using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Poll2
{
    internal class SingleRowPoll : StackPanel
    {
        // Action callback
        public event Action<string, int, bool> ClickAction;

        // Graphic Object
        private RoundedList round               = new RoundedList(new List<(Color, int)>());
        private LinearProgressBar progress      = new LinearProgressBar();
        private Border border                   = new Border();
        private TextBlock textNum               = new TextBlock();

        // Data
        private string text;
        public  int id;
        private bool selected = false;
        private int personal_id = -1234;
        public int num_selection = 0;

        public SingleRowPoll(string text, int id, bool selected = false)
        {
            // Data
            this.text       = text;
            this.id         = id;
            this.selected   = selected;


            Orientation         = Orientation.Vertical;
            HorizontalAlignment = HorizontalAlignment.Center;
            Width               = 280;

            //************
            // First row of items

            var grid = new Grid();
            ColumnDefinition col1 = new ColumnDefinition();
            ColumnDefinition col2 = new ColumnDefinition();
            ColumnDefinition col3 = new ColumnDefinition();
            ColumnDefinition col4 = new ColumnDefinition();
            col1.Width = GridLength.Auto;
            col2.Width = new GridLength(1, GridUnitType.Star);
            col3.Width = GridLength.Auto;
            col4.Width = GridLength.Auto;
            grid.ColumnDefinitions.Add(col1);
            grid.ColumnDefinitions.Add(col2);
            grid.ColumnDefinitions.Add(col3);
            grid.ColumnDefinitions.Add(col4);

            border.Width    = 18;
            border.Height   = 18;
            border.CornerRadius     = new CornerRadius(10);
            border.BorderThickness  = new Thickness(1);
            border.BorderBrush      = new SolidColorBrush(Colors.Black);
            border.Background       = new SolidColorBrush(Colors.White);
            border.Margin           = new Thickness(0, 0, 6, 0);
            border.MouseLeftButtonUp += Border_MouseLeftButtonUp;

            // border
            Grid.SetColumn(border, 0);
            grid.Children.Add(border);

            // Text (possible answer)
            var textBlock = new TextBlock();
            textBlock.Text = text;
            textBlock.FontWeight = FontWeights.SemiBold;
            textBlock.VerticalAlignment         = VerticalAlignment.Center;
            textBlock.HorizontalAlignment       = HorizontalAlignment.Stretch;
            textBlock.TextWrapping              = TextWrapping.Wrap;
            textBlock.Margin                    = new Thickness(0, 3, 0, 6);
            textBlock.MouseLeftButtonUp += Border_MouseLeftButtonUp;
            textBlock.MaxWidth = 220;
            Grid.SetColumn(textBlock, 1);
            grid.Children.Add(textBlock);

            // circles
            round.HorizontalAlignment = HorizontalAlignment.Right;
            Grid.SetColumn(round, 2);
            grid.Children.Add(round);

            // Num of circles
            textNum.Text = "0";
            textNum.FontWeight = FontWeights.DemiBold;
            textNum.VerticalAlignment = VerticalAlignment.Center;
            textNum.HorizontalAlignment = HorizontalAlignment.Center;
            textNum.Margin = new Thickness(2, 0, 2, 0);
            Grid.SetColumn(textNum, 3);
            grid.Children.Add(textNum);

            //************
            // Second row of items

            var stackHorizontal2 = new StackPanel();
            stackHorizontal2.Orientation = Orientation.Horizontal;

            // progress
            stackHorizontal2.Children.Add(progress);

            this.Children.Add(grid);
            this.Children.Add(stackHorizontal2);
        }

        public SingleRowPoll setClickAction(Action <string, int, bool> action)
        {
            ClickAction += action;
            return this;
        }

        public SingleRowPoll select(int id, bool select = true)
        {
            selectInternal(select, id);
            return this;
        }

        private void Border_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            selected = !selected;
            selectInternal(selected, personal_id);
            ClickAction?.Invoke(text, id, selected);
        }

        private void selectInternal(bool select, int id)
        {
            // Button layout
            setBorderLayoutSelection();

            // circles draw
            if (select)
            {
                num_selection++;
                round.insertNewColor(StaticUtility.getColorFromId(id), id);
            }
            else
            {
                num_selection--;
                round.removeColor(id);
            }
            textNum.Text = num_selection.ToString();
        }

        public void setProgressValue(int value)
        {
            // progress
            progress.setValue(value);
        }

        private void setBorderLayoutSelection()
        {
            if (selected)
            {
                border.Background = StaticUtility.getCheckedImage();
                border.BorderThickness = new Thickness(0);
            }
            else
            {
                border.Background = StaticUtility.getUncheckedBrush();
                border.BorderThickness = new Thickness(1);
            }
        }
    }
}
