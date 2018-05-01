using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Services
{
    public class BasketService : IBasketService
    {
        public const string BasketSessionName = "myshop_basket";

        IDataRepository<Product> products;
        IDataRepository<Basket> baskets;

        public BasketService(IDataRepository<Product> products, IDataRepository<Basket> baskets)
        {
            this.products = products;
            this.baskets = baskets;
        }

        public BasketSummaryModel GetBasketSummary(HttpContextBase ctx)
        {
            Basket b = this.getBasket(ctx, false);
            int? totalItems = (from item in b.Items
                               select item.Quantity).Sum();

            decimal? totalPrice = (from item in b.Items
                                   join p in this.products.List() on item.ProductID equals p.ID
                               select item.Quantity * p.Price).Sum();

            return new BasketSummaryModel(totalItems ?? 0, totalPrice ?? 0);
        }

        public List<BasketViewModel> GetBasketItems(HttpContextBase ctx)
        {
            Basket b = this.getBasket(ctx, false);
            return (
                from br in b.Items
                join p in this.products.List() on br.ProductID equals p.ID
                select new BasketViewModel()
                {
                    BasketItemId= br.ID,
                    ProductName= p.Name,
                    Image = p.Image,
                    ProductPrice = p.Price,
                    Quantity=br.Quantity,

                }).ToList();
        }

        private Basket getBasket(HttpContextBase ctx, bool doCreate)
        {
            HttpCookie cookie = ctx.Request.Cookies.Get(BasketSessionName);
            if(cookie == null)
            {
                return new Basket();
            }

            string basketId = cookie.Value;
            if (string.IsNullOrEmpty(basketId))
            {
                return new Basket();
            }

            Basket b = this.baskets.Get(basketId);
            if (b == null) {
                b = createBasket(ctx);
            }

            return b;
        }

        private Basket createBasket(HttpContextBase ctx)
        {
            Basket b = new Basket();
            this.baskets.Save(b);
            this.baskets.Commit();

            HttpCookie cook = new HttpCookie(BasketSessionName);
            cook.Value = b.ID;
            cook.Expires = DateTime.Now.AddDays(1);
            ctx.Response.Cookies.Add(cook);
            return b;
        }

        public void AddToBasket(HttpContextBase ctx,string productId)
        {
            Basket b = this.getBasket(ctx, true);
            BasketItem item = b.Items.FirstOrDefault((it) => it.ProductID == productId);
            if(item == null)
            {
                item = new BasketItem()
                {
                    BasketID= b.ID,
                    ProductID = productId,
                    Quantity = 1,
                };

                b.Items.Add(item);
            }
            else
            {
                item.Quantity += 1;
            }

            this.baskets.Commit();
        }

        public void RemoveFromBasket(HttpContextBase ctx, string itemId)
        {
            Basket basket = this.getBasket(ctx, true);
            BasketItem item = basket.Items.FirstOrDefault((i) => i.ID == itemId);
            if(item == null){
                return;
            }
            basket.Items.Remove(item);
            this.baskets.Commit();
        }
    }
}
