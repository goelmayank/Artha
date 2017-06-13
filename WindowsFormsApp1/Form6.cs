using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void cancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This sample is developed by Mayank Goel, Intern, ABB Pvt. Ltd. Core. Please read the Readme.htm for more details");
        }

        private void confirmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string path = @"c:\temp\Users.xml";
                DataSet ds = (DataSet)dataGridView1.DataSource;
                ds.WriteXml(path);
                DataOperations obj = new DataOperations();
                obj.log("Email Id: " + DataOperations.EmailId + " modified Users Privilege at: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff"));
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

        private void Form6_Load(object sender, EventArgs e)
        {
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(@"c:\temp\Users.xml");
            //dataGridView1.DataSource = dataSet.Tables[0];
            dataGridView1.DataSource = dataSet;
            dataGridView1.DataMember = "Person";
        }
    }
}
