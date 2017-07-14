using System;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Login Form
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Login : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Login"/> class.
        /// </summary>
        public Login()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Handles the Load event of the Form4 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void Form4_Load(object sender, EventArgs e)
        {

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
        /// Handles the Click event of the loginToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
        /// Handles the Click event of the registerToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void registerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register f2 = new Register();
            f2.FormClosed += F2_FormClosed2;
            f2.ShowDialog();
        }

        /// <summary>
        /// Handles the FormClosed2 event of the F2 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosedEventArgs"/> instance containing the event data.</param>
        private void F2_FormClosed2(object sender, FormClosedEventArgs e)
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
            if (e.KeyCode == Keys.Enter)
                loginToolStripMenuItem_Click(sender, e);
        }

        /// <summary>
        /// Handles the KeyDown event of the tbPassword control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        private void tbPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) loginToolStripMenuItem_Click(sender, e);
           // else if (e.KeyCode = Keys.Alt + Keys.A) aboutToolStripMenuItem_Click(sender, e);
        }
    }
}
