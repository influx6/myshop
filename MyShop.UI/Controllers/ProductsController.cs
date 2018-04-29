﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Data.Local;
using MyShop.Core.ViewModels;

namespace MyShop.UI.Controllers
{
    public class ProductsController : Controller
    {
        ProductRepository productsCtx;
        ProductCategoryRepository categories;

        public ProductsController()
        {
            productsCtx = new ProductRepository();
            categories = new ProductCategoryRepository();
        }

        public ActionResult Index()
        {
            return View(productsCtx.List().ToList());
        }

        public ActionResult Create()
        {
            return View(new ProductManagerViewModel {
                Product=new Product(),
                Categories=this.categories.List(),
            });
        }

        [HttpPost]
        public ActionResult Create(ProductManagerViewModel p)
        {
            if (!ModelState.IsValid)
            {
                return View(p);
            }

            this.productsCtx.Save(p.Product);
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
                Product p = this.productsCtx.Get(id);
                return View(new ProductManagerViewModel
                {
                    Product = p,
                    Categories = this.categories.List(),
                });
            }
            catch { 
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Edit(string id, ProductManagerViewModel p)
        {
            if (!ModelState.IsValid)
            {
                return View(p);
            }

            try
            {
                Product target = this.productsCtx.Get(id);
                target.UpdateFrom(p.Product);
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
            }catch
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Delete(string id)
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
    }
}