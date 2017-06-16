using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
            tbEmailId.Focus();
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
                MessageBox.Show("The email Id and password fields cannot be empty.");
            }
            else
            {
                if (tbRepeatPassword.Text == tbPassword.Text)
                {

                    if (obj.verifyEmailId(tbEmailId.Text))
                    {
                        if (obj.register(tbEmailId.Text, tbPassword.Text, "User"))
                        {
                            this.Hide();
                            ChooseLanguage f2 = new ChooseLanguage();
                            f2.FormClosed += F2_FormClosed;
                            f2.ShowDialog();
                        }
                        else
                        {
                            MessageBox.Show("Email Id is already registered");
                        }
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
        private void F2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.Hide();
            Login f2 = new Login();
            f2.FormClosed += F2_FormClosed1;
            f2.ShowDialog();
        }

        private void F2_FormClosed1(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void tbEmailId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) registerToolStripMenuItem_Click(sender, e);
        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) registerToolStripMenuItem_Click(sender, e);
        }

        private void tbRepeatPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) registerToolStripMenuItem_Click(sender, e);
        }
    }
}
