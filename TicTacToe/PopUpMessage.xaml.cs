using System.Windows;

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
            this.Close();
        }
    }
}