using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace YHScrape.Models
{
    [Table("CompanyData")]
    public class CompanyData
    {
        public CompanyData()
        {
            this.CompanyDatas = new HashSet<CompanyStatisticsData>();
            this.DailyQuotes = new HashSet<DailyQuote>();
        }
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string CompanyName { get; set; }
        [StringLength(10)]
        public string Ticker { get; set; }
        public virtual ICollection<CompanyStatisticsData> CompanyDatas { get; set; }
        public virtual ICollection<DailyQuote> DailyQuotes { get; set; }
    }

    

}
