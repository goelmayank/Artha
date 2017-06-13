using System;
using System.Data;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        DataOperations obj = new DataOperations();
        //private DataSet ds = new DataSet();
        private void Form2_Load(object sender, EventArgs e)
        {
            //the path in which XML file is saved
            DataSet ds = new DataSet();
            //Reading XML file and copying to dataset
            ds.ReadXml(DataOperations.path + "PG1000.xml");
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "table1";
            if (obj.getPrivilege(DataOperations.EmailId) == "User")
            {
                this.dataGridView1.AllowUserToAddRows = false;
                this.dataGridView1.AllowUserToDeleteRows = false;
                
            }
            else if (obj.getPrivilege(DataOperations.EmailId) == "Editor")
            {
                this.dataGridView1.AllowUserToDeleteRows = false;
            }
            editUsersToolStripMenuItem.Enabled = (obj.getPrivilege(DataOperations.EmailId) == "Admin")?true: false;
        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This sample is developed by Mayank Goel, Intern, ABB Pvt. Ltd. Core. Please read the Readme.htm for more details");
        }
        private void viewDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f4 = new Form4();
            f4.ShowDialog();
        }
        private void confirmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                //Adding columns to datatable
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    dt.Columns.Add(col.DataPropertyName, col.ValueType);
                }
                //adding new rows
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    DataRow row1 = dt.NewRow();
                    int c = 0;
                    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                    {
                        Console.WriteLine(row.Cells[i].Value);
                        //if value exists add that value else add "" for that field
                        if (row.Cells[i].Value == null)
                        {
                            row1[i] = " ";
                        }
                        else
                        {
                            c++;
                            row1[i] = row.Cells[i].Value;
                        }
                    }
                    if (c > 0)
                    {
                        dt.Rows.Add(row1);
                    }
                }
                //Copying from datatable to dataset
                ds.Tables.Add(dt);
                //writing new values to XML
                ds.WriteXml(DataOperations.path+ "PG1000.xml");


                DataOperations obj = new DataOperations();
                obj.log("Email Id: " + DataOperations.EmailId + " visited database at: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff"));
                MessageBox.Show("Saved successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }

        private void dataGridView1_DefaultValuesNeeded(object sender,DataGridViewRowEventArgs e)
        {
            e.Row.Cells["English"].Value = "";
            e.Row.Cells["Arabic"].Value = "";
            e.Row.Cells["German"].Value = "";
            e.Row.Cells["Italian"].Value = "";
            e.Row.Cells["Japanese"].Value = "";
            e.Row.Cells["Korean"].Value = "";
            e.Row.Cells["Norwegian"].Value = "";
            e.Row.Cells["Spanish"].Value = "";
            e.Row.Cells["Swedish"].Value = "";
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (obj.getPrivilege(DataOperations.EmailId) == "User")
            {
                dataGridView1.Rows[e.RowIndex].ReadOnly = true;
            }
            else if (obj.getPrivilege(DataOperations.EmailId) == "Editor")
            {
                dataGridView1.Rows[e.RowIndex].ReadOnly = true;
                if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                {
                    dataGridView1.Rows[e.RowIndex].ReadOnly = false;
                }
            }
        }

        private void editUsersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6 f6 = new Form6();
            f6.ShowDialog();
        }
    }
}
