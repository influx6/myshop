using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Basket : BaseType
    {
        public virtual ICollection<BasketItem> Items { get; set; }

        public Basket() : base()
        {
            this.Items = new List<BasketItem>();
        }
    }
}
