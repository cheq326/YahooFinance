using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace YHScrape.Models
{
    [Table("DailyQuote")]
    public class DailyQuote
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("CompanyData")]
        public int CompanyDataId { get; set; }
        [StringLength(10)]
        public string Symbol { get; set; }
        public decimal? Ask { get; set; }
        public decimal? Average_Daily_Volume { get; set; }
        public decimal? Ask_Size { get; set; }
        public decimal? Bid { get; set; }
        public decimal? Ask_Realtime { get; set; }
        public decimal? Bid_Realtime { get; set; }
        public decimal Book_Value { get; set; }
        public decimal? Bid_Size { get; set; }
        public decimal? Change_And_Percent_Change { get; set; }
        public decimal? Change { get; set; }
        [StringLength(100)]
        public string Commission { get; set; }
        [StringLength(100)]
        public string Change_Realtime { get; set; }
        [StringLength(100)]
        public string After_Hours_Change_Realtime { get; set; }
        public decimal? DividendPerShare { get; set; }
        public DateTime? Last_Trade_Date { get; set; }
        [StringLength(100)]
        public string Trade_Date { get; set; }
        public decimal? EarningsPerShare { get; set; }
        [StringLength(100)]
        public string Error_Indication_returned_for_symbol_changed_invalid { get; set; }
        public decimal? EPS_Estimate_Current_Year { get; set; }
        public decimal? EPS_Estimate_Next_Year { get; set; }
        public decimal? EPS_Estimate_Next_Quarter { get; set; }
        public decimal? Float_Shares { get; set; }
        public decimal? Day_Low { get; set; }
        public decimal? Day_High { get; set; }
        public decimal? FiftyTwoWeek_Low { get; set; }
        public decimal? FiftyTwoWeek_High { get; set; }
        [StringLength(100)]
        public string Holdings_Gain_Percent { get; set; }
        [StringLength(100)]
        public string Annualized_Gain { get; set; }
        [StringLength(100)]
        public string Holdings_Gain { get; set; }
        [StringLength(100)]
        public string Holdings_Gain_Percent_Realtime { get; set; }
        [StringLength(100)]
        public string Holdings_Gain_Realtime { get; set; }
        [StringLength(100)]
        public string More_Info { get; set; }
        [StringLength(100)]
        public string Order_Book_Realtime { get; set; }
        [StringLength(100)]
        public string Market_Capitalization { get; set; }
        [StringLength(100)]
        public string Market_Cap_Realtime { get; set; }
        [StringLength(100)]
        public string EBITDA { get; set; }
        public decimal? Change_From_FiftyTwoWeek_Low { get; set; }
        [StringLength(100)]
        public string Percent_Change_From_FiftyTwoWeek_Low { get; set; }
        [StringLength(100)]
        public string Last_Trade_Realtime_With_Time { get; set; }
        [StringLength(100)]
        public string Change_Percent_Realtime { get; set; }
        [StringLength(100)]
        public string Last_Trade_Size { get; set; }
        public decimal? Change_From_FiftyTwoWeek_High { get; set; }
        [StringLength(100)]
        public string Percent_Change_From_FiftyTwoWeek_High { get; set; }
        [StringLength(100)]
        public string Last_Trade_With_Time { get; set; }
        public decimal? Last_Trade_Price_Only { get; set; }
        [StringLength(100)]
        public string High_Limit { get; set; }
        [StringLength(100)]
        public string Low_Limit { get; set; }
        [StringLength(100)]
        public string Day_Range { get; set; }
        [StringLength(100)]
        public string Day_Range_Realtime { get; set; }
        public decimal? FiftyDay_Moving_Average { get; set; }
        public decimal? TwoHundredDay_Moving_Average { get; set; }
        public decimal? Change_From_TwoHundredDay_Moving_Average { get; set; }
        [StringLength(100)]
        public string Percent_Change_From_TwoHundredDay_Moving_Average { get; set; }
        public decimal? Change_From_FiftyDay_Moving_Average { get; set; }
        [StringLength(100)]
        public string Percent_Change_From_FiftyDay_Moving_Average { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Notes { get; set; }
        public decimal? Open { get; set; }
        public decimal? Previous_Close { get; set; }
        public decimal? Price_Paid { get; set; }
        [StringLength(100)]
        public string Change_in_Percent { get; set; }
        public decimal? Price_Sales { get; set; }
        public decimal? Price_Book { get; set; }
        public DateTime? Ex_Dividend_Date { get; set; }
        public decimal? PE_Ratio { get; set; }
        public DateTime? Dividend_Pay_Date { get; set; }
        [StringLength(100)]
        public string PE_Ratio_Realtime { get; set; }
        public decimal? PEG_Ratio { get; set; }
        public decimal? Price_EPS_Estimate_Current_Year { get; set; }
        public decimal? Price_EPS_Estimate_Next_Year { get; set; }
        [StringLength(100)]
        public string Shares_Owned { get; set; }
        public decimal? Short_Ratio { get; set; }
        [StringLength(100)]
        public string Last_Trade_Time { get; set; }
        [StringLength(100)]
        public string Trade_Links { get; set; }
        [StringLength(100)]
        public string Ticker_Trend { get; set; }
        public decimal? One_yr_Target_Price { get; set; }
        public decimal? Volume { get; set; }
        [StringLength(100)]
        public string Holdings_Value { get; set; }
        [StringLength(100)]
        public string Holdings_Value_Realtime { get; set; }
        [StringLength(100)]
        public string FiftyTwoWeek_Range { get; set; }
        [StringLength(100)]
        public string Day_Value_Change { get; set; }
        [StringLength(100)]
        public string Day_Value_Change_Realtime { get; set; }
        [StringLength(100)]
        public string Stock_Exchange { get; set; }
        public decimal? Dividend_Yield { get; set; }
        public DateTime? RequestTime { get; set; }

        public virtual CompanyData CompanyData { get; set; }
    }
}