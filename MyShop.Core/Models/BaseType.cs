using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class BaseType
    {
        public string ID { get; set; }
        public DateTime CreatedAt { get; set; }

        public BaseType()
        {
            this.ID = Guid.NewGuid().ToString();
            this.CreatedAt = DateTime.Now;
        }
    }
}
