using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace YHScrape
{
    public class DownloadStatisticsResult
    {
        public CompanyStatisticsDownloadSettings Settings { get; set; }

        public DownloadStatisticsResult(string symbol)
        {
            Settings = new CompanyStatisticsDownloadSettings(symbol);
        }
    }
    public class CompanyStatisticsDownloadSettings //: Base.SettingsBase
    {
        public string ID { get; set; }

        public CompanyStatisticsDownloadSettings()
        {
            this.ID = string.Empty;
        }
        public CompanyStatisticsDownloadSettings(string id)
        {
            this.ID = id;
        }


        protected string GetUrl()
        {
            if (this.ID == string.Empty) { throw new ArgumentException("ID is empty.", "ID"); }
            return string.Format("http://finance.yahoo.com/q/ks?s={0}", this.ID);
        }

        //public override object Clone()
        //{
        //    return new CompanyStatisticsDownloadSettings(this.ID);
        //}
    }
}
