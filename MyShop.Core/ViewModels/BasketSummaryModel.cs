using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModels
{
    public class BasketSummaryModel
    {
        public int BasketCount { get; set; }
        public decimal Total { get; set; }

        public BasketSummaryModel() { }
        public BasketSummaryModel(int c, decimal t)
        {
            this.Total = t;
            this.BasketCount = c;
        }
    }
}
