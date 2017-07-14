using System;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Add New Users Class
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class AddUser : Form
    {
        /// <summary>
        /// The object
        /// </summary>
        private DataOperations obj = new DataOperations();
        /// <summary>
        /// The document
        /// </summary>
        private XDocument doc = XDocument.Load(DataOperations.path + "Users.xml");
        /// <summary>
        /// The path
        /// </summary>
        string path = DataOperations.path + "Users.xml";
        /// <summary>
        /// The old row count
        /// </summary>
        int oldRowCount = 0;
        /// <summary>
        /// Initializes a new instance of the <see cref="AddUser"/> class.
        /// </summary>
        /// <param name="rowCnt">The row count.</param>
        public AddUser(int rowCnt)
        {
            InitializeComponent();
            oldRowCount = rowCnt;
        }

        /// <summary>
        /// Handles the Click event of the confirmToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void confirmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.EndEdit();
                int i = 0;
                foreach (DataGridViewRow item in dataGridView1.Rows)
                {
                    if ((i = item.Index) == dataGridView1.Rows.Count - 1) break;
                    if ((item.Cells[0].Value != null) && (item.Cells[1].Value != null) && (item.Cells[2].Value != null))
                    {
                        if (obj.register(item.Cells[0].Value.ToString(), item.Cells[1].Value.ToString(), item.Cells[2].Value.ToString()))
                        {

                        }
                        else
                        {
                            MessageBox.Show("Email Id " + item.Cells[0].Value.ToString() +
                                " is already registered");
                        }
                    }
                    else
                    {
                        MessageBox.Show("One or more fields in row no " + (i + 1) + " were empty");
                    }
                }
                obj.log("Admin " + DataOperations.EmailId +
                                " added to Users Privilege Table" +
                                i.ToString() + " users");
                MessageBox.Show("Saved successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the aboutToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This sample is developed by IAPG, ABB. Please read the Readme.docx for more details");
        }

        /// <summary>
        /// Handles the EditingControlShowing event of the dataGridView1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewEditingControlShowingEventArgs"/> instance containing the event data.</param>
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 2 && e.Control is ComboBox)
            {
                ComboBox comboBox = e.Control as ComboBox;
                comboBox.SelectedIndexChanged += LastColumnComboSelectionChanged;
            }
        }

        /// <summary>
        /// Lasts the column combo selection changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void LastColumnComboSelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentRow.Cells[2].Value = dataGridView1.CurrentCell.EditedFormattedValue.ToString();
        }
    }
}
