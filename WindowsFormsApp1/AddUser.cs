using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    public partial class AddUser : Form
    {
        private DataOperations obj = new DataOperations();
        private XDocument doc = XDocument.Load(DataOperations.path + "Users.xml");
        string path = DataOperations.path + "Users.xml";
        int oldRowCount = 0;
        public AddUser(int rowCnt)
        {
            InitializeComponent();
            oldRowCount = rowCnt;
        }

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
                //foreach (DataGridViewRow item in dataGridView1.Rows)
                //{
                //    if (item.Index == dataGridView1.Rows.Count - 1) break;
                //    doc.Descendants("Persons").FirstOrDefault().Add(new XElement("Person",
                //        new XElement("Id", (++oldRowCount).ToString()),
                //        new XElement("EmailId", (item.Cells[0].Value == null) ? string.Empty : item.Cells[0].Value.ToString()),
                //       new XElement("Password", (item.Cells[1].Value == null) ? string.Empty : Convert.ToBase64String(Encoding.UTF8.GetBytes(item.Cells[1].Value.ToString()))),
                //       new XElement("Privilege", (item.Cells[2].Value == null) ? string.Empty : item.Cells[2].Value.ToString())

                //        ));

                //    obj.log("Admin: " + DataOperations.EmailId + " added to Users Privilege Table row no : " + (oldRowCount).ToString());

                //}

                //doc.Save(path);

                //obj.log("Admin: " + DataOperations.EmailId + " visited Add Conversion Data");
                //MessageBox.Show("Saved successfully");
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

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 2 && e.Control is ComboBox)
            {
                ComboBox comboBox = e.Control as ComboBox;
                comboBox.SelectedIndexChanged += LastColumnComboSelectionChanged;
            }
        }

        private void LastColumnComboSelectionChanged(object sender, EventArgs e)
        {
            dataGridView1.CurrentRow.Cells[2].Value = dataGridView1.CurrentCell.EditedFormattedValue.ToString();
        }
    }
}
