using System;
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
    }
}
