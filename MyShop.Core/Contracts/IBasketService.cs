using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Core.Contracts
{
    public interface IBasketService
    {
        void AddToBasket(HttpContextBase ctx, string productId);
        List<BasketViewModel> GetBasketItems(HttpContextBase ctx);
        BasketSummaryModel GetBasketSummary(HttpContextBase ctx);
        void RemoveFromBasket(HttpContextBase ctx, string itemId);
    }
}
