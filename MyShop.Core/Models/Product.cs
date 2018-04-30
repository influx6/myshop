using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Product : BaseType
    {
        [StringLength(20)]
        [DisplayName("Product Name")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Range(0, 1000)]
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }

        public Product() : base(){}

        public Product(Product craft)
        {
            this.ID = Guid.NewGuid().ToString();
            this.Name = craft.Name;
            this.Description = craft.Description;
            this.Price = craft.Price;
            this.Category = craft.Category;
            this.Image = craft.Image;
        }

        public Product(string name, string description, string category, string image, decimal price)
        {
            this.ID = Guid.NewGuid().ToString();
            this.Name = name;
            this.Description = description;
            this.Price = price;
            this.Category = category;
            this.Image = image;
        }

        public void UpdateFrom(Product p)
        {
            this.Name = p.Name;
            this.Price = p.Price;
            this.Category = p.Category;
            this.Description = p.Description;
            this.Image = p.Image;
        }
    }
}
