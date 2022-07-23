using MancalaAssessment.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            Assert.AreEqual("Click \"New Game\" to get started!", new MainWindowViewModel().BannerText);
        }

    }
}