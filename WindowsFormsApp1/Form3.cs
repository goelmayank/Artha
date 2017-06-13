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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        DataOperations obj = new DataOperations();
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

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var checkedButton = groupBox1.Controls.OfType<RadioButton>()
                                      .FirstOrDefault(r => r.Checked);
            
            DataOperations.FromLanguage = checkedButton.Text;
            DataOperations.ApplicationJustGotStarted = false;
            var doc = XDocument.Load(DataOperations.path + "PG1000.xml").Descendants("Table1");
            int i= 0, height = 25;
            Array.Clear(DataOperations.txt, 0, DataOperations.txt.Length);
            Array.Clear(DataOperations.lbl, 0, DataOperations.lbl.Length);
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
                DataOperations.lbl[i].Location = new Point(0, height);
                DataOperations.lbl[i].Name = "label_" + ctrl.Text;
                DataOperations.lbl[i].Size = new Size(58, 13);
                DataOperations.lbl[i].Text = ctrl.Text;
                try
                {
                    DataOperations.dictionary[ctrl.Text].Clear();
                    DataOperations.dictionary[ctrl.Text] = doc.ToDictionary(
                        p => (string)p.Element(checkedButton.Text).Value,
                        p => (string)p.Element(ctrl.Text).Value
                    );
                    foreach (KeyValuePair<string, string> kvp in DataOperations.dictionary[ctrl.Text])
                    {
                        Console.WriteLine(string.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                i++; height += 27;
            }
            
            DataOperations.ClientSize = height + 10;
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }
        
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This sample is developed by Mayank Goel, Intern, ABB Pvt. Ltd. Core. Please read the Readme.htm for more details");
        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            DataOperations.toEnglishEnabled = (checkBox1.CheckState == CheckState.Checked || checkBox10.CheckState == CheckState.Checked);
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            DataOperations.toArabicEnabled = (checkBox2.CheckState == CheckState.Checked || checkBox10.CheckState == CheckState.Checked);
        }

        private void checkBox3_CheckedChanged_1(object sender, EventArgs e)
        {
            DataOperations.toGermanEnabled = (checkBox3.CheckState == CheckState.Checked || checkBox10.CheckState == CheckState.Checked);
        }

        private void checkBox4_CheckedChanged_1(object sender, EventArgs e)
        {
            DataOperations.toItalianEnabled = (checkBox4.CheckState == CheckState.Checked || checkBox10.CheckState == CheckState.Checked);
        }

        private void checkBox5_CheckedChanged_1(object sender, EventArgs e)
        {
            DataOperations.toJapaneseEnabled = (checkBox5.CheckState == CheckState.Checked || checkBox10.CheckState == CheckState.Checked);
        }

        private void checkBox6_CheckedChanged_1(object sender, EventArgs e)
        {
            DataOperations.toKoreanEnabled = (checkBox6.CheckState == CheckState.Checked || checkBox10.CheckState == CheckState.Checked);
        }

        private void checkBox7_CheckedChanged_1(object sender, EventArgs e)
        {
            DataOperations.toNorwegianEnabled = (checkBox7.CheckState == CheckState.Checked || checkBox10.CheckState == CheckState.Checked);
        }

        private void checkBox8_CheckedChanged_1(object sender, EventArgs e)
        {
            DataOperations.toSpanishEnabled = (checkBox8.CheckState == CheckState.Checked || checkBox10.CheckState == CheckState.Checked);
        }

        private void checkBox9_CheckedChanged_1(object sender, EventArgs e)
        {
            DataOperations.toSwedishEnabled = (checkBox9.CheckState == CheckState.Checked || checkBox10.CheckState == CheckState.Checked);
        }


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
