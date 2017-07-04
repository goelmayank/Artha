using System;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Automation;
using System.Windows.Forms;
using System.Drawing;
using Google.API.Translate;
using System.Xml.Linq;
using System.Text;
using System.Net;

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
        DataOperations obj = new DataOperations();
        //string result;
        public MainForm()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if ( obj.getPrivilege(DataOperations.EmailId) == "Admin" )
            {
                this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
                this.aboutToolStripMenuItem.Size = new Size(52, 20);
                this.aboutToolStripMenuItem.Text = "About";
                this.aboutToolStripMenuItem.Click += new EventHandler(this.aboutToolStripMenuItem_Click_1);
               
                this.returnToConvewrsionTableToolStripMenuItem.Name = "returnToConvewrsionTableToolStripMenuItem";
                this.returnToConvewrsionTableToolStripMenuItem.Size = new Size(179, 20);
                this.returnToConvewrsionTableToolStripMenuItem.Text = "Return to Conversion Table";
                this.returnToConvewrsionTableToolStripMenuItem.Click += new EventHandler(this.returnToConvewrsionTableToolStripMenuItem_Click);

                this.menuStrip1.Items.AddRange(new ToolStripItem[] {
                this.aboutToolStripMenuItem,
                this.returnToConvewrsionTableToolStripMenuItem});
                this.menuStrip1.Location = new Point(0, 0);
                this.menuStrip1.Name = "menuStrip1";
                this.menuStrip1.Size = new Size(375, 24);
                this.menuStrip1.TabIndex = 0;
                this.menuStrip1.Text = "menuStrip1";

                this.Controls.Add(this.menuStrip1);
            }

            for (int i = 0; i < DataOperations.txt.Length; i++)
                {
                    this.Controls.Add(DataOperations.txt[i]);
                    this.Controls.Add(DataOperations.lbl[i]);
                }
            this.ClientSize = new Size(369, DataOperations.ClientSize);
            
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

                //if (DataOperations.Autodetect)
                //{
                //    Console.WriteLine($"{element}");
                    //TranslateYourText($"{element.Current.Name}", DataOperations.FromLanguage, toLang);
                    //if (!string.IsNullOrEmpty(result= TranslateYourText($"{element.Current.Name}", DataOperations.FromLanguage, toLang)))
                    //{
                    //    return result;
                    //}
                //}

                // Replacing key with value from Dictionary
                return string.Join(" ", $"{element.Current.Name}".Split(' ').Select(
                    i => DataOperations.dictionary[toLang].Any(tlcls=> tlcls.srcLan==i) ?
                    DataOperations.dictionary[toLang].Where(tlcl => tlcl.srcLan == i).FirstOrDefault().trgLan : i));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return "";

        }

        private void aboutToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            MessageBox.Show("This sample is developed by Mayank Goel, Intern, ABB Pvt. Ltd. Core. Please read the Readme.htm for more details");
        }

        private void returnToConvewrsionTableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            ConversionTable f2 = new ConversionTable();
            f2.FormClosed += F2_FormClosed;
            f2.ShowDialog();
        }
        private void F2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
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