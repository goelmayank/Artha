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

        private void Search_Load(object sender, EventArgs e)
        {
            doc = XDocument.Load(path);
        }

        private void searchWord_TextChanged(object sender, EventArgs e)
        {
            string language = searchLanguage.Text;
            string word = searchWord.Text;
            try
            {
                string txtxml = "<Root>";
                foreach (var element in XDocument.Load(path).Descendants("Row"))
                {
                    if (element.Elements(language).SingleOrDefault(p => (string)p.Value == word) !=null)
                    {
                        txtxml+= element.ToString();
                    }
                }
                txtxml += "</Root>";
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

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyCode();
        }

        private void CopyCode()
        {
            if (obj.getPrivilege(DataOperations.EmailId) == "Editor")
            {
                return;
            }
            DataObject d = dataGridView1.GetClipboardContent();
            Clipboard.SetDataObject(d);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteCode();
        }

        private void DeleteCode()
        {
            try
            {
                if (obj.getPrivilege(DataOperations.EmailId) == "Editor")
                {
                    return;
                }
                if (dataGridView1.SelectedRows.Count == 0)
                    MessageBox.Show("No row selected. Click on the left margin to select a row");
                else
                {
                    foreach (DataGridViewRow dr in dataGridView1.SelectedRows)
                    {
                        doc.Descendants("Row").Where(x => x.Element("Id").Value == dataGridView1.Rows[dr.Index].Cells[0].Value.ToString()).Remove();
                        obj.log("Email Id: " + DataOperations.EmailId +
                            " deleted Conversion Table row no " + dataGridView1.Rows[dr.Index].Cells[0].Value.ToString()
                            );
                        dataGridView1.Rows.RemoveAt(dr.Index);
                        doc.Save(path);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return;
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                CopyCode();
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This sample is developed by Mayank Goel, Intern, ABB Pvt. Ltd. Core. Please read the Readme.htm for more details");
        }

        private void confirmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
