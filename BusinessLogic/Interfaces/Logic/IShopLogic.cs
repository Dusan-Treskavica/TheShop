using Common.Models;

namespace BusinessLogic.Interfaces.Logic
{
    public interface IShopLogic
    {
        SupplierArticle FindArticleByExpectedPrice(int id, int expectedPrice);
        void OrderArticleForBuyer(ShopArticle shopArticle, int buyerId);
        void SellArticle(ShopArticle shopArticle);
    }
}