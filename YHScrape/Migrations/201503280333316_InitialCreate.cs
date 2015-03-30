namespace YHScrape.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompanyData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyName = c.String(),
                        Ticker = c.String(),
                        test = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CompanyStatisticsData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyDataId = c.Int(nullable: false),
                        CollectionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CompanyData", t => t.CompanyDataId, cascadeDelete: true)
                .Index(t => t.CompanyDataId);
            
            CreateTable(
                "dbo.CompanyFinancialHighlights",
                c => new
                    {
                        CompanyStatisticsDataId = c.Int(nullable: false),
                        FiscalYearEnds = c.DateTime(),
                        MostRecentQuarter = c.DateTime(),
                        ProfitMarginPercent = c.Decimal(precision: 18, scale: 2),
                        OperatingMarginPercent = c.Decimal(precision: 18, scale: 2),
                        ReturnOnAssetsPercent = c.Decimal(precision: 18, scale: 2),
                        ReturnOnEquityPercent = c.Decimal(precision: 18, scale: 2),
                        RevenueInMillion = c.Decimal(precision: 18, scale: 2),
                        RevenuePerShare = c.Decimal(precision: 18, scale: 2),
                        QuarterlyRevenueGrowthPercent = c.Decimal(precision: 18, scale: 2),
                        GrossProfitInMillion = c.Decimal(precision: 18, scale: 2),
                        EBITDAInMillion = c.Decimal(precision: 18, scale: 2),
                        NetIncomeAvlToCommonInMillion = c.Decimal(precision: 18, scale: 2),
                        DilutedEPS = c.Decimal(precision: 18, scale: 2),
                        QuaterlyEarningsGrowthPercent = c.Decimal(precision: 18, scale: 2),
                        TotalCashInMillion = c.Decimal(precision: 18, scale: 2),
                        TotalCashPerShare = c.Decimal(precision: 18, scale: 2),
                        TotalDebtInMillion = c.Decimal(precision: 18, scale: 2),
                        TotalDebtPerEquity = c.Decimal(precision: 18, scale: 2),
                        CurrentRatio = c.Decimal(precision: 18, scale: 2),
                        BookValuePerShare = c.Decimal(precision: 18, scale: 2),
                        OperatingCashFlowInMillion = c.Decimal(precision: 18, scale: 2),
                        LeveredFreeCashFlowInMillion = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.CompanyStatisticsDataId)
                .ForeignKey("dbo.CompanyStatisticsData", t => t.CompanyStatisticsDataId)
                .Index(t => t.CompanyStatisticsDataId);
            
            CreateTable(
                "dbo.CompanyTradingInfo",
                c => new
                    {
                        CompanyStatisticsDataId = c.Int(nullable: false),
                        Beta = c.Decimal(precision: 18, scale: 2),
                        OneYearChangePercent = c.Decimal(precision: 18, scale: 2),
                        SP500OneYearChangePercent = c.Decimal(precision: 18, scale: 2),
                        OneYearHigh = c.Decimal(precision: 18, scale: 2),
                        OneYearLow = c.Decimal(precision: 18, scale: 2),
                        FiftyDayMovingAverage = c.Decimal(precision: 18, scale: 2),
                        TwoHundredDayMovingAverage = c.Decimal(precision: 18, scale: 2),
                        AverageVolumeThreeMonthInThousand = c.Decimal(precision: 18, scale: 2),
                        AverageVolumeTenDaysInThousand = c.Decimal(precision: 18, scale: 2),
                        SharesOutstandingInMillion = c.Decimal(precision: 18, scale: 2),
                        FloatInMillion = c.Decimal(precision: 18, scale: 2),
                        PercentHeldByInsiders = c.Decimal(precision: 18, scale: 2),
                        PercentHeldByInstitutions = c.Decimal(precision: 18, scale: 2),
                        SharesShortInMillion = c.Decimal(precision: 18, scale: 2),
                        ShortRatio = c.Decimal(precision: 18, scale: 2),
                        ShortPercentOfFloat = c.Decimal(precision: 18, scale: 2),
                        SharesShortPriorMonthInMillion = c.Decimal(precision: 18, scale: 2),
                        ForwardAnnualDividendRate = c.Decimal(precision: 18, scale: 2),
                        ForwardAnnualDividendYieldPercent = c.Decimal(precision: 18, scale: 2),
                        TrailingAnnualDividendYield = c.Decimal(precision: 18, scale: 2),
                        TrailingAnnualDividendYieldPercent = c.Decimal(precision: 18, scale: 2),
                        FiveYearAverageDividendYieldPercent = c.Decimal(precision: 18, scale: 2),
                        PayoutRatio = c.Decimal(precision: 18, scale: 2),
                        DividendDate = c.DateTime(),
                        ExDividendDate = c.DateTime(),
                        LastSplitDate = c.DateTime(),
                        LastSplitFactor = c.String(),
                    })
                .PrimaryKey(t => t.CompanyStatisticsDataId)
                .ForeignKey("dbo.CompanyStatisticsData", t => t.CompanyStatisticsDataId)
                .Index(t => t.CompanyStatisticsDataId);
            
            CreateTable(
                "dbo.CompanyValuationMeasures",
                c => new
                    {
                        CompanyStatisticsDataId = c.Int(nullable: false),
                        MarketCapitalisationInMillion = c.Decimal(precision: 18, scale: 2),
                        EnterpriseValueInMillion = c.Decimal(precision: 18, scale: 2),
                        TrailingPE = c.Decimal(precision: 18, scale: 2),
                        ForwardPE = c.Decimal(precision: 18, scale: 2),
                        PEGRatio = c.Decimal(precision: 18, scale: 2),
                        PriceToSales = c.Decimal(precision: 18, scale: 2),
                        PriceToBook = c.Decimal(precision: 18, scale: 2),
                        EnterpriseValueToRevenue = c.Decimal(precision: 18, scale: 2),
                        EnterpriseValueToEBITDA = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.CompanyStatisticsDataId)
                .ForeignKey("dbo.CompanyStatisticsData", t => t.CompanyStatisticsDataId)
                .Index(t => t.CompanyStatisticsDataId);
            
            CreateTable(
                "dbo.DailyQuote",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CompanyDataId = c.Int(nullable: false),
                        Symbol = c.String(),
                        Ask = c.Decimal(precision: 18, scale: 2),
                        Average_Daily_Volume = c.Decimal(precision: 18, scale: 2),
                        Ask_Size = c.Decimal(precision: 18, scale: 2),
                        Bid = c.Decimal(precision: 18, scale: 2),
                        Ask_Realtime = c.Decimal(precision: 18, scale: 2),
                        Bid_Realtime = c.Decimal(precision: 18, scale: 2),
                        Book_Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Bid_Size = c.Decimal(precision: 18, scale: 2),
                        Change_And_Percent_Change = c.Decimal(precision: 18, scale: 2),
                        Change = c.Decimal(precision: 18, scale: 2),
                        Commission = c.String(),
                        Change_Realtime = c.String(),
                        After_Hours_Change_Realtime = c.String(),
                        DividendPerShare = c.Decimal(precision: 18, scale: 2),
                        Last_Trade_Date = c.DateTime(),
                        Trade_Date = c.String(),
                        EarningsPerShare = c.Decimal(precision: 18, scale: 2),
                        Error_Indication_returned_for_symbol_changed_invalid = c.String(),
                        EPS_Estimate_Current_Year = c.Decimal(precision: 18, scale: 2),
                        EPS_Estimate_Next_Year = c.Decimal(precision: 18, scale: 2),
                        EPS_Estimate_Next_Quarter = c.Decimal(precision: 18, scale: 2),
                        Float_Shares = c.Decimal(precision: 18, scale: 2),
                        Day_Low = c.Decimal(precision: 18, scale: 2),
                        Day_High = c.Decimal(precision: 18, scale: 2),
                        FiftyTwoWeek_Low = c.Decimal(precision: 18, scale: 2),
                        FiftyTwoWeek_High = c.Decimal(precision: 18, scale: 2),
                        Holdings_Gain_Percent = c.String(),
                        Annualized_Gain = c.String(),
                        Holdings_Gain = c.String(),
                        Holdings_Gain_Percent_Realtime = c.String(),
                        Holdings_Gain_Realtime = c.String(),
                        More_Info = c.String(),
                        Order_Book_Realtime = c.String(),
                        Market_Capitalization = c.String(),
                        Market_Cap_Realtime = c.String(),
                        EBITDA = c.String(),
                        Change_From_FiftyTwoWeek_Low = c.Decimal(precision: 18, scale: 2),
                        Percent_Change_From_FiftyTwoWeek_Low = c.String(),
                        Last_Trade_Realtime_With_Time = c.String(),
                        Change_Percent_Realtime = c.String(),
                        Last_Trade_Size = c.String(),
                        Change_From_FiftyTwoWeek_High = c.Decimal(precision: 18, scale: 2),
                        Percent_Change_From_FiftyTwoWeek_High = c.String(),
                        Last_Trade_With_Time = c.String(),
                        Last_Trade_Price_Only = c.Decimal(precision: 18, scale: 2),
                        High_Limit = c.String(),
                        Low_Limit = c.String(),
                        Day_Range = c.String(),
                        Day_Range_Realtime = c.String(),
                        FiftyDay_Moving_Average = c.Decimal(precision: 18, scale: 2),
                        TwoHundredDay_Moving_Average = c.Decimal(precision: 18, scale: 2),
                        Change_From_TwoHundredDay_Moving_Average = c.Decimal(precision: 18, scale: 2),
                        Percent_Change_From_TwoHundredDay_Moving_Average = c.String(),
                        Change_From_FiftyDay_Moving_Average = c.Decimal(precision: 18, scale: 2),
                        Percent_Change_From_FiftyDay_Moving_Average = c.String(),
                        Name = c.String(),
                        Notes = c.String(),
                        Open = c.Decimal(precision: 18, scale: 2),
                        Previous_Close = c.Decimal(precision: 18, scale: 2),
                        Price_Paid = c.Decimal(precision: 18, scale: 2),
                        Change_in_Percent = c.String(),
                        Price_Sales = c.Decimal(precision: 18, scale: 2),
                        Price_Book = c.Decimal(precision: 18, scale: 2),
                        Ex_Dividend_Date = c.DateTime(),
                        PE_Ratio = c.Decimal(precision: 18, scale: 2),
                        Dividend_Pay_Date = c.DateTime(),
                        PE_Ratio_Realtime = c.String(),
                        PEG_Ratio = c.Decimal(precision: 18, scale: 2),
                        Price_EPS_Estimate_Current_Year = c.Decimal(precision: 18, scale: 2),
                        Price_EPS_Estimate_Next_Year = c.Decimal(precision: 18, scale: 2),
                        Shares_Owned = c.String(),
                        Short_Ratio = c.Decimal(precision: 18, scale: 2),
                        Last_Trade_Time = c.String(),
                        Trade_Links = c.String(),
                        Ticker_Trend = c.String(),
                        One_yr_Target_Price = c.Decimal(precision: 18, scale: 2),
                        Volume = c.Decimal(precision: 18, scale: 2),
                        Holdings_Value = c.String(),
                        Holdings_Value_Realtime = c.String(),
                        FiftyTwoWeek_Range = c.String(),
                        Day_Value_Change = c.String(),
                        Day_Value_Change_Realtime = c.String(),
                        Stock_Exchange = c.String(),
                        Dividend_Yield = c.Decimal(precision: 18, scale: 2),
                        RequestTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CompanyData", t => t.CompanyDataId, cascadeDelete: true)
                .Index(t => t.CompanyDataId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DailyQuote", "CompanyDataId", "dbo.CompanyData");
            DropForeignKey("dbo.CompanyValuationMeasures", "CompanyStatisticsDataId", "dbo.CompanyStatisticsData");
            DropForeignKey("dbo.CompanyTradingInfo", "CompanyStatisticsDataId", "dbo.CompanyStatisticsData");
            DropForeignKey("dbo.CompanyFinancialHighlights", "CompanyStatisticsDataId", "dbo.CompanyStatisticsData");
            DropForeignKey("dbo.CompanyStatisticsData", "CompanyDataId", "dbo.CompanyData");
            DropIndex("dbo.DailyQuote", new[] { "CompanyDataId" });
            DropIndex("dbo.CompanyValuationMeasures", new[] { "CompanyStatisticsDataId" });
            DropIndex("dbo.CompanyTradingInfo", new[] { "CompanyStatisticsDataId" });
            DropIndex("dbo.CompanyFinancialHighlights", new[] { "CompanyStatisticsDataId" });
            DropIndex("dbo.CompanyStatisticsData", new[] { "CompanyDataId" });
            DropTable("dbo.DailyQuote");
            DropTable("dbo.CompanyValuationMeasures");
            DropTable("dbo.CompanyTradingInfo");
            DropTable("dbo.CompanyFinancialHighlights");
            DropTable("dbo.CompanyStatisticsData");
            DropTable("dbo.CompanyData");
        }
    }
}
