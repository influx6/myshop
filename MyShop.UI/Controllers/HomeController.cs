using MyShop.Core.DataRepository;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.UI.Controllers
{
    public class HomeController : Controller
    {

        IDataRepository<Product> productsCtx;
        IDataRepository<ProductCategory> categories;

        public HomeController(IDataRepository<Product> products, IDataRepository<ProductCategory> categories)
        {
            this.productsCtx = products;
            this.categories = categories;
        }

        public ActionResult Index()
        {
            return View(this.productsCtx.List());
        }

        public ActionResult Detail(string id)
        {
            try
            {
                Product p = this.productsCtx.Get(id);
                return View(p);
            }
            catch { 
                return HttpNotFound();
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}