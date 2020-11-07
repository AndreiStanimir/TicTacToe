using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
namespace TicTacToe.Common
{
    public class Scores :ViewModelBase
    {
        
        public int Player { get => Get<int>(); set => Set(value); }
        public int Computer { get => Get<int>(); set => Set(value); }

        //public Scores()
        //{
        //    Player = 0;
        //    Computer = 0;
        //}
        //public Scores(int player=0, int computer=0)
        //{
        //    this.Player = player;
        //    this.Computer = computer;
        //}
        public void AddScore(Owner owner)
        {
            _ = (owner == Owner.Player1) ? Player++ : Computer++;
        }
    }
}
