namespace TicTacToe.Common
{
    public class Scores : ViewModelBase
    {
        public int Player { get => Get<int>(); set => Set(value); }
        public int Computer { get => Get<int>(); set => Set(value); }


    }
}