using Microsoft.VisualStudio.TestTools.UnitTesting;
using VanDerWaerdenGame.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VanDerWaerdenGame.Model.Tests
{
    [TestClass()]
    public class GameManagerTests
    {
        [TestMethod()]
        public void GameManager_DetectProgression()
        {
            Assert.IsFalse(GameManager.DetectProgression(new int[] { 1, 1, 2, 1 }, 3, 0, 1));
            Assert.IsTrue(GameManager.DetectProgression(new int[] { 0, 1, 1, 1 }, 3, 1, 1));

            var result = GameManager.DetectProgression(new int[]{ 1, 1, 2, 2, 1, 1, 1 }, 3);
            Assert.IsTrue(result);

            result = GameManager.DetectProgression(new int[] { 1, 1, 2, 2, 1, 1 }, 3);
            Assert.IsFalse(result);

        }
    }
}