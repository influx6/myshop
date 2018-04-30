using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class ProductCategory : BaseType
    {
        public string Value { get; set; }

        public ProductCategory() : base()
        {
        }

        public void UpdateFrom(ProductCategory c)
        {
            this.Value = c.Value;
        }
    }
}
