using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Conversion Table  Class
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class ConversionTable : Form
    {
        /// <summary>
        /// The path
        /// </summary>
        private string path = DataOperations.path + "PG1000.xml";
        /// <summary>
        /// The object
        /// </summary>
        private DataOperations obj = new DataOperations();
        /// <summary>
        /// The document
        /// </summary>
        private XDocument doc;
        /// <summary>
        /// Initializes a new instance of the <see cref="ConversionTable" /> class.
        /// </summary>
        public ConversionTable()
        {
            InitializeComponent();             
        }

        /// <summary>
        /// Handles the Load event of the Form2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the Click event of the aboutToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This sample is developed by IAPG, ABB. Please read the Readme.docx for more details");
        }

        /// <summary>
        /// Handles the Click event of the confirmToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the FormClosed event of the F2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosedEventArgs" /> instance containing the event data.</param>
        private void F2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the editUsersToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void editUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserForm f2 = new UserForm();
            f2.FormClosed += F2_FormClosed2;
            f2.ShowDialog();
        }

        /// <summary>
        /// Handles the FormClosed2 event of the F2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosedEventArgs" /> instance containing the event data.</param>
        private void F2_FormClosed2(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the CellValueChanged event of the dataGridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs" /> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the Click event of the addRowToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void addRowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
           int PrevRowId = Convert.ToInt32(dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[0].Value.ToString());
            AddConversionData f2 = new AddConversionData(PrevRowId+1);
            f2.FormClosed += F2_FormClosed1;
            f2.ShowDialog();
        }

        /// <summary>
        /// Handles the FormClosed1 event of the F2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosedEventArgs" /> instance containing the event data.</param>
        private void F2_FormClosed1(object sender, FormClosedEventArgs e)
        {
            Form2_Load(sender, e);
            this.Show();
        }

        /// <summary>
        /// Handles the Click event of the testToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            obj.log("Email Id: " + DataOperations.EmailId + " started Test");
            ChooseLanguage f2 = new ChooseLanguage();
            f2.FormClosed += F2_FormClosed;
            f2.ShowDialog();
        }

        /// <summary>
        /// Handles the Click event of the copyToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyCode();
        }

        /// <summary>
        /// Copies the code.
        /// </summary>
        private void CopyCode()
        {
            if (obj.getPrivilege(DataOperations.EmailId) == "Editor")
            {
                return;
            }
            DataObject d = dataGridView1.GetClipboardContent();
            Clipboard.SetDataObject(d);
        }

        /// <summary>
        /// Handles the KeyDown event of the dataGridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs" /> instance containing the event data.</param>
        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Control && e.KeyCode == Keys.C)
            {
                CopyCode();
            }            
        }

        /// <summary>
        /// Handles the Click event of the deleteToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteCode();
        }

        /// <summary>
        /// Deletes the code.
        /// </summary>
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

        /// <summary>
        /// Handles the Click event of the searchToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            obj.log("Email Id: " + DataOperations.EmailId + " started Search");
            Search f2 = new Search();
            f2.FormClosed += F2_FormClosed3;
            f2.ShowDialog();
        }

        /// <summary>
        /// Handles the FormClosed3 event of the F2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosedEventArgs" /> instance containing the event data.</param>
        private void F2_FormClosed3(object sender, FormClosedEventArgs e)
        {
            Form2_Load(sender, e);
            this.Show();
        }

    }
}
