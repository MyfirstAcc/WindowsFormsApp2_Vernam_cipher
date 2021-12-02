using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Alphabet
    {
        public Alphabet()
        {
        }
        public Dictionary<int, char> Symbols
        {
            get
            {
                return GenerateAlphabet();
            }
            private set
            {

            }
        }

        /// <summary>
        /// /виртуальный метод для создания словаря(Алфавита)
        /// </summary>
        /// <returns>словарь символов по номеру(как в методичке)</returns>
        protected virtual Dictionary<int, char> GenerateAlphabet()
        {
            Dictionary<int, char> dictAlph = new Dictionary<int, char>();
            var CharactersLatinUpper = Enumerable.Range('\x0041', '\x005A' - '\x0041' + 1)
                                 .Select(x => (char)x).ToList();

            foreach (char symbol in CharactersLatinUpper)
            {
                dictAlph.Add((symbol - '\x0041'), symbol);
            }
            var CharactersLatinDwn = Enumerable.Range('\x0061', '\x007A' - '\x0061' + 1)
                                 .Select(x => (char)x).ToList();
            foreach (char symbol in CharactersLatinDwn)
            {
                dictAlph.Add((symbol - '\x0061') + CharactersLatinUpper.Count, symbol);
            }


            /*
             * доп символы
             */

            dictAlph.Add(dictAlph.Count, '\x000A');
            dictAlph.Add(dictAlph.Count , '\x000D');
            dictAlph.Add(dictAlph.Count, '\x0020');
            dictAlph.Add(dictAlph.Count,'\x0028');
            dictAlph.Add(dictAlph.Count, '\x0029');
            dictAlph.Add(dictAlph.Count, '\x002C');
            dictAlph.Add(dictAlph.Count, '\x002D');
            dictAlph.Add(dictAlph.Count, '\x002E');
            dictAlph.Add(dictAlph.Count, '\x003A');
            dictAlph.Add(dictAlph.Count, '\x003B');

            var CharactersDigit = Enumerable.Range('\x0030', '\x0039' - '\x0030' + 1)
                                 .Select(x => (char)x).ToList();

            foreach (var symbol in CharactersDigit)
            {
                dictAlph.Add(dictAlph.Count, symbol);
            }
            return dictAlph;
        }

        public override string ToString()
        {
            var str = new StringBuilder();
            str.Append("{");
            foreach (var pair in Symbols)
            {
                str.Append(string.Format($" {pair.Key}:{pair.Value} "));
            }
            str.Append("}");
            return str.ToString();
        }

    }
    /// <summary>
    /// класс сын(Расширенный Алфавит)
    /// </summary>
    public class AdvancedAlphabet : Alphabet
    {
        protected override Dictionary<int, char> GenerateAlphabet()
        {
            var dict = new Dictionary<int, char>();
            var CharactersLatAndOther = Enumerable.Range('\x0020', '\x007A' - '\x0020' + 1)
                                  .Select(x => (char)x).ToList();          
            
            foreach (char symbol in CharactersLatAndOther)
            {
                dict.Add(symbol - '\x0020', symbol);
            }

            var CharactersCirill = Enumerable.Range('\x0410', '\x044F' - '\x0410' + 1)
                                  .Select(x => (char)x).ToList();
            //для буквы ё
            CharactersCirill.Insert(CharactersCirill.IndexOf('\x0415') + 1, '\x0401');
            CharactersCirill.Insert(CharactersCirill.IndexOf('\x0435') + 1, '\x0451');
            
            foreach (char symbol in CharactersCirill)
            {
                dict.Add(dict.Count, symbol);
            }



            return dict;
        }

    }


    public class AlphabetTest : Alphabet
    {
        protected override Dictionary<int, char> GenerateAlphabet()
        {
            var dictAlph = new Dictionary<int, char>();
            var CharactersLatinDwn = Enumerable.Range('\x0061', '\x007A' - '\x0061' + 1)
                                 .Select(x => (char)x).ToList();

            var i = 0;
            foreach (char symbol in CharactersLatinDwn)
            {
                dictAlph.Add(i++, symbol);
            }
            return dictAlph;

        }

    }

}
