using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.DataRepository;
using MyShop.Core.Models;
using MyShop.Data.Local;

namespace MyShop.UI.Controllers
{
    public class ProductCategoryController : Controller
    {
        IDataRepository<ProductCategory> context;

        public ProductCategoryController(IDataRepository<ProductCategory> context)
        {
            this.context = context;
        }

        public ActionResult Index()
        {
            return View(context.List().ToList());
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

            this.context.Save(p);
            return RedirectToAction("Index");
        }

        public ActionResult Get(string id)
        {
            return View(this.context.Get((pr) => pr.ID == id));
        }

        public ActionResult Edit(string id)
        {
            try
            {
                ProductCategory p = this.context.Get((pr) => pr.ID == id);
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
                ProductCategory target = this.context.Get((pr) => pr.ID == id);
                target.UpdateFrom(p);
                this.context.Update((pr) => pr.ID == id,target);
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
                this.context.Delete((pr) => pr.ID == id);
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
                ProductCategory p = this.context.Get((pr) => pr.ID == id);
                return View(p);
            }
            catch
            {
                return HttpNotFound();
            }
        }
    }
}