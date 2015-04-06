using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Configuration;
using System.Collections.Specialized;

namespace TestScrape
{
    class Program
    {
        static string logFile = System.IO.Directory.GetCurrentDirectory() + "\\" + ConfigurationManager.AppSettings["logFile"];
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());
            
            string tickerFile = ConfigurationManager.AppSettings["tickerFile"];
            string yqlMapFile = ConfigurationManager.AppSettings["yqlMapFile"];
            string outputFile = ConfigurationManager.AppSettings["outputFile"];
            YHScrape.Engines.YahooEngine yahooEngine = new YHScrape.Engines.YahooEngine();
            //yahooEngine.SaveDailyQuotesToCSV(tickerFile, yqlMapFile, outputFile);
            yahooEngine.SaveKeyStatsToCSV(tickerFile, outputFile);
            try
            {
                using (var ctx = new YHScrape.Entities.YahooFinanceContext())
                {
                    var stat = new YHScrape.Models.CompanyData();
                    stat.CompanyName = "Google Inc";
                    stat.Ticker = "GOOG";
                    //var quote = new YHScrape.Models.YahooDailyQuote();
                    //quote.CompanyDataId = 1;
                    //stat.Item = new YHScrape.Models.CompanyStatisticsData();
                    
                    ctx.YahooCompanyDatas.Add(stat);
                    //ctx.Quotes.Add(quote);
                    ctx.SaveChanges();                    
                }
            }
            catch (Exception e) { WriteLog(e.Message); WriteLog(e.StackTrace); }
        }

        public static void WriteLog(string message)
        {
            FileStream fs = new FileStream(logFile, FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(DateTime.Now + "\t" + message);
            sw.Flush();
            sw.Close();
        }
    }
}
