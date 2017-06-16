using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This sample is developed by Mayank Goel, Intern, ABB Pvt. Ltd. Core. Please read the Readme.htm for more details");
        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataOperations obj = new DataOperations();
            if (string.IsNullOrEmpty(tbEmailId.Text) && string.IsNullOrEmpty(tbPassword.Text))
            {
                MessageBox.Show("The email Id and password fields cannot be empty.");
            }
            else
            {
                if (obj.login(tbEmailId.Text, tbPassword.Text))
                {

                    DataOperations.EmailId = tbEmailId.Text;
                    if (obj.getPrivilege(tbEmailId.Text) == "User")
                    {
                        this.Hide();
                        ChooseLanguage f2 = new ChooseLanguage();
                        f2.FormClosed += F2_FormClosed;
                        f2.ShowDialog();
                    }
                    else
                    {
                        this.Hide();
                        ConversionTable f2 = new ConversionTable();
                        f2.FormClosed += F2_FormClosed;
                        f2.ShowDialog();
                        
                    }
                }
                else
                {
                    MessageBox.Show("Email Id or password does not match.");
                }
            }
        }              

        private void F2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register f2 = new Register();
            f2.FormClosed += F2_FormClosed2;
            f2.ShowDialog();
        }

        private void F2_FormClosed2(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void tbEmailId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                loginToolStripMenuItem_Click(sender, e);
        }

        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) loginToolStripMenuItem_Click(sender, e);
           // else if (e.KeyCode = Keys.Alt + Keys.A) aboutToolStripMenuItem_Click(sender, e);
        }
    }
}
