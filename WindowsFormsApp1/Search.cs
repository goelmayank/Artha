using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;

namespace WindowsFormsApp1
{
    public partial class Search : Form
    {
        private string path = DataOperations.path + "PG1000.xml";
        private DataOperations obj = new DataOperations();
        private XDocument doc;

        public Search()
        {
            InitializeComponent();
        }

        private void searchWord_TextChanged(object sender, EventArgs e)
        {
            string language = searchLanguage.Text;
            string word = searchWord.Text;
            try
            {
                string txtxml = "";
                foreach (var element in XDocument.Load(path).Descendants("Row"))
                {
                    if (element.Elements(language).SingleOrDefault(p => (string)p.Value == word) !=null)
                    {
                        txtxml+= element.ToString();
                    }
                }

                StringReader reader = new StringReader(txtxml);
                XDocument doc1 = XDocument.Load(reader);
                var res = doc1.Descendants("Row").ToList();

                dataGridView1.DataSource = res.ToDataTable();

                dataGridView1.Columns[0].Visible = false;
                this.dataGridView1.AllowUserToAddRows = false;

                obj.log("Email Id: " + DataOperations.EmailId + " Visited Search Table");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
