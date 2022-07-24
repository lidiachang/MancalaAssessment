using MancalaAssessment.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace MancalaAssessmentTests.ViewModelTests
{
    [TestClass]
    public class MainWindowViewModelTests
    {
        [TestMethod]
        public void BannerText_DefaultValue()
        {
            Assert.AreEqual("Click \"New Game\" to get started!", new MainWindowViewModel().BannerText);
        }
    [TestMethod]
        public void testPlay()
        {
            BoardViewModel game = new BoardViewModel();
            int sum = 48;
            game.Play(1, 1);
            Assert.AreEqual(game.StonesPlayer1[1], 0);
            Assert.AreEqual(game.StonesPlayer1[0], 5);
            Assert.AreEqual(game.StorePlayer1, 1);
            Assert.AreEqual(game.StonesPlayer2[5], 5);
            Assert.AreEqual(game.StonesPlayer2[4], 5);
            Assert.AreEqual(game.StonesPlayer1.Sum() + game.StonesPlayer2.Sum() + game.StorePlayer1 + game.StorePlayer2, sum);

            game.Play(0, 1);
            Assert.AreEqual(game.StonesPlayer1[0], 0);
            Assert.AreEqual(game.StorePlayer1, 2);
            Assert.AreEqual(game.StonesPlayer2[5], 6);
            Assert.AreEqual(game.StonesPlayer2[4], 6);
            Assert.AreEqual(game.StonesPlayer2[3], 5);
            Assert.AreEqual(game.StonesPlayer2[2], 5);
            Assert.AreEqual(game.StonesPlayer1.Sum() + game.StonesPlayer2.Sum() + game.StorePlayer1 + game.StorePlayer2, sum);

            game.Play(5, 2);
            Assert.AreEqual(game.StonesPlayer2[5], 0);
            Assert.AreEqual(game.StonesPlayer2[4], 7);
            Assert.AreEqual(game.StonesPlayer2[3], 6);
            Assert.AreEqual(game.StonesPlayer2[2], 6);
            Assert.AreEqual(game.StonesPlayer2[1], 5);
            Assert.AreEqual(game.StonesPlayer2[0], 5);
            Assert.AreEqual(game.StorePlayer2, 1);
            Assert.AreEqual(game.StonesPlayer1.Sum() + game.StonesPlayer2.Sum() + game.StorePlayer1 + game.StorePlayer2, sum);

        }

    }
}