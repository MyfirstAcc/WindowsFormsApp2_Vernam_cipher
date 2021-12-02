using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class VernamCipher
    {
        private static Regex RgxKey = new Regex(@"^(\d+\s?)+$");
        private readonly Alphabet alp;
        private Dictionary<char, int> alpDictionary;
        public VernamCipher(Alphabet alp)
        {
            this.alp = alp;
            alpDictionary = new Dictionary<char, int>();
            int indx = 0;
            foreach (var item in alp.Symbols.Values)
            {
                alpDictionary.Add(item, indx++);
            }

        }
        public string Encrypt(string message, string key)
        {

            string C = string.Empty;
            for (var i = 0; i < message.Length; i++)
            {
                var t = alpDictionary.Count + alpDictionary[message[i]];
                t += alpDictionary[key[i % key.Length]];
                C += alp.Symbols[t % alpDictionary.Count];

            }
            return C;
        }

        public string Decrypt(string message, string key)
        {
            string M = string.Empty;
            for (var i = 0; i < message.Length; i++)
            {
                var t = alpDictionary.Count + alpDictionary[message[i]];
                t += -1 * alpDictionary[key[i % key.Length]];
                M += alp.Symbols[t % alpDictionary.Count];
              
            }
            return M;
        }
    }
}
