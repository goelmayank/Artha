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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This sample is developed by Mayank Goel, Intern, ABB Pvt. Ltd. Core. Please read the Readme.htm for more details");
        }

        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataOperations obj = new DataOperations();
            if (string.IsNullOrEmpty(tbEmailId.Text) && string.IsNullOrEmpty(tbPassword.Text))
            {
                MessageBox.Show("The email id and password fields cannot be empty.");
            }
            else
            {
                if (tbRepeatPassword.Text == tbPassword.Text)
                {

                    if (obj.verifyEmailId(tbEmailId.Text))
                    {
                        obj.register(tbEmailId.Text, tbPassword.Text);
                        DataOperations.EmailId = tbEmailId.Text;
                        this.Hide();
                        Form2 f2 = new Form2();
                        f2.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Email Id is not valid");
                    }
                }
                else
                    MessageBox.Show("Password does not match");
            }
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f4 = new Form4();
            f4.ShowDialog();
        }
    }
}
