using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Configuration;

namespace TestScrape
{
    class Program
    {
        static void Main(string[] args)
        {
            string base_url = "http://download.finance.yahoo.com/d/quotes.csv?s={0}&f={1}";

            string sTickers = "";
            FileStream fs = File.OpenRead("..\\..\\TickerList.txt");
            StreamReader sr = new StreamReader(fs);
            while (!sr.EndOfStream)
            {
                sTickers += sr.ReadLine() + "+";
            }
            sr.Close();
            sTickers = sTickers.Remove(sTickers.Length-1, 1);

            fs = File.OpenRead("..\\..\\YQLMap.txt");
            sr = new StreamReader(fs);
            string sFormat = "";
            string sHeader = "";
            Dictionary<string, string> dicYQL = new Dictionary<string, string>();
            while(!sr.EndOfStream)
            {
                string[] pair = sr.ReadLine().Split(':');
                dicYQL.Add(pair[0], pair[1]);
                sFormat += pair[0];
                sHeader += pair[1] + ",";
            }
            sr.Close();
            sHeader += "RequestTime";
            string url = string.Format(base_url, sTickers, sFormat);
            StringBuilder sData = GetCSV(url);
            CreateOutputFile(sHeader, sData);
            
        }

        public static void CreateOutputFile(string sHeader, StringBuilder sData)
        {
            string sOutput = ConfigurationManager.AppSettings["output"];
            string sDir = string.Format(sOutput, DateTime.Today.Date.ToString("yyyyMMdd"), "");
            string sFileName = string.Format(sOutput, DateTime.Today.Date.ToString("yyyyMMdd"), "AllTickers.csv");
            if (!Directory.Exists(sDir))
            {
                Directory.CreateDirectory(sDir);
            }
            StreamWriter sw = new StreamWriter(sFileName);
            sw.WriteLine(sHeader);
            sw.WriteLine(sData);
            sw.Flush();
            sw.Close();
        }

        public static void CreateNamedOutputFile(string ticker)
        {
            string sOutput = ConfigurationManager.AppSettings["output"];
            sOutput = string.Format(sOutput, DateTime.Today.Date, ticker);
            if(!Directory.Exists(sOutput))
            {
                Directory.CreateDirectory(sOutput);
            }
        }

        public static void CreateFileHeader(string sHeader)
        {
            string sOutput = ConfigurationManager.AppSettings["output"];
            
            
            StreamWriter sw = new StreamWriter(sOutput);
            sw.WriteLine(sHeader);
            sw.Flush();
            sw.Close();
        }
        public static StringBuilder GetCSV(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            StringBuilder sbResult = new StringBuilder(string.Empty);
            StreamReader sr = new StreamReader(resp.GetResponseStream());
            while (!sr.EndOfStream)
            {
                sbResult.AppendLine(sr.ReadLine() + "," + DateTime.Now);
            }
            //string results = sr.ReadToEnd();
            sr.Close();
            return sbResult;
        } 
    }
}
