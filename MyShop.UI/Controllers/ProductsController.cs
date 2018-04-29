using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Data.Local;

namespace MyShop.UI.Controllers
{
    public class ProductsController : Controller
    {
        ProductRepository productsCtx; 

        public ProductsController()
        {
            productsCtx = new ProductRepository();
        }

        [HttpGet]
        [HttpHead]
        public ActionResult Index()
        {
            return View(productsCtx.List().ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View(new Product());
        }

        [HttpPost]
        public ActionResult Create(Product p)
        {
            if (!ModelState.IsValid)
            {
                return View(p);
            }

            this.productsCtx.Save(p);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Get(string id)
        {
            return View(this.productsCtx.Get(id));
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            try
            {
                Product p = this.productsCtx.Get(id);
                return View(p);
            }catch
            {
                return HttpNotFound();
            }
        }

        [HttpPut]
        public ActionResult Edit(string id, Product p)
        {
            if (!ModelState.IsValid)
            {
                return View(p);
            }

            try
            {
                Product target = this.productsCtx.Get(id);
                target.UpdateFrom(p);
                this.productsCtx.Update(target);
            }
            catch
            {
                return HttpNotFound();
            }

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public ActionResult Delete(string id)
        {
            try
            {
                this.productsCtx.Delete(id);
            }catch
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }
    }
}