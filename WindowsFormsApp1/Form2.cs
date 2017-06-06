using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        private void Form2_Load(object sender, EventArgs e)
        {
        }
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            DataOperations obj = new DataOperations();
            string Japanese = KeyInJapaneseBox.Text;
            string English = ValueInEnglishBox.Text;
            obj.ExecuteQuery("INSERT INTO Conversions(Japanese, English) " +
                                   "Values('" + Japanese + "', '" + English + "')");
            obj.saveToDictionary(Japanese, English);
            
            this.Hide();
            Form1 f1 = new Form1(); //this is the change, code for redirect
            f1.ShowDialog();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1(); //this is the change, code for redirect
            f1.ShowDialog();
        }
    }
}
