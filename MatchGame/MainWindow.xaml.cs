using System;
using System.Collections.Generic;
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

namespace MatchGame
{
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tenthOfSecondsElapsed;
        int matchesFound;

        public MainWindow()
        {
            InitializeComponent();

            //timer goes up by tenth of a second
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;
            SetUpGame();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tenthOfSecondsElapsed++;
            timeTextBlock.Text = (tenthOfSecondsElapsed / 10F).ToString("0.0s");

            //if user found all matches stop time and ask to play again
            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play again?";
            }
        }

        private void SetUpGame()
        {
            //create a list of eight paris of emoji
            List<string> animalEmoji = new List<string>()
            {
                "🦑","🦑",
                "🐡", "🐡",
                "🐘","🐘",
                "🐒","🐒",
                "🐧","🐧",
                "🦆","🦆",
                "🐖","🐖",
                "🦔","🦔",
            };

            //create a new random number generator
            Random random = new Random();

            //Find every TextBlock in the main grid and repeat the following statements for each of them
            foreach(TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                //does not run code inside for textblock timer
                if (textBlock.Name != "timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;

                    //pick a random number between 0 and the number of emojis left in the list and call it index
                    int index = random.Next(animalEmoji.Count);

                    //use the random number called index to get a random emoji from the list
                    string nextEmoji = animalEmoji[index];

                    //update the textbox with the random emoji from the list
                    textBlock.Text = nextEmoji;

                    //remove the random emoji from the list
                    animalEmoji.RemoveAt(index); 
                }
            }
            //starts timer
            timer.Start();
            tenthOfSecondsElapsed = 0;
            matchesFound = 0;
        }

        //declaring variables
        TextBlock lastTextBlockClicked;
        //findingMatch will be a bool to know if user has selected first emoji of pair yet
        bool findingMatch = false;

        //method that is called when user clicks textblock
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //coverting object sender to textblock
            TextBlock textBlock = sender as TextBlock;

            //if first emoji user clicked out of pair, go in here
            if (findingMatch == false)
            {
                //hide the emoji user clicked
                textBlock.Visibility = Visibility.Hidden;

                //variable used to check if user clicked same emoji
                lastTextBlockClicked = textBlock;

                //findingMatch updated to true so next click doesn't go in here
                findingMatch = true;
            }
            //if user clicked the same emoji as first click, go here
            else if (textBlock.Text == lastTextBlockClicked.Text)
            {
                //match found goes up by 1
                matchesFound++;
                //hide emoji clicked
                textBlock.Visibility = Visibility.Hidden;
                //reset findingMatch
                findingMatch = false;
            }
            //if user clicked 2 emojis not the same, go in here
            else
            {
                //make the first emoji clicked visible
                lastTextBlockClicked.Visibility = Visibility.Visible;
                //reset findingMatch
                findingMatch = false;
            }
        }

        //runs if user clicks time text block
        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //resets game if all 8 matches are found 
            if (matchesFound == 8)
            {
                //restart game
                SetUpGame();
            }
            
        }
    }
}
