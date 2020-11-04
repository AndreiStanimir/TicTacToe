using System.Windows;

namespace TicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new GameViewModel();
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