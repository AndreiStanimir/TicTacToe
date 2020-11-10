using System.Windows;
using System.Windows.Controls;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PopUpMessage popUpMessage;
        private TextBox popupTextBox;

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

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            string Text = popUpMessage.Textbox_Start.Text.Trim().ToLower();
            if (Text == "go go go")
            {
                ((GameViewModel)DataContext).ComputerPlaysSmart = true;
            }
            else
            {
                ((GameViewModel)DataContext).ComputerPlaysSmart = false;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}