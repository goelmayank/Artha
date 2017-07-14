using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    /// <summary>
    /// Choose Language Table
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class ChooseLanguage : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ChooseLanguage"/> class.
        /// </summary>
        public ChooseLanguage()
        {
            InitializeComponent();
        }
        /// <summary>
        /// The object
        /// </summary>
        DataOperations obj = new DataOperations();
        /// <summary>
        /// Handles the Load event of the Form3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void Form3_Load(object sender, EventArgs e)
        {
            checkBox1.CheckState = (DataOperations.toEnglishEnabled || DataOperations.toAllEnabled) ? CheckState.Checked : CheckState.Unchecked;
            checkBox2.CheckState = (DataOperations.toArabicEnabled || DataOperations.toAllEnabled) ? CheckState.Checked : CheckState.Unchecked;
            checkBox3.CheckState = (DataOperations.toGermanEnabled || DataOperations.toAllEnabled) ? CheckState.Checked : CheckState.Unchecked;
            checkBox4.CheckState = (DataOperations.toItalianEnabled || DataOperations.toAllEnabled) ? CheckState.Checked : CheckState.Unchecked;
            checkBox5.CheckState = (DataOperations.toJapaneseEnabled || DataOperations.toAllEnabled) ? CheckState.Checked : CheckState.Unchecked;
            checkBox6.CheckState = (DataOperations.toKoreanEnabled || DataOperations.toAllEnabled) ? CheckState.Checked : CheckState.Unchecked;
            checkBox7.CheckState = (DataOperations.toNorwegianEnabled || DataOperations.toAllEnabled) ? CheckState.Checked : CheckState.Unchecked;
            checkBox8.CheckState = (DataOperations.toSpanishEnabled || DataOperations.toAllEnabled) ? CheckState.Checked : CheckState.Unchecked;
            checkBox9.CheckState = (DataOperations.toSwedishEnabled || DataOperations.toAllEnabled) ? CheckState.Checked : CheckState.Unchecked;
            checkBox10.CheckState = (DataOperations.toAllEnabled || DataOperations.toAllEnabled) ? CheckState.Checked : CheckState.Unchecked;

            groupBox1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Text == DataOperations.FromLanguage).Checked = true;

        }

        /// <summary>
        /// Handles the Click event of the homeToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var checkedButton = groupBox1.Controls.OfType<RadioButton>()
                                      .FirstOrDefault(r => r.Checked);
            
            DataOperations.FromLanguage = checkedButton.Text;
            DataOperations.ApplicationJustGotStarted = false;
            var doc = XDocument.Load(DataOperations.path + "PG1000.xml").Descendants("Row");
            int i= 0, height = 13;
            if (obj.getPrivilege(DataOperations.EmailId) == "Admin")
            {
                height += 24;
            }
            Array.Clear(DataOperations.txt, 0, DataOperations.txt.Length);
            Array.Clear(DataOperations.lbl, 0, DataOperations.lbl.Length);
            string Message="User " + DataOperations.EmailId + " chose language conversion from " + checkedButton.Text + " to ";
            List<DataOperations.TargetVal> targets;
            DataOperations.TargetVal target;
            foreach (var ctrl in groupBox2.Controls.OfType<CheckBox>().Where(x => x.Checked))
            {
                DataOperations.txt[i] = new TextBox();
                DataOperations.lbl[i] = new Label();

                DataOperations.txt[i].Font = new Font("Calibri", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                DataOperations.txt[i].Location = new Point(64, height);
                DataOperations.txt[i].Name = ctrl.Text;
                DataOperations.txt[i].ReadOnly = true;
                DataOperations.txt[i].Size = new Size(293, 21);
                DataOperations.txt[i].Text = "";

                DataOperations.lbl[i].AutoSize = true;
                DataOperations.lbl[i].Location = new Point(12, height+3);
                DataOperations.lbl[i].Name = "label_" + ctrl.Text;
                DataOperations.lbl[i].Size = new Size(58, 13);
                DataOperations.lbl[i].Text = ctrl.Text;
                try

                {
                    targets = new List<DataOperations.TargetVal>();
                    foreach (var item in doc)
                    {
                        target = new DataOperations.TargetVal()
                        { srcLan = item.Element(checkedButton.Text) == null ? string.Empty : item.Element(checkedButton.Text).Value,
                            trgLan = item.Element(ctrl.Text) == null ? string.Empty : item.Element(ctrl.Text).Value };
                        targets.Add(target);
                    }
                    DataOperations.dictionary[ctrl.Text] = targets;
                    Message += ctrl.Text + ", ";
                    foreach (DataOperations.TargetVal kvp in DataOperations.dictionary[ctrl.Text])
                    {
                        Console.WriteLine(string.Format("Key = {0}, Value = {1}", kvp.srcLan, kvp.trgLan));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                i++; height += 27;
            }
            obj.log(Message);
            DataOperations.ClientSize = height + 10;
            this.Hide();
            MainForm f2 = new MainForm();
            f2.FormClosed += F2_FormClosed;
            f2.ShowDialog();
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
        /// Handles the Click event of the aboutToolStripMenuItem control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This sample is developed by IAPG, ABB. Please read the Readme.docx for more details");
        }

        /// <summary>
        /// Handles the 1 event of the checkBox1_CheckedChanged control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            DataOperations.toEnglishEnabled = (checkBox1.CheckState == CheckState.Checked || checkBox10.CheckState == CheckState.Checked);
        }

        /// <summary>
        /// Handles the 1 event of the checkBox2_CheckedChanged control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            DataOperations.toArabicEnabled = (checkBox2.CheckState == CheckState.Checked || checkBox10.CheckState == CheckState.Checked);
        }

        /// <summary>
        /// Handles the 1 event of the checkBox3_CheckedChanged control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void checkBox3_CheckedChanged_1(object sender, EventArgs e)
        {
            DataOperations.toGermanEnabled = (checkBox3.CheckState == CheckState.Checked || checkBox10.CheckState == CheckState.Checked);
        }

        /// <summary>
        /// Handles the 1 event of the checkBox4_CheckedChanged control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void checkBox4_CheckedChanged_1(object sender, EventArgs e)
        {
            DataOperations.toItalianEnabled = (checkBox4.CheckState == CheckState.Checked || checkBox10.CheckState == CheckState.Checked);
        }

        /// <summary>
        /// Handles the 1 event of the checkBox5_CheckedChanged control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void checkBox5_CheckedChanged_1(object sender, EventArgs e)
        {
            DataOperations.toJapaneseEnabled = (checkBox5.CheckState == CheckState.Checked || checkBox10.CheckState == CheckState.Checked);
        }

        /// <summary>
        /// Handles the 1 event of the checkBox6_CheckedChanged control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void checkBox6_CheckedChanged_1(object sender, EventArgs e)
        {
            DataOperations.toKoreanEnabled = (checkBox6.CheckState == CheckState.Checked || checkBox10.CheckState == CheckState.Checked);
        }

        /// <summary>
        /// Handles the 1 event of the checkBox7_CheckedChanged control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void checkBox7_CheckedChanged_1(object sender, EventArgs e)
        {
            DataOperations.toNorwegianEnabled = (checkBox7.CheckState == CheckState.Checked || checkBox10.CheckState == CheckState.Checked);
        }

        /// <summary>
        /// Handles the 1 event of the checkBox8_CheckedChanged control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void checkBox8_CheckedChanged_1(object sender, EventArgs e)
        {
            DataOperations.toSpanishEnabled = (checkBox8.CheckState == CheckState.Checked || checkBox10.CheckState == CheckState.Checked);
        }

        /// <summary>
        /// Handles the 1 event of the checkBox9_CheckedChanged control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void checkBox9_CheckedChanged_1(object sender, EventArgs e)
        {
            DataOperations.toSwedishEnabled = (checkBox9.CheckState == CheckState.Checked || checkBox10.CheckState == CheckState.Checked);
        }


        /// <summary>
        /// Handles the 1 event of the checkBox10_CheckedChanged control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void checkBox10_CheckedChanged_1(object sender, EventArgs e)
        {
            DataOperations.toAllEnabled = (checkBox10.CheckState == CheckState.Checked);
            checkBox1.CheckState = CheckState.Checked;
            checkBox2.CheckState = CheckState.Checked;
            checkBox3.CheckState = CheckState.Checked;
            checkBox4.CheckState = CheckState.Checked;
            checkBox5.CheckState = CheckState.Checked;
            checkBox6.CheckState = CheckState.Checked;
            checkBox7.CheckState = CheckState.Checked;
            checkBox8.CheckState = CheckState.Checked;
            checkBox9.CheckState = CheckState.Checked;
        }

    }
}
