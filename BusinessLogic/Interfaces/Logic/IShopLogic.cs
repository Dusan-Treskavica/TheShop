using Common.Models;

namespace BusinessLogic.Interfaces.Logic
{
    public interface IShopLogic
    {
        ShopArticle OrderArticleForBuyer(int articleId, int maxExpectedPrice, int buyerId);
        void SellShopArticle(ShopArticle shopArticle);
        ShopArticle GetShopArticleById(int articleId);
    }
}