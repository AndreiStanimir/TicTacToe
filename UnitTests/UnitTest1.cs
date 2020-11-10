using NUnit.Framework;
using TicTacToe;

namespace UnitTests
{
    public class BoardTest
    {
        private GameViewModel game;
        private TileViewModel player, computer, empty;

        [SetUp]
        public void Setup()
        {
            game = new GameViewModel();
            player = new TileViewModel(1);
            player.Owner = Owner.Player1;

            computer = new TileViewModel(1);
            computer.Owner = Owner.Computer;

            empty = new TileViewModel(1);
            empty.Owner = Owner.None;
        }

        [Test]
        public void PlayerWins()
        {
            game.Tiles = new TileViewModel[9]
            {
                player,computer,computer,
                computer,player,empty,
                empty,empty,player
            };
            game.CurrentPlayer = Owner.Player1;
            game.CheckTurn(game.Tiles[0]);
            Assert.AreEqual(game.CheckWin(), true);
            Assert.AreEqual(game.Winner, $"{Owner.Player1} has won!");
        }

        [Test]
        public void PlayerWins1()
        {
            game.Tiles = new TileViewModel[9]
            {
                computer,computer,empty,
                player,player,player,
                empty,empty,empty
            };
            game.CurrentPlayer = Owner.Player1;
            game.CheckTurn(game.Tiles[0]);

            Assert.AreEqual(game.CheckWin(), true);
            Assert.AreEqual(game.Winner, $"{Owner.Player1} has won!");
        }

        [Test]
        public void ComputerWins()
        {
            game.Tiles = new TileViewModel[9]
            {
                computer,player,player,
                computer,player,empty,
                computer,empty,player
            };
            game.CurrentPlayer = Owner.Computer;
            game.CheckTurn(game.Tiles[0]);

            Assert.AreEqual(true, game.CheckWin());
            Assert.AreEqual($"{Owner.Computer} has won!", game.Winner);
        }

        [Test]
        public void Draw()
        {
            game.Tiles = new TileViewModel[9]
            {
                computer,player,player,
                player,computer,computer,
                computer,player,player
            };
            game.CurrentPlayer = Owner.Computer;
            game.CheckTurn(game.Tiles[0]);

            Assert.AreEqual(false, game.CheckWin());
            Assert.AreEqual("Draw!", game.Winner);
        }

        [Test]
        public void MiniMax()
        {
            //var player = new TileViewModel(1);
            //player.Owner = Owner.Player1;
            game.Tiles = new TileViewModel[9]
            {
                computer,empty,empty,
                player,player,empty,
                empty,empty,empty
            };
            game.CurrentPlayer = Owner.Player1;
            game.CheckTurn(game.Tiles[4]);

            Assert.AreEqual(false, game.CheckWin());
            Assert.AreEqual(5, game.ComputeBestMove());
            //Assert.AreEqual(Owner.Computer, game.Tiles[5].Owner);
            //Assert.AreEqual("Draw!", game.Winner);
        }
    }
}