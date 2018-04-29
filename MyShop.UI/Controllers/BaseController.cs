using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.UI.Controllers
{
    public class BaseController : Controller
    {
        public const string ProductDBName = "products_db";
        public const string ProductCategoryDBName = "product_categories_db";
    }
}