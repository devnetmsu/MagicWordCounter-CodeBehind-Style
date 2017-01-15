using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MagicWordCounter_CodeBehind_Style;

namespace MagicWordCounter.Tests
{
    [TestClass]
    public class CountingTests
    {

        MainWindow testWindow;

        [TestInitialize]
        public void TestInit()
        {
            testWindow = new MainWindow();
            testWindow.TextToCount = Properties.Resources.TestString;
        }
        
        [TestMethod]
        public void BasicCounting()
        {
            testWindow.ExcludeArticles = false;
            testWindow.CountQuotesAsOneWord = false;
            testWindow.btnCount_Click(null, null);
            Assert.AreEqual("34 word(s)", testWindow.LabelText);
        }

        [TestMethod]
        public void ExcludeArticles()
        {
            testWindow.ExcludeArticles = true;
            testWindow.CountQuotesAsOneWord = false;
            testWindow.btnCount_Click(null, null);
            Assert.AreEqual("32 word(s)", testWindow.LabelText);
        }

        [TestMethod]
        public void QuotesAreOneWord()
        {
            testWindow.ExcludeArticles = false;
            testWindow.CountQuotesAsOneWord = true;
            testWindow.btnCount_Click(null, null);
            Assert.AreEqual("29 word(s)", testWindow.LabelText);
        }

        [TestMethod]
        public void ExcludeArticlesAndCountQuotesAsOneWord()
        {
            testWindow.ExcludeArticles = true;
            testWindow.CountQuotesAsOneWord = true;
            testWindow.btnCount_Click(null, null);
            Assert.AreEqual("28 word(s)", testWindow.LabelText);
        }
    }
}
