﻿using System;
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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SetUpGame();
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
    }
}
