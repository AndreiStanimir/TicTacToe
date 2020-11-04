using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System;

namespace TicTacToe
{
    public class GameViewModel : ViewModelBase
    {
        public Owner CurrentPlayer { get => Get<Owner>(); set => Set(value); }
        public TileViewModel[] Tiles { get; }
        public Command RestartCmd { get; }
        public string Winner { get => Get<string>(); set => Set(value); }
        public bool GameOver { get => Get<bool>(); set => Set(value); }

        public bool ComputerPlaysSmart { get; set; }
        public GameViewModel()
        {
            GameOver = false;
            CurrentPlayer = Owner.Player1;
            Tiles = Enumerable
                .Range(1, 9)
                .Select(i => new TileViewModel(i))
                .ToArray();

            foreach (var t in Tiles)
                t.Clicked += (s, e) => CheckTurn(s as TileViewModel);

            RestartCmd = new Command(() =>
            {
                GameOver = false;
                foreach (var t in Tiles)
                    t.Owner = Owner.None;
            }
            , () => true);
        }

        // -------------- game logic --------------------

        /*
        field indices:
         1 2 3
         4 5 6
         7 8 9
        */

        private readonly (int start, int offset)[] _winningCombinations = new (int, int)[]
        {
            (1, 1), (4, 1), (7, 1), // horizontal
            (1, 3), (2, 3), (3, 3), // vertical
            (1, 4), (3, 2)          // diagonal
        };

        public void CheckTurn(TileViewModel t)
        {
            t.Owner = CurrentPlayer;
            if (_winningCombinations.Any(w => HasWon(w) != Owner.None))
            {
                Winner = $"{CurrentPlayer} has won!";
                GameOver = true;
                return;
            }
            CurrentPlayer = CurrentPlayer == Owner.Player1 ? Owner.Player2 : Owner.Player1;

            if (CurrentPlayer == Owner.Player2)
            {
                if (!ComputerPlaysSmart) //Computer random move
                {
                    TileViewModel move = GetFreeTiles().Take(1).First();
                    move.ClickCmd.Execute(move);
                }
                else
                {

                }
            }
        }

        private Owner HasWon((int start, int offset) combination)
        {
            var owners = new Owner[]
            {
                ownerOfTile(0),
                ownerOfTile(combination.offset),
                ownerOfTile(combination.offset * 2),
            };
            var sum = owners.Sum(o => (int)o);
            if (owners.All(o => o != Owner.None))
            {
                if (sum == (int)Owner.Player1 * 3)
                    return Owner.Player1;
                else if (sum == (int)Owner.Player2 * 3)
                    return Owner.Player2;
                else return Owner.None;
            }
            return Owner.None;

            Owner ownerOfTile(int offset) => Tiles[combination.start + offset - 1].Owner; // adjust to zero-indexed array
        }

        private IEnumerable<TileViewModel> GetFreeTiles()
        {
            return Tiles.Where(t => t.Owner == Owner.None);
        }



        private int Minimax(TileViewModel[] tiles, Owner owner)
        {
            // Heavy inspiration https://towardsdatascience.com/tic-tac-toe-creating-unbeatable-ai-with-minimax-algorithm-8af9e52c1e7d
            if (_winningCombinations.Any(w => HasWon(w) != Owner.None))
            {
                return owner == Owner.Player2 ? 1 : -1;
            }
            IEnumerable<TileViewModel> freeTiles = tiles.Where(t => t.Owner == Owner.None);
            if (freeTiles.Count() == 0)
            {
                return 0;
            }
            var score = -2;
            var mode = -1;
            for (int i = 0; i < freeTiles.Count(); i++)
            {
                TileViewModel[] newTiles = new TileViewModel[tiles.Length];



                Array.Copy(tiles, newTiles, tiles.Length);
                var oppositePlayer = owner == Owner.Player1 ? Owner.Player2 : Owner.Player1;
                int scoreForMove = -Minimax(newTiles, oppositePlayer);
                if (scoreForMove > score)
                {
                    scoreForMove = score;
                    
                }
            }
        }
    }
}