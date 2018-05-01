using MyShop.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.UI.Controllers
{
    public class BasketController : Controller
    {
        public IBasketService ctx;

        public BasketController(IBasketService ctx)
        {
            this.ctx = ctx;
        }

        public ActionResult Index()
        {
            return View(this.ctx.GetBasketItems(this.HttpContext));
        }

        public PartialViewResult BasketSummary(string Id)
        {
            return PartialView(this.ctx.GetBasketSummary(this.HttpContext));
        }

        public ActionResult AddToBasket(string Id) {
            this.ctx.AddToBasket(this.HttpContext, Id);
            return RedirectToAction("index");
        }

        public ActionResult RemoveFromBasket(string Id) {
            this.ctx.RemoveFromBasket(this.HttpContext, Id);
            return RedirectToAction("index");
        }
    }
}