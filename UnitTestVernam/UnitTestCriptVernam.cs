using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WindowsFormsApp1;

namespace UnitTestVernam
{
    [TestClass]
    public class UnitTestCriptVernam
    {

        Alphabet alphabet;
        VernamCipher cipher;

        [TestInitialize]
        public void init()
        {
            alphabet = new AlphabetTest();
            cipher=new VernamCipher(alphabet);

        }
        [TestMethod]
        public void TestMethodEncript()
        {
            string text = "hello";
            string key = "xmckl";

            string encript = "eqnvz";

            Assert.AreEqual(encript, cipher.Encrypt(text, key));            
           
        }

        [TestMethod]
        public void TestMethodDecript()
        {
            string text = "eqnvz";
            string key = "xmckl";

            string encript = "hello";

            Assert.AreEqual(encript, cipher.Decrypt(text, key));

        }
    }
}
