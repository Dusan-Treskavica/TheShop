using TheShop.Model;

namespace TheShop.Interfaces.Services
{
    public interface ISupplierService
    {
        bool HasArticle(int articleId);
        Article GetById(int articleId);
        Article FindArticleByExpectedPrice(int id, int expectedPrice);
    }
}
