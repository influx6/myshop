﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModels
{
    public class BasketViewModel
    {
        public string BasketItemId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
    }
}