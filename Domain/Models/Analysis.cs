using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Analysis
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public decimal NetProfit { get; set; }
        public decimal TotalIncome { get; set; }
        public decimal TotalOutcome { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
    public class AnalysisConfigration : IEntityTypeConfiguration<Analysis>
    {
        public void Configure(EntityTypeBuilder<Analysis> builder)
        {


        }
    }
}
