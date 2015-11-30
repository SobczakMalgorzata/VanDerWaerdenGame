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
    public class VanDerWaerdenRulesTests
    {
        [TestMethod()]
        public void VanDerWaerdenRulesTests_EndGame()
        {
            var rules = new VanDerWaerdenGameRules();

            Assert.IsFalse(rules.IsFinalStateOfGame(new int[] { }));
            Assert.IsTrue(rules.IsFinalStateOfGame(new int[]{ 1, 1, 2, 2, 1, 1, 1 }));
            Assert.IsFalse(rules.IsFinalStateOfGame(new int[] { 1, 1, 2, 2, 1, 1 }));
        }
    }
}