using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YHScrape
{
    public class Helper
    {
        public static string RemoveParenthesesContend(string s)
        {
            if(s.Contains('('))
            {
                s = s.Substring(0, s.IndexOf('('));
            }
            return s;
        }
        public static int GetCompanyId(string ticker)
        {
            int id = 0;
            try
            {
                using (var ctx = new YHScrape.Entities.YahooFinanceContext())
                {
                    id = ctx.YahooCompanyDatas.Single(x => x.Ticker == ticker).Id;
                }
            }
            catch (Exception e) {  }
            return id;
        }
        public static object ParseString(string s)
        {
            int i;   
            return s;
        }
        public static double? ParseToDouble(string s)
        {
            if (s.Contains("N/A"))
            {
                return null;
            }
            double v = 0;
            double.TryParse(s.Replace("%", ""), out v);
            return v;
        }

        public static DateTime? ParseToDateTime(string s)
        {
            DateTime d = new DateTime();
            if (s.Contains("N/A") || !System.DateTime.TryParse(s, out d))
            {
                return null;
            }
            else
            {
                return d;
            }            
        }

        public static decimal? ParseToDecimalValue(string s)
        {
            decimal d;
            if (s.Contains("N/A"))
            {
                return null;
            }
            else if(s.Contains("%"))
            {
                if (!decimal.TryParse(s.Substring(0, s.Length - 1), out d))
                {
                    return null;
                }
                else
                {
                    return d;
                }           
            }
            else if(s.Contains(","))
            {
                if (!decimal.TryParse(s.Replace("," , ""), out d))
                {
                    return null;
                }
                else
                {
                    return d;
                }      
            }
            else if (s.EndsWith("T") || s.EndsWith("K"))
            {
                if(!decimal.TryParse(s.Substring(0, s.Length - 1), out d))
                {
                    return null;
                }
                else
                {
                    return d * 1000;
                }                
            }
            else if (s.EndsWith("M"))
            {
                if(!decimal.TryParse(s.Substring(0, s.Length - 1), out d))
                {
                    return null;
                }
                else
                {
                    return d * 1000000;
                }
            }
            else if (s.EndsWith("B"))
            {
                if (!decimal.TryParse(s.Substring(0, s.Length - 1), out d))
                {
                    return null;
                }
                else
                {
                    return d * 1000000000;
                }
            }
            else if (decimal.TryParse(s, out d))
            {
                return d;
            }
            else
            {
                return null;
            }
        }
        //public static double? GetMillionValue(string s)
        //{
        //    if (s.Contains("N/A"))
        //    {
        //        return null;
        //    }
        //    double v = 0;
        //    double.TryParse(s.Substring(0, s.Length - 1), out v);
        //    return v * GetStringFactor(s);
        //}
    }
}
