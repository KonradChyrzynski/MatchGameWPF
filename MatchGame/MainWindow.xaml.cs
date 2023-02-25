using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MatchGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthsOfSecondsElapsed;
        int matchesFound;

        TextBlock lastTextboxClicked;
        bool findingMatch = false;


        public MainWindow()
        {
            InitializeComponent();
            SetUpGame();
        }

        private void SetUpGame()
        {
            SetUpTimer();

            List<string> animalEmoji = new List<string>()
            {
                "🐱","🐱",
                "🦁","🦁",
                "🐯","🐯",
                "🦒","🦒",
                "🦊","🦊",
                "🦝","🦝",
                "🐮","🐮",
                "🐗","🐗"
            };

            Random random = new Random();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>().Where(x => x.Name != "timeTextBlock"))
            {
                int index = random.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                textBlock.Text = nextEmoji;
                animalEmoji.RemoveAt(index);
            };
        }

        private void SetUpTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            tenthsOfSecondsElapsed += 1;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10F).ToString("0.0s");
            if (matchesFound == 8) 
            {
                timer.Stop();
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textblock = sender as TextBlock;

            if (findingMatch == false)
            {
                textblock.Visibility = Visibility.Hidden;
                lastTextboxClicked = textblock;
                findingMatch = true;
            }
            else if (textblock.Text == lastTextboxClicked.Text)
            {
                matchesFound++;
                textblock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else 
            {
                lastTextboxClicked.Visibility = Visibility.Visible;
                findingMatch = false;
            }
        }

        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (matchesFound == 8)
            {
                SetUpGame();
            }
        }
    }
}
