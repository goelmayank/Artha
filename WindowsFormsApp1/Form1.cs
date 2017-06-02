using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Automation.Text;
using System.Windows.Forms;

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

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern long GetWindowText(IntPtr hwnd, StringBuilder lpString, int cch);

        [DllImport("User32.dll")]
        static extern IntPtr GetParent(IntPtr hwnd);

        [DllImport("User32.dll")]
        static extern IntPtr ChildWindowFromPoint(IntPtr hWndParent, POINT p);

        [DllImport("User32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern long GetClassName(IntPtr hwnd, StringBuilder lpClassName, int nMaxCount);

        POINT p;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
                lblXPos.Text = "Mouse X : " + Convert.ToString(p.X);
                lblYPos.Text = "Mouse Y : " + Convert.ToString(p.Y);
                IntPtr hwnd = WindowFromPoint(p);
                lblHWnd.Text = "Window Handle : " + hwnd.ToInt64();
                if (hwnd.ToInt64() > 0)
                {
                    rtbCaption.Text = GetCaptionOfWindow(hwnd);
                    rtbClassName.Text = GetClassNameOfWindow(hwnd);
                    ConversionTextBox.Text = GetTextBelowTheCursor(hwnd);
                    //For Parent 
                    IntPtr hWndParent = GetParent(hwnd);
                    if (hWndParent.ToInt64() > 0)
                    {
                        rtbWinParent.Text = GetCaptionOfWindow(hWndParent);
                    }
                }
                //this.Text = DateTime.Now.ToString("dd-MMM-yyyy hh:mm:ss.fff"); 
            }
        }


        string GetCaptionOfWindow(IntPtr hwnd)
        {
            string caption = "";
            StringBuilder windowText = null;
            try
            {
                int max_length = GetWindowTextLength(hwnd);
                windowText = new StringBuilder("", max_length + 5);
                GetWindowText(hwnd, windowText, max_length + 2);

                if (!String.IsNullOrEmpty(windowText.ToString()) && !String.IsNullOrWhiteSpace(windowText.ToString()))
                    caption = windowText.ToString();
            }
            catch (Exception ex)
            {
                caption = ex.Message;
            }
            finally
            {
                windowText = null;
            }
            return caption;
        }

        string GetClassNameOfWindow(IntPtr hwnd)
        {
            string className = "";
            StringBuilder classText = null;
            try
            {
                int cls_max_length = 1000;
                classText = new StringBuilder("", cls_max_length + 5);
                GetClassName(hwnd, classText, cls_max_length + 2);

                if (!String.IsNullOrEmpty(classText.ToString()) && !String.IsNullOrWhiteSpace(classText.ToString()))
                    className = classText.ToString();
            }
            catch (Exception ex)
            {
                className = ex.Message;
            }
            finally
            {
                classText = null;
            }
            return className;
        }
        string GetTextBelowTheCursor(IntPtr hwnd)
        {
            Dictionary<string, string> Dictionary = new Dictionary<string, string>();
            var array = File.ReadAllLines(@"c:\temp\Test.txt", Encoding.GetEncoding(932));
            for (var i = 0; i < array.Length; i += 2)
            {
                Dictionary.Add(array[i + 1], array[i]);
            }

            System.Drawing.Point mouse = Cursor.Position; // use Windows forms mouse code instead of WPF
            AutomationElement element = AutomationElement.FromPoint(new System.Windows.Point(mouse.X, mouse.Y));
            if (element == null)
            {
                // no element under mouse
                return "";
            }

            var String = $"{element.Current.Name}";

            // Replacing key with value from Dictionary
            var result = string.Join(" ", String.Split(' ').Select(i => Dictionary.ContainsKey(i) ? Dictionary[i] : i));

            return result;                

            object pattern;
            // the "Value" pattern is supported by many application (including IE & FF)
            if (element.TryGetCurrentPattern(ValuePattern.Pattern, out pattern))
            {
                ValuePattern valuePattern = (ValuePattern)pattern;
                Console.WriteLine(" Value=" + valuePattern.Current.Value);
            }

            // the "Text" pattern is supported by some applications (including Notepad)and returns the current selection for example
            if (element.TryGetCurrentPattern(TextPattern.Pattern, out pattern))
            {
                TextPattern textPattern = (TextPattern)pattern;
                foreach (TextPatternRange range in textPattern.GetSelection())
                {
                    Console.WriteLine(" SelectionRange=" + range.GetText(-1));
                }
            }
            Thread.Sleep(1000);
            Console.WriteLine(); Console.WriteLine();
            
        }
    }
}
