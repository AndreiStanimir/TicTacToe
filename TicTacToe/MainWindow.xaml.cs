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

            popUpMessage.Show();
        }
    }
}