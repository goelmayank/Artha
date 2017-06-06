using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Automation.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
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

            public POINT(System.Drawing.Point pt) : this(pt.X, pt.Y) { }

            public static implicit operator System.Drawing.Point(POINT p)
            {
                return new System.Drawing.Point(p.X, p.Y);
            }

            public static implicit operator POINT(System.Drawing.Point p)
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

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            DataOperations obj = new DataOperations();
            obj.ExecuteQuery("SELECT Japanese FROM Conversions;");

            listBox1.DisplayMember = "Japanese";
            listBox1.DataSource = obj.ConversionDataTable;

            obj = new DataOperations();
            obj.ExecuteQuery("SELECT English FROM Conversions;");

            listBox2.DisplayMember = "English";
            listBox2.DataSource = obj.ConversionDataTable;

            obj = new DataOperations();
            obj.ExecuteQuery("Select Japanese, English FROM Conversions");
            obj.LoadDictionary();
            //check the key value pairs in the dictionary
            //foreach (KeyValuePair<string, string> kvp in DataOperations.Dictionary)
            //{
            //    Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
            //}

        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            tmrCursorPos.Enabled = true;
            tmrCursorPos.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            tmrCursorPos.Stop();
            tmrCursorPos.Enabled = false;
        }

        private void tmrCursorPos_Tick(object sender, EventArgs e)
        {
           
            bool retVal = GetCursorPos(ref p);
            if (retVal)
            {
                IntPtr hwnd = WindowFromPoint(p);
                
                if (hwnd.ToInt64() > 0)
                {
                    ConversionTextBox.Text = GetTextBelowTheCursor(hwnd);
                }
            }
        }
        string GetTextBelowTheCursor(IntPtr hwnd)
        {

            System.Drawing.Point mouse = Cursor.Position; // use Windows forms mouse code instead of WPF
            AutomationElement element = AutomationElement.FromPoint(new System.Windows.Point(mouse.X, mouse.Y));
            if (element == null)
            {
                // no element under mouse
                return "";
            }

            var String = $"{element.Current.Name}";
            
            // Replacing key with value from Dictionary
            var result = string.Join(" ", String.Split(' ').Select(i => DataOperations.Dictionary.ContainsKey(i) ? DataOperations.Dictionary[i] : i));

            return result;                
        
        }

        private void BackendButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2(); //this is the change, code for redirect
            f2.ShowDialog();
        }
    }
}
