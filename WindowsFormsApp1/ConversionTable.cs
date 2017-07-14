using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    public partial class ConversionTable : Form
    {
        private string path = DataOperations.path + "PG1000.xml";
        private DataOperations obj = new DataOperations();
        private XDocument doc;
        public ConversionTable()
        {
            InitializeComponent();             
        }
        
        private void Form2_Load(object sender, EventArgs e)
        {
            doc = XDocument.Load(path);

            var xDocument = XDocument.Load(path);
            string txtxml = xDocument.ToString();

            StringReader reader = new StringReader(txtxml);
            XDocument doc1 = XDocument.Load(reader);
            var res = doc1.Descendants("Row").ToList();

            dataGridView1.DataSource = res.ToDataTable();
            
            dataGridView1.Columns[0].Visible = false;
            this.dataGridView1.AllowUserToAddRows = false;
            if (obj.getPrivilege(DataOperations.EmailId) == "Editor")
            {
                dataGridView1.ReadOnly = true;
            }
            editUsersToolStripMenuItem.Visible = (obj.getPrivilege(DataOperations.EmailId) == "Admin") ? true : false;
            obj.log("Email Id: " + DataOperations.EmailId + " Visited Conversion Table");
        }        

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This sample is developed by Mayank Goel, Intern, ABB Pvt. Ltd. Core. Please read the Readme.htm for more details");
        }

        private void confirmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.EndEdit();
                dataGridView1.BackgroundColor = Color.White;
                
                MessageBox.Show("Saved successfully");
                Form2_Load(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void F2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
              
        private void editUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserForm f2 = new UserForm();
            f2.FormClosed += F2_FormClosed2;
            f2.ShowDialog();
        }

        private void F2_FormClosed2(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                XElement upd = doc.Descendants("Row").ToList().Where(x =>
                    x.Element("Id").Value == dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString())
                    .Single();
                upd.Element(dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].HeaderText).Value = dataGridView1.CurrentCell.Value.ToString();
                dataGridView1.CurrentCell.Style.BackColor = Color.Tomato;
                obj.log("Email Id: " + DataOperations.EmailId + 
                    " modified Conversion Table row no "+ dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString() + 
                    " " + dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].HeaderText + 
                    " entry to " + dataGridView1.CurrentCell.Value.ToString()
                    );
                doc.Save(path);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void addRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
           int PrevRowId = Convert.ToInt32(dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[0].Value.ToString());
            AddConversionData f2 = new AddConversionData(PrevRowId+1);
            f2.FormClosed += F2_FormClosed1;
            f2.ShowDialog();
        }

        private void F2_FormClosed1(object sender, FormClosedEventArgs e)
        {
            Form2_Load(sender, e);
            this.Show();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            obj.log("Email Id: " + DataOperations.EmailId + " started Test");
            ChooseLanguage f2 = new ChooseLanguage();
            f2.FormClosed += F2_FormClosed;
            f2.ShowDialog();
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

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Control && e.KeyCode == Keys.C)
            {
                CopyCode();
            }            
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

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            obj.log("Email Id: " + DataOperations.EmailId + " started Search");
            Search f2 = new Search();
            f2.FormClosed += F2_FormClosed3;
            f2.ShowDialog();
        }

        private void F2_FormClosed3(object sender, FormClosedEventArgs e)
        {
            Form2_Load(sender, e);
            this.Show();
        }

    }
}
