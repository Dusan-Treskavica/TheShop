using Common.Models;

namespace TheShop.Interfaces.Services
{
    public interface IShopService
    {
        void OrderAndSellArticle(int articleId, int maxExpectedPrice, int buyerId);
        ShopArticle GetById(int articleId);
    }
}
