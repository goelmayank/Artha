using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Register Class
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Register : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Register"/> class.
        /// </summary>
        public Register()
        {
            InitializeComponent();
            tbEmailId.Focus();
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
        /// Handles the Click event of the registerToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
        /// <summary>
        /// Handles the FormClosed event of the F2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosedEventArgs"/> instance containing the event data.</param>
        private void F2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the loginToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {

            this.Hide();
            Login f2 = new Login();
            f2.FormClosed += F2_FormClosed1;
            f2.ShowDialog();
        }

        /// <summary>
        /// Handles the FormClosed1 event of the F2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosedEventArgs"/> instance containing the event data.</param>
        private void F2_FormClosed1(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the KeyDown event of the tbEmailId control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void tbEmailId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) registerToolStripMenuItem_Click(sender, e);
        }

        /// <summary>
        /// Handles the KeyDown event of the tbPassword control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) registerToolStripMenuItem_Click(sender, e);
        }

        /// <summary>
        /// Handles the KeyDown event of the tbRepeatPassword control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void tbRepeatPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) registerToolStripMenuItem_Click(sender, e);
        }
    }
}
