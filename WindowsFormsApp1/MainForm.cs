﻿using System;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Automation;
using System.Windows.Forms;
using System.Drawing;
using System.Xml.Linq;
using System.Text;

namespace WindowsFormsApp1
{
    public partial class MainForm : Form
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public POINT(Point pt) : this(pt.X, pt.Y) { }

            public static implicit operator Point(POINT p)
            {
                return new Point(p.X, p.Y);
            }

            public static implicit operator POINT(Point p)
            {
                return new POINT(p.X, p.Y);
            }
        }

        [DllImport("User32.dll")]
        static extern IntPtr WindowFromPoint(POINT p);

        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(ref POINT p);

        POINT p;

        public MainForm()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //if (DataOperations.ApplicationJustGotStarted)
            //{
            //    DataOperations.txt[0] = new TextBox();
            //    DataOperations.txt[0].Font = new Font("Calibri", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            //    DataOperations.txt[0].Location = new Point(78, 13);
            //    DataOperations.txt[0].Name = "Japanese";
            //    DataOperations.txt[0].ReadOnly = true;
            //    DataOperations.txt[0].Size = new Size(182, 21);
            //    DataOperations.txt[0].Text = "";

            //    DataOperations.lbl[0] = new Label();
            //    DataOperations.lbl[0].AutoSize = true;
            //    DataOperations.lbl[0].Location = new Point(12,16);
            //    DataOperations.lbl[0].Name = "label_English";
            //    DataOperations.lbl[0].Size = new Size(58, 13);
            //    DataOperations.lbl[0].Text = "English";

            //    this.Controls.Add(DataOperations.txt[0]);
            //    this.Controls.Add(DataOperations.lbl[0]);
            //    this.ClientSize = new Size(369, 50);

            //    Encoding enc = new UTF32Encoding(false, true, true);
            //    try
            //    {
            //        DataOperations.dictionary["English"].Clear();

            //        var doc = XDocument.Load(DataOperations.path + "PG1000.xml").Descendants("Row");

            //        DataOperations.dictionary["English"].Clear();
            //        DataOperations.dictionary["English"] = doc.ToDictionary(
            //            p => (string)p.Element("Japanese").Value,
            //            p => (string)p.Element("English").Value
            //        );
            //        //foreach (KeyValuePair<string, string> kvp in DataOperations.dictionary["English"])
            //        //{
            //        //    Console.WriteLine(string.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value));
            //        //}
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //    }
            //}
            //else
            {
                for (int i = 0; i < DataOperations.txt.Length; i++)
                {
                    this.Controls.Add(DataOperations.txt[i]);
                    this.Controls.Add(DataOperations.lbl[i]);
                }
                this.ClientSize = new Size(369, DataOperations.ClientSize);
            }
            tmrCursorPos.Enabled = true;
            tmrCursorPos.Start();
        }

        private void tmrCursorPos_Tick(object sender, EventArgs e)
        {
            bool retVal = GetCursorPos(ref p);
            if (retVal)
            {
                IntPtr hwnd = WindowFromPoint(p);

                if (hwnd.ToInt64() > 0)
                {
                    if (DataOperations.ApplicationJustGotStarted)
                    {
                        DataOperations.txt[0].Text = GetTextBelowTheCursor("English");
                    }
                    else
                    {
                        foreach (var tbx in DataOperations.txt)
                        {
                            if (tbx != null)
                            {
                                TextBox tb = this.Controls.Find(tbx.Name, true).FirstOrDefault() as TextBox;
                                tb.Text = GetTextBelowTheCursor(tbx.Name);
                            }
                        }
                    }
                }
            }
        }
        string GetTextBelowTheCursor(string toLang)
        {
            try
            {
                Point mouse = Cursor.Position; // use Windows forms mouse code instead of WPF
                AutomationElement element = AutomationElement.FromPoint(new System.Windows.Point(mouse.X, mouse.Y));

                if (element == null)
                {
                    // no element under mouse
                    return "";
                }
                // Replacing key with value from Dictionary
            
                return string.Join(" ", $"{element.Current.Name}".Split(' ').Select(
                    i => DataOperations.dictionary[toLang].Any(tlcls=> tlcls.srcLan==i) ?
                    DataOperations.dictionary[toLang].Where(tlcl => tlcl.srcLan == i).FirstOrDefault().trgLan : i));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return "";

        }
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This sample is developed by Mayank Goel, Intern, ABB Pvt. Ltd. Core. Please read the Readme.htm for more details");
        }
        //private void chooseLanguageToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    tmrCursorPos.Stop();
        //    tmrCursorPos.Enabled = false;
        //    this.Hide();
        //    ChooseLanguage f3 = new ChooseLanguage();
        //    f3.ShowDialog();
        //}

        //private void startToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    tmrCursorPos.Enabled = true;
        //    tmrCursorPos.Start();
        //}
        //private void stopReadingToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    tmrCursorPos.Stop();
        //    tmrCursorPos.Enabled = false;
        //}

    }
}