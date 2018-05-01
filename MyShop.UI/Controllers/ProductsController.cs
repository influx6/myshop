using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Data.Local;
using MyShop.Core.ViewModels;
using MyShop.Core.Contracts;
using System.IO;

namespace MyShop.UI.Controllers
{
    public class ProductsController : Controller
    {
        IDataRepository<Product> productsCtx;
        IDataRepository<ProductCategory> categories;

        public ProductsController(IDataRepository<Product> products, IDataRepository<ProductCategory> categories)
        {
            this.productsCtx = products;
            this.categories = categories;
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
        public ActionResult Create(ProductManagerViewModel p, HttpPostedFileBase image)
        {
            
            if (!ModelState.IsValid)
            {
                return View(p);
            }

            if (image != null && image.ContentLength > 0 ) {
                p.Product.Image = p.Product.ID + Path.GetExtension(image.FileName);
                image.SaveAs(Server.MapPath("//Content//product_images//") + p.Product.Image);
            }

            this.productsCtx.Save(p.Product);
            this.productsCtx.Commit();
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
        public ActionResult Edit(string id, ProductManagerViewModel p, HttpPostedFileBase image)
        {
            if (!ModelState.IsValid)
            {
                return View(p);
            }

            try
            {
                Product target = this.productsCtx.Get(id);

                if (image != null && image.ContentLength > 0 ) {
                    p.Product.Image = p.Product.ID + Path.GetExtension(image.FileName);
                    image.SaveAs(Server.MapPath("//Content//product_images//") + p.Product.Image);
                }

                target.UpdateFrom(p.Product);
                this.productsCtx.Update(target);
                this.productsCtx.Commit();
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
                this.productsCtx.Commit();
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