using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YHScrape.Models
{
    /// <summary>
    /// CompanyStatisticsData is a conatiner class for several statistics of a single company.
    /// </summary>
    /// <remarks></remarks>
    [Table("CompanyStatisticsData")]
    public class CompanyStatisticsData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("CompanyData")]
        public int CompanyDataId { get; set; }
        public DateTime CollectionDate { get; set; }

        public virtual CompanyData CompanyData { get; set; }
        public virtual CompanyValuationMeasures CompanyValuationMeasures { get; set; }
        public virtual CompanyFinancialHighlights CompanyFinancialHighlights { get; set; }
        public virtual CompanyTradingInfo CompanyTradingInfo { get; set; }

    }
    [Table("CompanyValuationMeasures")]
    public class CompanyValuationMeasures
    {
        [Key, ForeignKey("CompanyStatisticsData")]
        public int CompanyStatisticsDataId { get; set; }
        public virtual CompanyStatisticsData CompanyStatisticsData { get; set; }
        /// <summary>
        /// The total dollar value of all outstanding shares. Computed as shares times current market price. Capitalization is a measure of corporate size.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Formula: Current Market Price Per Share x Number of Shares Outstanding
        /// Intraday Value
        /// Shares outstanding is taken from the most recently filed quarterly or annual report and Market Cap is calculated using shares outstanding.</remarks>
        public decimal? MarketCapitalisationInMillion { get; set; }
        /// <summary>
        /// Enterprise Value is a measure of theoretical takeover price, and is useful in comparisons against income statement line items above the interest expense/income lines such as revenue and EBITDA.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Formula: Market Cap + Total Debt - Total Cash &amp; Short Term Investments</remarks>
        public decimal? EnterpriseValueInMillion { get; set; }
        /// <summary>
        /// A popular valuation ratio calculated by dividing the current market price by trailing 12-month (ttm) Earnings Per Share.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Formula: Current Market Price / Earnings Per Share
        /// Intraday Value
        /// Trailing Twelve Months</remarks>
        public decimal? TrailingPE { get; set; }
        /// <summary>
        /// A valuation ratio calculated by dividing the current market price by projected 12-month Earnings Per Share.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Formula: Current Market Price / Projected Earnings Per Share
        /// Fiscal Year Ending</remarks>
        public decimal? ForwardPE { get; set; }
        /// <summary>
        /// Forward-looking measure rather than typical earnings growth measures, which look eck in time (historical). Used to measure a stock's valuation against its projected 5-yr growth rate.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Formula: P/E Ratio / 5-Yr Expected EPS Growth
        /// 5 years expected</remarks>
        public decimal? PEGRatio { get; set; }
        /// <summary>
        /// A valuation ratio calculated by dividing the current market price by trailing 12-month (ttm) Total Revenues. Often used to value unprofitable companies.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Formula: Current Market Price / Total Revenues Per Share
        /// Trailing Twelve Months</remarks>
        public decimal? PriceToSales { get; set; }
        /// <summary>
        /// A valuation ratio calculated by dividing the current market price by the most recent quarter's (mrq) Book Value Per Share.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Formula: Current Market Price / Book Value Per Share
        /// Most Recent Quarter</remarks>
        public decimal? PriceToBook { get; set; }
        /// <summary>
        /// Firm value compared against revenue. Provides a more rigorous comparison than the Price/Sales ratio by removing the effects of capitalization from both sides of the ratio. Since revenue is unaffected by the interest income/expense line item, the appropriate value comparison should also remove the effects of capitalization, as EV does.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Formula: Enterprise Value / Total Revenues
        /// Trailing Twelve Months</remarks>
        public decimal? EnterpriseValueToRevenue { get; set; }
        /// <summary>
        /// Firm value compared against EBITDA (Earnings before interest, taxes, depreciation, and amortization). See Enterprise Value/Revenue.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Formula: Enterprise Value / EBITDA
        /// Trailing Twelve Months</remarks>
        public decimal? EnterpriseValueToEBITDA { get; set; }

        internal CompanyValuationMeasures(List<decimal?> values)
        {
            this.MarketCapitalisationInMillion = values[0];
            this.EnterpriseValueInMillion = values[1];
            this.TrailingPE = values[2];
            this.ForwardPE = values[3];
            this.PEGRatio = values[4];
            this.PriceToSales = values[5];
            this.PriceToBook = values[6];
            this.EnterpriseValueToRevenue = values[7];
            this.EnterpriseValueToEBITDA = values[8];
        }

    }
    [Table("CompanyFinancialHighlights")]
    public class CompanyFinancialHighlights
    {
        [Key, ForeignKey("CompanyStatisticsData")]
        public int CompanyStatisticsDataId { get; set; }
        public virtual CompanyStatisticsData CompanyStatisticsData { get; set; }

        //Fiscal Year
        /// <summary>
        /// The date of the end of the firm's accounting year.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public System.DateTime? FiscalYearEnds { get; set; }
        /// <summary>
        /// Date for the most recent quarter end for which data is available on the Key Statistics page. This period is often abbreviated as "MRQ."
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public System.DateTime? MostRecentQuarter { get; set; }

        //Profitability and Management Effectiveness
        /// <summary>
        /// Also known as Return on Sales, this value is the Net Income After Taxes for the trailing 12 months divided by Total Revenue for the same period and is expressed as a percentage.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Formula: (Net Income / Total Revenues) x 100
        /// Trailing Twelve Months</remarks>
        public decimal? ProfitMarginPercent { get; set; }
        /// <summary>
        /// This item represents the difference between the Total Revenues and the Total Operating Costs divided by Total Revenues, and is expressed as a percentage. Total Operating Costs consist of: (a) Cost of Goods Sold (b) Total (c) Selling, General &amp; Administrative Expenses (d) Total R &amp; D Expenses (e) Depreciation &amp; Amortization and (f) Total Other Operating Expenses, Total. A ratio used to measure a company's operating efficiency.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Formula: [(Total Revenues - Total Operating Costs) / (Total Revenues)] x 100
        /// Trailing Twelve Months</remarks>
        public decimal? OperatingMarginPercent { get; set; }
        /// <summary>
        /// This ratio shows percentage of Returns to Total Assets of the company. This is a useful measure in analyzing how well a company uses its assets to produce earnings.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Formula: Earnings from Continuing Operations / Average Total Equity
        /// Trailing Twelve Months</remarks>
        public decimal? ReturnOnAssetsPercent { get; set; }
        /// <summary>
        /// This is a measure of the return on money provided by the firms' owners. This ratio represents Earnings from Continuing Operations divided by average Total Equity and is expressed as a percentage.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Formula: [(Earnings from Continuing Operations) / Total Common Equity] x 100
        /// Trailing Twelve Months</remarks>
        public decimal? ReturnOnEquityPercent { get; set; }

        //Income Statement
        /// <summary>
        /// The amount of money generated by a company's business activities. Also known as Sales.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Trailing Twelve Months</remarks>
        public decimal? RevenueInMillion { get; set; }
        /// <summary>
        /// Revenue in relation to shares.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Formula: Total Revenues / Weighted Average Shares Outstanding
        /// Trailing Twelve Months</remarks>
        public decimal? RevenuePerShare { get; set; }
        /// <summary>
        /// The growth of Quarterly Total Revenues from the same quarter a year ago.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Formula: [(Qtrly Total Revenues x Qtrly Total Revenues (yr ago)) / Qtrly Total Revenues (yr ago)] x 100
        /// Year Over Year</remarks>
        public decimal? QuarterlyRevenueGrowthPercent { get; set; }
        /// <summary>
        /// This item represents Total Revenues minus Cost Of Goods Sold, Total.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Trailing Twelve Months</remarks>
        public decimal? GrossProfitInMillion { get; set; }
        /// <summary>
        /// The accounting acronym EBITDA stands for "Earnings Before Interest, Tax, Depreciation, and Amortization."
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Trailing Twelve Months</remarks>
        public decimal? EBITDAInMillion { get; set; }
        /// <summary>
        /// This ratio shows percentage of Net Income to Common Excluding Extra Items less Earnings Of Discontinued Operations to Total Revenues. This is the dollar amount accruing to common shareholders for dividends and retained earnings.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Formula: Net Income - Preferred Dividend and Other Adjustments - Earnings Of Discontinued Operations - Extraordinary Item &amp; Accounting Change
        /// Trailing Twelve Months</remarks>
        public decimal? NetIncomeAvlToCommonInMillion { get; set; }
        /// <summary>
        /// This is the Adjusted Income Available to Common Stockholders (based on Generally Accepted Accounting Principles, GAAP) for the trailing 12 months divided by the trailing 12 month weighted average shares outstanding. Diluted EPS uses diluted weighted average shares in the calculation, or the weighted average shares assuming all convertible securities are exercised.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Formula: (Net Income - Preferred Dividend and Other Adjustments)/ Weighted Average Diluted Shares Outstanding
        /// Trailing Twelve Months</remarks>
        public decimal? DilutedEPS { get; set; }
        /// <summary>
        /// The growth of Quarterly Net Income from the same quarter a year ago.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Formula: [(Qtrly Net Income x Qtrly Net Income (yr ago)) / Qtrly Net Income (yr ago)] x 100
        /// Year Over Year</remarks>
        public decimal? QuaterlyEarningsGrowthPercent { get; set; }

        //Balance Sheet and CashFlowStatement
        /// <summary>
        /// The Total Cash and Short-term Investments on the elance sheet as of the most recent quarter.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Most Recent Quarter</remarks>
        public decimal? TotalCashInMillion { get; set; }
        /// <summary>
        /// This is the Total Cash plus Short Term Investments divided by the Shares Outstanding at the end of the most recent fiscal quarter.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Most Recent Quarter</remarks>
        public decimal? TotalCashPerShare { get; set; }
        /// <summary>
        /// The Total Debt on the elance sheet as of the most recent quarter.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Formula: Short Term Borrowings + Current Portion of Long Term Debt + Current Portion of Capital Lease + Long Term Debt + Long Term Capital Lease + Finance Division Debt Current + Finance Division Debt Non Current
        /// Most Recent Quarter</remarks>
        public decimal? TotalDebtInMillion { get; set; }
        /// <summary>
        /// This ratio is Total Debt for the most recent fiscal quarter divided by Total Shareholder Equity for the same period.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Formula: [(Long-term Debt + Capital Leases + Finance Division Debt Non-Current + Short-term Borrowings + Current Portion of Long-term Debt + Current Portion of Capital Lease Obligation + Finance Division Debt Current) / (Total Common Equity + Total Preferred Equity)] x 100
        /// Most Recent Quarter</remarks>
        public decimal? TotalDebtPerEquity { get; set; }
        /// <summary>
        /// This is the ratio of Total Current Assets for the most recent quarter divided by Total Current Liabilities for the same period.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Formula: Total Current Assets / Total Current Liabilities
        /// Most Recent Quarter</remarks>
        public decimal? CurrentRatio { get; set; }
        /// <summary>
        /// This is defined as the Common Shareholder's Equity divided by the Shares Outstanding at the end of the most recent fiscal quarter.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Formula: Total Common Equity / Total Common Shares Outstanding
        /// Most Recent Quarter</remarks>
        public decimal? BookValuePerShare { get; set; }
        /// <summary>
        /// Net cash used or generated in operating activities during the stated period of time. It reflects net impact of all operating activity transactions on the cash flow of the entity. This GAAP figure is taken directly from the company's Cash Flow Statement and might include significant non-recurring items.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Formula: Net Income + Depreciation and Amortization, Total + Other Amortization + Other Non-Cash Items, Total + Change in Working Capital
        /// Trailing Twelve Months</remarks>
        public decimal? OperatingCashFlowInMillion { get; set; }
        /// <summary>
        /// This figure is a normalized item that excludes non-recurring items and also takes into consideration cash inflows from financing activities such as debt or preferred stock issuances.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks>Formula: (EBIT + Interest Expense) x (1 x Tax Rate) + Depreciation &amp; Amort., Total + Other Amortization + Capital Expenditure + Sale (Purchase) of Intangible assets - Change in Net Working Capital + Pref. Dividends Paid + Total Debt Repaid + Total Debt Issued + Repurchase of Preferred + Issuance of Preferred Stock   -- [Where: Tax Rate = 0.375]
        /// Trailing Twelve Months</remarks>
        public decimal? LeveredFreeCashFlowInMillion { get; set; }


        internal CompanyFinancialHighlights(System.DateTime? fiscalYEnds, System.DateTime? mostRecentQtr, List<decimal?> values)
        {
            this.FiscalYearEnds = fiscalYEnds;
            this.MostRecentQuarter = mostRecentQtr;

            this.ProfitMarginPercent = values[0];
            this.OperatingMarginPercent = values[1];

            this.ReturnOnAssetsPercent = values[2];
            this.ReturnOnEquityPercent = values[3];

            this.RevenueInMillion = values[4];
            this.RevenuePerShare = values[5];
            this.QuarterlyRevenueGrowthPercent = values[6];
            this.GrossProfitInMillion = values[7];
            this.EBITDAInMillion = values[8];
            this.NetIncomeAvlToCommonInMillion = values[9];
            this.DilutedEPS = values[10];
            this.QuaterlyEarningsGrowthPercent = values[11];

            this.TotalCashInMillion = values[12];
            this.TotalCashPerShare = values[13];
            this.TotalDebtInMillion = values[14];
            this.TotalDebtPerEquity = values[15];
            this.CurrentRatio = values[16];
            this.BookValuePerShare = values[17];

            this.OperatingCashFlowInMillion = values[18];
            this.LeveredFreeCashFlowInMillion = values[19];
        }

    }

    [Table("CompanyTradingInfo")]
    public class CompanyTradingInfo
    {
        [Key, ForeignKey("CompanyStatisticsData")]
        public int CompanyStatisticsDataId { get; set; }
        public virtual CompanyStatisticsData CompanyStatisticsData { get; set; }

        //StockPriceHistory
        /// <summary>
        /// The Beta used is Beta of Equity. Beta is the monthly price change of a particular company relative to the monthly price change of the S&amp;P500. The time period for Beta is 3 years (36 months) when available.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal? Beta { get; set; }
        /// <summary>
        /// The percentage change in price from 52 weeks ago.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal? OneYearChangePercent { get; set; }
        /// <summary>
        /// The S&amp;P 500 Index's percentage change in price from 52 weeks ago.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal? SP500OneYearChangePercent { get; set; }
        /// <summary>
        /// This price is the highest Price the stock traded at in the last 12 months. This could be an intraday high.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal? OneYearHigh { get; set; }
        /// <summary>
        /// This price is the lowest Price the stock traded at in the last 12 months. This could be an intraday low.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal? OneYearLow { get; set; }
        /// <summary>
        /// A simple moving average that is calculated by dividing the sum of the closing prices in the last 50 trading days by 50.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal? FiftyDayMovingAverage { get; set; }
        /// <summary>
        /// A simple moving average that is calculated by dividing the sum of the closing prices in the last 200 trading days by 200.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal? TwoHundredDayMovingAverage { get; set; }

        //Share Statistics
        /// <summary>
        /// This is the average daily trading volume during the last 3 months.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal? AverageVolumeThreeMonthInThousand { get; set; }
        /// <summary>
        /// This is the average daily trading volume during the last 10 days.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal? AverageVolumeTenDaysInThousand { get; set; }
        /// <summary>
        /// This is the number of shares of common stock currently outstanding—the number of shares issued minus the shares held in treasury. This field reflects all offerings and acquisitions for stock made after the end of the previous fiscal period.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal? SharesOutstandingInMillion { get; set; }
        /// <summary>
        /// This is the number of freely traded shares in the hands of the public. Float is calculated as Shares Outstanding minus Shares Owned by Insiders, 5% Owners, and Rule 144 Shares.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal? FloatInMillion { get; set; }
        /// <summary>
        /// This is the number of shares currently borrowed by investors for sale, but not yet returned to the owner (lender).
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal? PercentHeldByInsiders { get; set; }
        public decimal? PercentHeldByInstitutions { get; set; }
        public decimal? SharesShortInMillion { get; set; }
        /// <summary>
        /// This represents the number of days it would take to cover the Short Interest if trading continued at the average daily volume for the month. It is calculated as the Short Interest for the Current Month divided by the Average Daily Volume.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal? ShortRatio { get; set; }
        /// <summary>
        /// Number of shares short divided by float.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal? ShortPercentOfFloat { get; set; }
        /// <summary>
        /// Shares Short in the prior month. See Shares Short.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal? SharesShortPriorMonthInMillion { get; set; }


        //Dividends and Splits
        /// <summary>
        /// The annualized amount of dividends expected to be paid in the current fiscal year.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal? ForwardAnnualDividendRate { get; set; }
        /// <summary>
        /// Formula: (Forward Annual Dividend Rate / Current Market Price) x 100
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal? ForwardAnnualDividendYieldPercent { get; set; }
        /// <summary>
        /// The sum of all dividends paid out in the trailing 12-month period.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal? TrailingAnnualDividendYield { get; set; }
        /// <summary>
        /// Formula: (Trailing Annual Dividend Rate / Current Market Price) x 100
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal? TrailingAnnualDividendYieldPercent { get; set; }
        /// <summary>
        /// The average Forward Annual Dividend Yield in the past 5 years.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal? FiveYearAverageDividendYieldPercent { get; set; }
        /// <summary>
        /// The ratio of Earnings paid out in Dividends, expressed as a percentage.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public decimal? PayoutRatio { get; set; }
        /// <summary>
        /// The payment date for a declared dividend.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public System.DateTime? DividendDate { get; set; }
        /// <summary>
        /// The first day of trading when the seller, rather than the buyer, of a stock is entitled to the most recently announced dividend payment. The date set by the NYSE (and generally followed on other U.S. exchanges) is currently two business days before the record date. A stock that has gone ex-dividend is denoted by an x in newspaper listings on that date.
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        public System.DateTime? ExDividendDate { get; set; }
        public System.DateTime? LastSplitDate { get; set; }
        [StringLength(100)]
        public string LastSplitFactor { get; set; }



        internal CompanyTradingInfo(List<decimal?> values, System.DateTime? dividendDate, System.DateTime? exDividendDate, System.DateTime? lastSplitDate, string lastSplitFactor)
        {
            this.Beta = values[0];
            this.OneYearChangePercent = values[1];
            this.SP500OneYearChangePercent = values[2];
            this.OneYearHigh = values[3];
            this.OneYearLow = values[4];
            this.FiftyDayMovingAverage = values[5];
            this.TwoHundredDayMovingAverage = values[6];

            this.AverageVolumeThreeMonthInThousand = values[7];
            this.AverageVolumeTenDaysInThousand = values[8];
            this.SharesOutstandingInMillion = values[9];
            this.FloatInMillion = values[10];
            this.PercentHeldByInsiders = values[11];
            this.PercentHeldByInstitutions = values[12];
            this.SharesShortInMillion = values[13];
            this.ShortRatio = values[14];
            this.ShortPercentOfFloat = values[15];
            this.SharesShortPriorMonthInMillion = values[16];

            this.ForwardAnnualDividendRate = values[17];
            this.ForwardAnnualDividendYieldPercent = values[18];
            this.TrailingAnnualDividendYield = values[19];
            this.TrailingAnnualDividendYieldPercent = values[20];
            this.FiveYearAverageDividendYieldPercent = values[21];
            this.PayoutRatio = values[22];
            this.DividendDate = dividendDate;
            this.ExDividendDate = exDividendDate;
            this.LastSplitFactor = lastSplitFactor;
            this.LastSplitDate = lastSplitDate;
        }

    }
}
