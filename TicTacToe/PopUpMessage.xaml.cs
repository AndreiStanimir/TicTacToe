using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for PopUpMessage.xaml
    /// </summary>
    public partial class PopUpMessage : Window
    {
        public PopUpMessage()
        {
            InitializeComponent();
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            if (Textbox_Start.Text == "go go go")
            {
                ((GameViewModel)DataContext).ComputerPlaysSmart = true;
            }
            else
            {
                ((GameViewModel)DataContext).ComputerPlaysSmart = false;
            }
        }
    }
}
