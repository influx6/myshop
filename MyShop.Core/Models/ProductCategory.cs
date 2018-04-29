using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class ProductCategory
    {
        public string ID { get; set; }
        public string Value { get; set; }

        public ProductCategory()
        {
            this.ID = Guid.NewGuid().ToString();
        }

        public void UpdateFrom(ProductCategory c)
        {
            this.Value = c.Value;
        }
    }
}
