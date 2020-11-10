using System;
using System.Collections.Generic;
using System.Linq;
using TicTacToe.Common;

namespace TicTacToe
{
    public class GameViewModel : ViewModelBase
    {
        public Owner CurrentPlayer { get => Get<Owner>(); set => Set(value); }
        public TileViewModel[] Tiles { get; set; }
        public Command RestartCmd { get; }
        public string Winner { get => Get<string>(); set => Set(value); }
        public bool GameOver { get => Get<bool>(); set => Set(value); }
        public bool ComputerPlaysSmart { get => Get<bool>(); set => Set(value); }

        public Scores Scores { get => Get<Scores>(); set => Set(value); }

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

            Scores = FileWrite.ReadScores();

            ComputerPlaysSmart = true;
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
            if (CheckWin())
            {
                Winner = $"{CurrentPlayer} has won!";

                GameOver = true;
                FileWrite.WriteWinner(CurrentPlayer);
                //Scores.AddScore(CurrentPlayer);
                if (CurrentPlayer == Owner.Player1)
                    Scores.Player += 1;
                else
                    Scores.Computer += 1;
                FileWrite.WriteTotalScore(Scores);
                //save to file
                return;
            }
            if (GetFreeTiles().Count() == 0)
            {
                Winner = "Draw!";
                GameOver = true;
                FileWrite.WriteWinner(CurrentPlayer);
                return;
            }

            CurrentPlayer = CurrentPlayer == Owner.Player1 ? Owner.Computer : Owner.Player1;

            if (CurrentPlayer == Owner.Computer)
            {
                if (!ComputerPlaysSmart) //Computer random move
                {
                    var moves = GetFreeTiles();
                    var random = new Random();
                    var index = random.Next(moves.Count());

                    var move = moves.ElementAt(index);
                    move.ClickCmd.Execute(move);
                }
                else
                {
                    int move_index = ComputeBestMove();
                    Tiles[move_index].ClickCmd.Execute(Tiles[move_index]);
                }
            }
        }

        public bool CheckWin()
        {
            return CheckWin(Tiles);
        }

        public bool CheckWin(TileViewModel[] tiles)
        {
            return _winningCombinations.Any(w => HasWon(w, tiles) != Owner.None);
        }

        public Owner HasWon((int start, int offset) combination, TileViewModel[] tiles)
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
                else if (sum == (int)Owner.Computer * 3)
                    return Owner.Computer;
                else return Owner.None;
            }
            return Owner.None;

            Owner ownerOfTile(int offset) => tiles[combination.start + offset - 1].Owner; // adjust to zero-indexed array
        }

        private IEnumerable<TileViewModel> GetFreeTiles()
        {
            return Tiles.Where(t => t.Owner == Owner.None);
        }

        public int ComputeBestMove()
        {
            var owner = Owner.Computer;
            var score = -1000;
            var move = -1;
            for (int i = 0; i < Tiles.Count(); i++)
            {
                var tile = Tiles[i];
                if (tile.IsEmpty())
                {
                    TileViewModel[] newTiles = new TileViewModel[Tiles.Length];
                    for (int j = 0; j < Tiles.Length; j++)
                    {
                        newTiles[j] = new TileViewModel(Tiles[j]);
                    }
                    newTiles[i].Owner = owner;

                    var oppositePlayer = owner == Owner.Player1 ? Owner.Computer : Owner.Player1;
                    int scoreForMove = -Minimax(newTiles, oppositePlayer, false);

                    if (scoreForMove > score)
                    {
                        score = scoreForMove;
                        move = i;
                    }
                }
            }
            return move;
        }

        private int Minimax(TileViewModel[] tiles, Owner owner, bool isMachineMove)
        {
            // Heavy inspiration https://towardsdatascience.com/tic-tac-toe-creating-unbeatable-ai-with-minimax-algorithm-8af9e52c1e7d
            if (CheckWin(tiles))
            {
                var me = isMachineMove ? Owner.Computer : Owner.Player1;
                return me != owner ? 1 : -1;
            }
            //IEnumerable<TileViewModel> freeTiles = tiles.Where(t => t.Owner == Owner.None);
            //if (freeTiles.Count() == 0)
            //{
            //    return 0;
            //}
            var score = -1000;
            for (int i = 0; i < tiles.Count(); i++)
            {
                var tile = tiles[i];
                if (tile.IsEmpty())
                {
                    TileViewModel[] newTiles = new TileViewModel[tiles.Length];
                    for (int j = 0; j < tiles.Length; j++)
                    {
                        newTiles[j] = new TileViewModel(tiles[j]);
                    }
                    newTiles[i].Owner = owner;

                    var oppositePlayer = owner == Owner.Player1 ? Owner.Computer : Owner.Player1;
                    int scoreForMove = -Minimax(newTiles, oppositePlayer, !isMachineMove);

                    if (scoreForMove == 1)
                        return scoreForMove;
                    if (scoreForMove > score)
                    {
                        score = scoreForMove;
                    }
                }
            }
            if (score == -1000)
                return 0;
            return score;
        }
    }
}