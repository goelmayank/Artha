using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    public partial class ConversionTable : Form
    {
        private DataOperations obj = new DataOperations();
        private XDocument doc = XDocument.Load(DataOperations.path + "PG1000.xml");
        private string path = DataOperations.path + "PG1000.xml";
        public ConversionTable()
        {
            InitializeComponent();
        }
        //private DataSet ds = new DataSet();
        private void Form2_Load(object sender, EventArgs e)
        {
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
            //addRowToolStripMenuItem.Visible = !(editUsersToolStripMenuItem.Visible = (obj.getPrivilege(DataOperations.EmailId) == "Admin") ? true : false);
            editUsersToolStripMenuItem.Visible = (obj.getPrivilege(DataOperations.EmailId) == "Admin") ? true : false;
            obj.log("Email Id: " + DataOperations.EmailId + " Visited Conveersion Table");
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
                doc.Save(path);
                MessageBox.Show("Saved successfully");
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
                /* upd.Element("English").Value = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                 upd.Element("Arabic").Value = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                 upd.Element("German").Value = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                 upd.Element("Italian").Value = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                 upd.Element("Japanese").Value = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                 upd.Element("Korean").Value = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                 upd.Element("Norwegian").Value = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                 upd.Element("Spanish").Value = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                 upd.Element("Swedish").Value = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                 */
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

        private void PasteClipboard()
        {
            try
            {
                string s = Clipboard.GetText();
                string[] lines = s.Split('\n');
                int iFail = 0, iRow = dataGridView1.CurrentCell.RowIndex;
                int iCol = dataGridView1.CurrentCell.ColumnIndex;
                DataGridViewCell oCell;
                foreach (string line in lines)
                {
                    if (iRow < dataGridView1.RowCount && line.Length > 0)
                    {
                        string[] sCells = line.Split('\t');
                        for (int i = 0; i < sCells.GetLength(0); ++i)
                        {
                            if (iCol + i < this.dataGridView1.ColumnCount)
                            {
                                oCell = dataGridView1[iCol + i, iRow];
                                if (!oCell.ReadOnly)
                                {
                                    if (oCell.Value.ToString() != sCells[i])
                                    {
                                        oCell.Value = Convert.ChangeType(sCells[i], oCell.ValueType);
                                        oCell.Style.BackColor = Color.Tomato;
                                    }
                                    else
                                        iFail++;//only traps a fail if the data has changed and you are pasting into a read only cell
                                }
                            }
                            else
                            { break; }
                        }
                        iRow++;
                    }
                    else
                    { break; }
                    if (iFail > 0)
                        MessageBox.Show(string.Format("{0} updates failed due to read only column setting", iFail));
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("The data you pasted is in the wrong format for the cell");
                return;
            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyCode();
        }

        private void CopyCode()
        {
            DataObject d = dataGridView1.GetClipboardContent();
            Clipboard.SetDataObject(d);
        }

        private void PasteCode()
        {
            try
            {
                string s = Clipboard.GetText();
                string[] lines = s.Split('\n');
                int linesToAdd = lines.Length - (dataGridView1.Rows.Count - dataGridView1.CurrentCell.RowIndex);
                int iFail = 0, iRow = dataGridView1.CurrentCell.RowIndex;
                int iCol = dataGridView1.CurrentCell.ColumnIndex;
                DataGridViewCell oCell;
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    if (iRow < dataGridView1.RowCount && line.Length > 0)
                    {
                        string[] sCells = line.Split('\t');
                        for (int i = 0; i < sCells.GetLength(0); ++i)
                        {
                            if (iCol + i < this.dataGridView1.ColumnCount)
                            {
                                oCell = dataGridView1[iCol + i, iRow];
                                if (oCell.Value == null)
                                {
                                    oCell.Value = Convert.ChangeType(sCells[i], oCell.ValueType);
                                }
                                else if (oCell.Value.ToString() != sCells[i])
                                {
                                    oCell.Value = Convert.ChangeType(sCells[i], oCell.ValueType);
                                }
                                else
                                    iFail++;//only traps a fail if the data has changed and you are pasting into a read only cell
                                if (linesToAdd-- > 0)
                                    dataGridView1.Rows.Add();
                            }
                            else
                            { break; }
                        }
                        iRow++;
                    }
                    else
                    { break; }
                    if (iFail > 0)
                        MessageBox.Show(string.Format("{0} updates failed due to read only column setting", iFail));
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("The data you pasted is in the wrong format for the cell");
                return;
            }
        }

        private void pasteCtrlVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteCode();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Control && e.KeyCode == Keys.Delete) || (e.Shift && e.KeyCode == Keys.Delete))
            {
                CopyCode();
            }
            if ((e.Control && e.KeyCode == Keys.Insert) || (e.Shift && e.KeyCode == Keys.Insert))
            {
                PasteCode();
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
                foreach (DataGridViewRow dr in dataGridView1.SelectedRows)
                {
                    dataGridView1.Rows.RemoveAt(dr.Index);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }
    }
}
