using System;

namespace TicTacToe
{
    public class TileViewModel : ViewModelBase
    {
        public event EventHandler Clicked;

        public int Id { get; set; }
        public Owner Owner { get => Get<Owner>(); set { Set(value); ClickCmd?.UpdateCanExecute(); } }

        public Command ClickCmd { get; set; }

        public TileViewModel(int fieldId)
        {
            Owner = Owner.None;
            ClickCmd = new Command(() => Clicked?.Invoke(this, null), () => Owner == Owner.None);
            Id = fieldId;
        }

        public bool IsEmpty()
        {
            return this.Owner == Owner.None;
        }

        public TileViewModel(TileViewModel tileViewModel)
        {
            this.Owner = tileViewModel.Owner;
        }
    }
}