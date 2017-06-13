using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    class DataOperations
    {
        public DataOperations()
        {
        }
        public static string path = "../../../Database/";
        public static bool ApplicationJustGotStarted = true;
        public static string FromLanguage = "Japanese";
        public static string EmailId;
        public static TextBox[] txt = new TextBox[9];
        public static Label[] lbl = new Label[9];
        
        public static Dictionary<string, Dictionary<string, string>> dictionary = new Dictionary<string, Dictionary<string, string>>()
        {
            { "Arabic", new Dictionary<string, string>() },
            { "English", new Dictionary<string, string>() },
            { "German", new Dictionary<string, string>() },
            { "Italian", new Dictionary<string, string>() },
            { "Japanese", new Dictionary<string, string>() },
            { "Korean", new Dictionary<string, string>() },
            { "Morwegian", new Dictionary<string, string>() },
            { "Spanish", new Dictionary<string, string>() },
            { "Swedish", new Dictionary<string, string>() }
        };
        public static bool toEnglishEnabled = true;
        public static bool toArabicEnabled = false;
        public static bool toGermanEnabled = false;
        public static bool toItalianEnabled = false;
        public static bool toJapaneseEnabled = false;
        public static bool toKoreanEnabled = false;
        public static bool toNorwegianEnabled = false;
        public static bool toSpanishEnabled = false;
        public static bool toSwedishEnabled = false;
        public static bool toAllEnabled = false;
        public static int ClientSize;

        public void LoadDictionary()
        {
        }
        public void register(string EmailId, string Password)
        {
            XDocument doc = XDocument.Load(path+"Users.xml");
            XElement person = new XElement("Person");
            person.Add(new XElement("EmailId", EmailId));
            person.Add(new XElement("Password", Convert.ToBase64String(Encoding.UTF8.GetBytes(Password))));
            person.Add(new XElement("Privilege", "User"));
            doc.Element("Persons").Add(person);
            doc.Save(@"c:\temp\Users.xml");
            log("EmailId: " + EmailId + " registered at: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff"));
        }
        public bool login(string EmailId, string Password)
        {
            XDocument doc = XDocument.Load(path+"Users.xml");
            foreach (XElement Person in doc.Descendants("Person"))
            {
                if (string.Compare(Person.Element("EmailId").Value, EmailId, true) == 0 && Encoding.UTF8.GetString(Convert.FromBase64String(Person.Element("Password").Value)) == Password)
                {
                    log("Email Id: " + EmailId + " logged in at: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff"));
                    return true;
                    
                }
            }
            return false;
        }
        public void log(string text)
        {
            string strHostName = "";
            strHostName = Dns.GetHostName();

            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);

            IPAddress[] addr = ipEntry.AddressList;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(text+" IP Address: " + addr[addr.Length - 1].ToString());
            // flush every 20 seconds as you do it
            File.AppendAllText(path + "log.txt", sb.ToString());
            sb.Clear();
        }
        public bool verifyEmailId(string email)
        {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
        }
        public string getPrivilege(string EmailId)
        {
            XDocument doc = XDocument.Load(path +"Users.xml");
            foreach (XElement Person in doc.Descendants("Person"))
            {
                if (Person.Element("EmailId").Value == EmailId)
                {
                    return Person.Element("Privilege").Value;
                }
            }
            return "User";
        }
    }
}