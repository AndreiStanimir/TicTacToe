using System;

namespace TicTacToe
{
    public class TileViewModel : ViewModelBase
    {
        public event EventHandler Clicked;

        public Owner Owner { get => Get<Owner>(); set { Set(value); ClickCmd?.UpdateCanExecute(); } }

        public Command ClickCmd { get; }

        public TileViewModel(int fieldId)
        {
            Owner = Owner.None;
            ClickCmd = new Command(() => Clicked?.Invoke(this, null), () => Owner == Owner.None);
        }
    }
}
