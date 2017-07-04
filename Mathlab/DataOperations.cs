using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.XPath;

namespace WindowsFormsApp1
{
    class DataOperations
    {
        public static string path = "../../../Database/";
        public static bool ApplicationJustGotStarted = true;
        public static string FromLanguage = "Japanese";
        public static string EmailId;
        public static TextBox[] txt = new TextBox[9];
        public static Label[] lbl = new Label[9];
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
        public static bool Autodetect = true;
        private XDocument UsersDoc = XDocument.Load(path + "Users.xml");
        
       public class TargetVal
        {
           public string srcLan;
           public string trgLan;
        }

        public static Dictionary<string,List<TargetVal>> dictionary = new Dictionary<string, List<TargetVal>>()
        {
            { "Arabic", new List<TargetVal>() }, 
            { "English", new List<TargetVal>() },
            { "German", new List<TargetVal>() },
            { "Italian", new List<TargetVal>() },
            { "Japanese", new List<TargetVal>() },
            { "Korean", new List<TargetVal>() },
            { "Norwegian", new List<TargetVal>() },
            { "Spanish", new List<TargetVal>() },
            { "Swedish", new List<TargetVal>() },
        };



        public bool register(string Email, string Password, string Privilege)
        {
            if (!alreadyRegistered(Email))
            {
                int count = UsersDoc.XPathSelectElements("//Person").Count();
                UsersDoc.Descendants("Persons").FirstOrDefault().Add(new XElement("Person",
                        new XElement("Id", count),
                        new XElement("EmailId", Email),
                       new XElement("Password", Convert.ToBase64String(Encoding.UTF8.GetBytes(Password))),
                       new XElement("Privilege", Privilege)
                        ));
                
                UsersDoc.Save(path + "Users.xml");
                log("New user with Email Id " + EmailId + " is registered");

                return true;
            }
            else
            {
                return false;
            }
        }
        public bool alreadyRegistered(string EmailId)
        {
            
            foreach (XElement Person in UsersDoc.Descendants("Person"))
            {
                if (string.Compare(Person.Element("EmailId").Value, EmailId, true) == 0)
                {
                    return true;
                }
            }
            return false;
        }
        public bool login(string EmailId, string Password)
        {
            
            foreach (XElement Person in UsersDoc.Descendants("Person"))
            {
                if (string.Compare(Person.Element("EmailId").Value, EmailId, true) == 0 && Encoding.UTF8.GetString(Convert.FromBase64String(Person.Element("Password").Value)) == Password)
                {
                    log("Email Id: " + EmailId + " logged in");
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
            sb.AppendLine(text +" at: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff")+ " from IP Address: " + addr[addr.Length - 1].ToString());
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
                if (string.Compare(Person.Element("EmailId").Value, EmailId, true) == 0)
                {
                    return Person.Element("Privilege").Value;
                }
            }
            return "User";
        }
    }
}