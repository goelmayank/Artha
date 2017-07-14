using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;


namespace WindowsFormsApp1
{
    /// <summary>
    /// Search Form
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Search : Form
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
        /// Initializes a new instance of the <see cref="Search" /> class.
        /// </summary>
        public Search()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the Search control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void Search_Load(object sender, EventArgs e)
        {
            doc = XDocument.Load(path);
        }

        /// <summary>
        /// Handles the TextChanged event of the searchWord control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
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
            this.Close();
        }
    }
}
