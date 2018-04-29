using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Data.Local;

namespace MyShop.UI.Controllers
{
    public class ProductCategoryController : Controller
    {
        ProductCategoryRepository productsCtx;

        public ProductCategoryController()
        {
            productsCtx = new ProductCategoryRepository();
        }

        public ActionResult Index()
        {
            return View(productsCtx.List().ToList());
        }

        public ActionResult Create()
        {
            return View(new ProductCategory());
        }

        [HttpPost]
        public ActionResult Create(ProductCategory p)
        {
            if (!ModelState.IsValid)
            {
                return View(p);
            }

            this.productsCtx.Save(p);
            return RedirectToAction("Index");
        }

        public ActionResult Get(string id)
        {
            return View(this.productsCtx.Get(id));
        }

        public ActionResult Edit(string id)
        {
            try
            {
                ProductCategory p = this.productsCtx.Get(id);
                return View(p);
            }
            catch
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Edit(string id, ProductCategory p)
        {
            if (!ModelState.IsValid)
            {
                return View(p);
            }

            try
            {
                ProductCategory target = this.productsCtx.Get(id);
                target.UpdateFrom(p);
                this.productsCtx.Update(target);
            }
            catch
            {
                return HttpNotFound();
            }

            return RedirectToAction("Index");
        }

        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string id)
        {
            try
            {
                this.productsCtx.Delete(id);
            }
            catch
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
        {
            try
            {
                ProductCategory p = this.productsCtx.Get(id);
                return View(p);
            }
            catch
            {
                return HttpNotFound();
            }
        }
    }
}