using AccessConnections_cs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace WindowsFormsApp1
{
    class DataOperations
    {
        private string fileName = "";
        public DataOperations()
        {
            fileName = "..\\..\\..\\Databases\\ConversionDatabase.accdb";
            ConversionDataTable = new DataTable();
            
        }
        public DataOperations(string databaseFileName)
        {
            fileName = databaseFileName;
            ConversionDataTable = new DataTable();
            
        }
        public DataTable ConversionDataTable { get; set; }
        public DataTable EnglishDataTable { get; set; }
        public void ExecuteQuery(string query)
        {
            using (OleDbConnection cn = new OleDbConnection { ConnectionString = fileName.BuildConnectionString() })
            {
                using (OleDbCommand cmd = new OleDbCommand { Connection = cn })
                {
                    cmd.CommandText = query;
                    DataTable dt = new DataTable { TableName = "Conversions" };
                    cn.Open();
                    ConversionDataTable.Load(cmd.ExecuteReader());
                }
            }
        }
        public static Dictionary<string, string> Dictionary = new Dictionary<string, string>();

        public void LoadDictionary()
        {
            //Dictionary = File.ReadLines(@"c:\temp\file.csv", Encoding.GetEncoding(932))
            //    .Select(line => line.Split(',')).ToDictionary(data => data[0], data => data[1]);

            Dictionary = XDocument.Load(@"c:\temp\file.xml").Descendants("row")
                             .ToDictionary(p => (string)p.Element("JP").Value,
                                           p => (string)p.Element("EN").Value);

            //foreach (var node in XDocument.Load(@"c:\temp\file.xml").Descendants("Row"))
            //{
            //    if (node.Name.LocalName.Equals("row", StringComparison.CurrentCultureIgnoreCase))
            //    {
            //        Dictionary.Add(node.Element("JP").Value, node.Element("EN").Value);
            //    }
            //    else
            //    {
            //        Dictionary.Add(node.Name.LocalName, node.Value);
            //    }
            //}

            //using (OleDbConnection cn = new OleDbConnection { ConnectionString = fileName.BuildConnectionString() })
            //{
            //    using (OleDbCommand cmd = new OleDbCommand { Connection = cn })
            //    {
            //        OleDbDataReader reader = cmd.ExecuteReader();
            //        while (reader.Read())
            //        {
            //            Dictionary.Add(reader.GetString(0), reader.GetString(1));
            //        }

            //    }
            //}

        }
        public void saveToDictionary(string Japanese,string English)
        {
            XDocument doc = XDocument.Load(@"c:\temp\file.xml");
            XElement row = new XElement("Row");
            row.Add(new XElement(Japanese));
            row.Add(new XElement(English));
            doc.Element("Root").Add(row);
            doc.Save(@"c:\temp\file.xml");
        }


    }
}
