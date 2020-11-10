using System;
using System.Windows;
using System.Windows.Controls;

namespace TicTacToe.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PopUpMessage popUpMessage;
        private TextBox popupTextBox;

        private ChangeName changeName;
        private TextBox changeNameTextBox;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new GameViewModel();
            popUpMessage = new PopUpMessage();
        }

        private void ButtonAutoMode_Click(object sender, RoutedEventArgs e)
        {
            popUpMessage = new PopUpMessage();
            popUpMessage.Button_Start.Click -= ButtonStart_Click;
            popUpMessage.Button_Start.Click += ButtonStart_Click;
            popupTextBox = popUpMessage.Textbox_Start;
            popUpMessage.Show();
        }
        private void ButtonChangeName_Click(object sender, RoutedEventArgs e)
        {
            changeName = new ChangeName();
            changeName.Button_ChangeName.Click -= ButtonInsidePopupChangeName_Click;
            changeName.Button_ChangeName.Click += ButtonInsidePopupChangeName_Click;
            changeNameTextBox = changeName.Textbox_ChangeName;
            changeName.Show();
        }


        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            string text = popUpMessage.Textbox_Start.Text.Trim().ToLower();
            if (text == "go go go")
            {
                ((GameViewModel)DataContext).ComputerPlaysSmart = true;
            }
            else
            {
                ((GameViewModel)DataContext).ComputerPlaysSmart = false;
            }
        }
        private void ButtonInsidePopupChangeName_Click(object sender, RoutedEventArgs e)
        {
            string? text = changeNameTextBox.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(text))
            {
                ((GameViewModel)DataContext).PlayerName = "Player";
            }
            else
            {
                ((GameViewModel)DataContext).PlayerName = Char.ToUpper(text[0]) + text.Substring(1);
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}