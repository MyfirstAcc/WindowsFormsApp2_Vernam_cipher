using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using WindowsFormsApp1;

namespace UnitTestVernam
{
    /// <summary>
    /// Для проверки правильности заполненности таблиц алфавита 
    /// </summary>
    [TestClass]
    public class UnitTestAphabet
    {
        
        [TestMethod]
        public void TestMethodAlphabet()
        {
            Alphabet alphabet = new Alphabet();
            var aDict=alphabet.Symbols;        
            int i = 0;
            foreach (var item in aDict)
            {
               Assert.AreEqual(i++, item.Key);
            }
        }

        [TestMethod]
        public void TestMethodAlphabetAdv()
        {
            Alphabet alphabet = new AdvancedAlphabet();
            var aDict = alphabet.Symbols;
            int i = 0;
            foreach (var item in aDict)
            {
                Assert.AreEqual(i++, item.Key);
            }

        }

    }
}
