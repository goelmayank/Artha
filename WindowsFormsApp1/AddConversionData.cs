using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    public partial class AddConversionData : Form
    {
        private DataOperations obj = new DataOperations();
        private XDocument doc = XDocument.Load(DataOperations.path + "PG1000.xml");
        string path = DataOperations.path + "PG1000.xml";
        int oldRowCont = 0;
        public AddConversionData(int rowCnt)
        {
            InitializeComponent();
            oldRowCont = rowCnt;
        }

        private void confirmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.EndEdit();
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    if (item.Index == dataGridView1.Rows.Count - 1) break;
                    doc.Descendants("Root").FirstOrDefault().Add(new XElement("Row", 
                        new XElement("Id", (++oldRowCont).ToString()),
                        new XElement("English", (item.Cells[0].Value == null)? string.Empty: item.Cells[0].Value.ToString()),
                       new XElement("Arabic", (item.Cells[1].Value == null) ? string.Empty : item.Cells[1].Value.ToString()),
                       new XElement("German", (item.Cells[2].Value == null) ? string.Empty : item.Cells[2].Value.ToString()),
                       new XElement("Italian", (item.Cells[3].Value == null) ? string.Empty : item.Cells[3].Value.ToString()),
                       new XElement("Japanese", (item.Cells[4].Value == null) ? string.Empty : item.Cells[4].Value.ToString()),
                       new XElement("Korean", (item.Cells[5].Value == null) ? string.Empty : item.Cells[5].Value.ToString()),
                       new XElement("Norwegian", (item.Cells[6].Value == null) ? string.Empty : item.Cells[6].Value.ToString()),
                       new XElement("Spanish", (item.Cells[7].Value == null) ? string.Empty : item.Cells[7].Value.ToString()),
                       new XElement("Swedish", (item.Cells[8].Value == null) ? string.Empty : item.Cells[8].Value.ToString())
                        ));       
                    
                    obj.log(obj.getPrivilege(DataOperations.EmailId) + " " + DataOperations.EmailId + " added to Conversion Table row no : " +(oldRowCont).ToString());

                }

                doc.Save(path);

                MessageBox.Show("Saved successfully");                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This sample is developed by Mayank Goel, Intern, ABB Pvt. Ltd. Core. Please read the Readme.htm for more details");
        }
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyCode();
        }
             

        private void pasteCtrlVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PasteCode();
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
                string[] lines = s.Split( '\n');
                int linesToAdd = lines.Length - (dataGridView1.Rows.Count - dataGridView1.CurrentCell.RowIndex );
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
                                if (linesToAdd-- > 0)
                                    dataGridView1.Rows.Add();
                                oCell = dataGridView1[iCol + i, iRow];
                                if (oCell.Value==null)
                                {
                                    oCell.Value = Convert.ChangeType(sCells[i], oCell.ValueType);
                                }
                                else if(oCell.Value.ToString() != sCells[i])
                                {
                                    oCell.Value = Convert.ChangeType(sCells[i], oCell.ValueType);
                                }
                                else
                                    iFail++;//only traps a fail if the data has changed and you are pasting into a read only cell
                                
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
