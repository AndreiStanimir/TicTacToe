using System.Windows;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PopUpMessage popUpMessage;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new GameViewModel();
            popUpMessage = new PopUpMessage();

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

            popUpMessage = new PopUpMessage();
            popUpMessage.Button_Start.Click += buttonStart_Click;

            popUpMessage.Show();

        }
        private void buttonStart_Click(object sender, RoutedEventArgs e)
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