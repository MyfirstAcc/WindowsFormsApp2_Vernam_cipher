using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    /// <summary>
    /// класс шифрования One-time pad
    /// </summary>
    public class VernamCipher
    {
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
        /// <summary>
        /// алгоритм взят с https://en.wikipedia.org/wiki/One-time_pad 
        /// </summary>
        /// <param name="message"> сообщение</param>
        /// <param name="key">ключ</param>
        /// <returns>криптограмма</returns>
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
        /// <summary>
        /// алгоритм взят с https://en.wikipedia.org/wiki/One-time_pad 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="key"></param>
        /// <returns>зашифрованное сообщение</returns>
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
