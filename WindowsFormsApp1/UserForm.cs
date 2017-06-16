using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    public partial class UserForm : Form
    {
        private DataOperations obj = new DataOperations();
        private XDocument doc = XDocument.Load(DataOperations.path + "Users.xml");
        string path = DataOperations.path + "Users.xml";
        public UserForm()
        {
            InitializeComponent();
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

                obj.log("Admin " + DataOperations.EmailId + " visited Users Privilege Table");
                MessageBox.Show("Saved successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Hide();
            ConversionTable f2 = new ConversionTable();
            f2.FormClosed += F2_FormClosed;
            f2.ShowDialog();
        }

        private void F2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void fillDGV()
        {
            var res = doc.Descendants("Person").ToList();
            dataGridView1.DataSource = res.ToDataTable();
            dataGridView1.Columns["Password"].Visible = false;
            dataGridView1.Columns["Id"].Visible = false;
            dataGridView1.Columns["Privilege"].Visible = false;
            this.dataGridView1.AllowUserToAddRows = false;
        }
        private void Form6_Load(object sender, EventArgs e)
        {
            fillDGV();
            DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn();           
            col.DataPropertyName = "Privilege";
            col.HeaderText = "Privilege";
            dataGridView1.Columns.Add(col);
            col.Items.Add("User");
            col.Items.Add("Editor");
            col.Items.Add("Admin");
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            XElement upd = doc.Descendants("Person").ToList().Where(x => x.Element("Id").Value == dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString())
                    .Single();
            //upd.Element("EmailId").Value = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            upd.Element(dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].HeaderText).Value = dataGridView1.CurrentCell.Value.ToString();
            obj.log("Admin " + DataOperations.EmailId + " modified User Privilege Table row no " +
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() +
                " " + dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].HeaderText +
                " Entry to " + dataGridView1.CurrentCell.Value.ToString()
                );
        }

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            int PrevRowId = Convert.ToInt32(dataGridView1.Rows[dataGridView1.Rows.Count - 2].Cells[0].Value.ToString());
            AddUser f2 = new AddUser(PrevRowId + 1);
            f2.FormClosed += F2_FormClosed1;
            f2.ShowDialog();
        }

        private void F2_FormClosed1(object sender, FormClosedEventArgs e)
        {
            fillDGV();
            this.Show();
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 4 && e.Control is ComboBox)
            {
                ComboBox comboBox = e.Control as ComboBox;
                comboBox.SelectedIndexChanged += LastColumnComboSelectionChanged;
            }
        }

        private void LastColumnComboSelectionChanged(object sender, EventArgs e)
        {
            //doc = 

            XElement upd = doc.Descendants("Person").ToList().Where(x => x.Element("Id").Value == dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString())
                    .Single();
            
            upd.Element("Privilege").Value = dataGridView1.CurrentCell.EditedFormattedValue.ToString();

            obj.log("Admin " + DataOperations.EmailId + " modified User Privilege Table row no " +
                dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString() +
                " Privilege Entry to " + upd.Element("Privilege").Value
                );
        }
    }
}
