using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private class NodeTable
        {
            public NodeTable(string dec, char symb, string number)
            {
                this.DecField = dec;
                this.Symbol = symb;
                this.NumberField = number;
            }
            public string DecField { get; set; }
            public char Symbol { get; set; }
            public string NumberField { get; set; }
        }

        private Alphabet aplph;     //класс для aлфавита
        private VernamCipher cipher; //класс для Алгоритма Вернама
        private static Random rand;
        public Form1()
        {
            rand = new Random();
            InitializeComponent();
        }

        /// <summary>
        /// отрисовка таблицы
        /// </summary>
        private void DrawTable()
        {
            listView2.Clear();
            DataTable dataTable = GetUnicodeTable();

            foreach (DataColumn column in dataTable.Columns)
            {
                listView2.Columns.Add(column.ToString(), 75);
            }

            foreach (DataRow row in dataTable.Rows)
            {
                ListViewItem item = new ListViewItem(row[0].ToString());
                for (var i = 1; i < dataTable.Columns.Count; i++)
                {
                    item.SubItems.Add(row[i].ToString());
                }
                listView2.Items.Add(item);
            }


            listView2.GridLines = true;
        }

        /// <summary>
        /// Таблица алфавита
        /// </summary>
        /// <returns> объект для работы с таблицей</returns>
        private DataTable GetUnicodeTable()
        {
            DataTable table = new DataTable();

            table.Columns.Add("НОМЕР");
            table.Columns.Add("СИМВОЛ");
            table.Columns.Add("DEC");

            var dict = aplph.Symbols;
            foreach (var item in dict)
            {
                table.Rows.Add(item.Key, item.Value, (int)item.Value);
            }

            return table;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            radioButtonDef.Checked = true;
            //textBoxMessage.Text = "Hello, students.";

        }

        private void textBoxMessage_Validated(object sender, EventArgs e)
        {
            string s = PrepareEncrypt();
            DrawTableSymbol(s);
        }


        private string PrepareEncrypt()
        {
            List<char> characters;
            characters = textBoxMessage.Text.ToList();
            var set = new HashSet<char>(characters);//избавляемся от повторений 
            var charactersNoReapeat = set.ToList();
            Dictionary<char, int> alph = new Dictionary<char, int>();
            var K = string.Empty;
            var K1 = string.Empty;
          

            var x = 0;
            foreach (char c in charactersNoReapeat)
            {
                alph.Add(c, x++);
            }

            foreach (var item in characters)
            {
                K += alph[item];             

            }

            cipher = new VernamCipher(aplph);

            foreach (var item in characters)
            {
                var r = rand.Next(1, aplph.Symbols.Count - 1);                
                K1 += aplph.Symbols[r];
            }


            textBoxDigit.Text = K;
            textBoxKey.Text = K1;
            textBoxCript.Text = cipher.Encrypt(textBoxMessage.Text, K1);
            return new string(charactersNoReapeat.ToArray());
        }

        private void DrawTableSymbol(string charactersNoReapeat)
        {
            listView1.Clear();
            errorProvider1.SetError(textBoxMessage, "");
            toolStripStatusLabel2.Text = textBoxMessage.Text.Length.ToString();


            var str = new List<NodeTable>();
            var n = 0;
            foreach (char c in charactersNoReapeat)
            {
                var z = aplph.Symbols.Where(px => px.Value == c).FirstOrDefault();
                str.Add(new NodeTable(z.Key.ToString(), z.Value, Convert.ToString(n++)));

            }
            listView1.Columns.Add("Номер:");
            foreach (var c in str)
            {
                listView1.Columns.Add(string.Format("{0}", c.NumberField), 30);
            }

            var items = new List<string>();
            items.Add("Символ:");
            foreach (var c in str)
            {
                items.Add(new string(new char[] { c.Symbol }));
            }

            listView1.Items.Add(new ListViewItem(items.ToArray()));

            items = new List<string>();
            items.Add("Символ(DEC):");
            foreach (var c in str)
            {

                items.Add(string.Format("{0}", (int)c.Symbol));
            }
            listView1.Items.Add(new ListViewItem(items.ToArray()));
            listView1.GridLines = true;
        }

        private bool ValidString(string str, out string errMsg)
        {

            if (string.IsNullOrEmpty(str))
            {
                errMsg = "Строка не должна быть пустой!";
                return false;

            }
            errMsg = "";
            return true;
        }

        private void textBoxKey_Validating(object sender, CancelEventArgs e)
        {
            //string errMsg;
            //if (!ValidString(textBoxKey.Text, out errMsg))
            //{

            //    textBoxMessage.Select(0, textBoxKey.Text.Length);
            //    e.Cancel = true;
            //    errorProvider1.SetError(textBoxKey, errMsg);
            //}
        }

        private void textBoxKey_Validated(object sender, EventArgs e)
        {

        }

        private void radioButtonDef_CheckedChanged(object sender, EventArgs e)
        {
            aplph = new Alphabet();
            DrawTable();
        }

        private void radioButtonAdv_CheckedChanged(object sender, EventArgs e)
        {
            aplph = new AdvancedAlphabet();
            DrawTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBoxDecript.Text = cipher.Decrypt(textBoxCript.Text, textBoxKey.Text);
        }
    }
}
