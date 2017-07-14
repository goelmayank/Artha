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
    /// <summary>
    /// Data Opearations class
    /// </summary>
    class DataOperations
    {
        /// <summary>
        /// The path
        /// </summary>
        public static string path = "../../../Database/";
        /// <summary>
        /// The application just got started
        /// </summary>
        public static bool ApplicationJustGotStarted = true;
        /// <summary>
        /// From language
        /// </summary>
        public static string FromLanguage = "Japanese";
        /// <summary>
        /// The email identifier
        /// </summary>
        public static string EmailId;
        /// <summary>
        /// The text
        /// </summary>
        public static TextBox[] txt = new TextBox[9];
        /// <summary>
        /// The label
        /// </summary>
        public static Label[] lbl = new Label[9];
        /// <summary>
        /// To english enabled
        /// </summary>
        public static bool toEnglishEnabled = true;
        /// <summary>
        /// To arabic enabled
        /// </summary>
        public static bool toArabicEnabled = false;
        /// <summary>
        /// To german enabled
        /// </summary>
        public static bool toGermanEnabled = false;
        /// <summary>
        /// To italian enabled
        /// </summary>
        public static bool toItalianEnabled = false;
        /// <summary>
        /// To japanese enabled
        /// </summary>
        public static bool toJapaneseEnabled = false;
        /// <summary>
        /// To korean enabled
        /// </summary>
        public static bool toKoreanEnabled = false;
        /// <summary>
        /// To norwegian enabled
        /// </summary>
        public static bool toNorwegianEnabled = false;
        /// <summary>
        /// To spanish enabled
        /// </summary>
        public static bool toSpanishEnabled = false;
        /// <summary>
        /// To swedish enabled
        /// </summary>
        public static bool toSwedishEnabled = false;
        /// <summary>
        /// To all enabled
        /// </summary>
        public static bool toAllEnabled = false;
        /// <summary>
        /// The client size
        /// </summary>
        public static int ClientSize;
        /// <summary>
        /// The autodetect
        /// </summary>
        public static bool Autodetect = true;
        /// <summary>
        /// The users document
        /// </summary>
        private XDocument UsersDoc = XDocument.Load(path + "Users.xml");

        /// <summary>
        /// Custom Class for storing source and Language key value pairs
        /// </summary>
        public class TargetVal
        {
            /// <summary>
            /// The source lan
            /// </summary>
            public string srcLan;
            /// <summary>
            /// The TRG lan
            /// </summary>
            public string trgLan;
        }

        /// <summary>
        /// The dictionary
        /// </summary>
        public static Dictionary<string, List<TargetVal>> dictionary = new Dictionary<string, List<TargetVal>>()
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

        /// <summary>
        /// Registers the specified email.
        /// </summary>
        /// <param name="Email">The email.</param>
        /// <param name="Password">The password.</param>
        /// <param name="Privilege">The privilege.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Alreadies the registered.
        /// </summary>
        /// <param name="EmailId">The email identifier.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Logins the specified email identifier.
        /// </summary>
        /// <param name="EmailId">The email identifier.</param>
        /// <param name="Password">The password.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Logs the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        public void log(string text)
        {
            string strHostName = "";
            strHostName = Dns.GetHostName();

            IPHostEntry ipEntry = Dns.GetHostEntry(strHostName);

            IPAddress[] addr = ipEntry.AddressList;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(text + " at: " + DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + " from IP Address: " + addr[addr.Length - 1].ToString());
            // flush every 20 seconds as you do it
            File.AppendAllText(path + "log.txt", sb.ToString());
            sb.Clear();
        }

        /// <summary>
        /// Verifies the email identifier.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns></returns>
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
        /// <summary>
        /// Gets the privilege.
        /// </summary>
        /// <param name="EmailId">The email identifier.</param>
        /// <returns></returns>
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