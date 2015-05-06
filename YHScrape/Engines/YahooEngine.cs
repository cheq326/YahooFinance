using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using YHScrape.Models;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Xml;
using HtmlAgilityPack;
using System.Web;

namespace YHScrape.Engines
{
    public class YahooEngine
    {
        private const string BASE_URL_QUOTE = "http://download.finance.yahoo.com/d/quotes.csv?s={0}&f={1}";
        private const string BASE_URL_KEYSTATS = "http://finance.yahoo.com/q/ks?s={0}";
            //"http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20({0})&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

        /// <summary>
        /// Get Yahoo daily quotes from the specified url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string FetchDailyQuotes(string url)
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
            return sbResult.ToString();
        }
        /// <summary>
        /// Get tickers from the provided file location
        /// </summary>
        /// <param name="tickerListFileName">file path</param>
        /// <returns></returns>
        private string GetTickerListFromFile(string tickerListFileName)
        {
            string sTickers = "";
            FileStream fs = null;
            StreamReader sr = null;
            try
            {
                if (File.Exists(tickerListFileName))
                {
                    fs = File.OpenRead(tickerListFileName);
                    sr = new StreamReader(fs);
                }
                else// check current path
                {
                    string dir = Directory.GetCurrentDirectory();
                    fs = File.OpenRead(dir + "\\TickerList.txt");
                    sr = new StreamReader(fs);
                }
                if (sr != null)
                {
                    while (!sr.EndOfStream)
                    {
                        sTickers += sr.ReadLine() + "+";
                    }
                    sr.Close();
                    sTickers = sTickers.Remove(sTickers.Length - 1, 1);
                }
               
            }
            catch (Exception e) { }
            return sTickers;
        }
        private string GetTickerListFromDB()
        {
            string sTickers = "";

            return sTickers;
        }
        private OrderedDictionary LoadYQLMapToDictionary(string YQLMapFile)
        {
            FileStream fs = null;
            StreamReader sr = null;
            OrderedDictionary dicYQL = new OrderedDictionary();
            try
            {
                if (File.Exists(YQLMapFile))
                {
                    fs = File.OpenRead(YQLMapFile);
                    sr = new StreamReader(fs);
                }
                else// check current path
                {
                    string dir = Directory.GetCurrentDirectory();
                    fs = File.OpenRead(dir + "\\YQLMap.txt");
                    sr = new StreamReader(fs);
                }
                if (sr != null)
                {
                    //string sFormat = "";
                    //string sHeader = "";
                    
                    while (!sr.EndOfStream)
                    {
                        string[] pair = sr.ReadLine().Split(':');
                        dicYQL.Add(pair[0], pair[1]);
                        //sFormat += pair[0];
                        //sHeader += pair[1] + ",";
                    }
                    sr.Close();
                    //sHeader += "RequestTime";
                    //string url = string.Format(BASE_URL_QUOTE, GetTickerListFromFile(tickerListFile), sFormat);
                    //StringBuilder sData = FetchDailyQuotes(url);
                    //CreateOutputFile(outputFileName, sHeader, sData);
                }
            }
            catch (Exception e) { }

            return dicYQL;
        }

        /// <summary>
        /// Save Yahoo Daily Quote to a CSV file
        /// </summary>
        /// <param name="tickerListFile">location of tickerlist file</param>
        /// <param name="YQLMapFile">location of YQLMap file</param>
        /// <param name="outputFileName">location of output file</param>
        public void SaveDailyQuotesToCSV(string tickerListFile, string YQLMapFile, string outputFileName)
        {
            string sFormat = "";
            string sHeader = "";
            OrderedDictionary dicYQL = LoadYQLMapToDictionary(YQLMapFile);
            string[] dicKeys = new string[dicYQL.Count];
            string[] dicValues = new string[dicYQL.Count];
            dicYQL.Keys.CopyTo(dicKeys, 0);
            dicYQL.Values.CopyTo(dicValues, 0);
            try
            {
                for (int i = 0; i < dicYQL.Count; i++)
                {
                    sFormat += dicKeys[i];
                    sHeader += dicValues[i] + ",";                    
                }
                sHeader += "RequestTime";
                string url = string.Format(BASE_URL_QUOTE, GetTickerListFromFile(tickerListFile), sFormat);
                string sData = FetchDailyQuotes(url);
                string sFileName = string.Format(outputFileName, DateTime.Today.Date.ToString("yyyyMMdd"), "AllTickers.csv");
                CreateOutputFile(sFileName, sHeader, sData);
            }
            catch (Exception e) { }
            
        }
        private void CreateOutputFile(string outputFile, string sHeader, string sData)
        {
            string sDir = Path.GetDirectoryName(outputFile);
            
            if (!Directory.Exists(sDir))
            {
                Directory.CreateDirectory(sDir);
            }
            StreamWriter sw = new StreamWriter(outputFile);
            sw.WriteLine(sHeader);
            sw.WriteLine(sData);
            sw.Flush();
            sw.Close();
        }

        //private static void Parse(ObservableCollection<DailyQuote> quotes, XDocument doc)
        //{
        //    XElement results = doc.Root.Element("results");

        //    foreach (DailyQuote quote in quotes)
        //    {
        //        XElement q = results.Elements("quote").First(w => w.Attribute("symbol").Value == quote.Symbol);

        //        quote.Ask = GetDecimal(q.Element("Ask").Value);
        //        quote.Bid = GetDecimal(q.Element("Bid").Value);
        //        quote.AverageDailyVolume = GetDecimal(q.Element("AverageDailyVolume").Value);
        //        quote.BookValue = GetDecimal(q.Element("BookValue").Value);
        //        //quote.ChangePercent = GetDecimal(q.Element("Change").Value);
        //        quote.Change = GetDecimal(q.Element("Change").Value);
        //        quote.Currency = q.Element("Currency").Value;
        //        quote.DividendShare = GetDecimal(q.Element("DividendShare").Value);
        //        quote.LastTradeDate = GetDateTime(q.Element("LastTradeDate") + " " + q.Element("LastTradeTime").Value);
        //        quote.EarningsShare = GetDecimal(q.Element("EarningsShare").Value);
        //        quote.EpsEstimateCurrentYear = GetDecimal(q.Element("EPSEstimateCurrentYear").Value);
        //        quote.EpsEstimateNextYear = GetDecimal(q.Element("EPSEstimateNextYear").Value);
        //        quote.EpsEstimateNextQuarter = GetDecimal(q.Element("EPSEstimateNextQuarter").Value);
        //        quote.DailyLow = GetDecimal(q.Element("DaysLow").Value);
        //        quote.DailyHigh = GetDecimal(q.Element("DaysHigh").Value);
        //        quote.YearlyLow = GetDecimal(q.Element("YearLow").Value);
        //        quote.YearlyHigh = GetDecimal(q.Element("YearHigh").Value);
        //        quote.MarketCapitalization = GetDecimal(q.Element("MarketCapitalization").Value);
        //        quote.Ebitda = GetDecimal(q.Element("EBITDA").Value);
        //        quote.ChangeFromYearLow = GetDecimal(q.Element("ChangeFromYearLow").Value);
        //        quote.PercentChangeFromYearLow = GetDecimal(q.Element("PercentChangeFromYearLow").Value);
        //        quote.ChangeFromYearHigh = GetDecimal(q.Element("ChangeFromYearHigh").Value);
        //        quote.LastTradePrice = GetDecimal(q.Element("LastTradePriceOnly").Value);
        //        quote.PercentChangeFromYearHigh = GetDecimal(q.Element("PercebtChangeFromYearHigh").Value); //missspelling in yahoo for field name
        //        quote.FiftyDayMovingAverage = GetDecimal(q.Element("FiftydayMovingAverage").Value);
        //        quote.TwoHunderedDayMovingAverage = GetDecimal(q.Element("TwoHundreddayMovingAverage").Value);
        //        quote.ChangeFromTwoHundredDayMovingAverage = GetDecimal(q.Element("ChangeFromTwoHundreddayMovingAverage").Value);
        //        quote.PercentChangeFromTwoHundredDayMovingAverage = GetDecimal(q.Element("PercentChangeFromTwoHundreddayMovingAverage").Value);
        //        quote.PercentChangeFromFiftyDayMovingAverage = GetDecimal(q.Element("PercentChangeFromFiftydayMovingAverage").Value);
        //        quote.Name = q.Element("Name").Value;
        //        quote.Open = GetDecimal(q.Element("Open").Value);
        //        quote.PreviousClose = GetDecimal(q.Element("PreviousClose").Value);
        //        quote.ChangeInPercent = GetDecimal(q.Element("ChangeinPercent").Value);
        //        quote.PriceSales = GetDecimal(q.Element("PriceSales").Value);
        //        quote.PriceBook = GetDecimal(q.Element("PriceBook").Value);
        //        quote.ExDividendDate = GetDateTime(q.Element("ExDividendDate").Value);
        //        quote.PeRatio = GetDecimal(q.Element("PERatio").Value);
        //        quote.DividendPayDate = GetDateTime(q.Element("DividendPayDate").Value);
        //        quote.PegRatio = GetDecimal(q.Element("PEGRatio").Value);
        //        quote.PriceEpsEstimateCurrentYear = GetDecimal(q.Element("PriceEPSEstimateCurrentYear").Value);
        //        quote.PriceEpsEstimateNextYear = GetDecimal(q.Element("PriceEPSEstimateNextYear").Value);
        //        quote.ShortRatio = GetDecimal(q.Element("ShortRatio").Value);
        //        quote.OneYearPriceTarget = GetDecimal(q.Element("OneyrTargetPrice").Value);
        //        quote.Volume = GetDecimal(q.Element("Volume").Value);
        //        quote.StockExchange = q.Element("StockExchange").Value;

        //        quote.LastUpdate = DateTime.Now;
        //    }
        //}

        private static decimal? GetDecimal(string input)
        {
            if (input == null) return null;

            input = input.Replace("%", "");

            decimal value;

            if (Decimal.TryParse(input, out value)) return value;
            return null;
        }

        private static DateTime? GetDateTime(string input)
        {
            if (input == null) return null;

            DateTime value;

            if (DateTime.TryParse(input, out value)) return value;
            return null;
        }

        /// <summary>
        /// Get Yahoo key stats for the specified ticker
        /// </summary>
        /// <param name="ticker"></param>
        /// <returns></returns>
        private HtmlDocument FetchKeyStats(string ticker)
        {
            string url = string.Format(BASE_URL_KEYSTATS, ticker);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();            
            StreamReader sr = new StreamReader(resp.GetResponseStream());
            XmlDocument doc = new XmlDocument();
            string result = sr.ReadToEnd();
            HtmlDocument htmlData = new HtmlDocument();
            //resultat.LoadHtml(source);
            htmlData.LoadHtml(HttpUtility.HtmlDecode(result));

            return htmlData;
        }
        /// <summary>
        /// Save Yahoo Key Stats to a CSV file
        /// </summary>
        /// <param name="tickerListFile">location of tickerlist file</param>
        /// <param name="outputFileName">location of output file</param>
        public void SaveKeyStatsToCSV(string tickerListFile, string outputFileName)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            try
            {
                string[] tickers = GetTickerListFromFile(tickerListFile).Split('+');
                foreach (string ticker in tickers)
                {
                    htmlDoc = FetchKeyStats(ticker);
                    //foreach (var error in htmlDoc.ParseErrors)
                    //{
                    //    Console.WriteLine(error.Line);
                    //    Console.WriteLine(error.Reason);
                    //}

                    var query = //from table in htmlData.DocumentNode.SelectNodes("//table[contains(@class,'datamodoutline')]").Cast<HtmlNode>()
                                from row in htmlDoc.DocumentNode.SelectNodes(".//tr[td[contains(@class,'yfnc_tablehead')]]|.//tr[td[contains(@class,'yfnc_tabledata')]]").Cast<HtmlNode>()
                                //from cellData in table.SelectNodes(".//td[contains(@class,'yfnc_tabledata')]").Cast<HtmlNode>()
                                //select row; 
                                select new { Head = row.FirstChild.InnerText, Data = row.LastChild.InnerText };
                    
                    string sHead = "";
                    string sData = "";
                    int iCounter = 0;
                    foreach (var row in query)
                    {
                        sHead += Helper.RemoveParenthesesContend(row.Head) + ",";
                        sData += row.Data.Replace(',', ' ') + ",";
                        iCounter++;
                    }
                    sHead += "RequestTime";
                    sData += DateTime.Now;
                    string sFileName = string.Format(outputFileName, DateTime.Today.Date.ToString("yyyyMMdd"), ticker + ".csv");
                    CreateOutputFile(sFileName, sHead, sData);
                }
            }
            catch (Exception e) { }
        }

        /// <summary>
        /// Save Yahoo Key Stats to DB
        /// </summary>
        /// <param name="tickerListFile">location of tickerlist file</param>
        public void SaveKeyStatsToDB(string tickerListFile)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            try
            {
                string[] tickers = GetTickerListFromFile(tickerListFile).Split('+');
                
                using (var ctx = new YHScrape.Entities.YahooFinanceContext())
                {
                    foreach (string ticker in tickers)
                    {
                        htmlDoc = FetchKeyStats(ticker);
                        var query = //from table in htmlData.DocumentNode.SelectNodes("//table[contains(@class,'datamodoutline')]").Cast<HtmlNode>()
                                    from row in htmlDoc.DocumentNode.SelectNodes(".//tr[td[contains(@class,'yfnc_tablehead')]]|.//tr[td[contains(@class,'yfnc_tabledata')]]").Cast<HtmlNode>()
                                    //from cellData in table.SelectNodes(".//td[contains(@class,'yfnc_tabledata')]").Cast<HtmlNode>()
                                    //select row; 
                                    select new { Head = row.FirstChild.InnerText, Data = row.LastChild.InnerText };
                        int iCount = 0;
                        List<decimal?> valMeasureData = new List<decimal?>();
                        List<decimal?> financeHL = new List<decimal?>();
                        List<decimal?> tradeInfo = new List<decimal?>();
                        DateTime? fiscalYearEnds = null;
                        DateTime? recentQtr = null;
                        DateTime? dividendDate = null;
                        DateTime? exDividendDate = null;
                        DateTime? lastSplitDate = null;
                        string lastSplitFactor = "";
                        foreach (var row in query)
                        {
                            if (iCount <= 8)
                            {
                                valMeasureData.Add(Helper.ParseToDecimalValue(row.Data));
                            }
                            else if (iCount == 9)
                            {
                                fiscalYearEnds = Helper.ParseToDateTime(row.Data);
                            }
                            else if (iCount == 10)
                            {
                                recentQtr = Helper.ParseToDateTime(row.Data);
                            }
                            else if (iCount > 10 && iCount <= 30)
                            {
                                financeHL.Add(Helper.ParseToDecimalValue(row.Data));
                            }
                            else if (iCount > 30 && iCount <= 53)
                            {
                                tradeInfo.Add(Helper.ParseToDecimalValue(row.Data));
                            }
                            else if (iCount == 54)
                            {
                                dividendDate = Helper.ParseToDateTime(row.Data);
                            }
                            else if (iCount == 55)
                            {
                                exDividendDate = Helper.ParseToDateTime(row.Data);
                            }
                            else if (iCount == 56)
                            {
                                lastSplitFactor = row.Data;
                            }
                            else if (iCount == 57)
                            {
                                lastSplitDate = Helper.ParseToDateTime(row.Data);
                            }
                            iCount++;
                        }
                        
                        CompanyData comData = new CompanyData();
                        CompanyStatisticsData statData = new CompanyStatisticsData();
                        statData.CollectionDate = DateTime.Now;
                        statData.CompanyValuationMeasures = new CompanyValuationMeasures(valMeasureData);
                        statData.CompanyFinancialHighlights = new CompanyFinancialHighlights(fiscalYearEnds, recentQtr, financeHL);
                        statData.CompanyTradingInfo = new CompanyTradingInfo(tradeInfo, dividendDate, exDividendDate, lastSplitDate, lastSplitFactor);
                        var comInfo = ctx.YahooCompanyDatas.Where(c => c.Ticker == ticker).SingleOrDefault();
                        if (comInfo == null)
                        {
                            comData.Ticker = ticker;
                            comData.CompanyName = "";
                            ctx.YahooCompanyDatas.Add(comData);
                            comData.CompanyStatDatas.Add(statData);
                            ctx.YahooCompanyDatas.Add(comData);
                        }
                        else
                        {
                            comInfo.CompanyStatDatas = comData.CompanyStatDatas;
                        }
                        
                    }
                    
                    ctx.SaveChanges();
                }
            }
            catch (Exception e) { }
        }

        
    }
}