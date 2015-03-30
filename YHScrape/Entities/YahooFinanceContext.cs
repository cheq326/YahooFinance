using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using YHScrape.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace YHScrape.Entities
{
    public class YahooFinanceContext : DbContext
    {
        public DbSet<CompanyData> YahooCompanyDatas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure Code First to ignore PluralizingTableName convention 
            // If you keep this convention then the generated tables will have pluralized names. 
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        } 
    }
}
