using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using Poll2;

namespace Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<(string ,int)> list = new List<(string, int)>()
                {
                    ("Delete every memeber of this group", 1),
                    ("Eat every meal inside the fridge", 2),
                    ("Sleep over than 12 hours", 3),
                    ("Be me", 4),
                };
        private Poll poll;
        private News news;

        public MainWindow()
        {
            DataContext = this;
            poll        = new Poll(list, "Do you need somthing to create something else?", 1);
            news = new("Big news", "Titolo delle news", "Descrizione piccolina per la big news", new List<string>()
            {
                "Primo punto",
            });

            InitializeComponent();

            poll.selectAnswer(list[1].Item2, 1234, true);
            poll.selectAnswer(list[2].Item2, 1234, true);
            poll.selectAnswer(list[1].Item2, 1111, true);


            //calendar.insertOrRemoveNewPartecipant(123);
        }

        public object ChildBorder
        {
            get
            {
                return news;
            }
        }
    }
}
