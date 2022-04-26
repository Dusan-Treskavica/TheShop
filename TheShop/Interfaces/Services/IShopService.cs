using TheShop.Model;

namespace TheShop.Interfaces.Services
{
    public interface IShopService
    {
        void OrderAndSellArticle(int id, int maxExpectedPrice, int buyerId);
        Article GetById(int id);
    }
}
